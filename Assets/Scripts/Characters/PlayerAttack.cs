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
    public int attackID;
    public SpecialEffect specialEffect;
    public enum AttackType {thrown, closeCombat};
    public enum SpecialEffect {none, invincible};

    public void Attack(Player player, Vector3 attackDirection)
    {    
        // Figure out values based on type
        float duration = 10;
        Transform parent = null;
        if (attackType == AttackType.closeCombat)
        {   
            duration = Player.GetStats().attackSpeed*attackTime.y;
            parent = player.transform;      
        }

        // Create and adjust Projectille
        Projectile projectile = Instantiate(projectilePrefab, player.transform.position, Quaternion.identity, parent).GetComponent<Projectile>(); 
        projectile.SetValues(attackDirection.normalized, "Player", duration);
        float degree = 270 - 90 * Player.FacingFromVector(attackDirection);
        projectile.transform.Rotate(new Vector3(0,0,degree)); 
        projectile.AdjustSecondaryValues(1, 1 / Player.GetStats().attackSpeed);

        // Create a Soundeffect
        Instantiate(soundPrefab,player.transform.position, Quaternion.identity); 

        // Special Effect
        switch (specialEffect)
        {
            case SpecialEffect.invincible:
                player.GetComponent<Damageable>().BecomeInvincible(0.5f);
                break;
        }
    }

}
