using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
 

    public void WaitTurn()
    {
        foreach (ITurnDependant characters in GetComponents<ITurnDependant>())
        {
           
            characters.WaitTurn();
            
        }
       
    }
}
