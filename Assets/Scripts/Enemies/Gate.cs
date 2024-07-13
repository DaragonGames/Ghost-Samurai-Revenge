using UnityEngine;

public class Gate : Enemy
{
    public override void MoveCharacter() { return; }

    public override void CharacterAction() {
        GameObject minion = Instantiate(spawnAble, transform.position, Quaternion.identity);
        minion.GetComponent<Brawler>().SetRoomID(roomID);
        minion.GetComponent<Brawler>().DeclareAsMinion();
    }
}
