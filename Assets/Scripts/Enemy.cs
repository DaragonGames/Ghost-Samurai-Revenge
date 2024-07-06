using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Changeable Stats
    private float contactDamage = 10f;
    private float movementSpeed = 3f;
    private float health = 20f;

    // Code based Variables: do not change
    private Vector3 knockback = Vector3.zero;
    private int roomID = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.value > 0.5f)
        {
            movementSpeed = 1;
            health = 80;
            contactDamage = 20;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += knockback * Time.deltaTime;
        knockback -= knockback.normalized * Time.deltaTime;               

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (GameManager.Instance.currentRoomID != roomID)
        {
            return;
        }
        MoveCharacter();
    }

    public void MoveCharacter()
    {
        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D collision2D) {
        if (collision2D.gameObject.tag == "Player")
        {
            collision2D.transform.GetComponent<Player>().TakeDamage(contactDamage, 0);
        }
    }

    public void TakeDamage(float amount, float piercingDamage, Vector3 knockback)
    {
        health -= amount + piercingDamage;
        this.knockback = knockback;
    }

    public void SetRoomID(int id){ roomID = id;}
}
