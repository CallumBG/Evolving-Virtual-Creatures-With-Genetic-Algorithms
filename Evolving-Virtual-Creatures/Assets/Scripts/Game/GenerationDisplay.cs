using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GenerationDisplay : MonoBehaviour
{
    TMP_Text generationCountText;
    // Start is called before the first frame update
    void Start()
    {
        generationCountText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    public void Update()
    {
        generationCountText.text = "Generation: " + Evolution.GenerationCount.ToString();
    }
}
