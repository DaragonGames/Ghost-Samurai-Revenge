using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Knockback : MonoBehaviour
{   
    
    private Rigidbody2D rb;
    private float knockbackCounter = 0f;

    // Set by Game values
    private float knockbacktime;
    private AnimationCurve knockbackForceCurve;

    // Set by Movementscript
    private Vector3 characterMovementForce = Vector3.zero;

    // Set by Recieve Knockback Function
    private Vector3 direction;
    private float force;

    void Start()
    {
        knockbacktime = GameValues.knockbacktime;
        knockbackForceCurve = GameValues.kockbackAnimationCurve;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (knockbackCounter > 0)
        {
            knockbackCounter -= Time.fixedDeltaTime;
            float curve = knockbackForceCurve.Evaluate(knockbacktime - knockbackCounter );
            rb.velocity = direction*force*curve + characterMovementForce;
        }
        else
        {
            // Resets Values just in case
            direction = Vector3.zero;
            force = 0;
            knockbackCounter = 0;
            characterMovementForce = Vector3.zero;
        }        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameValues.kockbackBounceTags.Contains(collision.transform.tag) && knockbackCounter > 0)
        {
            direction = Vector3.Reflect(direction, collision.contacts[0].normal).normalized;
        }        
    }

    public void RecieveKnockback(Vector3 direction, float force)
    {
        if (knockbackCounter > 0)
        {
            Vector3 totalForce = this.direction*this.force + direction.normalized*force;
            if (this.force < force)
            {
                this.force = force;
            }
            this.direction = totalForce.normalized;
        }
        else
        {
            this.direction = direction.normalized;
            this.force = force;
            knockbackCounter= knockbacktime;
        }
    }

    public bool IsBeingKnockedback() { return knockbackCounter > 0;}
    public void SetCharacterMovementForce(Vector3 direction, float speed) 
    {
        speed = Mathf.Min(speed,force*GameValues.inputInfluenceOnKnockback); 
        characterMovementForce = direction.normalized * speed;
    }

}
