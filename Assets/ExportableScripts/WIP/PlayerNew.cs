using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNew : MonoBehaviour
{
    // Temp
    public GameObject loseScreen;
    private bool enteringRoom;


    // Components
    private Animator animator;
    private CharacterMovement movement;
    private Damageable damageable;

    void Start()
    {
        //stats = new PlayerStats();
        //health = stats.maxHealth;
        //ammunition = stats.maxAmmunition;
        //GameManager.Instance.player = gameObject;
        
        // Add Functions to Events
        GameManager.EnterNewRoom += EnterRoom; 
        damageable.DeathEvent += Die;

        // Save Component References
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
        damageable = GetComponent<Damageable>();
        
    }

    void OnDestroy()
    {
        GameManager.EnterNewRoom -= EnterRoom;        
    }

    void Update()
    {/*
        











        // Everything old here
        if (GameManager.Instance.gameState == GameManager.GameState.GameOver)
        {
            animator.SetBool("Attacking", false);  
            animator.SetBool("Moving", false);  
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
        }  */
    }

    

    public void EnterRoom(Vector3 center)
    {
        enteringRoom = true;
        Vector3 direction = center - transform.position;
        direction.z = 0;
        movement.Movement(direction.normalized, 1);
        StartCoroutine(enterRoomDelay());
    }

    private IEnumerator enterRoomDelay()
    {
        yield return new WaitForSeconds(1); 
        enteringRoom = false;
    }


    private void Die()
    {
        GameManager.Instance.gameState = GameManager.GameState.GameOver;
        animator.SetBool("death", true); 
        animator.SetBool("Attacking", false);  
        animator.SetBool("Moving", false);  
        loseScreen.SetActive(true);
    }

}
