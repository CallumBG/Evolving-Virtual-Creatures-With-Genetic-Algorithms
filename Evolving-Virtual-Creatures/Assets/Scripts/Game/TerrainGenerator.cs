using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!CurrentGameConfig.hillsSelected)
        {
            //Removes the hills terrain if flat is chosen
            GameObject baseTerrain = GameObject.Find("Terrain");
            baseTerrain.SetActive(false);
            
        }
        else
        {
            GameObject flatTerrain = GameObject.Find("Flat");
            flatTerrain.SetActive(false);
            GameObject baseTerrain = GameObject.Find("Terrain");
            baseTerrain.transform.position = new Vector3 (-128, -10, -128);
            int newRandomX = UnityEngine.Random.Range(-20, 20);
            int newRandomZ = UnityEngine.Random.Range(-20, 20);
            this.transform.position += new Vector3(newRandomX, 0, newRandomZ);
            
        
        }
        Evolution.StartEvolution();
    }

}
