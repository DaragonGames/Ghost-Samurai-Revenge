using System.Collections;
using UnityEngine;

public class Ranger : Shooter
{    
    public float attackRange = 6f;
    public float fleeRange = 2.5f;

    protected override void MoveCharacter() { 
        Vector3 playerPosition = Player.GetPosition();
        Vector3 distanceToPlayer = playerPosition - transform.position;
        if ( distanceToPlayer.magnitude < fleeRange) 
        {
            movementDirection = distanceToPlayer.normalized *-1;
            animator.SetInteger("facing", FacingFromVector(movementDirection));
        }
        if (distanceToPlayer.magnitude > attackRange) 
        {
            movementDirection = distanceToPlayer.normalized;
            animator.SetInteger("facing", FacingFromVector(movementDirection));
        }
    }

}
