using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionBlade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * 4;
        transform.Rotate(new Vector3(0, 0,1000*Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Storage")
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "DestroyableObstacle")
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Spikes")
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            if (col.GetComponent<Ghost>() == null)
            {
                Destroy(col.gameObject);
            }            
        }
    }
}
