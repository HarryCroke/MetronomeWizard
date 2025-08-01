using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int Health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Skull")
        {
            Health -= other.gameObject.GetComponent<Necroskull>().damage;
            print(Health);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Evil")
        {
            Health -= Mathf.FloorToInt(other.gameObject.GetComponent<Projectile>().Damage);
            print(Health);
            Destroy(other.gameObject);
        }
    }
}
