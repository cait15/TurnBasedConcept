using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUiFeedbackManager : MonoBehaviour, ITurnDependant
{
    PlayerUiFeedback flash;
    SelectionFeedBack selected;

   public void SelectionHandler(GameObject ColliderDetected)
   {
      ObjectDeselction();
      if (ColliderDetected == null) 
      return;

      selected = ColliderDetected.GetComponent<SelectionFeedBack>();
if (selected != null)
selected.selecting();

      Characters chara = ColliderDetected.GetComponent<Characters>();
      if (chara != null)
      {
         if (chara.CheckMovement() == false)
         {
            return;
         }
      }

      if (chara == null)
      {
         return;
      }
      flash = ColliderDetected.GetComponent<PlayerUiFeedback>();
      flash.FlashFeedback();
   }

   private void ObjectDeselction()
   {
      if (flash == null) 
      return;
      flash.StopFeedBack();
      flash = null;
      
      if (selected == null)
         return;
      selected.deselecting();
      selected = null;
   }

   public void WaitTurn()
   {
      ObjectDeselction();
   }

   
}
