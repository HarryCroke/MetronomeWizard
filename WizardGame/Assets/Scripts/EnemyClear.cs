using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClear : MonoBehaviour
{
    public GameObject[] TriggeredObject;
    public GameObject[] EnemiesToDestroy;

    private void Start()
    {
        foreach (GameObject e in EnemiesToDestroy)
        {
            e.GetComponent<Necroskull>().EnemyClear = this;
        }
    }

    public void OnKill(GameObject enemy)
    {
        int index = System.Array.IndexOf(EnemiesToDestroy, enemy);
        EnemiesToDestroy[index] = null;

        foreach (GameObject e in EnemiesToDestroy)
        {
            if (e != null)
            {
                return;
            }
        }
        
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

    private void Update()
    {
        
    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Player" && TriggeredObject != null)
    //     {
    //         foreach (GameObject o in TriggeredObject)
    //         {
    //             o.GetComponent<ITriggerable>().Trigger();
    //         }
    //         
    //         if (gameObject.GetComponent<AudioSource>())
    //         {
    //             AudioSource source = gameObject.GetComponent<AudioSource>();
    //             Utilities.PlayAtRandomPitch(source, source.clip);
    //         }
    //
    //     }
    // } 
}
