using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Changeable Stats
    public float contactDamage = 10f;
    public float piercingContactDamage = 0;
    public float movementSpeed = 3f;
    public float health = 20f;
    public float defense = 0;
    public float knockbackPower = 0;

    // Prefabs
    public GameObject itemPrefab;
    public GameObject smokePrefab;
    
    // Components
    private Damageable damageable;
    private Knockback knockback;
    protected Animator animator;
    private CharacterMovement characterMovement;
    private DamageDealer damageDealer;

    // Internal Variables
    protected Vector3 movementDirection;

    void Start()
    {
        damageable = GetComponent<Damageable>();
        knockback = GetComponent<Knockback>();
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
        damageDealer= GetComponent<DamageDealer>();

        contactDamage *= 1 + 0.15f*GameManager.Instance.gameData.progression;
        movementSpeed *= 1 + 0.1f*GameManager.Instance.gameData.progression;
        health *= 1 + 0.2f*GameManager.Instance.gameData.progression;

        damageable.DeathEvent += Die;
        damageable.SetValues(health, defense, false);
        damageDealer.SetDamage(contactDamage,piercingContactDamage,knockbackPower);
    }

    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.GameOver || GameManager.Instance.currentRoomID != roomID)
        {
            return;
        }   

        if (movementDirection.magnitude > 0)
        {
            characterMovement.Movement(movementDirection, movementSpeed);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }     

        OnUpdate();

    }

    protected virtual void OnUpdate() {}

    public void Die()
    {
        float ran = Random.value;
        if (ran <= (GameManager.GetLuck() * 0.025f + 0.10f) && !isMinion)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
        Destroy(Instantiate(smokePrefab, transform.position, Quaternion.identity),0.4f);            
        Destroy(gameObject);
    }








    // Refactor Maybe

    protected int roomID;    
    private bool isMinion = false;
    public virtual void SetRoomID(int id){ roomID = id;}
    public virtual void DeclareAsMinion(){ isMinion = true;}    

    // Refactor Please
    public virtual void TakeDamage(float amount, float piercingDamage, Vector3 knockback, float knockbackStrength)
    {
        damageable.TakeDamage(amount, piercingDamage);
        this.knockback.RecieveKnockback(knockback, knockbackStrength);
    }

}