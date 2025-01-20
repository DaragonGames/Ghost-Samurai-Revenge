using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RoomLayouts", menuName = "ScriptableObjects/RoomLayout", order = 1)]
public class RoomsCollection : ScriptableObject
{
    public List<GameObject> deadEndSide;
    public List<GameObject> deadEndTop;
    public List<GameObject> passageSide;
    public List<GameObject> passageTop;
    public List<GameObject> passageCorner;
    public List<GameObject> open;
}
