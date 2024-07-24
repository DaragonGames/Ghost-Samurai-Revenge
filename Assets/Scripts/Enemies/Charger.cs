using System.Collections;
using UnityEngine;

public class Charger : Enemy
{
    private bool charging;
    private Vector3 targetDirection;

    void Start() {
        //base.Start();
        actionCounter = actionSpeed/2;
    }

    public override void MoveCharacter() 
    { 
        if (targetDirection.x > 0) 
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (targetDirection.x < 0) 
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (charging) 
        {
            transform.position += targetDirection * movementSpeed * Time.deltaTime;
        } 
        GetComponent<Animator>().SetBool("charging", charging);       
    }

    public override void CharacterAction() 
    {         
        StartCoroutine(chargingDuration());
        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        targetDirection = (playerPosition - transform.position).normalized;
    }

    private IEnumerator chargingDuration()
    {
        charging = true;
        yield return new WaitForSeconds(actionSpeed/2); 
        charging = false;      
    }
}
