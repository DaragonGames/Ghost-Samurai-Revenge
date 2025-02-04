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
        id = Random.Range(0, 8);
        GetComponent<SpriteRenderer>().sprite = sprites[id];     
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {            
            GameManager.OnCollectItemEvent(id);
            Instantiate(soundPrefab,transform.position,Quaternion.identity);
            Destroy(transform.parent.gameObject);
        }
    }



}
