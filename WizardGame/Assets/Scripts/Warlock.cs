using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : Necroskull
{
    private int attackCountDown1, attackCountDown2;
    public int AttackMaxCountDown;
    public GameObject FireboltPrefab;
    public AudioClip SpellSound;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        attackCountDown1 = AttackMaxCountDown;
        attackCountDown2 = AttackMaxCountDown + 1;
        CountDown = MaxCountDown + 3;
    }

    public override void OnMetronomePulse()
    {
        base.OnMetronomePulse();

        if(!canSeePlayer) return;
        
        attackCountDown1--;
        attackCountDown2--;

        if (attackCountDown1 == 0)
        {
            Attack();
            attackCountDown1 = AttackMaxCountDown;
            Utilities.PlayAtRandomPitch(source, SpellSound, 0.1f);
        }
        else if (attackCountDown2 == 0)
        {
            Attack();
            attackCountDown2 = AttackMaxCountDown;
            Utilities.PlayAtRandomPitch(source, SpellSound, 0.1f);
        }
    }

    void Attack()
    {
        GameObject f = Instantiate(FireboltPrefab, transform.position, Quaternion.identity);
        Projectile p = f.GetComponent<Projectile>();
        p.Direction = transform.forward;
    }
}
