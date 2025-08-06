using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementRangeFeedback : MonoBehaviour
{
   public Tilemap highlightTilemap;

   public TileBase highlightTile;

   public void ClearHighLight()
   {
      highlightTilemap.ClearAllTiles();
   }

   public void HighLightTiles(IEnumerable<Vector2Int> worldPositions)
   {
      ClearHighLight();
      foreach (Vector2Int tilePosition in worldPositions)
      {
         highlightTilemap.SetTile((Vector3Int)tilePosition, highlightTile);
      }
   }
}
