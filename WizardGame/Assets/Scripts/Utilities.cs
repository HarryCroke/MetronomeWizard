using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static float VarySoundPitch(float basePitch, float variation)
    {
        //int x = PentatonicSemitones[Random.Range(0, PentatonicSemitones.Length-1)];
        //return basePitch *= Mathf.Pow(1.059463f, x);
        return basePitch * Random.Range(1 - (variation/2), 1 + (variation/2));
    }

    public static void PlayAtRandomPitch(AudioSource audioSource, AudioClip clip, float variation = 0.4f)
    {
        audioSource.pitch = VarySoundPitch(1, variation);
        audioSource.PlayOneShot(clip);
    }
}
