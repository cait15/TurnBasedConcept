using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Grid
{
   private Dictionary<Vector2Int, LandSO> grid = new Dictionary<Vector2Int, LandSO>();


   public void AddToGrid(LandSO landtype, List<Vector2Int> collection) // places list into grid dic
   {
      foreach (Vector2Int emptycell in collection)
      {
         grid[emptycell] =
            landtype; // loops thru the cell in the collection and stores it in the dic, it also tells us which terrain it is and how muc it is
      }
   }

   public readonly static List<Vector2Int> neighbours4Directions = new List<Vector2Int>()
   {
      new Vector2Int(0,1 ),
      new Vector2Int(1,0 ),
      new Vector2Int(-1,0 ),
      new Vector2Int(0,-1 )
// need to access the adjacenttiles for the path
   };

   public bool CheckIfPositionIsValid(Vector2Int intPosition)
   {
      return grid.ContainsKey(intPosition) && grid[intPosition].CanwalkOn;
   }
   
   public int GetMovementCost(Vector2Int tileWorldPosition)
   {
      return  grid[tileWorldPosition].costOfMovement;
   }

   public LandSO GetTileTypeAt(Vector2Int position)
   {
      return grid[position];
   }

   public List<Vector2Int> GetNeighboursFor(Vector2Int worldPosition)
   {
      List<Vector2Int> positions = new List<Vector2Int>();
      foreach (Vector2Int direction in neighbours4Directions)
      {
         Vector2Int tempPosition = worldPosition + direction;
         if (grid.ContainsKey(tempPosition))
         {
            positions.Add(tempPosition);
         }
      }

      return positions;
   }
}
