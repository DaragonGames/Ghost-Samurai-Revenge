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
    public AttackType attackType;
    public enum AttackType {thrown, closeCombat};

    public void Attack(Player player, Vector3 attackDirection)
    {    
        // Figure out values based on type
        float duration = 10;
        Transform parent = null;
        if (attackType == AttackType.closeCombat)
        {   
            duration = player.GetStats().attackSpeed*attackTime.y;
            parent = player.transform;      
        }

        // Create and adjust Projectille
        GameObject projectile = Instantiate(projectilePrefab, player.transform.position, Quaternion.identity, parent); 
        projectile.GetComponent<Projectile>().SetValues(attackDirection.normalized, "Player", duration);
        AdjustPosition(projectile.transform, attackDirection);   

        // Create a Soundeffect
        Instantiate(soundPrefab,player.transform.position, Quaternion.identity); 
    }

    private void AdjustPosition(Transform projectile, Vector3 attackDirection)
    {
        switch (attackType)
        {
            case AttackType.thrown:
                projectile.transform.position += attackDirection.normalized;
                break;
            case AttackType.closeCombat:
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
                break;
        }
    }
}
