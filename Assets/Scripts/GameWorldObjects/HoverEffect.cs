using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    private float hoverDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeHoverDirection());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up*Time.deltaTime*hoverDirection*0.15f;
    }

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
