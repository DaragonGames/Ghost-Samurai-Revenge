using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tori : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D collision2D) {
        if (collision2D.gameObject.tag == "Player")
        {
            GameManager.Instance.CollectSheet();
            GameManager.Instance.LeaveGrove();
        }
    }
}
