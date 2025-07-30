using System;
using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Direction * (Velocity * Time.deltaTime);
    }
}
