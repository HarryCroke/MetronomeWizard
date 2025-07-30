using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public FirstPersonController Player;
    
    public float DashSpeed;
    public GameObject FireboltPrefab;
    
    public void CastSpell(SpellType type)
    {
        switch (type)
        {
            case SpellType.Firebolt:
                Firebolt();
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

    private void Firebolt()
    {
        GameObject f = Instantiate(FireboltPrefab, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        Projectile p = f.GetComponent<Projectile>();
        p.Direction = transform.forward;
    }

}

public enum SpellType
{
    None,
    Firebolt,
    Jump,
    Dash
}
