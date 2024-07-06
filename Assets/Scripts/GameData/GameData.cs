/*
*   Everything we collect is saved here 
*   Every value that should be in a save file can be saved here
*   Save Data from other points here and read them at other points
*/

public class GameData
{


    public int MovementSpeedUpgradesCollected;
    public int AttackSpeedUpgradesCollected;
    public int HealthUpgradesCollected;
    public int DamageUpgradesCollected;
    public int LuckUpgradesCollected;


    public float minDamage = 1;

    public void collectItem(int id) 
    {
        switch (id)
        {
            case 0:
                MovementSpeedUpgradesCollected++;
                break;
        }
        GameManager.Instance.player.GetComponent<Player>().UpdateStats();
    }

}
