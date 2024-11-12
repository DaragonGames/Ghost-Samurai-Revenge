using UnityEngine;

public class Charger : Enemy
{
    private bool charging;
    private Vector3 targetDirection;

    private float counter = 0;
    private float actionTime = 3f;

    protected override void OnUpdate()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            charging = !charging;
            counter = actionTime;
            GetComponent<Animator>().SetBool("charging", charging);  
        }
        if (charging)
        {
            movementDirection = targetDirection;
        }
        else
        {
            movementDirection = Vector3.zero;
            Vector3 playerPosition = Player.GetPosition();
            targetDirection = (playerPosition - transform.position).normalized;
            GetComponent<SpriteRenderer>().flipX = targetDirection.x < 0;
        }
    }

}
