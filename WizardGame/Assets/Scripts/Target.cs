using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject[] TriggeredObject;
    
    private void OnTriggerEnter(Collider other)
    {
        print("Collison");
        if (other.tag == "Bolt" && TriggeredObject != null)
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
