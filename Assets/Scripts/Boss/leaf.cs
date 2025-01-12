using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class leaf : Projectile
{
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,sprites.Length)];
        Destroy(gameObject,duration);
        GetComponent<DamageDealer>().SetDamage(contactDamage,piercingDamage,knockbackStrength,"Enemy");
    }

}
