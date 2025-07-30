using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public FirstPersonController Player;
    
    public float DashSpeed;
    
    public void CastSpell(SpellType type)
    {
        switch (type)
        {
            case SpellType.Firebolt:
                print("Fire!");
                break;
            
            case SpellType.Jump:
                Jump();
                break;
            
            case SpellType.Dash:
                Dash();
                break;
            
            default:
                print("dud");
                break;
        }
    }

    private void Jump()
    {
        Player.Jump();
    }

    private void Dash()
    {
        Vector3 targetVelocity = transform.TransformDirection(new Vector3(0,0,DashSpeed));
        Player.rb.AddForce(targetVelocity, ForceMode.VelocityChange);
    }

}

public enum SpellType
{
    None,
    Firebolt,
    Jump,
    Dash
}
