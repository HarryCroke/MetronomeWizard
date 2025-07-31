using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour, ITriggerable
{

    public void Trigger()
    {
        Destroy(gameObject);
    }
}
