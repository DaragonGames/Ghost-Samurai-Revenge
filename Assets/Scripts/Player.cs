using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject attackPrefab;

    private float health;
    private PlayerStats stats;
    private bool immuneToDamage, attacking, enteringRoom;
    private Vector3 destinatedDirection;




    // Start is called before the first frame update
    void Start()
    {
        stats = new PlayerStats();
        health = stats.maxHealth;
        GameManager.Instance.player = gameObject;
        GameManager.EnterNewRoom += EnterRoom; 
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
    }

    void HandleAttacks()
    {
        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            // Set the Attack State
            attacking = true;
            StartCoroutine(attackingFrames(1f));

            // Create the Projectile
            Vector3 attackDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            attackDirection.z = 0;
            attackDirection.Normalize();
            GameObject projectile = Instantiate(attackPrefab, transform.position + attackDirection, Quaternion.identity,transform);
            
            // Rotate the Projectile
            float angle = Vector3.Angle(attackDirection, Vector3.left);
            if (attackDirection.y >0)
            {
                angle = angle * -1;
            }
            projectile.transform.Rotate(new Vector3(0,0,angle));
        }
    }

    void HandleMovement()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey("w"))
        {
            movement += Vector3.up;
        }
        if (Input.GetKey("s"))
        {
            movement += Vector3.down;    
        } 
        if (Input.GetKey("a"))
        {
            movement += Vector3.left;       
        }
        if (Input.GetKey("d"))
        {
            movement += Vector3.right;
        } 

        float sprintSpeedFactor = Input.GetKey(KeyCode.LeftShift) ? stats.sprintingSpeedMultiplier : 1f;

        transform.position += movement.normalized * Time.deltaTime * sprintSpeedFactor * stats.movementSpeed;  

    }

    public void TakeDamage(float amount)
    {
        if (immuneToDamage)
        {
            return;
        }
        health -= amount;
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
    }

    private IEnumerator enterRoomDelay()
    {
        yield return new WaitForSeconds(1); 
        enteringRoom = false;
    }

    public float GetHealth() { return health; }

}
