using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTrigger : MonoBehaviour
{
    public GameObject[] TriggeredObject;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && TriggeredObject != null)
        {
            foreach (GameObject o in TriggeredObject)
            {
                o.GetComponent<ITriggerable>().Trigger();
            }
            
            if (gameObject.GetComponent<AudioSource>())
            {
                AudioSource source = gameObject.GetComponent<AudioSource>();
                Utilities.PlayAtRandomPitch(source, source.clip);
            }

        }
    } 
    
}
