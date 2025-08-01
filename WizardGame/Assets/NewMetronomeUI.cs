using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NewMetronomeUI : MonoBehaviour, IPulseReceiver
{
    public Image BeatImage;
    [NonSerialized]
    public Metronome Metronome;
    
    private float[] beatLocations = new float[] { -448, -320, -192, -64, 64, 192, 320, 448};
    public Image[] SpellImages;
    public Sprite[] SpellSprites;

    public Image LeftHand;
    public Sprite[] MetronomeSprites;

    private void Start()
    {
        Metronome.onBeat += OnMetronomePulse;
    }

    public void OnMetronomePulse()
    {
        int beat = Metronome.beat;
        Vector3 loc = BeatImage.rectTransform.localPosition;
        BeatImage.rectTransform.localPosition = new Vector3(beatLocations[beat], loc.y, loc.z);
        
        LeftHand.sprite = MetronomeSprites[beat%2];
        StartCoroutine(MetronomeMiddle());
    }

    public void UpdateIcons()
    {
        for (int j = 0; j < 8; j++)
        {
            switch (Metronome.SpellList[j])
            {
                case SpellType.None:
                    SpellImages[j].sprite = SpellSprites[0];
                    break;
                case SpellType.Firebolt:
                    SpellImages[j].sprite = SpellSprites[1];
                    break;
                case SpellType.Jump:
                    SpellImages[j].sprite = SpellSprites[2];
                    break;
                case SpellType.Dash:
                    SpellImages[j].sprite = SpellSprites[3];
                    break;
            }
        }
    }

    IEnumerator MetronomeMiddle()
    {
        yield return new WaitForSeconds(Metronome.delay/2);
        LeftHand.sprite = MetronomeSprites[2];
    }
}
