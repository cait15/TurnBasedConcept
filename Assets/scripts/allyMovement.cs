using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class allyMovement : MonoBehaviour
{
    public Camera camera;
    
    private Characters selectedCharacter;
    
    public Maphandler map;
    private List<Vector2Int> movementRange;

    public MovementRangeFeedback highlight;
    public void MovementOfAlly(Vector3 endPos)
    {
        if (this.selectedCharacter == null)
            return;
        if (this.selectedCharacter.CheckMovement() == false)
            return;



        Vector2 direction = CalculatingMovement(endPos);
        Vector2Int characterTilePosition = Vector2Int.FloorToInt((Vector2)this.selectedCharacter.transform.position + direction);
        if (movementRange.Contains( characterTilePosition))
        {
            int movementCost = map.GetMovementCost(characterTilePosition);

            this.selectedCharacter.MovementHandler((Vector3)direction, movementCost);
            if (this.selectedCharacter.CheckMovement())
            {
                MovementRangeCalculations();
            }
            else
            {
                highlight.ClearHighLight();
            }
        }
        else
        {
            Debug.Log($"cantMove here ");
        }

       

    }

    private Vector2 CalculatingMovement(Vector3 endPos)
    {
        Vector2 direction = (endPos - this.selectedCharacter.transform.position);
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            float sign = Mathf.Sign(direction.x);
            direction = Vector2.right * sign;
        }
        else
        {
            float sign = Mathf.Sign(direction.y);
            direction = Vector2.up * sign;
        }

        return direction;
    }

    public void SelectionHandle(GameObject Detected)
    { highlight.ClearHighLight();
        if (Detected == null)
        {
            ResettingCharacterMovementandFeedBack();
            return;
        }


        if (Detected.CompareTag("Player"))
        {
            this.selectedCharacter = Detected.GetComponent<Characters>();
        }
        else
        {
            this.selectedCharacter = null;
        }

        if (this.selectedCharacter == null)
        {
            return;
        }
         MovementRangeCalculations();
        
    }

    private void MovementRangeCalculations()
    {
        movementRange = GetMovementRange(this.selectedCharacter).Keys.ToList();
        highlight.HighLightTiles(movementRange);
    }

    public Dictionary<Vector2Int, Vector2Int?> GetMovementRange(Characters selectedCharacter)
    {
        return map.GetMovementRange(selectedCharacter.transform.position, selectedCharacter.CurrentMovePoints);
    }

    private void ResettingCharacterMovementandFeedBack()
    {
        highlight.ClearHighLight();
        this.selectedCharacter = null;
    }
}
