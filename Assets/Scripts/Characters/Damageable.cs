using System;
using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public bool isObject;

    // To be set from outside
    private float maxHp;
    private float hp;
    private float defense;
    private float invincibleTime;
    
    // Not to be set
    private bool invincible = false;
    public event Action DeathEvent;
    public event Action DamageEvent;

    // Set from Inspector
    public GameObject soundPrefab;
    
    public void TakeDamage(float damage, float piercingDamage, bool canDamageObjects)
    {
        if (isObject && !canDamageObjects) {return;}
        if (damage + piercingDamage <= 0) {return;}
        float damageTaken = Mathf.Max(1, damage-defense) + piercingDamage;

        if (invincible || hp <= 0 || damageTaken <= 0)
        {
            return;
        }
        
        DamageEvent?.Invoke();

        Instantiate(soundPrefab, transform.position, Quaternion.identity);
        hp -= damageTaken;
        StartCoroutine(invincibleByDamage());
        if (damageTaken >= GameValues.minDmgHitStop && maxHp / damageTaken > GameValues.minDmgPercentageHitStop)
        {
            //GameManager.Instance.stunframes = GameValues.stunlockFrames;
            //Time.timeScale = 0f;      
        }

        if (hp <= 0)
        {
            DeathEvent.Invoke();
        }     
    }

    private IEnumerator invincibleByDamage()
    {
        invincible = true;
        for (float i = 0; i < invincibleTime; i+=0.02f )
        {
            float redflash = i / invincibleTime;
            GetComponent<SpriteRenderer>().color = new Color(1, redflash, redflash);
            yield return new WaitForSeconds(0.02f); 
        }
        GetComponent<SpriteRenderer>().color = new Color(1,1,1);
        invincible = false;
    }

    private IEnumerator invincibleByEffect(float time)
    {
        invincible = true;
        yield return new WaitForSeconds(time);
        invincible = false;
    }

    public void BecomeInvincible(float duration)
    {
        StartCoroutine(invincibleByEffect(duration));
    }

    public void GainHealth(float value)
    {
        hp += value;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    public void SetValues(float maxHp, float defense, bool isPlayer)
    {
        this.defense = defense;    
        this.maxHp = maxHp;
        if (hp == 0)
        {
            hp = maxHp;  
        }        
        invincibleTime = isPlayer ? GameValues.invincibleTimePlayer : GameValues.invincibleTimeEnemy;
    }

    public void SetValues(float hp)
    {
        maxHp = hp;
        this.hp = hp;
        defense = 1000;
        invincibleTime = 0.22f;
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
