using System.Collections;
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
    public float stunResistance = 0;

    // Prefabs
    public GameObject itemPrefab;
    public GameObject smokePrefab;
    public GameObject stunPrefab;
    
    // Components
    protected Damageable damageable;
    protected Animator animator;
    private CharacterMovement characterMovement;
    private DamageDealer damageDealer;

    // Internal Variables
    protected Vector3 movementDirection;
    protected bool isStunned = false;

    void Start()
    {
        damageable = GetComponent<Damageable>();
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
        damageDealer= GetComponent<DamageDealer>();

        contactDamage *= 1 + 0.15f*GameManager.Instance.gameData.progression;
        movementSpeed *= 1 + 0.1f*GameManager.Instance.gameData.progression;
        health *= 1 + 0.2f*GameManager.Instance.gameData.progression;

        damageable.DeathEvent += Die;
        damageable.SetValues(health, defense, false);
        damageDealer.SetDamage(contactDamage,piercingContactDamage,knockbackPower, tag);
        OnStart();
    }

    protected virtual void OnStart() {}

    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.GameOver || GameManager.Instance.currentRoomID != roomID)
        {
            return;
        }
        if (isStunned) 
        {
            characterMovement.Movement(Vector3.zero, 0);
            animator.SetBool("moving", false);
            return;
        }   

        if (movementDirection.magnitude > 0)
        {
            characterMovement.Movement(movementDirection, movementSpeed);
            animator.SetBool("moving", true);
        }
        else
        {
            characterMovement.Movement(movementDirection, movementSpeed);
            animator.SetBool("moving", false);
        }     

        OnUpdate();

    }

    protected virtual void OnUpdate() {}

    protected virtual void StopOngoingAction() {}

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

    public void GetStunned(float stunTime)
    {
        stunTime -= stunResistance*0.1f;
        if (stunTime < 0 || isStunned) {return;}
        StartCoroutine(StunTime(stunTime));
        StopOngoingAction();
        Vector3 position = transform.position + Vector3.left*0.25f + Vector3.up*0.3f;
        Destroy(Instantiate(stunPrefab, position, Quaternion.identity, transform), stunTime);
    }

    IEnumerator StunTime(float stunTime)
    {
        isStunned = true;
        animator.SetBool("stunned", true);
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
        animator.SetBool("stunned", false);
    }








    // Refactor Maybe

    protected int roomID;    
    private bool isMinion = false;
    public virtual void SetRoomID(int id){ roomID = id;}
    public virtual void DeclareAsMinion(){ isMinion = true;}    

    public int FacingFromVector(Vector3 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) )
        {
            return (direction.x > 0) ? 1 : 3;
        }
        else
        {
            return (direction.y > 0) ? 0 : 2;
        }       
    }

}