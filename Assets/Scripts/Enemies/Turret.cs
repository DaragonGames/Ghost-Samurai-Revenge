using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Shooter : Enemy
{
    public GameObject spawnAble;
    public GameObject shootSoundPrefab;
    public Vector3 actionTimes;
    public float attackSpeed = 1;
    private float counter = 0;
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
            counter = actionTimes.z + (actionTimes.y + actionTimes.x) / attackSpeed;
            counter += -0.25f * Random.Range(-1, 1);
            ongoingAction = StartCoroutine(Attack());
        }
    }

    protected override void OnStart() 
    {
        counter = Random.value;
        animator.SetFloat("attackSpeed", attackSpeed);
    }

    protected virtual void MoveCharacter() {}

    IEnumerator Attack()
    {
        animator.SetBool("attacking", true);
        isAttacking = true;
        yield return new WaitForSeconds(actionTimes.x / attackSpeed);
        Shoot();
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(actionTimes.y / attackSpeed);    
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
        projectile.GetComponentInChildren<Projectile>().SetValues(attackDirection, "Enemy", 4);
    }

    private Coroutine ongoingAction;

    protected override void StopOngoingAction() 
    {
        if (ongoingAction != null)
        {
            StopCoroutine(ongoingAction);
        }
        animator.SetBool("attacking", false);
        isAttacking = false;
    }


}
