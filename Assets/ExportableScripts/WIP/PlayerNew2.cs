using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNew2 : MonoBehaviour
{
    public GameObject slicePrefab;
    public GameObject arrowPrefab;
    public GameObject swordSoundPrefab;
    public GameObject shurikenSoundPrefab;
    public GameObject hitSoundPrefab;
    public GameObject loseScreen;

    private float health;
    private PlayerStats stats;
    private bool immuneToDamage, attacking, enteringRoom;
    private Vector3 destinatedDirection;
    private Animator animator;
    private int facing = 0;
    private float ammunition = 0;
    private float redflash = 0;



    //public float attackSpeedModifier = 0.5f;
    //public float movementSpeedModifier = 2;



    // Start is called before the first frame update
    void Start()
    {
        stats = new PlayerStats();
        health = stats.maxHealth;
        ammunition = stats.maxAmmunition;
        GameManager.Instance.player = gameObject;
        GameManager.EnterNewRoom += EnterRoom; 
        animator = GetComponent<Animator>();
    }

    void OnDestroy()
    {
        GameManager.EnterNewRoom -= EnterRoom;        
    }

    private enum States {idl, moving, attacking, entering, gameOver};
    private States state = States.idl;

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
        // animator.SetInteger("Facing", facing);  
    }


    // Update is called once per frame
    void Update()
    {
        SetAnimation();
        if (GameManager.Instance.gameState == GameManager.GameState.GameOver)
        {
            state = States.gameOver;
            return;
        }
        if (health <= 0)
        {
            GameManager.Instance.gameState = GameManager.GameState.GameOver;
            state = States.gameOver;
            animator.SetBool("death", true); 
            loseScreen.SetActive(true);
            return;
        }

        if (enteringRoom)
        {
            transform.position += destinatedDirection*Time.deltaTime;
        }
        else
        {
            HandleMovement();
            HandleAttacks();
        }    
        animator.SetBool("Attacking", attacking);    
        ammunition += Time.deltaTime * stats.AmmunitionReloadRate;
        if (ammunition > stats.maxAmmunition)
        {
            ammunition = stats.maxAmmunition;
        }
        if (redflash > 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1-redflash, 1-redflash);
            redflash -= Time.deltaTime* 1.2f;
        }
    }

    void HandleAttacks()
    {
        if (attacking)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            // Set the Attack State            
            StartCoroutine(attackingFrames(stats.attackSpeed));
            CreateSlice();
            attacking = true;
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            // Set the Attack State
            if (ammunition >= 1)
            {
                ammunition-=1;
                CreateProjectile();
            }            
        }
    }

    void CreateProjectile() 
    {
        Instantiate(shurikenSoundPrefab, transform.position, Quaternion.identity); 
        // Create the Projectile
        Vector3 attackDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        attackDirection.z = 0;
        attackDirection.Normalize();
        Vector3 spawnPosition = transform.position + attackDirection;
        GameObject projectile = Instantiate(arrowPrefab, spawnPosition, Quaternion.identity);        
        projectile.GetComponent<Projectile>().SetShirukenValues(stats, attackDirection);       
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


    void FaceTowardsInput()
    {
        // Change Characters Facing Direction
        Vector3 mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (Mathf.Abs(mouseDirection.x) > Mathf.Abs(mouseDirection.y) )
        {
            if (mouseDirection.x > 0)
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
            if (mouseDirection.y > 0)
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

    void HandleMovement()
    {
        if (attacking) {return;}  

        Vector3 movement = Vector3.zero;

        if (Input.GetKey("w"))
        {
            movement += Vector3.up;
            facing = 0;
        }
        if (Input.GetKey("s"))
        {
            movement += Vector3.down;  
            facing = 2;
        } 
        if (Input.GetKey("a"))
        {
            movement += Vector3.left;
            facing = 3;    
        }
        if (Input.GetKey("d"))
        {
            movement += Vector3.right;
            facing = 1;
        }
        animator.SetInteger("Facing", facing);   
        animator.SetBool("Moving", movement.magnitude > 0);

        transform.position += movement.normalized * Time.deltaTime * stats.movementSpeed;  

    }

    public void TakeDamage(float amount, float piercingDamage)
    {
        if (immuneToDamage)
        {
            return;
        }
        float damageFlat = amount * (1-stats.defense) + piercingDamage - stats.trueDefense;
        health -= Mathf.Max(Mathf.Min(damageFlat, stats.maxDamage),GameManager.Instance.gameData.minDamage);
        immuneToDamage = true;
        StartCoroutine(immunityFrames(stats.invisibleFramesDuration));
        Instantiate(hitSoundPrefab, transform.position, Quaternion.identity);
        redflash = 0.8f;
    }

    public void EnterRoom(Vector3 center)
    {
        enteringRoom = true;
        destinatedDirection = center - transform.position;
        destinatedDirection.z = 0;
        destinatedDirection.Normalize();
        StartCoroutine(enterRoomDelay());
    }

    private IEnumerator immunityFrames(float time)
    {
        yield return new WaitForSeconds(time); 
        immuneToDamage = false;
    }

    private IEnumerator attackingFrames(float time)
    {
        yield return new WaitForSeconds(time); 
        attacking = false;    
        GetComponent<Animator>().speed = 1;    
    }

    private IEnumerator enterRoomDelay()
    {
        yield return new WaitForSeconds(1); 
        enteringRoom = false;
    }

    public float GetHealthPercentage() { return health/stats.maxHealth; }

    public float GetAmmunition() { return ammunition;}
    public void GainHealth(float value) { 
        health += value; 
        if (health > stats.maxHealth)
        {
            health = stats.maxHealth;
        }
    }
    public void UpdateStats() {stats.SetStats();}
    public PlayerStats GetStats() {return stats;}

}
