using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Metronome : MonoBehaviour
{
    [FormerlySerializedAs("audioClip")] public AudioClip AudioClip;
    [FormerlySerializedAs("audioSource")] public AudioSource AudioSource;
    public MetronomeUI MetronomeUI;

    [FormerlySerializedAs("BPM")] public float Bpm;
    private float delay;
    
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
        
        yield return new WaitForSeconds(delay);
        
        StartCoroutine(MetronomePulse());
    }
}
