using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject hitSoundPrefab;
    public GameObject loseScreen;

    private PlayerStats stats;    
    private float ammunition = 0;

    // NEW STUFF or clean
    private enum States {start, idl, moving, attacking, entering, gameOver};
    private States state = States.start;
    private InputManager inputManager;
    private Animator animator;
    private CharacterMovement characterMovement;
    private Damageable damageable;

    public PlayerAttack primaryAttack;
    public PlayerAttack secondaryAttack;
    public static Player Instance;
    private float attackEnergy = 1;

    // Start is called before the first frame update
    void Start()
    {
        stats = new PlayerStats();
        Instance = this;
            

        // New Stuff or based
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        characterMovement = GetComponent<CharacterMovement>();
        damageable = GetComponent<Damageable>();
        inputManager.primaryEvent += PrimaryAttack;
        inputManager.secondaryEvent += SecondaryAttack;
        GameManager.EnterNewRoom += EnterRoom;    

        // Temp
        damageable.SetValues(stats.maxHealth, stats.defense);
        damageable.DeathEvent += Die;

        
    }

    void OnDestroy()
    {
        GameManager.EnterNewRoom -= EnterRoom;        
    }

    // Update is called once per frame
    void Update()
    {
        // Old

        if (GameManager.Instance.gameState == GameManager.GameState.GameOver)
        {
            state = States.gameOver; 
            SetAnimation();
            return;
        }

        ammunition += Time.deltaTime * stats.AmmunitionReloadRate;
        if (ammunition > stats.maxAmmunition)
        {
            ammunition = stats.maxAmmunition;
        } 

        // Clean

        if (state == States.moving || state == States.idl)
        {
            HandleMovement();
        }

        if (state == States.gameOver || state == States.attacking)
        {
            characterMovement.Movement(Vector3.zero,0); 
        }

        SetAnimation();

    }





    // NEW STUFF or GOOD

    private void SetAnimation() 
    {
        switch (state)
        {
            case States.idl:
                animator.SetBool("Attacking", false);  
                animator.SetBool("Moving", false);  
                break;
            case States.moving:
                animator.SetBool("Attacking", false);  
                animator.SetBool("Moving", true);  
                break;
            case States.attacking:
                animator.SetBool("Attacking", true);  
                animator.SetBool("Moving", false);  
                break;
            case States.entering:
                animator.SetBool("Attacking", false);  
                animator.SetBool("Moving", true);  
                break;
            case States.gameOver:
                animator.SetBool("Attacking", false);  
                animator.SetBool("Moving", false);  
                break;
        } 
    }

    private void HandleMovement()
    {
        Vector3 movement = inputManager.movement;        
 
        if (movement.magnitude > 0)
        {
            state = States.moving; 
            animator.SetInteger("Facing", FacingFromVector(movement));
        }
        else
        {
            state = States.idl;  
            animator.SetInteger("Facing", FacingFromVector(inputManager.aiming));        
        }
        characterMovement.Movement(movement,stats.movementSpeed); 
        
    }

    // Combat related stuff

    private void PrimaryAttack(Vector3 direction)
    {
        Attack(primaryAttack, direction);
    }

    private void SecondaryAttack(Vector3 direction)
    {
        Attack(secondaryAttack, direction);       
    }

    private void Attack(PlayerAttack attack, Vector3 direction)
    {
        if (state == States.moving || state == States.idl)
        {
            // Only attack when there is enough energy for it
            if (attackEnergy < attack.attackCost)
            {
                return;
            }
            else
            {
                attackEnergy -= attack.attackCost;
            }

            // Make Character Face towards the Attack            
            animator.SetInteger("Facing", FacingFromVector(direction));

            // Start the internal Process of the Attack
            StartCoroutine(AttackSequence(primaryAttack, direction));
        }        
    }

    private IEnumerator AttackSequence(PlayerAttack attack, Vector3 direction)
    {
        // Phase 1: Pre Attack - Set Values before Attack
        Vector3 time = attack.attackTime * stats.attackSpeed;
        state = States.attacking;
        GetComponent<Animator>().speed = stats.attackSpeed; 
        yield return new WaitForSeconds(time.x);

        // Phase 2: Attack
        attack.Attack(this, direction);
        yield return new WaitForSeconds(time.y);

        // Phase 3: Post Attack - Still in the Attack State, but no longer a threat
        yield return new WaitForSeconds(time.z);

        // Phase 4: End Attack - Reset Values
        if (state == States.attacking)
        {
            state = States.idl; 
        }        
        GetComponent<Animator>().speed = 1;    
    }

    // Handeling the Short peacefull Moment of entering a new room

    private void EnterRoom(Vector3 center)
    {
        Vector3 destinatedDirection = center - transform.position;
        destinatedDirection.z = 0;
        destinatedDirection.Normalize();
        if (state != States.start)
        {   
            StartCoroutine(enterRoomDelay(destinatedDirection));            
        }
        else
        {
            state = States.idl;
        }        
    }
    
    private IEnumerator enterRoomDelay(Vector3 direction)
    {  
        characterMovement.Movement(direction,1); 
        state = States.entering;        
        yield return new WaitForSeconds(1);
        if (state == States.entering)
        {
            state = States.idl; 
        }  
    }

    // Helper Functions

    public static int FacingFromVector(Vector3 direction)
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





















    // OLD STUFF or Improve this

    void Die()
    {
        GameManager.Instance.gameState = GameManager.GameState.GameOver;
        animator.SetBool("death", true); 
        state = States.gameOver; 
        SetAnimation() ;
        loseScreen.SetActive(true);
        characterMovement.Movement(Vector3.zero,0); 
    }

    public void TakeDamage(float amount, float piercingDamage)
    {
        damageable.TakeDamage(amount,piercingDamage);
        Instantiate(hitSoundPrefab, transform.position, Quaternion.identity);
        StartCoroutine(redFlash());
    }

    private IEnumerator redFlash()
    {
        for (int i = 0; i<16; i++ ) 
        {
            float redflash = 0.8f - 0.05f * i;
            GetComponent<SpriteRenderer>().color = new Color(1, 1-redflash, 1-redflash);
            yield return new WaitForSeconds(0.05f); 
        }
        GetComponent<SpriteRenderer>().color = new Color(1,1,1);
    }

    public float GetHealthPercentage() { return damageable.GetHealthPercentage(); }

    public float GetAmmunition() { return ammunition;}
    public void UpdateStats() {stats.SetStats();}
    public PlayerStats GetStats() {return stats;}

    public static Vector3 GetPosition()
    {
        return Instance.transform.position;
    }

}
