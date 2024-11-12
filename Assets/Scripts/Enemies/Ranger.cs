using UnityEngine;

public class Ranger : Enemy
{
    public GameObject spawnAble;
    public float attackRange = 12f;
    public float fleeRange = 2.5f;
    public GameObject shootSoundPrefab;
    private float counter = 0;
    private float actionTime = 3f;

    protected override void OnUpdate()
    {
        MoveCharacter();
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            counter = actionTime;
            Shoot();
        }
    }

    private void MoveCharacter() { 
        movementDirection = Vector3.zero;
        Vector3 playerPosition = Player.GetPosition();
        Vector3 distanceToPlayer = playerPosition - transform.position;
        if ( distanceToPlayer.magnitude < fleeRange) 
        {
            movementDirection = distanceToPlayer.normalized *-1;
        }
        if (distanceToPlayer.magnitude > attackRange) 
        {
            movementDirection = distanceToPlayer.normalized;
        }
    }

    private void Shoot() {
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
