using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlayer : MonoBehaviour
{
    public Vector3 offset;
    public BoatController boat;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        var pos = (boat.isPlayerDriving) ? boat.transform.position : player.position; 
        transform.position = pos + offset;
    }
}
