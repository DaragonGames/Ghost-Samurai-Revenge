using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawnables", menuName = "ScriptableObjects/Attacks", order = 2)]
public class PlayerAttack : ScriptableObject
{
    public Vector3 attackTime;
    public float attackCost;
    public GameObject projectilePrefab;
    public GameObject soundPrefab;
    public bool isSclice;

    void CreateSlice(Transform transform, PlayerStats stats, Vector3 attackDirection)
    {
        Instantiate(soundPrefab, transform.position, Quaternion.identity); 
        // Create the Projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, transform); 
        projectile.GetComponent<Projectile>().SetSliceValues(stats, stats.attackSpeed);

        // Change Position & Rotation based on Facing Direction
        float facing = Player.FacingFromVector(attackDirection);
        switch (facing)
        {
            case 0:
                projectile.transform.position += new Vector3(0, 0.8f,0);
                projectile.transform.Rotate(new Vector3(0,0,90));
                break;
            case 1:
                projectile.transform.position += new Vector3(0.65f, -0.1f,0);
                break;
            case 2:
                projectile.transform.position +=new Vector3(0, -0.8f,0);
                projectile.transform.Rotate(new Vector3(0,0,90));
                break;
            case 3:
                projectile.transform.position += new Vector3(-0.65f, -0.1f,0);
                break;
        }
    } 

    void CreateProjectile(Transform transform, PlayerStats stats, Vector3 attackDirection) 
    {
        Instantiate(soundPrefab, transform.position, Quaternion.identity); 
        Vector3 spawnPosition = transform.position + attackDirection.normalized;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);        
        projectile.GetComponent<Projectile>().SetShirukenValues(stats, attackDirection.normalized);       
    }

    public void Attack(Player player, Vector3 attackDirection)
    {
        if (isSclice)
        {
            CreateSlice(player.transform, player.GetStats(), attackDirection);
        }
        else
        {
            CreateProjectile(player.transform, player.GetStats(),attackDirection);
        }
    }
}
