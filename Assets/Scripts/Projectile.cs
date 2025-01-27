using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{    
    // Those should be public
    public float movementSpeed = 4;
    public float acceleration = 0;
    public float roationSpeed = 0;
    public float contactDamage = 10;
    public float piercingDamage = 0;
    public float knockbackStrength = 0;    
    public int piercing = 0;
    public float stunPower = 0;
    public string sourceTag;

    void Update()
    {
        transform.Rotate(new Vector3(0,0,roationSpeed*360*Time.deltaTime));
        GetComponent<Rigidbody2D>().velocity *= 1+(acceleration*Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile projectile = col.GetComponent<Projectile>();
        if (col.gameObject.tag == sourceTag)
        {
            return;
        }
        

        // Check for Effects
        if (stunPower > 0)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            { 
                enemy.GetStunned(stunPower);
            }
        }

        // Check for Piercing
        if (col.GetComponent<Damageable>() != null)
        {
            Pierce();
            return;
        }
        if (col.gameObject.tag == "wall" || col.gameObject.tag == "Obstacle")
        {
            Pierce(20);
            return;              
        } 
        if (projectile != null)
        {
            if (projectile.sourceTag != sourceTag)
            {
                Pierce(500);
            }
        }        
    }

    private void Pierce(int value = 1) {
        piercing-=value;
        if (piercing <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void SetValues(Vector3 direction, string source, float duration)
    {
        GetComponent<Rigidbody2D>().velocity = direction.normalized * movementSpeed;
        sourceTag = source;
        Destroy(gameObject, duration);
        GetComponent<DamageDealer>().SetDamage(contactDamage,piercingDamage,knockbackStrength,sourceTag);

        if (sourceTag == "Enemy")
        {
            contactDamage *= 1 + 0.15f*GameManager.Instance.gameData.progression;
        }
        if (sourceTag == "Player")
        {
            contactDamage *= Player.GetStats().attackDamage;
        }
    }

    public void SetValues(Vector3 direction, string source, float duration, float acceleration, float roationSpeed)
    {
        SetValues(direction, source, duration);
        this.acceleration = acceleration;
        this.roationSpeed = roationSpeed;
    }

}
