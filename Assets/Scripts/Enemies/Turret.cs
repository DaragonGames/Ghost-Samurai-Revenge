using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : Enemy
{
    public GameObject shootSoundPrefab;

    public override void MoveCharacter() { return; }

    public override void CharacterAction() {
        Instantiate(shootSoundPrefab, transform.position, Quaternion.identity);
        
        // Create the Projectile
        Vector3 attackDirection = Player.GetPosition() - transform.position;
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
