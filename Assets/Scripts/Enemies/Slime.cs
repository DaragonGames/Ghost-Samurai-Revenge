
using UnityEngine;

public class Slime : Enemy
{

    private float initialHealth;
    void Start() {
        //base.Start();
        initialHealth = health;
    }

    public override void MoveCharacter() { 
        Vector3 direction = (Player.GetPosition() - transform.position).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    public override void CharacterAction() { return;}

    public override void CharacterFinalAction() { 
        
        if (initialHealth >= 40)
        {
            GameObject obj = Instantiate(spawnAble, transform.position+Vector3.left*0.5f, Quaternion.identity);
            obj.GetComponent<Slime>().health = initialHealth*0.5f;
            //obj.transform.lossyScale = 1;
        }
        
        return;
    
    }



}
