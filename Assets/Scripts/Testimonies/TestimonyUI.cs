using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TestimonyUI : MonoBehaviour
{
    private int id = 0;
    public TMP_Text testifierTMP;
    public TMP_Text testimonyTMP;
    public TMP_Text descriptionTMP;

    void Start()
    {        
        SetTestimony();
    }

    public void SelectFate()
    {
        GameManager.Instance.SelectTestimony(id);
    }

    public void SetTestimony()
    {
        id = TestimonyHandler.GetTestimony();
        testifierTMP.text = TestimonyHandler.testimonyData[id].testifier;
        testimonyTMP.text = TestimonyHandler.testimonyData[id].testimony;
        descriptionTMP.text = TestimonyHandler.testimonyData[id].description;
    }
}
