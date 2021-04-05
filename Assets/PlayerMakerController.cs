using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMakerController : MonoBehaviour
{
    public float tileSize = 64f;
    public float borderSize = 20f;
    public Vector2Int ZeroTile = new Vector2Int(3, 2);
    public RectTransform playerMarker;

    public Transform player;
    public PhysicsBoatController boat;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // fuck boats
        var pos = (boat.isPlayerDriving) ? (boat.transform.position) : player.transform.position;

        // uh okay, pretty good
        var playerTileX = Mathf.Round(pos.x / 1000f);
        var playerTileY = -Mathf.Round(pos.z / 1000f);
        var playerXTileOffset = Mathf.Abs(pos.x % 1000f);
        var playerYTileOffset = Mathf.Abs(pos.z % 1000f);
        
        // has to do with tiles being center placed instead of top left???
        playerXTileOffset = (playerXTileOffset > 500) ? playerXTileOffset - 500 : playerXTileOffset + 500;
        playerYTileOffset = (playerYTileOffset > 500) ? playerYTileOffset - 500 : playerYTileOffset + 500;

        // so now we need to transform world tiles to map tiles????
        var mapTileX = ZeroTile.x + playerTileX;
        var mapTileY = ZeroTile.y + playerTileY;
        var mapTileXOffset = (playerXTileOffset / 1000f) * tileSize;
        var mapTileYOffset = (playerYTileOffset / 1000f) * tileSize;
        
        Debug.Log(playerTileY);

        // FUCK, and then canvas render pos max min fuck
        var x = borderSize + (mapTileX * tileSize) + mapTileXOffset;
        var y = -(borderSize + (mapTileY * tileSize) + mapTileYOffset);
        playerMarker.anchoredPosition = new Vector2(x, y);
        // playerMarker.offsetMin = new Vector2(x, y);
        // playerMarker.offsetMax = new Vector2(x + playerMarkerSize.x, y + playerMarkerSize.y);
    }
}
