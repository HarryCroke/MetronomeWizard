using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spells : MonoBehaviour
{
    public FirstPersonController Player;
    
    public float DashSpeed;
    public GameObject FireboltPrefab;

    public Image RightHand;
    public Sprite[] HandSprites;
    private int currentHandSprite;

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
        Vector3 targetVelocity = transform.TransformDirection(new Vector3(0,0,DashSpeed));
        Player.rb.AddForce(targetVelocity, ForceMode.VelocityChange);
        currentHandSprite = 4;
        UpdateHandSprite();
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
