using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainChoice : MonoBehaviour
{

    public Button FlatButton, HillsButton;
    // Start is called before the first frame update
    public void hillsSelected()
    {
        if(CurrentGameConfig.hillsSelected)
        {
        }
        else
        {
            CurrentGameConfig.hillsSelected = true;
            Debug.Log(CurrentGameConfig.hillsSelected);
        }
            HillsButton.image.color = Color.green;
            FlatButton.image.color = Color.white;
    }

    public void flatSelected()
    {
        if(CurrentGameConfig.hillsSelected)
        {
            CurrentGameConfig.hillsSelected = false;
            Debug.Log(CurrentGameConfig.hillsSelected);
        }
            HillsButton.image.color = Color.white;
            FlatButton.image.color = Color.green;
    }

}
