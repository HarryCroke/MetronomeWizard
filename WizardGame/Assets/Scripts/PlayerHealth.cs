using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int Health = 100;
    public TextMeshProUGUI HealthText;
    private bool invincible = false;
    public AudioClip[] HurtSounds;
    public AudioSource HurtSource;
    
    public Color FilterColor;
    public Color TeleportColor;
    public Image ColourFilter;
    public float FilterDuration;
    private float FilterProgress;

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Skull")
        {
            TakeDamage(other.gameObject.GetComponent<Necroskull>().damage);
            print(Health);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Evil")
        {
            TakeDamage(Mathf.FloorToInt(other.gameObject.GetComponent<Projectile>().Damage));
            print(Health);
            Destroy(other.gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        if(invincible) return;
        Health -= damage;
        if(Health <= 0) OnDeath();
        
        HealthText.text = Health.ToString();
        StartCoroutine(InvincibilityFrames());
        Utilities.PlayAtRandomPitch(HurtSource, HurtSounds[UnityEngine.Random.Range(0, HurtSounds.Length)], 0.2f);
        FlashColour(FilterColor);
    }

    private IEnumerator InvincibilityFrames()
    {
        invincible = true;
        yield return new WaitForSeconds(0.3f);
        invincible = false;
    }

    public void FlashColour(Color colour)
    {
        ColourFilter.color = colour;
        FilterProgress = FilterDuration;
    }

    private void FixedUpdate()
    {
        if (FilterProgress > 0)
        {
            FilterProgress -= Time.deltaTime;
            float NewAlpha = (FilterProgress/FilterDuration) * FilterColor.a;
            Color NewColour = new Color(FilterColor.r, FilterColor.g, FilterColor.b, NewAlpha);
            ColourFilter.color = NewColour;
        }
    }

    private void OnDeath()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
