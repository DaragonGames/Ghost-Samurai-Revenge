using System.Collections.Generic;
using UnityEngine;

public class Ghost_Old : Enemy
{
    public GameObject GhostUI;

    private Vector3 movingGoal;
    private GhostAttacks attacks;
    private enum states {wandering, fleeing, retaliation, waiting, attacking, postAttack};
    private states state = states.wandering; 
    private float attackCounter;
    private float postAttackCounter;
    private bool searching = false;
    private float anger;

    void Start()
    {
        roomID = GameManager.Instance.currentRoomID ; // Test if really needed
        movingGoal = transform.position + Vector3.right*90;
        attacks = GetComponent<GhostAttacks>();
        anger = GameManager.Instance.gameData.ghostWrath;
        if (anger == 0)
        {
            gameObject.SetActive(false);
        }
        Debug.Log("Ghost" +anger);
        Debug.Log("Progression" +GameManager.Instance.gameData.progression);
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
                    Flee();
                }
                break;
            case states.fleeing:
                Move();
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
                case states.fleeing:
                    DoneFleeing();
                    break;
                case states.postAttack:
                    SetMovingGoalInRoom();
                    break;
            }
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

    private void Flee()
    {
        Debug.Log("Fleeing");
        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        Vector3 oppositeDirection = (transform.position - playerPosition).normalized;
        movingGoal.x = oppositeDirection.x*18 + transform.position.x;
        movingGoal.y = oppositeDirection.y*18 + transform.position.y;
        state = states.fleeing;
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
        float ran = Random.value;
        if (ran < anger/2)
        {
            // Attack
            state = states.retaliation;
            attackCounter = attacks.ThrowingBladeAttack().x +0.1f;
            anger += 5;
            return;
        }
        if (ran < anger)
        {
            // Attack
            state = states.retaliation;
            attackCounter = attacks.SpittingLeavesAttack().x +0.1f;
            anger += 5;
            return;
        }
        if (ran < anger*2)
        {
            
            Flee();
        }
        anger += 1;
        // Option 3 is do nothing
    }

    private void DoneFleeing() 
    {       
        // Continue Fleeing when Being Chased
        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        float distance = (transform.position - playerPosition).magnitude;
        if (distance < 15)
        {
            Flee();
            anger += 1;
            return;
        }
        anger += 3;

        float wrath = GameManager.Instance.gameData.ghostWrath + Random.value *25f;;

        // If not angry search for Room or wander around
        if (anger < 0.5f)
        {
            if (wrath > 50 )
            {
                // Search for a possible boss room
                searching = true;
            }
            state = states.wandering;
            SetMovingGoalInMap();
        }
        else
        {
            // If angry disappear or search / enforce bossroom
            if (wrath < 50)
            {
                GameManager.Instance.gameData.ghostWrath += anger*0.1f; 
                gameObject.SetActive(false);                              
            }
            else if (wrath > 100)
            {
                GameManager.Instance.gameData.ghostWrath += anger*0.05f;
                EnforceBossRoom();                
            }
            else
            {
                searching = true;
                state = states.wandering;
                SetMovingGoalInMap();
            }
        }        
    }

    private void EnforceBossRoom()
    {
        Transform level = transform.parent.parent;
        List<Transform> options = new List<Transform>();
        for (int i = 0; i < level.childCount; i++)
        {
            options.Add(level.GetChild(i));
        }
        while (options.Count > 0)
        {
            Transform selected = options[Random.Range(0, options.Count)];
            Room room = selected.GetComponent<Room>();
            if (room.GetIsEntered())
            {
                options.Remove(selected);
            }
            else
            {
                CreateBossRoom(room);
                return;
            }
        }
        Room currentRoom = transform.parent.GetComponent<Room>();
        if (currentRoom.GetID() != GameManager.Instance.currentRoomID)
        {
            // Clear Room
            transform.parent = transform.parent.parent;
            Room newRoom = GetComponentInParent<LevelGenerator>().RecreateAsBossRoom(currentRoom);
            CreateBossRoom(newRoom);
            
        }   
        else
        {
            // Deactivate Ghost if there is no possible Room
            gameObject.SetActive(false);
        }  
    }

    public void CreateBossRoom(Room room)
    {
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
            if (!room.GetIsEntered() && searching)
            {
                CreateBossRoom(room);
            }
        }     
    }

}
