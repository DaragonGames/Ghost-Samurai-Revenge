using System.Collections;
using UnityEngine;

public class Ranger : Enemy
{
    public GameObject spawnAble;
    public float attackRange = 12f;
    public float fleeRange = 2.5f;
    public GameObject shootSoundPrefab;
    private float counter = 0;
    private float actionTime = 3.5f;
    private bool isAttacking = false;

    protected override void OnUpdate()
    {
        movementDirection = Vector3.zero;
        if (!isAttacking)
        {
            MoveCharacter();
        }
        
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            counter = actionTime;
            StartCoroutine(Attack());
        }
    }

    private void MoveCharacter() { 
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

    IEnumerator Attack()
    {
        int facing = FacingFromVector(Player.GetPosition()- transform.position);
        animator.SetInteger("facing", facing);
        animator.SetBool("attacking", true);
        isAttacking = true;
        yield return new WaitForSeconds(2);
        Shoot();
        yield return new WaitForSeconds(0.65f);
        animator.SetBool("attacking", false);
        isAttacking = false;
    }

    private void Shoot() {
        Instantiate(shootSoundPrefab, transform.position, Quaternion.identity);
        
        // Create the Projectile
        Vector2 attackDirection = (Player.GetPosition() - transform.position).normalized;
        GameObject projectile = Instantiate(spawnAble, transform.position, Quaternion.identity);
        
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
