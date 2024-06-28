using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float contactDamage = 10f;
    private float movementSpeed = 1f;
    private float health = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
            collision2D.transform.GetComponent<Player>().TakeDamage(contactDamage);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }
}
