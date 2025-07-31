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
    
    public Spells Spells;
    [NonSerialized]
    public SpellType[] SpellList = new SpellType[8];

    [FormerlySerializedAs("BPM")] public float Bpm;
    private float delay;
    private int beat = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        delay = 60 / Bpm;
        MetronomeUI.Delay = delay;
        StartCoroutine(MetronomePulse());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MetronomePulse()
    {
        AudioSource.PlayOneShot(AudioClip);
        Spells.CastSpell(SpellList[beat]);
        print(beat);
        
        beat += 1;
        if(beat > 7) beat = 0;
        
        yield return new WaitForSeconds(delay);
        
        StartCoroutine(MetronomePulse());
    }
}
