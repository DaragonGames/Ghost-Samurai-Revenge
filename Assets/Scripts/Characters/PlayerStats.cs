public class PlayerStats
{
    public float movementSpeed;
    public float attackSpeed;
    public float arrowDamage;
    public float sliceDamage;
    public float maxHealth;
    public float defense;
    public float knockbackStrength = 15;    
    public float piercingDamage = 0;
    public float critChance = 0;
    public float critDamageMultiplier = 2;
    public float maxEnergy = 1;




    public float EnergyReloadRate = 0.15f;
    public float arrowSpeed = 5;
    public int arrowPiercing = 0;
    public int arrowFlightDuration = 3;


    public PlayerStats() 
    {
        SetStats();
    }

    private float defaultSpeed = 8;
    private float defaultAttackSpeed = 0.5f;
    private float defaultArrowDamage = 20;
    private float defaultSliceDamage = 20;
    private float defaultHealth = 100;
    private float defaultDefense = 0;
    private float upgradeSpeed = 0.8f;
    private float upgradeAttackSpeed = .05f;
    private float upgradeArrowDamage = 10;
    private float upgradeSliceDamage = 10;
    private float upgradeHealth = 20;
    private float upgradeDefense = 5;


    public void SetStats()
    {
        GameData gameData = GameManager.Instance.gameData;
        movementSpeed = defaultSpeed + gameData.MovementSpeedUpgradesCollected * upgradeSpeed;
        attackSpeed = defaultAttackSpeed - gameData.AttackSpeedUpgradesCollected * upgradeAttackSpeed;
        arrowDamage = defaultArrowDamage + gameData.DamageUpgradesCollected * upgradeArrowDamage;
        sliceDamage = defaultSliceDamage + gameData.DamageUpgradesCollected * upgradeSliceDamage;
        maxHealth = defaultHealth + gameData.HealthUpgradesCollected * upgradeHealth;
        defense = defaultDefense + gameData.DefenseUpgradesCollected * upgradeDefense;
    }


}
