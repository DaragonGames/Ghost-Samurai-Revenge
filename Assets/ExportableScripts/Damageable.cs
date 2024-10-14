using System;
using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    // To be set from outside
    private float maxHp;
    private float hp;
    private float defense;
    
    // Not to be set
    private bool invincible = false;
    public event Action DeathEvent;
    
    public void TakeDamage(float damage, float piercingDamage)
    {
        if (invincible)
        {
            return;
        }
        float damageTaken = Mathf.Max(0, damage-defense) + piercingDamage;
        hp -= damageTaken;
        if (hp <= 0)
        {
            DeathEvent.Invoke();
            return;
        }
        if (damageTaken > 0)
        {
            StartCoroutine(invisibelTimer());
        }        
    }

    private IEnumerator invisibelTimer()
    {
        invincible = true;
        yield return new WaitForSeconds(GameValues.invincibleTime); 
        invincible = false;
    }

    public void GainHealth(float value)
    {
        hp += value;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public void SetValues(float maxHp, float defense)
    {
        this.maxHp = maxHp;
        hp = maxHp;
        this.defense = defense;        
    }

    public void IncreaseMaxHealth(float value)
    {
        maxHp += value;
        hp += value;
    }

    public float GetHealthPercentage()
    {
        return (hp/maxHp);
    }



}
