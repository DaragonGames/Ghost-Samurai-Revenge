using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int id;
    public Sprite[] sprites;
    public GameObject soundPrefab;    
    public static int[] uncollectedItems = new int[8];

    void Start()
    {
        SetID();
        GetComponent<SpriteRenderer>().sprite = sprites[id];     
    }

    void SetID()
    {
        id = Random.Range(0, 8);
        if (GameManager.Instance.gameData.UpgradesCollected(id) + uncollectedItems[id] >= 5)
        {
            SetID();
            return;
        }
        if (Random.value > 0.4 + 0.08f*GameManager.GetLuck() && id == 0)
        {
            id=1;
        }
        if (Random.value*100 < GameManager.GetLuck()*GameManager.GetLuck() && id == 7)
        {
            id=1;
        }
        uncollectedItems[id] += 1;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {            
            GameManager.OnCollectItemEvent(id);
            Instantiate(soundPrefab,transform.position,Quaternion.identity);
            uncollectedItems[id] -= 1;
            Destroy(transform.parent.gameObject);
        }
    }


}
