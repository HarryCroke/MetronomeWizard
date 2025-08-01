using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necroskull : MonoBehaviour, IPulseReceiver
{
    public float health = 10f;
    public int damage = 10;
    private float range = 100;
    private GameObject player;
    private bool canSeePlayer;
    private int CountDown;
    public int MaxCountDown;
    public float RotationSpeed;
    public float DashSpeed;
    private float currentSpeed;
    public float Deceleration;
    private Rigidbody rb;
    private AudioSource source;

    [NonSerialized]
    public EnemyClear EnemyClear;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        player = GameObject.Find("FirstPersonController");
        Metronome.onBeat += OnMetronomePulse;
        CountDown = MaxCountDown;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > range) return;
        if(Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit))
        if (hit.collider.tag == "Player")
        {
            canSeePlayer = true;
        }
        else
        {
            canSeePlayer = false;
        }

        if (currentSpeed > 0.1)
        {
            transform.position += transform.forward * (currentSpeed);
            currentSpeed -= Deceleration;
        }
        else
        {
            currentSpeed = 0;
        }

    }

    private void Update()
    {
        if (canSeePlayer)
        {
            Vector3 relativePos = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.time * RotationSpeed);
            transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
        }
    }

    public void OnMetronomePulse()
    {
        if(!canSeePlayer) return;
        CountDown--;
        if (CountDown == 0)
        {
            Dash();
            CountDown = MaxCountDown;
        }
    }

    void Dash()
    {
        if(rb == null) return;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        currentSpeed = DashSpeed;
        Utilities.PlayAtRandomPitch(source, source.clip, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bolt")
        {
            health -= other.GetComponent<Projectile>().Damage;
            if (health <= 0)
            {
                if(EnemyClear != null) EnemyClear.OnKill(this.gameObject);

                StartCoroutine(LateDestroy());
            }
        }
    }

    IEnumerator LateDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
