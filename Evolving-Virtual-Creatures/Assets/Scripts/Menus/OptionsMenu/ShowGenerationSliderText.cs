using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGenerationSliderText : MonoBehaviour
{
    //Text element that will be edited
    Text percentageText;
    // Start is called before the first frame update
    void Start()
    {
        CurrentGameConfig.generationSize = 5;
        percentageText = GetComponent<Text>();
    }

    //Updates the text when the slider is moved
    public void textUpdate(float value)
    {
        CurrentGameConfig.generationSize = value;
        percentageText.text = value.ToString();
    }
}
