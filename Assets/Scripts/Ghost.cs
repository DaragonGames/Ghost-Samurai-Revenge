using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Ghost : Enemy
{
    public GameObject orbitingBlade;
    public GameObject demonBlade;
    public GameObject leaf;
    public GameObject turret;
    public GameObject charger;

    private Room bossRoom; 
    public void SetBossRoom(Room room){bossRoom=room;}
    private GameObject[] turrets = new GameObject[4];

    private float actionTimer = -3;
    private bool attacking = false;
    public Vector3 movingGoal;

    // Start is called before the first frame update
    void Start()
    {
        roomID = GameManager.Instance.currentRoomID ;
        movingGoal = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        actionTimer +=Time.deltaTime; 
        if (actionTimer >= 0)
        {
            Attack();
        }
        if (!attacking)
        {
            Vector3 direction =movingGoal-transform.position;
            float step = movementSpeed * Time.deltaTime;
            transform.position += step * direction.normalized;
            if (direction.magnitude < step *2)
            {
                Debug.Log("Go somewhere");
                movingGoal.x = Random.Range(-5, 5);
                movingGoal.y = Random.Range(-2.25f, 2.25f);
            }
        }
    }

    //
    // Attacks and attack Behaviour
    // 

    private void Attack()
    {
        int rand = Random.Range(0,5);
        switch(rand)
        {
            case 0:
                SwirlingBladeCirle();
                actionTimer = -5f;
                break;
            case 1:
                attacking = true;
                StartCoroutine(SpitLeafes(2.5f, GameManager.Instance.player.transform.position));
                actionTimer = -3f;
                break;
            case 2:
                attacking = true;
                StartCoroutine(BladeAttack());
                actionTimer = -3f;
                break;
            case 3:
                ChargerAttack();
                actionTimer = -3f;
                break;
            case 4:
                TurretAttack();
                actionTimer = -3f;
                break;
        }
    }

    public void SwirlingBladeCirle() {
        GameObject projectile = Instantiate(orbitingBlade, transform.position, Quaternion.identity, transform);
        projectile.GetComponentInChildren<Projectile>().SetValues("Enemy",Vector3.zero, 2f);
        // Rotate the Projectile
        Vector3 attackDirection = GameManager.Instance.player.transform.position - transform.position;
        attackDirection.z = 0;
        float angle = Vector3.Angle(attackDirection, Vector3.right);
        if (attackDirection.y <0)
        {
            angle = angle * -1;
        }
        projectile.transform.Rotate(new Vector3(0,0,angle));
    }

    IEnumerator SpitLeafes(float timeLeft, Vector3 target) {

        // Run this multiple times in random intervalls
        float ran = Random.value*0.1f+0.05f;
        yield return new WaitForSeconds(ran);
        timeLeft-=ran;
        if(timeLeft>0)
        {
            StartCoroutine(SpitLeafes(timeLeft, target));
        }
        else
        {
            attacking = false;
        }

        // Creates a leaf projektiles
        GameObject projectile = Instantiate(leaf, transform.position+Vector3.up*0.5f, Quaternion.identity);
        Vector3 attackDirection = target - transform.position;
        attackDirection.z = 0;
        attackDirection.Normalize();
        attackDirection.y += Random.Range(-0.35f,0.35f);
        attackDirection.x += Random.Range(-0.35f,0.35f);
        attackDirection.Normalize();

        float roationSpeed = Random.Range(0.4f,2f);

        projectile.GetComponent<Projectile>().SetValues("Enemy",attackDirection, roationSpeed,Random.value*-2f);

    }

    IEnumerator BladeAttack()
    {
        CreateBlade();
        yield return new WaitForSeconds(1f);
        CreateBlade();
        yield return new WaitForSeconds(1f);
        CreateBlade();
        yield return new WaitForSeconds(1f);
        attacking = false;
    }

    private void CreateBlade()
    {
        Vector3 attackDirection = GameManager.Instance.player.transform.position - transform.position;
        attackDirection.z = 0;
        attackDirection.Normalize();

        GameObject projectile = Instantiate(demonBlade, transform.position+attackDirection, Quaternion.identity);
        projectile.GetComponentInChildren<Projectile>().SetValues("Enemy",attackDirection, 3f,4f);
        return;
    }

    private void TurretAttack()
    {
        Vector3 center = bossRoom.transform.position;

        if (turrets[0] == null)
        {
            turrets[0] = Instantiate(turret, center+new Vector3(6.5f,2.5f,0f), Quaternion.identity);
            turrets[0].GetComponent<Enemy>().SetRoomID(GameManager.Instance.currentRoomID);
        }

        if (turrets[1] == null)
        {
            turrets[1] = Instantiate(turret, center+new Vector3(-6.5f,2.5f,0f), Quaternion.identity);
            turrets[1].GetComponent<Enemy>().SetRoomID(GameManager.Instance.currentRoomID);
        }

        if (turrets[2] == null)
        {
            turrets[2] = Instantiate(turret, center+new Vector3(6.5f,-2.5f,0f), Quaternion.identity);
            turrets[2].GetComponent<Enemy>().SetRoomID(GameManager.Instance.currentRoomID);
        }

        if (turrets[3] == null)
        {
            turrets[3] = Instantiate(turret, center+new Vector3(-6.5f,-2.5f,0f), Quaternion.identity);
            turrets[3].GetComponent<Enemy>().SetRoomID(GameManager.Instance.currentRoomID);
        }

    }

    private void ChargerAttack()
    {
        Instantiate(charger, transform.position+new Vector3(1.5f,0,0), Quaternion.identity)
        .GetComponent<Enemy>().SetRoomID(GameManager.Instance.currentRoomID);

        Instantiate(charger, transform.position+new Vector3(-1.5f,0,0), Quaternion.identity)
        .GetComponent<Enemy>().SetRoomID(GameManager.Instance.currentRoomID);
    }
}
