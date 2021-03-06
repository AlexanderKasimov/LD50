using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsModule : MonoBehaviour
{
    public float HP = 100f;

    public float curHP;

    public float ATK = 1f;

    public bool isDead = false;

    private DeathHandler deathHandler;

    private HitEffect hitEffect;

    public float mentalState = 50f;

    private void Awake() 
    {
        curHP = HP;
        deathHandler = GetComponent<DeathHandler>();
        hitEffect = GetComponent<HitEffect>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleDamage(float damage)
    {
        if (isDead)
        {
            return;
        }
        curHP -= damage;
        if (curHP <= 0)
        {
            isDead = true;
            //DeathHandler;
            if (deathHandler)
            {
                deathHandler.HandleDeath();
            }
        }
        //Play material hit efffect if any
        if (hitEffect)
        {
            hitEffect.PlayEffect();
        }
    }

    public void ChangeMentalState(float value)
    {
        mentalState = Mathf.Max(0f,Mathf.Min(mentalState + value, 100f));
    }
}
