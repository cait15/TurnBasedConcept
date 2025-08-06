using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemyUiSelector : MonoBehaviour
{
    public Camera camera;
    public LayerMask layerMask;
    private Collider2D[] characterObjects = new Collider2D[0];
    private int selectedIndex = 0;
    private bool firstSelection = false;
    private Vector3 startPos;
    public float threshold = 0.5f;
   
    
    public UnityEvent<GameObject> HandleMouse;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            HandleMouseDown();
        } 
        
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            HandleMouseUp();
        } 
    }

    private void HandleMouseUp()
    {
        Vector3 endPos = MouseInput();
        if (Vector2.Distance(startPos, endPos) > threshold)
        {
          
            return;
        }

        if (firstSelection == false)
        {
            PerformSelection();
        }
        return;
       
    }

    private void HandleMouseDown()
    {
        startPos = MouseInput();

        firstSelection = characterObjects.Length == 0;
        if (firstSelection)
        {
            PerformSelection();
        }

     
    }

    private void PerformSelection()
    {
        Collider2D collider = HandlingOfMultipleObjectSelection(startPos);
          GameObject selectedCharacter = collider == null ? null : collider.gameObject;
        HandleMouse?.Invoke(selectedCharacter);
        if (selectedCharacter != null)
        {
       
        }
    }

    private Collider2D HandlingOfMultipleObjectSelection(Vector3 clickedPosition)
    {
        Collider2D[] tempObj = Physics2D.OverlapPointAll(clickedPosition, layerMask);
        Collider2D selectedColllider = null;
        if (tempObj.Length == 0)
        {
            characterObjects = new Collider2D[0];
        }
        else
        {
            if (CheckTheSameSelection(tempObj))
            {
                selectedIndex++;
                selectedIndex = selectedIndex >= characterObjects.Length ? 0 : selectedIndex;
            }
            else
            {
                characterObjects = tempObj;
                selectedIndex = 0;
            }

            return characterObjects[selectedIndex];
        }

        return selectedColllider;
    }

    private bool CheckTheSameSelection(Collider2D[] tempObj)
    {
        if (characterObjects == null || characterObjects.Length == 0)
            return false;
        return (tempObj.Length == characterObjects.Length) &&
               tempObj.Intersect(characterObjects).Count() == characterObjects.Length;
    } 


    private Vector3 MouseInput()
    {
        Vector3 mouseInput = camera.ScreenToWorldPoint(Input.mousePosition);
        mouseInput.z = 0f;
        return mouseInput;
    }
}
