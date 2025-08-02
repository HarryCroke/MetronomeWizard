using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Spells : MonoBehaviour
{
    public FirstPersonController Player;
    
    public float DashSpeed;
    public GameObject FireboltPrefab;

    public Image RightHand;
    public Sprite[] HandSprites;
    private int currentHandSprite;
    public float Deceleration;

    public AudioSource SpellSource;
    public AudioClip[] SpellSounds;
    
    public void CastSpell(SpellType type)
    {
        switch (type)
        {
            case SpellType.Firebolt:
                Firebolt();
                Utilities.PlayAtRandomPitch(SpellSource, SpellSounds[0], 0.1f);
                break;
            
            case SpellType.Jump:
                Jump();
                Utilities.PlayAtRandomPitch(SpellSource, SpellSounds[1], 0.1f);
                break;
            
            case SpellType.Dash:
                Dash();
                Utilities.PlayAtRandomPitch(SpellSource, SpellSounds[2], 0.1f);
                break;
            
            default:
                break;
        }
    }

    private void Jump()
    {
        Player.Jump();
        currentHandSprite = 2;
        UpdateHandSprite();
    }

    private void Dash()
    {
        Player.DashVelocity = DashSpeed;
        currentHandSprite = 4;
        UpdateHandSprite();
        //Quaternion.Euler(0, -45, 0);
    }

    private void FixedUpdate()
    {
        Player.DashVelocity *= Deceleration;
        // if (Player.DashVelocity.magnitude > 0.1f)
        // {
        //     Player.DashVelocity = Vector3.zero;
        // }
    }

    private void Firebolt()
    {
        GameObject f = Instantiate(FireboltPrefab, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        Projectile p = f.GetComponent<Projectile>();
        p.Direction = transform.forward;
        currentHandSprite = 0;
        UpdateHandSprite();
    }

    private void UpdateHandSprite()
    {
        RightHand.sprite = HandSprites[currentHandSprite];
    }

    public void SetSpecificHand(int sprite)
    {
        currentHandSprite = sprite;
        UpdateHandSprite();
    }

    public void ProgressHand()
    {
        currentHandSprite++;
        UpdateHandSprite();
    }

}

public enum SpellType
{
    None,
    Firebolt,
    Jump,
    Dash
}
