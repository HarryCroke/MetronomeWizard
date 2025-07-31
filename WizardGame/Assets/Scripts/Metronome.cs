using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [FormerlySerializedAs("BPM")] public float Bpm;
    private float delay;
    private int beat = 0;

    public delegate void OnBeat();
    public static OnBeat onBeat;
    
    // This is only used for UI (bug fix)
    [NonSerialized]
    public float currentTime = 0;
    [NonSerialized]
    public float maxTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        delay = 60 / Bpm;
        MetronomeUI.Delay = delay;
        StartCoroutine(MetronomePulse());
        Player = GetComponent<FirstPersonController>();
        maxTime = delay * 8;
    }

    // Update is called once per frame
    void Update()
    {
        // Again, this is just for the UI
        currentTime += Time.deltaTime;
        if(currentTime >= maxTime) currentTime = 0;
    }

    IEnumerator MetronomePulse()
    {
        if (!Player.MenuOpen)
        {
            Spells.CastSpell(SpellList[beat]);
            AudioSource.PlayOneShot(AudioClip);
        }

        if (onBeat != null) onBeat();
        
        print(beat);
        
        beat += 1;
        if(beat > 7) beat = 0;
        
        yield return new WaitForSeconds(delay);
        
        StartCoroutine(MetronomePulse());
    }

    public void FillOutSpellList()
    {
        for (int i = 0; i < 8; i++)
        {
            SpellList[i] = SpellSlots[i].Type;
        }
    }
}
