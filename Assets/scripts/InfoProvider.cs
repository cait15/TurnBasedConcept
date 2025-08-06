using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoProvider : MonoBehaviour
{
   public SpriteRenderer sprite;
   public Sprite Image => sprite.sprite;
   public string NameToDisplay => gameObject.name;
   // use event to get health in here
   //reference highcloud project
   
}
