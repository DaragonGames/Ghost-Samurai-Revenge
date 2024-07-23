using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int id;
    public Sprite[] sprites;
    public GameObject soundPrefab;
    private float hoverDirection = 1;

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
        StartCoroutine(ChangeHoverDirection());
    }

    void Update()
    {
        transform.position += Vector3.up*Time.deltaTime*hoverDirection*0.15f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {            
            GameManager.Instance.gameData.collectItem(id);
            GameManager.OnCollectItemEvent(id);
            Instantiate(soundPrefab,transform.position,Quaternion.identity);
            Destroy(transform.parent.gameObject);
        }
    }

    public void SetId(int id) { this.id = id; }

    IEnumerator ChangeHoverDirection()
    {
        yield return new WaitForSeconds(0.5f);
        hoverDirection *= -1;
        if (gameObject != null)
        {
            StartCoroutine(ChangeHoverDirection());
        }        
    }
}
