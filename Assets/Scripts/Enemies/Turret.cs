using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : Enemy
{

    public override void MoveCharacter() { return; }

    public override void CharacterAction() {
        Vector3 position = new Vector3();
        Instantiate(spawnAble,position, Quaternion.identity);
    }
}
