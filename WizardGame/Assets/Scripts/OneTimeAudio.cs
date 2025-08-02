using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeAudio : MonoBehaviour
{
    private AudioSource audioSource;

    public void Begin(AudioClip clip)
    {
        audioSource = GetComponent<AudioSource>();
        Utilities.PlayAtRandomPitch(audioSource, clip, 0.2f);
        StartCoroutine(WaitDestroy());
    }

    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        float length = audioSource.clip.length + 1;
        yield return new WaitForSeconds(length);
        Destroy(gameObject);
    }
}
