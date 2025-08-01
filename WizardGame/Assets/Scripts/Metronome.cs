using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Metronome : MonoBehaviour
{
    [FormerlySerializedAs("audioClip")] public AudioClip AudioClip;
    [FormerlySerializedAs("audioSource")] public AudioSource AudioSource;
    public NewMetronomeUI MetronomeUI;
    public FirstPersonController Player;
    
    public Spells Spells;
    public SpellSlot[] SpellSlots;
    private SpellType[] SpellList = new SpellType[8];
    
    [NonSerialized]
    public float delay;
    [NonSerialized]
    public int beat = 0;

    public delegate void OnBeat();
    public static OnBeat onBeat;
    
    public float Leeway = 0.075f;
    private bool spellAlreadyCast, spellReset;
    
    [FormerlySerializedAs("BPM")] public float Bpm;
    private float currentAudioTime, previousAudioTime;
    public AudioSource MusicSource;
    public Intervals[] intervals;
    
    // This is only used for UI (bug fix)
    [NonSerialized]
    public float currentTime = 0;
    [NonSerialized]
    public float maxTime = 0;

    private bool Playing;

    private float fixedDelta;
    
    private float timeSinceLastBeat, timeUntilNextBeat;
    private float PreviousBeatTime, CurrentBeatTime;
    
    // Start is called before the first frame update
    void Awake()
    {
        delay = 60 / Bpm;
        MetronomeUI.Metronome = this;
        StartCoroutine(DelayedStart());
        Player = GetComponent<FirstPersonController>();
        maxTime = delay * 8;
        print(delay);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Intervals interval in intervals)
        {
            previousAudioTime = currentAudioTime;
            currentAudioTime = MusicSource.time;
            float sampleTime =
                (MusicSource.timeSamples / (MusicSource.clip.frequency * interval.GetIntervalLength(Bpm)));
            interval.CheckForNewInterval(sampleTime);
        }
        
        // Should be called "AudioDelta"
        fixedDelta = currentAudioTime - previousAudioTime;
        
        // Again, this is just for the UI
        if (Playing)
        {
            timeUntilNextBeat -= fixedDelta;
            timeSinceLastBeat += fixedDelta;

            if (timeSinceLastBeat > Leeway && !spellReset)
            {
                spellAlreadyCast = false;
                spellReset = true;
            }
        }
        
    }

    // IEnumerator MetronomePulse()
    // {
    //     if (!Player.MenuOpen)
    //     {
    //         //Spells.CastSpell(SpellList[beat]);
    //         AudioSource.PlayOneShot(AudioClip);
    //     }
    //
    //     if (onBeat != null) onBeat();
    //
    //     timeTillNextBeat = delay; 
    //     timeSinceLastBeat = 0;
    //     
    //     beat += 1;
    //     if(beat > 7) beat = 0;
    //     
    //     yield return new WaitForSeconds(delay);
    //     
    //     StartCoroutine(MetronomePulse());
    // }

    public void OnSampledBeat()
    {
        
        beat += 1;
        if(beat > 7) beat = 0;

        PreviousBeatTime = CurrentBeatTime;
        CurrentBeatTime = MusicSource.time;
        delay = CurrentBeatTime - PreviousBeatTime;
        spellReset = false;
        
        if (!Player.MenuOpen)
        {
            //Spells.CastSpell(SpellList[beat]);
            AudioSource.PlayOneShot(AudioClip);
        }

        if (onBeat != null) onBeat();

        timeUntilNextBeat = delay;
        timeSinceLastBeat = 0;
        

    }

    public void FillOutSpellList()
    {
        for (int i = 0; i < 8; i++)
        {
            SpellList[i] = SpellSlots[i].Type;
        }
    }

    public void OnFire()
    {
        int spellAdjustment = 0;
        if (timeUntilNextBeat < timeSinceLastBeat)
        {
            spellAdjustment = 1;
        }
        
        if (Player.MenuOpen || spellAlreadyCast) return;
        
        if (timeSinceLastBeat < Leeway || timeUntilNextBeat < Leeway)
        {
            Spells.CastSpell(SpellList[beat+spellAdjustment]);
            if(SpellList[beat+spellAdjustment] != SpellType.None) StartCoroutine(HandAnimation());
            spellAlreadyCast = true;
        }
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1);
        //StartCoroutine(MetronomePulse());
        MusicSource.Play();
        Playing = true;
        //OnSampledBeat();
    }

    IEnumerator HandAnimation()
    {
        yield return new WaitForSeconds(delay/4);
        Spells.ProgressHand();
    }
}

[System.Serializable]
public class Intervals
{
    public float Steps;
    public UnityEvent Trigger;
    private int lastInterval;

    public float GetIntervalLength(float bpm)
    {
        return 60f / (bpm * Steps);
    }

    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);
            Trigger.Invoke();
        }
    }
}
