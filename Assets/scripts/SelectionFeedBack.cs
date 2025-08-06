using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionFeedBack : MonoBehaviour, ITurnDependant
{
    private int ogSortinglayer;

    public SpriteRenderer sprite;
    public SpriteRenderer spriteShape;

    private int layertoselect;
    // Start is called before the first frame update
    void Start()
    {
        layertoselect = SortingLayer.NameToID("SelectedObject");
        ogSortinglayer = sprite.sortingLayerID;
        ogSortinglayer = spriteShape.sortingLayerID;
    }

    private void ToggleSelection(bool val)
    {
        if (val)
        {
            sprite.sortingLayerID = layertoselect;
            spriteShape.sortingLayerID = layertoselect;
        }
        else
        {
            sprite.sortingLayerID = ogSortinglayer;
            spriteShape.sortingLayerID = ogSortinglayer;
        }
    }

   public void selecting()
    {
        ToggleSelection(true);
    }

  public  void deselecting()
    {
        ToggleSelection(false);
    }

 
    // Update is called once per frame
    void Update()
    {
        
    }

    public void WaitTurn()
    {
        deselecting();
    }
}
