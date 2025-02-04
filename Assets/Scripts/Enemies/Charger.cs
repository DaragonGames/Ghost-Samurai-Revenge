using System.Collections;
using UnityEngine;

public class Charger : Enemy
{
    public float chargeDamage;
    private float touchDamage;

    private bool charging;
    private Vector3 targetDirection;

    private float counter = 1;
    private float actionTime = 3f;

    protected override void OnUpdate()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            ongoingAction = StartCoroutine(Attack());
            charging = true;
            counter = actionTime;
            counter += -0.25f * Random.Range(-1, 1);
        }
        if (!charging)
        {
            movementDirection = Vector3.zero;
            Vector3 playerPosition = Player.GetPosition();
            targetDirection = (playerPosition - transform.position).normalized;
            GetComponent<SpriteRenderer>().flipX = targetDirection.x < 0;
        }
    }

    protected override void OnStart() 
    {
        counter = Random.value;
        touchDamage = contactDamage;
    }

    IEnumerator Attack()
    {
        // Pre Attack Set Animation and wait a second
        GetComponent<Animator>().SetBool("charging", true);  
        yield return new WaitForSeconds(1);
        // Start Moving and reset Animation Value
        GetComponent<Animator>().SetBool("charging", false);  
        GetComponent<Knockback>().knockbackResistance = 15;
        movementDirection = targetDirection;
        damageDealer.SetDamage(chargeDamage,piercingContactDamage,knockbackPower, tag);
        yield return new WaitForSeconds(1.2f);
        // Stop the movement and make the enemy aim again
        charging = false;
        GetComponent<Knockback>().knockbackResistance = 5;
        damageDealer.SetDamage(touchDamage,piercingContactDamage,knockbackPower, tag);
    }

    private Coroutine ongoingAction;

    protected override void StopOngoingAction() 
    {
        if (ongoingAction != null)
        {
            StopCoroutine(ongoingAction);
        }
        GetComponent<Animator>().SetBool("charging", false);  
        charging = false;
        GetComponent<Knockback>().knockbackResistance = 5;
        damageDealer.SetDamage(touchDamage,piercingContactDamage,knockbackPower, tag);
    }

}
