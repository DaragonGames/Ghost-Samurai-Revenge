using UnityEngine;

public class Item : MonoBehaviour
{
    private int id;
    public Sprite[] sprites;

    void Start()
    {
        id = Random.Range(0, 8);
        if (Random.value > 0.4 + 0.08f*GameManager.GetLuck() && id == 0)
        {
            id=1;
        }
        if (Random.value*100 < GameManager.GetLuck()*GameManager.GetLuck() && id == 7)
        {
            id=1;
        }
        GetComponent<SpriteRenderer>().sprite = sprites[id];
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameManager.OnCollectItemEvent(id);
            GameManager.Instance.gameData.collectItem(id);
            Destroy(gameObject);
        }
    }

    public void SetId(int id) { this.id = id; }
}
