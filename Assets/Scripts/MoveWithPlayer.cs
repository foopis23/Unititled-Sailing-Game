using UnityEngine;
using UnityEngine.Serialization;

public class MoveWithPlayer : MonoBehaviour
{
    public Vector3 offset;
    public PhysicsBoatController boat;
    public Transform boatTransform;
    [FormerlySerializedAs("player")] public Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        var pos = (boat.IsPlayerDriving()) ? boatTransform.position : playerTransform.position; 
        transform.position = pos + offset;
    }
}
