using UnityEngine;

public interface IBoatController
{
    public bool IsPlayerDriving();
    public void GetInBoat(GameObject player);
}
