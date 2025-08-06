using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Characters : MonoBehaviour, ITurnDependant
{
    public int maxMovePoints = 2;
    private int currentMovePoints;
    private Vector3 dir;
  
    public delegate void TriggerActionRay( Vector3 direction);
    public static event TriggerActionRay OnAttackedRay;

    public bool isEnemy;
    private bool ColorMatch;
    private bool ShapeMatch;
    
    [SerializeField] private AudioSource walkaudioSource;
    [SerializeField] private AudioSource AttackaudioSource;
    
    public int CurrentMovePoints
    {
        get => currentMovePoints;
       
    }

    public UnityEvent MovementDone;
   

    public LayerMask enemyDetectionLayer;
    
 
    void Start()
    {
        RestoreMovement();
    }

    private void RestoreMovement()
    {
        currentMovePoints = maxMovePoints;
    }

  
   public bool CheckMovement()
    {
        return currentMovePoints > 0;
    }

   public void MovementHandler(Vector3 direction, int cost)
   {
       
     
       dir = direction;
        if (currentMovePoints - cost < 0)
        {
        
            return;
        }

        currentMovePoints -= cost;
      
        GameObject enemyUnit2 = CheckingforEnemy(direction);
        if (enemyUnit2 == null)
        {
         
            transform.position += direction;
            walkaudioSource.Play();
        }

        else
        {
            PerformAttack(enemyUnit2.GetComponent<AllyHealth>());
        }

        
        

        if (currentMovePoints <= 0)
        {
            MovementDone?.Invoke();
        }



    }

   private void OnEnable()
   {
       AllyAttackMechanic.OnAttackedColorOnly += SwitchingColorBool;
       AllyAttackMechanic.OnAttackedShapeOnly += SwitchingShapeBool;
   }

   private void OnDisable()
   {
       AllyAttackMechanic.OnAttackedColorOnly -= SwitchingColorBool;
       AllyAttackMechanic.OnAttackedShapeOnly -= SwitchingShapeBool;
   }

   void SwitchingColorBool(bool color)
   {
       ColorMatch = color;
       StartCoroutine(FalsingBools());
      
   }

   void SwitchingShapeBool(bool Shape)
   {
       ShapeMatch = Shape;
       StartCoroutine(FalsingBools());
   }


   private void PerformAttack(AllyHealth health)
   {
       AttackaudioSource.Play();
       if (ColorMatch)
       {
          
           health.Damage(10);
           Debug.Log("ColorMatch");
           ColorMatch = false;
       }
       if (ShapeMatch)
       {
           Debug.Log("ShapeMAtch");
           health.Damage(10);
           ShapeMatch = false;
       }
       if (isEnemy)
       {
           health.Damage(10);
           Debug.Log("IsEnemy");
       }
       else
       {
           
           Debug.Log("No match no damage");
       }
       currentMovePoints = 0;

       StartCoroutine(FalsingBools());


   }

   private IEnumerator FalsingBools()
   {
       yield return new WaitForSeconds(0.5f);
   
       ShapeMatch = false;
       ColorMatch = false;
       yield return new WaitForSeconds(0);
   }
    public void DestroyUnit()
    {
     
        Destroy(this.gameObject);
    }


    
    private GameObject CheckingforEnemy(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, enemyDetectionLayer);
        
       
        
        if (hit.collider != null)
        {
         
            OnAttackedRay?.Invoke(direction);
            return hit.collider.gameObject;
        }

        return null;

    }

    public void WaitTurn()
    {
     
        RestoreMovement();
    }
}
