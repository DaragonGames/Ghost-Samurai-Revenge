using System.Collections;
using System.Collections.Generic;
using UnityEditor.Scripting;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private float damage;
    private float piercingDamage;
    private float knockbackPower;
    private string sourceTag;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != sourceTag)
        {
            DealDamage(collision.gameObject);
            DealKnockback(collision.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != sourceTag)
        {
            DealDamage(collider.gameObject);
            DealKnockback(collider.gameObject);
        }
    }

    void DealDamage(GameObject target)
    {
        Damageable main = target.GetComponent<Damageable>();
        if (main != null)
        {
            main.TakeDamage(damage,piercingDamage);
        }        
    }
    void DealKnockback(GameObject target)
    {
        Knockback knockback = target.GetComponent<Knockback>();
        if (knockback != null && knockbackPower > 0)
        {
            Vector3 direction = target.transform.position - transform.position;
            knockback.RecieveKnockback(direction,knockbackPower);
            transform.position -= direction.normalized*0.01f;
        }       
    }

    public void SetDamage(float damage, float piercingDamage, float knockbackPower, string tag)
    {
        this.damage = damage;
        this.piercingDamage = piercingDamage;
        this.knockbackPower = knockbackPower;
        this.sourceTag = tag;
    }
}
