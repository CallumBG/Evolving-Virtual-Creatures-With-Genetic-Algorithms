using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMutationRateSliderText : MonoBehaviour
{
    //Text element that will be edited
    Text percentageText;
    // Start is called before the first frame update
    void Start()
    {
        CurrentGameConfig.mutationRate = 1;
        percentageText = GetComponent<Text>();
    }

    //Updates the text when the slider is moved
    public void textUpdate(float value)
    {
        CurrentGameConfig.mutationRate = value;
        percentageText.text = value + "%";
    }
}
