using System.Collections;
using Unity.Collections;
using UnityEngine;

public class GhostAttacks : MonoBehaviour
{
    public GameObject orbitingBlade;
    public GameObject demonBlade;
    public GameObject leaf;
    public GameObject turret;
    public GameObject charger;
    private GameObject[] turrets = new GameObject[4];
    private int lastAttack;

    public Vector2 Attack()
    {
        // Ensure some variety in the attacks
        int rand = lastAttack;
        while (rand == lastAttack)
        {
            rand = Random.Range(0,5);
        }
        lastAttack = rand;

        // Execute the attacks
        switch(rand)
        {
            case 0:
                return BladeCircleAttack();
            case 1:
                return SpittingLeavesAttack();
            case 2:
                return ThrowingBladeAttack();
            case 3:
                return SummonMinions();
            case 4:
                return SummonTowers();
            default:
                return new Vector2(0,0);
        }        
    }

    private Vector2 BladeCircleAttack() {
        StartCoroutine(DoBladeCircleAttack());
        return new Vector2(4f,1f);
    }

    public Vector2 SpittingLeavesAttack() {
        StartCoroutine(PrepareSpittingLeavesAttack());
        return new Vector2(3,2);
    }

    public Vector2 ThrowingBladeAttack() {
        StartCoroutine(DoThrowingBladeAttack());
        return new Vector2(3f,2f);
    }

    private Vector2 SummonMinions() {
        GetComponent<Animator>().SetBool("minionSummon", true);
        SummonEnemy(charger, transform.position+new Vector3(1.5f,0,0));
        SummonEnemy(charger, transform.position+new Vector3(-1.5f,0,0));
        StartCoroutine(MinionAnimationDelay());
        return new Vector2(1,2f);
    }

    private Vector2 SummonTowers() {

        Vector3 center = transform.parent.position;
        GetComponent<Animator>().SetBool("minionSummon", true);

        for (int i = 0; i<4; i++)
        {
            float x = (0.5f - (i%2))  *13f;
            float y = (i>1) ? -2.5f : 2.5f;
            if (turrets[i] == null)
            {
                turrets[i] = SummonEnemy(turret,center+new Vector3(x,y));
            }
        }     
        StartCoroutine(MinionAnimationDelay());
        return new Vector2(1,4f);
    }

    IEnumerator MinionAnimationDelay() {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Animator>().SetBool("minionSummon", false);
    }

    IEnumerator DoBladeCircleAttack() {
        GetComponent<Animator>().SetBool("swordSummon", true);
        // prepare Attack
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().SetBool("swordSummon", false);

        // Actual Attack
        GameObject projectile = Instantiate(orbitingBlade, transform.position, Quaternion.identity, transform);
        float roationSpeed = -2.25f+Random.value*0.5f;
        projectile.GetComponentInChildren<Projectile>().SetValues(Vector3.zero, "Enemy", 3f, 0f, roationSpeed);
        

        // Rotate the Projectile
        Vector3 attackDirection = Player.GetPosition() - transform.position;
        attackDirection.z = 0;
        float angle = Vector3.Angle(attackDirection, Vector3.right);
        if (attackDirection.y <0)
        {
            angle = angle * -1;
        }
        projectile.transform.Rotate(new Vector3(0,0,angle));
    }

    IEnumerator DoThrowingBladeAttack()
    {
        GetComponent<Animator>().SetBool("swordSummon", true);
        // prepare Attack
        yield return new WaitForSeconds(1f);

        // Actual Attack
        CreateBlade();
        yield return new WaitForSeconds(1f);
        CreateBlade();
        yield return new WaitForSeconds(1f);
        CreateBlade();
        GetComponent<Animator>().SetBool("swordSummon", false);
    }

    IEnumerator PrepareSpittingLeavesAttack()
    {
        GetComponent<Animator>().SetBool("spitting", true);
        yield return new WaitForSeconds(1f);
        Vector3 target = Player.GetPosition();
        StartCoroutine(DoSpittingLeavesAttack(2f,target));
        GetComponent<Animator>().SetBool("spitting", false);
    }

    IEnumerator DoSpittingLeavesAttack(float timeLeft, Vector3 target) 
    {
        // Creates a leaf projektiles
        CreatLeave(target);

        // Run this multiple times in random intervalls   
        float waitTime = Random.value*0.1f+0.05f;  
        yield return new WaitForSeconds(waitTime);
        timeLeft-=waitTime;        

        if(timeLeft>0)
        {
            StartCoroutine(DoSpittingLeavesAttack(timeLeft, target));
        }
    }

    private void CreateBlade()
    {
        Vector3 attackDirection = Player.GetPosition() - transform.position;
        attackDirection.z = 0;
        attackDirection.Normalize();

        GameObject projectile = Instantiate(demonBlade, transform.position+attackDirection, Quaternion.identity);
        float roationSpeed = Random.Range(-2.5f,-3f);
        projectile.GetComponentInChildren<Projectile>().SetValues(attackDirection, "Enemy", 4f, 2f, roationSpeed);


        return;
    }

    private void CreatLeave(Vector3 target) {
        GameObject projectile = Instantiate(leaf, transform.position+Vector3.up*0.5f, Quaternion.identity);
        Vector3 attackDirection = target - transform.position;
        attackDirection.z = 0;
        attackDirection.Normalize();
        attackDirection.y += Random.Range(-0.35f,0.35f);
        attackDirection.x += Random.Range(-0.35f,0.35f);
        attackDirection.Normalize();

        float roationSpeed = Random.Range(0.4f,2f);
        float acceleration = Random.Range(0,0.2f);
        projectile.GetComponentInChildren<Projectile>().SetValues(attackDirection, "Enemy", 2f, acceleration, roationSpeed);

    }   

    private GameObject SummonEnemy(GameObject prefab, Vector3 position)
    {
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        obj.GetComponent<Enemy>().SetRoomID(GameManager.Instance.currentRoomID);
        obj.GetComponent<Enemy>().DeclareAsMinion();
        return obj;
    }

}