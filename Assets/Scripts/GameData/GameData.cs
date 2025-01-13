/*
*   Everything we collect is saved here 
*   Every value that should be in a save file can be saved here
*   Save Data from other points here and read them at other points
*/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData
{
    public int progression = 0;
    public float ghostWrath;

    public int MovementSpeedUpgradesCollected;
    public int AttackSpeedUpgradesCollected;
    public int HealthUpgradesCollected;
    public int DamageUpgradesCollected;
    public int DefenseUpgradesCollected;
    public int LuckUpgradesCollected;
    public int collectedSheets;


    public float minDamage = 1;

    public void collectItem(int id) 
    {
        switch (id)
        {
            case 0:
                collectedSheets++;
                break;
            case 1:
                Player.Instance.GetComponent<Damageable>().GainHealth(20);
                break;
            case 2:
                MovementSpeedUpgradesCollected= Mathf.Min(MovementSpeedUpgradesCollected+1, 5);
                break;
            case 3:
                AttackSpeedUpgradesCollected= Mathf.Min(AttackSpeedUpgradesCollected+1, 5);
                break;
            case 4:
                HealthUpgradesCollected= Mathf.Min(HealthUpgradesCollected+1, 5);
                if (Player.Instance != null)
                {
                    Player.Instance.GetComponent<Damageable>().IncreaseMaxHealth(10);
                }                
                break;
            case 5:
                DamageUpgradesCollected= Mathf.Min(DamageUpgradesCollected+1, 5);
                break;
            case 6:
                DefenseUpgradesCollected= Mathf.Min(DefenseUpgradesCollected+1, 5);
                break;
            case 7:
                LuckUpgradesCollected= Mathf.Min(LuckUpgradesCollected+1, 5);
                break;
        }
        if (Player.Instance != null)
        {
            Player.Instance.GetComponent<Player>().UpdateStats();
        }  
    }

    public List<int> dataAsList()
    {
        List<int> output = new List<int>();
        output.AddRange(Enumerable.Repeat(2, MovementSpeedUpgradesCollected));
        output.AddRange(Enumerable.Repeat(3, AttackSpeedUpgradesCollected));
        output.AddRange(Enumerable.Repeat(4, HealthUpgradesCollected));
        output.AddRange(Enumerable.Repeat(5, DamageUpgradesCollected));
        output.AddRange(Enumerable.Repeat(6, DefenseUpgradesCollected));
        output.AddRange(Enumerable.Repeat(7, LuckUpgradesCollected));
        return output;
    }

    public int UpgradesCollected(int id)
    {
        switch (id)
        {
            case 2:
                return MovementSpeedUpgradesCollected;
            case 3:
                return AttackSpeedUpgradesCollected;
            case 4:
                return HealthUpgradesCollected;
            case 5:
                return DamageUpgradesCollected;
            case 6:
                return DefenseUpgradesCollected;
            case 7:
                return LuckUpgradesCollected;
            default:
                return 0;
        }
    }

}
