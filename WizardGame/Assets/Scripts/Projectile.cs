using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    public float Velocity;
    public float Damage;
    public float LifeTime;
    [NonSerialized]
    public Vector3 Direction;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(LifeTime);
        DestroySelf();
    }
    
    IEnumerator CollisionDestroy()
    {
        yield return new WaitForSeconds(0.05f);
        DestroySelf();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Direction * (Velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tag == "Bolt")
        {
            if (other.tag != "Player" && other.tag != "Evil" && other.tag != "Bolt")
            {
                StartCoroutine(CollisionDestroy());
            }
        }

        else
        {
            if (other.tag != "Skull" && other.tag != "Evil" && other.tag != "Bolt")
            {
                StartCoroutine(CollisionDestroy());
            }
        }

        
    }
}
