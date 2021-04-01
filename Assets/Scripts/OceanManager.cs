using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    public BoatController boat;
    public Transform player;
    public GameObject[] oceanTiles;

    public float lastTileX;
    public float lastTileZ;
    public float lastQuadX;
    public float lastQuadZ;

    private List<Vector2> _requiredTilePosition;

    private void Start()
    {
        _requiredTilePosition = new List<Vector2>();
    }

    private void Update()
    {
        var playerPosition = boat.isPlayerDriving ? boat.transform.position : player.position;
        
        // Tile the player is in
        var tileX = Mathf.Round(playerPosition.x / 1000);
        var tileZ = Mathf.Round(playerPosition.z / 1000);
        
        // Quadrant of the tile the player is in
        var quadX = (playerPosition.x < tileX * 1000) ? -1 : 1;
        var quadZ = (playerPosition.z < tileZ * 1000) ? -1 : 1;

        // if nothing has changed since the last update, don't do anything
        if (tileX == lastTileX && tileZ == lastTileZ && quadX == lastQuadX && quadZ == lastQuadZ)
            return;
        

        //We need tiles in these pos
        _requiredTilePosition.Add(new Vector2(tileX, tileZ));
        _requiredTilePosition.Add(new Vector2(tileX + quadX, tileZ));
        _requiredTilePosition.Add(new Vector2(tileX, tileZ + quadZ));
        _requiredTilePosition.Add(new Vector2(tileX + quadX, tileZ + quadZ));
        
        // Move the tiles and shit
        foreach (var tile in oceanTiles)
        {
            var tilePos = tile.transform.position;
            var x = Mathf.Round(tilePos.x / 1000);
            var z = Mathf.Round(tilePos.z / 1000);

            var tileLocation = new Vector2(x, z);

            if (_requiredTilePosition.Contains(tileLocation))
            {
                _requiredTilePosition.Remove(tileLocation);
            }
            else
            {
                var newPos = _requiredTilePosition[_requiredTilePosition.Count - 1];
                tile.transform.position = new Vector3(newPos.x * 1000, 0, newPos.y * 1000);
                _requiredTilePosition.RemoveAt(_requiredTilePosition.Count - 1);
            }
        }

        _requiredTilePosition.Clear();
        lastTileX = tileX;
        lastTileZ = tileZ;
        lastQuadX = quadX;
        lastQuadZ = quadZ;
    }
}
