using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour, ITriggerable
{
    public GameObject TeleportPoint;
    
    public void Trigger()
    {
        GameObject player = GameObject.Find("FirstPersonController");
        player.transform.position = TeleportPoint.transform.position;
        player.GetComponent<PlayerHealth>().FlashColour(player.GetComponent<PlayerHealth>().TeleportColor);
    }
}
