using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float duration = 1f;
    private int piercing = 10000;
    private string sourceTag = "Player";
    private float contactDamage = 20;
    private Vector3 direction = new Vector3(0, 0,0);
    private float movementSpeed = 1;


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
        Vector3 knockbackDirection = (col.transform.position - transform.position).normalized;
        if (col.gameObject.tag == "Player" && sourceTag == "Enemy")
        {            
            col.transform.GetComponent<Player>().TakeDamage(contactDamage);
            Pierce();
        }
        if (col.gameObject.tag == "Enemy" && sourceTag == "Player")
        {
            col.transform.GetComponent<Enemy>().TakeDamage(contactDamage,knockbackDirection*3);
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
}
