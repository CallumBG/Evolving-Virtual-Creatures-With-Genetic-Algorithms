using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Evolution
{
    public static int GenerationCount = 1;
    // Start is called before the first frame update
    public static void StartEvolution()
    {
        GameObject initialPopulationManager = new GameObject("Population Manager");
        PopulationManager myPopulationManger = initialPopulationManager.AddComponent<PopulationManager>() as PopulationManager;
        myPopulationManger.MakeInitialPopulation();

    }


  
}
