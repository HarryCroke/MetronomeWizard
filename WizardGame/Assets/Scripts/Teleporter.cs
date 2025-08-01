using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour, ITriggerable
{
    public GameObject TeleportPoint;
    
    public void Trigger()
    {
        GameObject.Find("FirstPersonController").transform.position = TeleportPoint.transform.position;
    }
}
