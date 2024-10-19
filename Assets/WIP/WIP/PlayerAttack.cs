using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject soundPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateSlice(PlayerStats stats, float facing)
    {
        Instantiate(soundPrefab, transform.position, Quaternion.identity); 
        // Create the Projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, transform); 
        projectile.GetComponent<Projectile>().SetSliceValues(stats, stats.attackSpeed);

        // Change Position & Rotation based on Facing Direction
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
        GetComponent<Animator>().speed = 0.6f / stats.attackSpeed;
    } 

    void CreateProjectile(PlayerStats stats, Vector3 attackDirection) 
    {
        Instantiate(soundPrefab, transform.position, Quaternion.identity); 
        Vector3 spawnPosition = transform.position + attackDirection.normalized;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);        
        projectile.GetComponent<Projectile>().SetShirukenValues(stats, attackDirection.normalized);       
    }

    public void Attack(PlayerStats stats, Vector3 attackDirection, float facing, bool isSclice)
    {
        if (isSclice)
        {
            
        }
    }
}
