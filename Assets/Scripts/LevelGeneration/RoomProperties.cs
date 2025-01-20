using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomProperties : MonoBehaviour
{
    public float defaultWeight = 1;
    public float treasureValue;
    public float dangerValue;

    public float GetAdjustedWeight() 
    {      
        // Get Danger Faktor

        /*
        get the diff value
        danger = [0,2]
        counter = [-10,10]
        Abs( danger - (counter*0,1 +1) )        
        */
        float counterValue = 0;
        float counterFactor = 0.1f;
        float diff = Mathf.Abs(dangerValue - counterValue * counterFactor - 1);
        float dangerFaktor = 1 / (diff + x); 



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
    private static float x = 0.414f;
    private float RM = 2;

    public void SetX()
    {

        x = (-RM + Mathf.Sqrt(RM *RM +4))/2.0f;
    }

}
