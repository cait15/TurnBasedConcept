using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUiFeedback : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer sprite;
    public float InvisTime;
    public float visTime;

    public void FlashFeedback()
    {
        if (sprite == null) 
        return;
        StopFeedBack();
        StartCoroutine(Flashing());
    }

    private IEnumerator Flashing()
    {
        Color spriteColor = sprite.color;
        spriteColor.a = 0.5f;
        sprite.color = spriteColor;
        yield return new WaitForSeconds(InvisTime);
        spriteColor.a = 1;
        sprite.color = spriteColor;
        yield return new WaitForSeconds(visTime);
        StartCoroutine(Flashing());
    }

    public void StopFeedBack()
    {
        StopAllCoroutines();
        
        Color spriteColor = sprite.color;
        spriteColor.a = 1;
        sprite.color = spriteColor;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
