using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMetronomeUI : MonoBehaviour, IPulseReceiver
{
    public Image BeatImage;
    [NonSerialized]
    public Metronome Metronome;
    
    private float[] beatLocations = new float[] { -448, -320, -192, -64, 64, 192, 320, 448};

    private void Start()
    {
        Metronome.onBeat += OnMetronomePulse;
    }

    public void OnMetronomePulse()
    {
        int beat = Metronome.beat;
        Vector3 loc = BeatImage.rectTransform.localPosition;
        BeatImage.rectTransform.localPosition = new Vector3(beatLocations[beat], loc.y, loc.z);
    }
}
