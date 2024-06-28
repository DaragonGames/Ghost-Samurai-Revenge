using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float duration = 5f;
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

    void OnCollisionEnter2D(Collision2D collision2D) 
    {
        if (collision2D.gameObject.tag == "Player" && sourceTag == "Enemy")
        {            
            collision2D.transform.GetComponent<Player>().TakeDamage(contactDamage);
            Pierce();
        }
        if (collision2D.gameObject.tag == "Enemy" && sourceTag == "Player")
        {
            collision2D.transform.GetComponent<Enemy>().TakeDamage(contactDamage);
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
