using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public virtual void PickUp(GameObject player)
    {
        gameObject.SetActive(false);
    }
}
