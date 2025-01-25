using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomProperties : MonoBehaviour
{
    // Set those Values in the Inspector
    public float defaultWeight = 1;
    public float treasureValue;
    public float dangerValue;

    // Adjust these Values for Balancing
    private float counterFactor = 0.1f;
    private float dangerImpact = 2f;

    // Don't touch these values directly
    private static float x = 0.5f;

    // Debug Values Replace ASAP
    float dangerCounter = 0;
    float treasureCounter = 0;

    public float GetAdjustedWeight() 
    {      
        // Get the Danger Factor        
        float diff = Mathf.Abs(dangerValue - dangerCounter * counterFactor - 1)*0.5f*dangerImpact;
        float dangerFaktor = 1 / (diff + x); 

        diff = Mathf.Abs(treasureValue - treasureCounter);

        return defaultWeight*dangerFaktor;
    }

/*
lets say we go for 2 enemies in average
so when we have a room with 4
we have a +2 on our counter
so now we want to adjust the weight value by a multiplication
based on the danger level

so we compare danger level with the counter
counter ? danger = 1 / x = factor
1 / danger = factor


get the diff value
danger = [0,2]
counter = [-10,10]
Abs( danger - (counter*0,1 +1) ) = [0, 2]

0 - (-10*0.1 + 1) = 0
2 - (-10*0.1 + 1) = 2
0 - (10*0.1 + 1) = -2
2 - (10*0.1 + 1) = 0

1 / 1 + diff = [0.33 , 1]
1 / diff = [0.5, 1/0]
1 / diff+0.5 = [0.4, 2]

f(x) = 1 / diff + x
[1/x+2 ,1/x]
Min = 1 / Rm + x
Max = 1 / x

k = 1 / Min
Max  = k
1 / Min = Max

1 / (1 / Rm + x) = 1 / x
0 = x^2 + Rm*x -1
0 = x^2 + 2x -1

Result
1 / diff + 0,414

(-RM + Root(RM^2 +4))/2


*/
    

    public void SetX()
    {
        x = (-dangerImpact + Mathf.Sqrt(dangerImpact*dangerImpact +4))/2.0f;
    }

}
