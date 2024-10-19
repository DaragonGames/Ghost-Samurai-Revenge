using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Knockback knockback;
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
    }

    public void Movement(Vector3 direction, float speed)
    {
        if (knockback !=null)
        {
            if (knockback.IsBeingKnockedback())
            {
                knockback.SetCharacterMovementForce(direction, speed);
                return;
            }
        }
        rb.velocity = direction * speed;
    }
}
