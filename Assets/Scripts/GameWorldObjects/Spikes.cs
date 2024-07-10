using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {            
            col.transform.GetComponent<Player>().TakeDamage(0, 10); 
        }
        if (col.gameObject.tag == "Enemy")
        {
            col.transform.GetComponent<Enemy>().TakeDamage(0,10, Vector3.zero,0); 
        }  
    }
}
