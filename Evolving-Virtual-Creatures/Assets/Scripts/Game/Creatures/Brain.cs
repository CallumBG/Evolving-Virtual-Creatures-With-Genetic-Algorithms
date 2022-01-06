using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Brain
{
    public int[] networkLayers;
    public float[][] networkNeurons;
    public float[][][] networkWeights;
    public float fitness;

    /*
    Creates a new brain using an array where:
    The Length of the array represents the number of layers in the neural network
    Each element in the array corresponds to the number of neurons in each of those layers
    */
    public Brain(int[] networkLayers)
    {
        
        this.networkLayers = new int[networkLayers.Length];
        //Copies the elements from the array passed to the new brain
        for (int i = 0; i < networkLayers.Length; i++)
        {
            this.networkLayers[i] = networkLayers[i];
        }

        
        InitializeNeurons();
        InitializeWeights();
    }

    //Initialize the neuron matrix
    public void InitializeNeurons()
    {
        List<float[]> neuronList = new List<float[]>();
        //Add neuron values to each layer
        for (int i = 0; i < networkLayers.Length; i++)
        {
            neuronList.Add(new float[networkLayers[i]]);
        }

        // Convert to array
        networkNeurons = neuronList.ToArray();
    }


    // Make a deep copy of a passed in brain
    public Brain(Brain copyBrain)
    {

        this.networkLayers = new int[copyBrain.networkLayers.Length];
        //Itterates through all the layers and copies them to the new network
        for (int i = 0; i < copyBrain.networkLayers.Length; i++)
        {
            this.networkLayers[i] = copyBrain.networkLayers[i];
        }

        InitializeNeurons();
        InitializeWeights();
        //Copys the network weights
        copyWeights(copyBrain.networkWeights);
    }

    //Make a deep copy of the network weights
    private void copyWeights(float[][][] copyWeights)
    {
        //Itterates through all the weights and copys them to the new network weights
        for (int i = 0; i < networkWeights.Length; i++)
        {
            for (int j = 0; j < networkWeights[i].Length; j++)
            {
                for (int k = 0; k < networkWeights[i][j].Length; k++)
                {
                    networkWeights[i][j][k] = copyWeights[i][j][k];
                }
            }
        }
    }

       
    // Initialize the weights matrix
    public void InitializeWeights()
    {


        List<float[][]> weightsList = new List<float[][]>();
        // Itterate through all the network layers starting from the 2nd layer, as the first layer is the input layer
        for (int i = 1; i < networkLayers.Length; i++)
        {
            List<float[]> layerWeightList = new List<float[]>();

            int neuronsInPreviousLayer = networkLayers[i - 1];
            //Itterate through all the neurons in that layer
            for (int j = 0; j < networkNeurons[i].Length; j++)
            {
                float[] neuronWeights = new float[neuronsInPreviousLayer];
                //Iterate through all the weights of connections for that neuron
                for (int k = 0; k < neuronsInPreviousLayer; k++)
                {
                    neuronWeights[k] = UnityEngine.Random.Range(-0.5f, 0.5f);
                }
                //Add the neuron weights
                layerWeightList.Add(neuronWeights);
            }
            //Add the neurons
            weightsList.Add(layerWeightList.ToArray());

        }
        //Add the network layer
        networkWeights = weightsList.ToArray();
    }

    //Feeds a set of inputs through the network and returns the final output layer values
    public float[] FeedForward(float[] inputs)
    {
        //Set the values for the first layer as the input values
        for (int i = 0; i < inputs.Length; i++)
        {
            networkNeurons[0][i] = inputs[i];
        }

        //Itterate through each layer starting at the second layer as the first layer is the input layer
        for (int i = 1; i < networkLayers.Length; i++)
        {
            //Itterate through each neuron in that layer
            for (int j = 0; j < networkNeurons[i].Length; j++)
            {
                //Bias value
                float value = 0.25f;
                //Itterate through each weight connection to that neuron
                for (int k = 0; k < networkNeurons[i - 1].Length; k++)
                {
                    value += networkWeights[i - 1][j][k] * networkNeurons[i - 1][k];
                }

                //Gives the outout a value between -1 and 1
                networkNeurons[i][j] = (float)Math.Tanh(value);
            }

        }
        //Returns the output values
        return networkNeurons[networkNeurons.Length-1];
    }

    //Itterates through every weight connection with a small chance of mutation, to set the weight to a new random value
    public void Mutate()
    {
        //Itterates through each layer
        for (int i = 0; i < networkWeights.Length; i++)
        {
            //Itterates through each neuron in that layer
            for (int j = 0; j < networkWeights[i].Length; j++)
            {
                //Itterates through each weight of connection for that neuron
                for (int k = 0; k < networkWeights[i][j].Length; k++)
                {
                    float weight = networkWeights[i][j][k];
                    //Random number between 0 and 1
                    float randomNumber = UnityEngine.Random.Range(0f, 1f) * 1000;

                    //Multiple ways to randomise the weight values
                    //Each with a 0.2% chance to trigger
                    if(randomNumber <= 2f)
                    {
                        //Flips the value of the weight, 1 would convert to -1
                        weight *= -1f;
                    }
                    else if(randomNumber <= 4f)
                    {
                        //Creates a new random weight between -0.5 and 0.5
                        weight = UnityEngine.Random.Range(-0.5f, 0.5f);

                    }
                    else if(randomNumber <= 6f)
                    {
                        //Multiples the initial weight by a value between 1 and 2, this will increase the weight
                        float factor = UnityEngine.Random.Range(0f, 1f) + 1f;
                        weight *= factor;

                    }
                    else if (randomNumber <= 8)
                    {
                        //Multiples the initial weight by a value between 0 and 1, this will decrease the weight
                        float factor = UnityEngine.Random.Range(0f, 1f);
                        weight *= factor;
                    }


                    networkWeights[i][j][k] = weight;
                }
            }
        }
    }
}
