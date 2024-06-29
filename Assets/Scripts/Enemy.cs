using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float contactDamage = 10f;
    private float movementSpeed = 1f;
    private float health = 200f;

    private Vector3 knockback = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += knockback * Time.deltaTime;
        knockback -= knockback.normalized * Time.deltaTime;
        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;

        if ( health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collision2D) {
        if (collision2D.gameObject.tag == "Player")
        {
            Debug.Log(collision2D.gameObject.tag);
            collision2D.transform.GetComponent<Player>().TakeDamage(contactDamage);
        }
    }

    public void TakeDamage(float amount, Vector3 knockback)
    {
        health -= amount;
        this.knockback = knockback;
    }
}
