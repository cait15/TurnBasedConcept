using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoHandler : MonoBehaviour, ITurnDependant
{

    public UIcharacterInfo infoPannel;
    // Start is called before the first frame update
    void Start()
    {
        HidetheInfoPanel();
    }

    public void HandleSelection(GameObject selectedCharacter)
    {
        HidetheInfoPanel();
        if (selectedCharacter == null)
        {
            return;
        }

        InfoProvider Information = selectedCharacter.GetComponent<InfoProvider>();
        if (Information == null)
        {
            return;
        }

       ShowtheDetails(Information);
    }

    private void ShowtheDetails(InfoProvider information)
    {
        infoPannel.toggleVis(true);
        infoPannel.setDat(information.Image, information.NameToDisplay);
    }

    public void HidetheInfoPanel()
    {
        infoPannel.toggleVis(false);
    }

  

    public void WaitTurn()
    {
        HidetheInfoPanel();
    }
}
