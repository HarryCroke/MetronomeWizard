using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingObject : MonoBehaviour
{
    
    [SerializeField] [Tooltip("Y transform of object during animation")]
    private AnimationCurve BobbingCurve;
    // Current progress along the throw curve
    private float animationProgress;
    // Position when scene is loaded
    private Vector3 initialPosition;

    [SerializeField] [Tooltip("Length of each animation in seconds")]
    public float AnimationLength = 1;
    [SerializeField] [Tooltip("Distance to raise object up and down in units")]
    public float AnimationDistance = 1;

    [SerializeField] [Tooltip("Random range to multiply animation length by")]
    public Vector2 AnimationVariance = new Vector2(1,1);
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        animationProgress = UnityEngine.Random.value;
        //print(animationProgress);
        initialPosition = gameObject.transform.position;
        AnimationLength *= UnityEngine.Random.Range(AnimationVariance.x, AnimationVariance.y);
    }

    // Update is called once per frame
    void Update()
    {
        animationProgress += Time.deltaTime / AnimationLength;
        gameObject.transform.position = new Vector3(initialPosition.x,
            initialPosition.y + (BobbingCurve.Evaluate(animationProgress)*AnimationDistance), initialPosition.z);
        if (animationProgress >= 1)
        {
            animationProgress = 0;
        }
        
    }
}
