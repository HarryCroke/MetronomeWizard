using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MetronomeUI : MonoBehaviour
{
    public Image Bar;
    public Image Line;
    [NonSerialized]
    public float Delay;
    public Metronome Metronome;


    
    // Start is called before the first frame update
    void Start()
    {
        Line.rectTransform.localPosition = new Vector3(Bar.rectTransform.localPosition.x - (Bar.rectTransform.sizeDelta.x/2), 0, Bar.rectTransform.localPosition.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        float barLeft = Bar.rectTransform.localPosition.x - (Bar.rectTransform.sizeDelta.x / 2);
        float barRight = Bar.rectTransform.localPosition.x + (Bar.rectTransform.sizeDelta.x / 2);
        float linePosition = Mathf.Lerp(barLeft, barRight, Metronome.currentTime / Metronome.maxTime);
        Line.rectTransform.localPosition = new Vector3(linePosition, 0, Bar.rectTransform.localPosition.z);
    }
}
