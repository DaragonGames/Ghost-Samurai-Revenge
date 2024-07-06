using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Changeable Stats
    private float contactDamage = 10f;
    private float movementSpeed = 3f;
    private float health = 20f;
    private float knockbackResistance = 0f;

    // Code based Variables: do not change
    private Vector3 knockback = Vector3.zero;
    private int roomID = -1;

    // Update is called once per frame
    void Update()
    {
        // Handle Knockback
        transform.position += knockback * Time.deltaTime;
        knockback -= knockback.normalized * Time.deltaTime;  
        if (knockback.magnitude <= Time.deltaTime*1.5f)
        {
            knockback = Vector3.zero;
        }             

        // Kill Enemie when they have no Health
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        // Allow Actions Only if you are in the same Room as the enemy 
        if (GameManager.Instance.currentRoomID != roomID)
        {
            return;
        }
        MoveCharacter();
        CharacterAction();
    }

    public virtual void MoveCharacter()
    {
        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;
    }
    public virtual void CharacterAction() {}

    void OnCollisionStay2D(Collision2D collision2D) {
        if (collision2D.gameObject.tag == "Player")
        {
            collision2D.transform.GetComponent<Player>().TakeDamage(contactDamage, 0);
        }
    }

    public void TakeDamage(float amount, float piercingDamage, Vector3 knockback, float knockbackStrength)
    {
        health -= amount + piercingDamage;
        this.knockback =knockback * Mathf.Min(knockbackStrength - knockbackResistance, 0);
    }

    public void SetRoomID(int id){ roomID = id;}
}
