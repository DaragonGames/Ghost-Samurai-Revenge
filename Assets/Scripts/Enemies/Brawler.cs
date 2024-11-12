using UnityEngine;

public class Brawler : Enemy
{
    protected override void OnUpdate()
    {
        Vector3 playerPosition = Player.GetPosition();
        movementDirection = (playerPosition - transform.position).normalized;
    }
}
