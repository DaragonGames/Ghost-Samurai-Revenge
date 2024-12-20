using System.Collections;
using UnityEngine;

public class Charger : Enemy
{
    private bool charging;
    private Vector3 targetDirection;

    private float counter = 1;
    private float actionTime = 5f;

    protected override void OnUpdate()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            StartCoroutine(Attack());
            charging = true;
            counter = actionTime;
        }
        if (!charging)
        {
            movementDirection = Vector3.zero;
            Vector3 playerPosition = Player.GetPosition();
            targetDirection = (playerPosition - transform.position).normalized;
            GetComponent<SpriteRenderer>().flipX = targetDirection.x < 0;
        }
    }

    IEnumerator Attack()
    {
        // Pre Attack Set Animation and wait a second
        GetComponent<Animator>().SetBool("charging", true);  
        yield return new WaitForSeconds(1);
        // Start Moving and reset Animation Value
        GetComponent<Animator>().SetBool("charging", false);  
        movementDirection = targetDirection;
        yield return new WaitForSeconds(2);
        // Stop the movement and make the enemy aim again
        charging = false;
    }

}
