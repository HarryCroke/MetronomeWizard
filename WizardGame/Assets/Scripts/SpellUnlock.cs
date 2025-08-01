using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUnlock : MonoBehaviour
{
    public GameObject UnlockedSpell;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UnlockedSpell.SetActive(true);
            Destroy(gameObject);
        }
    }
}
