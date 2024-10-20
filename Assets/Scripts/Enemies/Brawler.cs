using UnityEngine;

public class Brawler : Enemy
{
    public override void MoveCharacter() { 
        Vector3 playerPosition = Player.GetPosition();
        Vector3 direction = (playerPosition - transform.position).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    public override void CharacterAction() { return;}
}
