using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Map/Data")]
public class LandSO : ScriptableObject
{
    public bool CanwalkOn = false;
    public int costOfMovement = 10;
}
