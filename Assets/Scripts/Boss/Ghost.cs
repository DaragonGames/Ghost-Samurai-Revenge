using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    public GameObject GhostUI;
    public GameObject destructionBladesPrefab;

    private Vector3 movingGoal;
    private GhostAttacks attacks;
    private enum states {wandering, fleeing, retaliation, waiting, attacking, postAttack};
    private states state = states.wandering; 
    private float attackCounter;
    private float postAttackCounter;
    private float anger;

    void Start()
    {
        // Does this work
        Transform t = transform.parent.GetComponentInChildren<Room>().transform;
        transform.parent = t;

        movingGoal = transform.position;
        attacks = GetComponent<GhostAttacks>();
        anger = GameManager.Instance.gameData.ghostWrath;
        if (anger == 0)
        {
            gameObject.SetActive(false);
        }
        if (anger >= 40)
        {
            PrepareBossRoom();
        }
    }

    void Update()
    {
        switch (state) {
            case states.wandering:
                Move();
                break;
            case states.retaliation:
                attackCounter -= Time.deltaTime;
                if (attackCounter < 0) 
                {
                    state = states.wandering;
                }
                break;
            case states.fleeing:
                Flee();
                break;
            case states.waiting:
                if (roomID == GameManager.Instance.currentRoomID)
                {
                    GhostUI.SetActive(true);
                    state = states.postAttack;
                    postAttackCounter = 2f;
                }
                break;
            case states.attacking:
                attackCounter -= Time.deltaTime;
                if (attackCounter < 0) 
                {
                    state = states.postAttack;
                }
                break;
            case states.postAttack:
                Move();
                postAttackCounter -= Time.deltaTime;
                if (postAttackCounter < 0) 
                {
                    Vector2 times = attacks.Attack();
                    state = states.attacking;
                    attackCounter = times.x;
                    postAttackCounter = times.y;
                }
                break;
        }
    }

    private void Move() {
        Vector3 direction = movingGoal-transform.position;
        float step = movementSpeed * Time.deltaTime;
        transform.position += step * direction.normalized;
        if (direction.magnitude < step)
        {
            switch (state)
            {
                case states.wandering:
                    SetMovingGoalInMap();
                    break;

                case states.postAttack:
                    SetMovingGoalInRoom();
                    break;
            }
        }
    }

    private void Flee()
    {                 
        Vector3 fleeingDirection = transform.position - GameManager.Instance.player.transform.position;
        transform.position += 5 * fleeingDirection.normalized * Time.deltaTime;  
        if (fleeingDirection.magnitude > 18)
        {
            gameObject.SetActive(false);
        }                   
    }

    private void SetMovingGoalInRoom()
    {
        movingGoal.x = Random.Range(-5, 5) + transform.parent.position.x;
        movingGoal.y = Random.Range(-2.25f, 2.25f) + transform.parent.position.y;
    }

    private void SetMovingGoalInMap()
    {
        float[] map = GetComponentInParent<LevelGenerator>().RoomSize();
        movingGoal.x = Random.Range(map[0], map[2]);
        movingGoal.y = Random.Range(map[1], map[3]);
    }

    public override void TakeDamage(float amount, float piercingDamage, Vector3 knockback, float knockbackStrength) 
    {
        base.TakeDamage(amount, piercingDamage, knockback, knockbackStrength);
        if (state == states.wandering) 
        {
            ReactToDamage();
        }
    }

    private void ReactToDamage() 
    {
        // Can either flee, attack or do nothing
        float ran = Random.value*75 + anger;
        if (ran > 100)
        {   
            // Flee or Boss Room
            GameManager.Instance.gameData.ghostWrath += 10;
            float wrath = GameManager.Instance.gameData.ghostWrath;
            if (wrath >= 40)
            {
                CreateBossRoom();
            }
            else
            {
                state = states.fleeing;
            }
            return;
        }
        if (ran > 60)
        {
            // Attack
            state = states.retaliation;            
            if (Random.value > 0.5f)
            {
                attackCounter = attacks.ThrowingBladeAttack().x +0.1f;
            }
            else
            {
                attackCounter = attacks.SpittingLeavesAttack().x +0.1f;
            }
        }
        anger += 7.5f;
    }

    private void CreateBossRoom()
    {
        if (roomID != GameManager.Instance.currentRoomID)
        {
            return;
        }

        // Move towards the center
        Vector3 center = transform.parent.position;
        movingGoal = center;

        // Prepare Room
        Instantiate(destructionBladesPrefab, center + Vector3.up*4, Quaternion.identity); 
        transform.parent.GetComponent<Room>().SetBossRoom();    

        // Prepare for the next Combat
        state = states.postAttack;
        postAttackCounter = 5;
        health = 500;
        StartCoroutine(immunityFrames(2.5f));
        GhostUI.SetActive(true);

    }

    private void PrepareBossRoom()
    {
        // Get a random room
        Transform level = transform.parent.parent;
        Transform roomT = level.GetChild(Random.Range(0, level.childCount));
        Room room = roomT.GetComponent<Room>();

        // try again if it is the start room
        if (room.GetIsStart())
        {
            PrepareBossRoom();
            return;
        }

        // Adjust the Ghost stats for upcoming battle
        room.SetBossRoom();
        transform.parent = room.transform;
        transform.localPosition = Vector3.zero;        
        SetRoomID(room.GetID());
        SetMovingGoalInRoom();
        health = 500;
        state = states.waiting;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Room")
        {
            Room room = col.GetComponent<Room>();
            SetRoomID(room.GetID());
            transform.parent = col.transform;
        }     
    }

}
