using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private Vector3 supposedPosition = new Vector3(0,0,-10);
    private float counter = 0;

    void Start()
    {
        GameManager.EnterNewRoom += TravelToTarget; 
    }

    void OnDestroy() 
    {
        GameManager.EnterNewRoom -= TravelToTarget; 
    }

    void Update()
    {
        if (transform.position != supposedPosition)
        {
            counter += Time.deltaTime;
            Vector3 direction = supposedPosition - transform.position;
            transform.position += direction * Time.deltaTime*2;
            if (direction.magnitude < Time.deltaTime*2f)
            {
                transform.position = supposedPosition;
                counter = 0;
            }
            else
            {
                transform.position += direction.normalized*Time.deltaTime*1.5f;
            }
        }

    }

    void TravelToTarget(Vector3 target)
    {
        supposedPosition = new Vector3(target.x, target.y, -10);
    }
}
