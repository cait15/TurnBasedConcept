using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMovementFeedback : MonoBehaviour, ITurnDependant
{
    public SpriteRenderer Sprite;
    public Color darkened;
    private Color Active;
    void Start()
    {
        Active = Sprite.color;
    }

    // Update is called once per frame

    public void darkenfeedback()
    {
        Sprite.color = darkened;
    }

    public void ActiveFeedback()
    {
        Sprite.color = Active;
    }
    void Update()
    {
        
    }

    public void WaitTurn()
    {
        ActiveFeedback();
    }
}
