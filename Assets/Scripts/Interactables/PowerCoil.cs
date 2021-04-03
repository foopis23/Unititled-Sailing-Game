using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCoil : QuestItem
{
    public override void PickUp(GameObject player)
    {
        PlayerData.Instance.hasPowerCoil = true;
        base.PickUp(player);
    }
}
