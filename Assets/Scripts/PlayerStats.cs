public class PlayerStats
{
    public float movementSpeed;
    public float attackSpeed;
    public float arrowDamage;
    public float sliceDamage;
    public float maxHealth;
    public float knockbackStrength = 3;
    public float defense = 0;
    public float piercingDamage = 0;
    public float critChance = 0;
    public float critDamageMultiplier = 2;
    public float maxAmmunition = 3;




    public float AmmunitionReloadRate = 0.33f;
    public float arrowSpeed = 5;
    public int arrowPiercing = 0;
    public int arrowFlightDuration = 3;


    public PlayerStats() 
    {
        SetStats();
    }

    private float defaultSpeed = 6;
    private float defaultAttackSpeed = 1;
    private float defaultArrowDamage = 20;
    private float defaultSliceDamage = 20;
    private float defaultHealth = 100;
    private float defaultDefense = 0;
    private float upgradeSpeed = 0.5f;
    private float upgradeAttackSpeed = .1f;
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

    public void OverWriteByDebugger(float movementSpeed, float attackSpeed, float arrowDamage, float sliceDamage, float maxHealth, float defense)
    {
        this.movementSpeed = movementSpeed;
        this.attackSpeed = attackSpeed;
        this.arrowDamage = arrowDamage;
        this.sliceDamage = sliceDamage;
        this.maxHealth = maxHealth;
        this.defense = defense;
    }


}
