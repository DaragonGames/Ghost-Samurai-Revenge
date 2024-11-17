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
    private float roationSpeed = 0;
    private float acceleration=0;
    private bool isSlice = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duration);
        if (sourceTag == "Enemy")
        {
            contactDamage *= 1 + 0.15f*GameManager.Instance.gameData.progression;
        }
        GetComponent<DamageDealer>().SetDamage(contactDamage,piercingDamage,knockbackStrength,sourceTag);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * Time.deltaTime * movementSpeed;
        transform.Rotate(new Vector3(0,0,roationSpeed*360*Time.deltaTime));
        movementSpeed += acceleration*Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == sourceTag)
        {
            return;
        }
        if (col.gameObject.tag == "Projectile" && isSlice)
        {     
            col.GetComponent<Projectile>().Pierce();
            //Destroy(col.gameObject);  
            return; 
        }
        if (col.gameObject.tag == "Room")
        {
            return;
        }
        Pierce();
        /*
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
        */



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
        if (col.gameObject.tag == "wall" || col.gameObject.tag == "Obstacle")
        {
            if (piercing < 20)
            {
                Destroy(gameObject);
            }
            else
            {
                Pierce();
            }               
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

    public void SetShirukenValues(PlayerStats stats, Vector3 direction)
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
        roationSpeed = 4;
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
        isSlice = true;
    }

    public void SetValues(string source, Vector3 direction)
    {
        this.direction = direction;
        sourceTag = source;
    }
    public void SetValues(string source, Vector3 direction, float roationSpeed)
    {
        this.direction = direction;
        this.roationSpeed = roationSpeed;
        sourceTag = source;
    }

    public void SetValues(string source, Vector3 direction, float roationSpeed, float acceleration)
    {
        this.acceleration = acceleration;
        this.direction = direction;
        this.roationSpeed = roationSpeed;
        sourceTag = source;
    }
}
