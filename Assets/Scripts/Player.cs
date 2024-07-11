using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject slicePrefab;
    public GameObject arrowPrefab;

    private float health;
    private PlayerStats stats;
    private bool immuneToDamage, attacking, enteringRoom;
    private Vector3 destinatedDirection;
    private Animator animator;




    // Start is called before the first frame update
    void Start()
    {
        stats = new PlayerStats();
        health = stats.maxHealth;
        GameManager.Instance.player = gameObject;
        GameManager.EnterNewRoom += EnterRoom; 
        animator = GetComponent<Animator>();
    }

    void OnDestroy()
    {
        GameManager.EnterNewRoom -= EnterRoom; 
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            GameManager.Instance.gameState = GameManager.GameState.GameOver;
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
            attacking = true;
            StartCoroutine(attackingFrames(stats.attackSpeed));
            CreateProjectile(arrowPrefab, "moving");
            
        }
    }

    void CreateProjectile(GameObject prefab, string type) 
    {
        // Create the Projectile
        Vector3 attackDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        attackDirection.z = 0;
        attackDirection.Normalize();
        Vector3 spawnPosition = transform.position + attackDirection;
        GameObject projectile = Instantiate(prefab, spawnPosition, Quaternion.identity);
        
        // Rotate the Projectile
        float angle = Vector3.Angle(attackDirection, Vector3.left);
        if (attackDirection.y >0)
        {
            angle = angle * -1;
        }
        projectile.transform.Rotate(new Vector3(0,0,angle));

        // Set its Values
        switch (type)
        {
            case "static":
                projectile.GetComponent<Projectile>().SetSliceValues(stats, stats.attackSpeed);
                projectile.transform.parent = transform;
                break;
            case "moving":
                projectile.GetComponent<Projectile>().SetArrowValues(stats, attackDirection);
                break;
        }        
    }

    void CreateSlice()
    {
        // Create the Projectile
        GameObject projectile = Instantiate(slicePrefab, transform.position, Quaternion.identity, transform); 
        projectile.GetComponent<Projectile>().SetSliceValues(stats, stats.attackSpeed);

        // Change Characters Facing Direction
        Vector3 attackDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (Mathf.Abs(attackDirection.x) > Mathf.Abs(attackDirection.y) )
        {
            if (attackDirection.x > 0)
            {
                animator.SetInteger("Facing", 1);
                projectile.transform.position += new Vector3(0.65f, -0.1f,0);
            }
            else
            {
                animator.SetInteger("Facing", 3);
                projectile.transform.position += new Vector3(-0.65f, -0.1f,0);
            }
        }
        else
        {
            if (attackDirection.y > 0)
            {
                animator.SetInteger("Facing", 0);
                projectile.transform.position += new Vector3(0, 0.8f,0);
                projectile.transform.Rotate(new Vector3(0,0,90));
            }
            else
            {
                animator.SetInteger("Facing", 2);
                projectile.transform.position +=new Vector3(0, -0.8f,0);
                projectile.transform.Rotate(new Vector3(0,0,90));
            }
        }

        GetComponent<Animator>().speed = 0.6f / stats.attackSpeed;



        

        // Rotate?
    }

    void HandleMovement()
    {
        if (attacking) {return;}  

        Vector3 movement = Vector3.zero;

        if (Input.GetKey("w"))
        {
            movement += Vector3.up;
            animator.SetInteger("Facing", 0);
        }
        if (Input.GetKey("s"))
        {
            movement += Vector3.down;  
            animator.SetInteger("Facing", 2);  
        } 
        if (Input.GetKey("a"))
        {
            movement += Vector3.left;
            animator.SetInteger("Facing", 3);       
        }
        if (Input.GetKey("d"))
        {
            movement += Vector3.right;
            animator.SetInteger("Facing", 1);  
        } 
        animator.SetBool("Moving", movement.magnitude > 0);

        transform.position += movement.normalized * Time.deltaTime * stats.movementSpeed;  

    }

    public void TakeDamage(float amount, float piercingDamage)
    {
        if (immuneToDamage)
        {
            return;
        }
        float damageFlat = amount * (1-stats.defensePercentage) + piercingDamage - stats.trueDefense;
        health -= Mathf.Max(Mathf.Min(damageFlat, stats.maxDamage),GameManager.Instance.gameData.minDamage);
        immuneToDamage = true;
        StartCoroutine(immunityFrames(stats.invisibleFramesDuration));
    }

    public void EnterRoom(Vector3 center)
    {
        enteringRoom = true;
        destinatedDirection = center - transform.position;
        destinatedDirection.z = transform.position.z;
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

    public float GetHealth() { return health; }
    public void UpdateStats() {stats.SetStats();}

}
