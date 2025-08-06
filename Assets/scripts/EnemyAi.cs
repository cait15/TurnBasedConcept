using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAi : MonoBehaviour, IenemyAi
{
    private Characters character;
    private allyMovement movement;
    public event Action TurnHasFinshed;

    private void Awake()
    {
        movement = FindObjectOfType<allyMovement>();
        character = GetComponent<Characters>();
    }

    public void StartingTurn()
    {
        Debug.Log("enemy taking turn");
        Dictionary<Vector2Int, Vector2Int?> movementRange = movement.GetMovementRange(character);
        List<Vector2Int> pathing = GetPathToRandomPosition(movementRange);
        Queue<Vector2Int> pathQueue = new Queue<Vector2Int>(pathing);

        StartCoroutine(TestCoroutineMovementPlswork(pathQueue));
    }
    
    private List<Vector2Int> GetPathToRandomPosition(Dictionary<Vector2Int, Vector2Int?> movementRange)
    {
        List<Vector2Int> possibleDestionation = movementRange.Keys.ToList();
        possibleDestionation.Remove(Vector2Int.RoundToInt(transform.position));
      
        Vector2Int selectedDestination = 
            possibleDestionation[
                UnityEngine.Random.Range(0, possibleDestionation.Count)];

        List<Vector2Int> listToRetuen = 
            GetPathTo(selectedDestination, movementRange);

        return listToRetuen;
    }

    private List<Vector2Int> GetPathTo(Vector2Int destination, Dictionary<Vector2Int, Vector2Int?> movementRange)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        path.Add(destination);

        while (movementRange[destination] != null)
        {
            path.Add(movementRange[destination].Value);
            destination = movementRange[destination].Value;
        }

        path.Reverse();

        return path.Skip(1).ToList();
    }
    
    
    public void DestroyUnit()
    {
        //FinishedMoving?.Invoke();
        Destroy(this.gameObject);
    }
 

    private IEnumerator TestCoroutineMovementPlswork( Queue<Vector2Int> pathQueue)
    {
        yield return new WaitForSeconds(0.5f);
        if (character.CheckMovement() == false || pathQueue.Count <= 0)
        {
            TurnHasFinshed?.Invoke();
            yield break;
        }

        Vector2Int pos = pathQueue.Dequeue();
        Vector3Int direction = Vector3Int.RoundToInt(new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0) - transform.position);
        character.MovementHandler(direction, 0);
        
        yield return new WaitForSeconds(0.5f);
        if (pathQueue.Count > 0)
        {
            StartCoroutine(TestCoroutineMovementPlswork(pathQueue));
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            TurnHasFinshed?.Invoke();
        }



    }
}
