using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIInputManager : MonoBehaviour
{
    public Selectable defaultSelected;
    InputAction anyKey;

    // Start is called before the first frame update
    void Start()
    {
        Controls controls = new Controls();
        anyKey = controls.UI.AnyKey;
        anyKey.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (anyKey.triggered)
            {
                defaultSelected.Select();
            }
        }
    }
}
