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
        if (charging) 
        {
            transform.position += targetDirection * movementSpeed * Time.deltaTime;
        }        
    }

    public override void CharacterAction() 
    { 
        charging = true;
        StartCoroutine(chargingDuration());
        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        targetDirection = (playerPosition - transform.position).normalized;
    }

    private IEnumerator chargingDuration()
    {
        yield return new WaitForSeconds(actionSpeed/2); 
        charging = false;      
    }
}
