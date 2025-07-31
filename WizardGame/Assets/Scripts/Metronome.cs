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
    public MetronomeUI MetronomeUI;
    public FirstPersonController Player;
    
    public Spells Spells;
    public SpellSlot[] SpellSlots;
    private SpellType[] SpellList = new SpellType[8];
    
    private float delay;
    private int beat = 0;

    public delegate void OnBeat();
    public static OnBeat onBeat;
    
    private float timeTillNextBeat, timeSinceLastBeat;
    public float Leeway = 0.15f;
    private bool spellAlreadyCast;
    
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

    private float fixedDelta = 0.02f;
    
    // Start is called before the first frame update
    void Start()
    {
        delay = 60 / Bpm;
        MetronomeUI.Delay = delay;
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
            float sampleTime =
                (MusicSource.timeSamples / (MusicSource.clip.frequency * interval.GetIntervalLength(Bpm)));
            interval.CheckForNewInterval(sampleTime);
        }
        
        previousAudioTime = currentAudioTime;
        currentAudioTime = MusicSource.time;
        
        fixedDelta = currentAudioTime - previousAudioTime;
        
        // Again, this is just for the UI
        if (Playing)
        {
            currentTime += fixedDelta;
            if(currentTime >= maxTime) currentTime = 0;
            timeSinceLastBeat += fixedDelta;
            timeTillNextBeat -= fixedDelta;
        }
        
        if(Mathf.Abs(timeTillNextBeat - Leeway) < 0.02f) spellAlreadyCast = false;
    }

    IEnumerator MetronomePulse()
    {
        if (!Player.MenuOpen)
        {
            //Spells.CastSpell(SpellList[beat]);
            AudioSource.PlayOneShot(AudioClip);
        }

        if (onBeat != null) onBeat();

        timeTillNextBeat = delay; 
        timeSinceLastBeat = 0;
        
        beat += 1;
        if(beat > 7) beat = 0;
        
        yield return new WaitForSeconds(delay);
        
        StartCoroutine(MetronomePulse());
    }

    public void OnSampledBeat()
    {
        if (!Player.MenuOpen)
        {
            //Spells.CastSpell(SpellList[beat]);
            AudioSource.PlayOneShot(AudioClip);
        }

        if (onBeat != null) onBeat();

        timeTillNextBeat = delay; 
        timeSinceLastBeat = 0;
        
        beat += 1;
        if(beat > 7) beat = 0;
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
        if (Player.MenuOpen || spellAlreadyCast) return;
        
        if (timeSinceLastBeat < Leeway || timeTillNextBeat < Leeway)
        {
            Spells.CastSpell(SpellList[beat]);
            spellAlreadyCast = true;
        }
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1);
        //StartCoroutine(MetronomePulse());
        MusicSource.Play();
        Playing = true;
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
