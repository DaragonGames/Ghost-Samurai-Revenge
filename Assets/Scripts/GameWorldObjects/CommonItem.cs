using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonItem : MonoBehaviour
{
    private int id;
    public Sprite[] sprites;
    public GameObject soundPrefab;  

    void Start()
    {
        id = 1;
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        if (Player.Instance.GetHealthPercentage() == 1)     
        {
            Destroy(transform.parent.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {            
            DirectEffect();
            GameManager.OnCollectItemEvent(id);            
            Instantiate(soundPrefab,transform.position,Quaternion.identity);
            Destroy(transform.parent.gameObject);
        }
    }

    public void DirectEffect()
    {
        Player.Instance.GetComponent<Damageable>().GainHealth(25);
    }



}
