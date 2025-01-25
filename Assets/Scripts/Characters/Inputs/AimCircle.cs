using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCircle : MonoBehaviour
{
    private InputManager inputManager;

    void Start()
    {
        inputManager = GetComponentInParent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = inputManager.aiming;
        float angle = Vector3.Angle(input, Vector3.up);
        if (input.x > 0)
        {
            angle = -angle;
        }
        transform.rotation = Quaternion.Euler(0, 0,angle);
    }
}
