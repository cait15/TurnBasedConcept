using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIcharacterInfo : MonoBehaviour
{
    public TextMeshProUGUI name;
    public Image portait;

    public void setDat(Sprite sprite, string text)
    {
        name.text = text;
        portait.sprite = sprite;
    }

    public void toggleVis(bool val)
    {
        gameObject.SetActive(val);
    }
}
