using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject slicePrefab;
    public GameObject arrowPrefab;
    public GameObject swordSoundPrefab;
    public GameObject shurikenSoundPrefab;
    public GameObject hitSoundPrefab;
    public GameObject loseScreen;

    private PlayerStats stats;
    private Vector3 destinatedDirection;
    
    private int facing = 0;
    private float ammunition = 0;

    // NEW STUFF or clean
    private enum States {start, idl, moving, attacking, entering, gameOver};
    private States state = States.start;
    private InputManager inputManager;
    private Animator animator;
    private CharacterMovement characterMovement;
    private Damageable damageable;

    // Start is called before the first frame update
    void Start()
    {
        stats = new PlayerStats();
        ammunition = stats.maxAmmunition;
        GameManager.Instance.player = gameObject;
            

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

        // Clean or WIP

        Vector3 movement = Vector3.zero;
        float speed = 0;

        switch (state)
        {
            case States.idl:
                movement = HandleMovement();
                speed = stats.movementSpeed;
                break;
            case States.moving:
                movement = HandleMovement();
                speed = stats.movementSpeed;
                break;
            case States.attacking:
                break;
            case States.entering:
                movement = destinatedDirection;
                speed = 1;
                break;
            case States.gameOver:
                SetAnimation(); 
                break;
        }
  
        SetAnimation(); 
        characterMovement.Movement(movement,speed); 

        ammunition += Time.deltaTime * stats.AmmunitionReloadRate;
        if (ammunition > stats.maxAmmunition)
        {
            ammunition = stats.maxAmmunition;
        }     
        
    }

    Vector3 HandleMovement()
    {
        Vector3 movement = inputManager.movement;
        
 
        if (movement.magnitude > 0)
        {
            state = States.moving;
            FacingFromVector(movement);
        }
        else
        {
            FacingFromVector(inputManager.aiming);
            state = States.idl;
        }

        return movement;  

    }



    // NEW STUFF

    void SetAnimation() 
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

    void PrimaryAttack(Vector3 attack)
    {
        if (state == States.moving || state == States.idl)
        {
            StartCoroutine(attackingFrames(stats.attackSpeed));
            state = States.attacking;
            FacingFromVector(attack);
            CreateSlice();
        }       
    }

    void SecondaryAttack(Vector3 attack)
    {
        if (state == States.moving || state == States.idl) 
        {
            FacingFromVector(attack);
            ammunition-=1;
            CreateProjectile(attack); 
        }        
    }
  
    void FacingFromVector(Vector3 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) )
        {
            if (direction.x > 0)
            {
                facing = 1;
            }
            else
            {
                facing = 3;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                facing = 0;
            }
            else
            {
                facing = 2;
            }
        }
        animator.SetInteger("Facing", facing);
    }



    void Die()
    {
        GameManager.Instance.gameState = GameManager.GameState.GameOver;
        animator.SetBool("death", true); 
        state = States.gameOver; 
        SetAnimation() ;
        loseScreen.SetActive(true);
        characterMovement.Movement(Vector3.zero,0); 
    }







    // OLD STUFF

    void CreateProjectile(Vector3 attackDirection) 
    {
        Instantiate(shurikenSoundPrefab, transform.position, Quaternion.identity); 
        Vector3 spawnPosition = transform.position + attackDirection.normalized;
        GameObject projectile = Instantiate(arrowPrefab, spawnPosition, Quaternion.identity);        
        projectile.GetComponent<Projectile>().SetShirukenValues(stats, attackDirection.normalized);       
    }

    void CreateSlice()
    {
        Instantiate(swordSoundPrefab, transform.position, Quaternion.identity); 
        // Create the Projectile
        GameObject projectile = Instantiate(slicePrefab, transform.position, Quaternion.identity, transform); 
        projectile.GetComponent<Projectile>().SetSliceValues(stats, stats.attackSpeed);

        // Change Position & Rotation based on Facing Direction
        switch (facing)
        {
            case 0:
                projectile.transform.position += new Vector3(0, 0.8f,0);
                projectile.transform.Rotate(new Vector3(0,0,90));
                break;
            case 1:
                projectile.transform.position += new Vector3(0.65f, -0.1f,0);
                break;
            case 2:
                projectile.transform.position +=new Vector3(0, -0.8f,0);
                projectile.transform.Rotate(new Vector3(0,0,90));
                break;
            case 3:
                projectile.transform.position += new Vector3(-0.65f, -0.1f,0);
                break;
        }
        GetComponent<Animator>().speed = 0.6f / stats.attackSpeed;
    } 

    private IEnumerator attackingFrames(float time)
    {
        yield return new WaitForSeconds(time); 
        if (state == States.attacking)
        {
            state = States.idl; 
        }        
        GetComponent<Animator>().speed = 1;    
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

    public void EnterRoom(Vector3 center)
    {
        destinatedDirection = center - transform.position;
        destinatedDirection.z = 0;
        destinatedDirection.Normalize();
        StartCoroutine(enterRoomDelay());
    }



    private IEnumerator enterRoomDelay()
    {
        if (state != States.start)
        {   
            state = States.entering;
        }
        yield return new WaitForSeconds(1);
        if (state == States.entering || state == States.start)
        {
            state = States.idl; 
        }  
    }

    public float GetHealthPercentage() { return damageable.GetHealthPercentage(); }

    public float GetAmmunition() { return ammunition;}
    public void UpdateStats() {stats.SetStats();}
    public PlayerStats GetStats() {return stats;}

}
