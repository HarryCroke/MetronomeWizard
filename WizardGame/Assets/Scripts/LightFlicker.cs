using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightFlicker : MonoBehaviour, IPulseReceiver
{
    
    [SerializeField] [Tooltip("Light intensity during animation")]
    private AnimationCurve IntensityCurve;
    // Current progress along the throw curve
    private float animationProgress;

    [SerializeField] [Tooltip("Starting Intensity value")]
    // Position when scene is loaded
    private float InitialIntensity, InitialRange;
    
    [SerializeField] [Tooltip("Reference to flickering light")]
    private Light PointLight;

    [SerializeField] [Tooltip("Length of each animation in seconds")]
    public float AnimationLength = 1;
    [SerializeField] [Tooltip("Amount to alter light intensity")]
    public float AnimationDistance = 1;

    [SerializeField] [Tooltip("Random range to multiply animation length by")]
    public Vector2 AnimationVariance = new Vector2(1,1);
    
    // // Start is called before the first frame update
    void Start()
    {
        Metronome.onBeat += OnMetronomePulse;
        AnimationLength = GameObject.Find("FirstPersonController").GetComponent<Metronome>().delay;
    }
    //
    // // Update is called once per frame
    void Update()
    {
        animationProgress += Time.deltaTime / AnimationLength;
        PointLight.intensity = InitialIntensity + IntensityCurve.Evaluate(animationProgress);
        PointLight.range = InitialRange + (IntensityCurve.Evaluate(animationProgress)*5);
        if (animationProgress >= 1)
        {
            animationProgress = 0;
        }

    }
    


    public void OnMetronomePulse()
    {
        print("Got your message");
        animationProgress = 0;
    }
}
