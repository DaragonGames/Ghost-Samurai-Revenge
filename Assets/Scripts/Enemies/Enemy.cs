using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Changeable Stats
    public float contactDamage = 10f;
    public float piercingContactDamage = 0;
    public float movementSpeed = 3f;
    public float health = 20f;
    public float knockbackResistance = 0f;
    public float healthGeneration = 0;
    public float defensePercentage = 0;
    public float trueDefense = 0;
    public float actionSpeed = 2f;

    // Prefabs
    public GameObject spawnAble;
    public GameObject itemPrefab;

    // Code based Variables: do not change
    private Vector3 knockback = Vector3.zero;
    protected int roomID = -1;
    private bool invincible = false;
    protected float actionCounter;
    private bool isMinion = false;

    void Start()
    {
        actionCounter = actionSpeed;
    }

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
            float ran = Random.value;
            Debug.Log(ran);
            if (ran <= (GameManager.GetLuck() * 0.025f + 0.10f) && !isMinion)
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
            CharacterFinalAction();
            Destroy(gameObject);
        }
        health += healthGeneration*Time.deltaTime;

        // Allow Actions Only if you are in the same Room as the enemy 
        if (GameManager.Instance.currentRoomID != roomID)
        {
            return;
        }
        MoveCharacter();
        actionCounter -= Time.deltaTime;
        if (actionCounter <= 0)
        {
            CharacterAction();
            actionCounter = actionSpeed;
        }
        CharacterBonusAction();        
    }

    public virtual void MoveCharacter() {}
    public virtual void CharacterAction() {}
    public virtual void CharacterBonusAction() {}
    public virtual void CharacterFinalAction() {}

    void OnCollisionStay2D(Collision2D collision2D) {
        if (collision2D.gameObject.tag == "Player")
        {
            collision2D.transform.GetComponent<Player>().TakeDamage(contactDamage, piercingContactDamage);
        }
    }

    public void TakeDamage(float amount, float piercingDamage, Vector3 knockback, float knockbackStrength)
    {
        if (invincible)
        {
            return;
        }
        float damageFlat = amount * (1-defensePercentage) + piercingDamage - trueDefense;
        health -= Mathf.Max(damageFlat ,GameManager.Instance.gameData.minDamage);
        this.knockback =knockback * Mathf.Max(knockbackStrength - knockbackResistance, 0);
        invincible = true;
        StartCoroutine(immunityFrames());
    }

    private IEnumerator immunityFrames()
    {
        yield return new WaitForSeconds(0.15f); 
        invincible = false;
    }

    public virtual void SetRoomID(int id){ roomID = id;}
    public virtual void DeclareAsMinion(){ isMinion = true;}
}
