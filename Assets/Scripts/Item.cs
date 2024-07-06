using UnityEngine;

public class Item : MonoBehaviour
{
    private int id;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameManager.Instance.gameData.collectItem(id);
            Destroy(gameObject);
        }
    }

    public void SetId(int id) { this.id = id; }
}
