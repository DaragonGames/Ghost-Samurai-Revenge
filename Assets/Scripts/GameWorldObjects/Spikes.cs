using UnityEngine;

public class Spikes : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Feet")
        {         
            col.transform.GetComponentInParent<Damageable>().TakeDamage(0,10);
        }
    }
}
