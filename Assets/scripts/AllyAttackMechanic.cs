using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllyAttackMechanic : MonoBehaviour
{
    private Ray2D ray;
    private Color playercolor1;

    public delegate void TriggerAction(bool colorMatch);

    public static event TriggerAction OnAttackedColorOnly;

    public string tag;
    public string tag1;
    
    
    public delegate void TriggerAction1( bool ShapeMatch);

    public static event TriggerAction1 OnAttackedShapeOnly;
        
    private Vector3 dir;

    private bool colormatch = false;
    
    private bool Shapematch = false;

    private void Awake()
    {


    }

    void Start()
    {
        tag1 = gameObject.name;
     
        
        Color playercolor = GetComponent<SpriteRenderer>().color;
        playercolor1 = playercolor;


      
    }

    private void Update()
    {
     
        Debug.DrawRay(transform.position, dir, Color.green); // debugging purposes
    }

   
    private void OnEnable()
    {
        Characters.OnAttackedRay += shootRaayCast;
    }

    private void OnDisable()
    {
        Characters.OnAttackedRay -= shootRaayCast;
    }


    void shootRaayCast(Vector3 Direction)
    {
        //Debug.Log(tag);
        dir = Direction;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction, 1.5f, LayerMask.GetMask("EnemyShape"));
        if (hit.collider != null)
        {
            tag = hit.transform.name;
            Debug.Log(tag);
            Color enemy = hit.collider.gameObject.GetComponent<SpriteRenderer>().color;
            Debug.Log($"this is the enemy color " + enemy);
            
            Debug.Log($"this is the ally  color " + playercolor1);
            if (playercolor1.Equals(enemy))
            {
                colormatch = true;
                Debug.Log("IT WORKS YES?");
                OnAttackedColorOnly?.Invoke(colormatch);
            }
             if(tag1 == tag)
             {
                 Shapematch = true;
                OnAttackedShapeOnly?.Invoke(Shapematch);
            }

            Vector2 right = transform.TransformDirection(Vector2.right) * 1.5f;



            Debug.DrawRay(transform.position, right, Color.green);
        }

        StartCoroutine(FalsingBools());



    }
    
    private IEnumerator FalsingBools()
    {
        Shapematch = false;
        colormatch = false;
        yield return new WaitForSeconds(0.5f);
    }
}
