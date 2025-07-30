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

    private float currentTime = 0;
    private float maxTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Line.rectTransform.localPosition = new Vector3(Bar.rectTransform.localPosition.x - (Bar.rectTransform.sizeDelta.x/2), 0, Bar.rectTransform.localPosition.z);
        maxTime = Delay * 8;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= maxTime) currentTime = 0;

        float barLeft = Bar.rectTransform.localPosition.x - (Bar.rectTransform.sizeDelta.x / 2);
        float barRight = Bar.rectTransform.localPosition.x + (Bar.rectTransform.sizeDelta.x / 2);
        float linePosition = Mathf.Lerp(barLeft, barRight, currentTime / maxTime);
        Line.rectTransform.localPosition = new Vector3(linePosition, 0, Bar.rectTransform.localPosition.z);
    }
}
