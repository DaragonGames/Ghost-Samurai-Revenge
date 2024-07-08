using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ranger : Enemy
{
    public float attackRange = 12f;
    public float fleeRange = 2.5f;

    public override void MoveCharacter() { 
        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        Vector3 distanceToPlayer = playerPosition - transform.position;
        if ( distanceToPlayer.magnitude < fleeRange) 
        {
            transform.position -= distanceToPlayer.normalized * movementSpeed * Time.deltaTime;
        }
        if (distanceToPlayer.magnitude > attackRange) 
        {
            transform.position += distanceToPlayer.normalized * movementSpeed * Time.deltaTime;
        }
    }

    public override void CharacterAction() {
        // Create the Projectile
        Vector3 attackDirection = GameManager.Instance.player.transform.position - transform.position;
        attackDirection.z = 0;
        attackDirection.Normalize();
        Vector3 spawnPosition = transform.position + attackDirection*0.7f;
        GameObject projectile = Instantiate(spawnAble, spawnPosition, Quaternion.identity);
        
        // Rotate the Projectile
        float angle = Vector3.Angle(attackDirection, Vector3.left);
        if (attackDirection.y >0)
        {
            angle = angle * -1;
        }
        projectile.transform.Rotate(new Vector3(0,0,angle));

        // Set Values
        projectile.GetComponent<Projectile>().SetValues("Enemy",attackDirection);
    }
}
