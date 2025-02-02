using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomProperties : MonoBehaviour
{
    // Set those Values in the Inspector
    public float defaultWeight = 1;
    public float treasureValue = 1;
    public float dangerValue = 1;

    // Adjust these Values for Balancing
    private float dcf = 0.1f; // Treasure Counter Factor
    private float dangerImpact = 2f;
    private float tcf = 0.5f; // Treasure Counter Factor
    private float treasureImpact = 2f;

    // Debug Values Replace ASAP
    float dangerCounter = 0;
    float treasureCounter = 0;

    public float GetAdjustedWeight() 
    {      
        // Get the Danger Factor    
        float i = (dangerImpact*dangerImpact-1) / dangerImpact;  
        float x = 1 / dangerImpact;
        float diff = Mathf.Min(Mathf.Abs(dangerValue - 1 - dangerCounter * dcf)*0.5f, 1)*i;
        float dangerFaktor = 1 / (diff + x); 

        // Get the Treasure Factor
        i = (treasureImpact*treasureImpact-1) / treasureImpact;  
        x = 1 / treasureImpact;
        diff = Mathf.Min(Mathf.Abs((treasureValue - treasureCounter) * tcf), 1) * i;
        float treasureFaktor = 1 / (diff + x); 

        return defaultWeight*dangerFaktor*treasureFaktor;
    }

    /*
    *   Explaining the Math
    *   Danger Impact is how strongly it affects the weight
    *   2 means twice as likely or unlikely 
    *   This mean the worst chance time the best chance is 1 
    *   f1 * f2 = 1
    *   f1 = worst & f2 = best
    *   the formular for the factor is 1 / diff + x
    *   diff is between 0 and i
    *   f1 = 1 / i + x and f2 = 1 / x
    *   therefore 1 = (1 / i + x) * (1 / x)
    *   lastly x + i = k
    *   k is the Danger Impact
    *   Now we put this in a calculator and get
    *   x  = 1 / k
    *   i = (k * k - 1) / k
    */

}
