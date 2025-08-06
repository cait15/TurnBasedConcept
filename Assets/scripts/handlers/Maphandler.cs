using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class Maphandler : MonoBehaviour
{
    public Tilemap islandcollTilemap, forestTilemap, MountainsTilemap;

    private List<Vector2Int> islandtiles, foresttiles, mountaintiles, emptytiles;
    public bool showingEmpty;
     public bool showingMountains;
     public bool ShowingForest;

    private Grid grid;

    private Vector3Int GetTileCellPosition(Vector3 worldPosition)
    {
        return Vector3Int.CeilToInt(islandcollTilemap.CellToWorld(islandcollTilemap.WorldToCell(worldPosition)));
    }

    private Vector3Int GetWorldPosition(Vector2Int cellPosition)
    {
        return Vector3Int.CeilToInt(islandcollTilemap.CellToWorld((Vector3Int)cellPosition));
        
    }

    private List<Vector2Int> GetTilesFrom(Tilemap tilemap)
    {
        List<Vector2Int> temp = new List<Vector2Int>();
        foreach (Vector2Int cellPosition in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile((Vector3Int)cellPosition) == false)
                continue;
            Vector3Int worldPosition = GetWorldPosition(cellPosition);
                temp.Add((Vector2Int)worldPosition);
        }// getting our tiles from the world


        return temp;
    }

    private List<Vector2Int> getEmptyTiles(List<Vector2Int> islandTiles, List<Vector2Int> nonEmptyTiles)
    {
        HashSet<Vector2Int> emptyTilesHashset = new HashSet<Vector2Int>(islandTiles);
        emptyTilesHashset.ExceptWith(nonEmptyTiles);
        return new List<Vector2Int>(emptyTilesHashset);// getting a list of empty tiles

    }
    
    void Awake()
    {
        foresttiles = GetTilesFrom(forestTilemap); // saves tiles into our tile list variables
        mountaintiles = GetTilesFrom(MountainsTilemap);
        islandtiles = GetTilesFrom(islandcollTilemap);
        emptytiles = getEmptyTiles(islandtiles, foresttiles.Concat(mountaintiles).ToList());// this excludes the forrest and mountain tiles, im calling in the private list method
        MapGridPrep();


        // which has parameters in it ( list vector2Int) and then im supplementing the forrest and mountain tiles in there
    }

    private void MapGridPrep()
    {
        grid = new Grid();
        grid.AddToGrid(forestTilemap.GetComponent<MapTypes>().GetTerrainData(),foresttiles);
        grid.AddToGrid(MountainsTilemap.GetComponent<MapTypes>().GetTerrainData(),mountaintiles);
        grid.AddToGrid(islandcollTilemap.GetComponent<MapTypes>().GetTerrainData(),emptytiles);
    }
    
    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false)
            return;
        DrawGizmoOf(emptytiles, Color.white, showingEmpty);
        DrawGizmoOf(foresttiles, Color.yellow, ShowingForest);
        DrawGizmoOf(mountaintiles, Color.red, showingMountains);
    }

    private void DrawGizmoOf(List<Vector2Int> tiles, Color color, bool isshowing)// for debugging purposes, Im leaving it in because i was messing with it, it just checks if all the tiles are being correctly marked
    
    {
        if (isshowing)
        {
            Gizmos.color = color;
            foreach (var pos in tiles)
            {
                Gizmos.DrawSphere(new Vector3(pos.x, pos.y , 0),0.3f);
            }
            {
                
            }
        }
    }
    
    public Dictionary<Vector2Int, Vector2Int?> GetMovementRange(Vector3 transformPosition, int selectedCharacterCurrentMovePoints)
    {
        Vector3Int cellworldPosition = GetTileCellPosition(transformPosition);
        return GraphSearchAlgo.BFS(grid, (Vector2Int)cellworldPosition, selectedCharacterCurrentMovePoints);
    }

    public int GetMovementCost(Vector2Int characterTilePosition)
    {
        return grid.GetMovementCost(characterTilePosition);
    }
}

