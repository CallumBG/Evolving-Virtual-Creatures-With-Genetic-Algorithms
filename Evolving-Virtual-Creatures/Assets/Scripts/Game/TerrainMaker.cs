using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMaker : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public int depth = 10;

    float scale = 20;

    /*
     * Creates a terrain with noise to give the effect of hills
     * Created from the start and moved to position if selected
     * Deleted if not selected
     */

    // Start is called before the first frame update
    /*
    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        this.transform.position = new Vector3(-1, -1, -14);
        
    }
    */
    //Called once this component is added
    void Awake()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        this.transform.position = new Vector3(-1, -1, -14);
        //Creates the terrain game object
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        //Creates random values and moves it to this position, so the creatures always spawn at a different part of the terrain
        int newRandomX = UnityEngine.Random.Range(-20, 20);
        int newRandomZ = UnityEngine.Random.Range(-20, 20);
        this.transform.position += new Vector3(newRandomX, 0, newRandomZ);
    }

    //Transforms the terrain based on the defined values
    TerrainData GenerateTerrain(TerrainData terrainData)
    {

        terrainData.heightmapResolution =  width + 1;
        //Creates the terrain based on the defined width, depth and height
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    //Creates an array of each points height in the terrain
    float[,] GenerateHeights()
    {
        
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeights(x, y);
            }
        }

        return heights;
    }

    //Takes in a point and returns a random value to create a random height
    float CalculateHeights(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float) y / height * scale;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
