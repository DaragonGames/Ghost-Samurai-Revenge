using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float duration = 3;
    public int piercing = 0;
    public float movementSpeed = 4;
    public float contactDamage = 10;
    public float knockbackStrength = 0;
    public float piercingDamage = 0;
    public float critChance = 0;
    public float critDamageMultiplier = 1;
    private string sourceTag;
    private Vector3 direction = new Vector3(0, 0,0);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * Time.deltaTime * movementSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == sourceTag)
        {
            return;
        }
        Vector3 knockbackDirection = (col.transform.position - transform.position).normalized;
        if (col.gameObject.tag == "Player" && sourceTag == "Enemy")
        {            
            col.transform.GetComponent<Player>().TakeDamage(contactDamage, piercingDamage);
            Pierce();   
        }
        if (col.gameObject.tag == "Enemy" && sourceTag == "Player")
        {
            float crit = (Random.value<critChance) ? critDamageMultiplier : 1;
            col.transform.GetComponent<Enemy>().TakeDamage(contactDamage*crit,piercingDamage*crit, knockbackDirection,knockbackStrength);
            Pierce();   
        }  
        if (col.gameObject.tag == "Storage")
        {
            col.transform.GetComponent<DestroyableStorage>().GetAttacked();
            Pierce();   
        }  
        if (col.gameObject.tag == "DestroyableObstacle")
        {
            col.transform.GetComponent<DestroyableObstacle>().GetAttacked();
            Pierce();   
        } 
    }

    private void Pierce() {
        if (piercing == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            piercing--;
        }
    }

    public void SetArrowValues(PlayerStats stats, Vector3 direction)
    {  
        this.direction = direction;
        sourceTag = "Player";
        contactDamage = stats.arrowDamage;
        knockbackStrength = stats.knockbackStrength;
        movementSpeed = stats.arrowSpeed;
        piercing = stats.arrowPiercing;
        duration = stats.arrowFlightDuration;        
        piercingDamage = stats.piercingDamage;
        critChance = stats.critChance;
        critDamageMultiplier = stats.critDamageMultiplier;
    }

    public void SetSliceValues(PlayerStats stats, float duration)
    {   
        direction = Vector3.zero;
        sourceTag = "Player";   
        contactDamage = stats.sliceDamage;
        knockbackStrength = stats.knockbackStrength;
        movementSpeed = 0;
        piercing = 1000;
        this.duration = duration; 
        piercingDamage = stats.piercingDamage;
        critChance = stats.critChance;
        critDamageMultiplier = stats.critDamageMultiplier;
    }

    public void SetValues(string source, Vector3 direction)
    {
        this.direction = direction;
        sourceTag = source;
    }
}
