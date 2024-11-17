using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag == "Player" || tag == "Enemy")
        {            
            col.transform.GetComponent<Damageable>().TakeDamage(0,10);
        }

    }
}
