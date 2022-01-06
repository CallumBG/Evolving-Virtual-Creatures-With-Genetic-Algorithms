using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;

public class PopulationManager : MonoBehaviour
{

    public List<Creature> population = new List<Creature>();
    public List<Creature> populationData = new List<Creature>();

    public List<float> fitnessValues = new List<float>();

    public List<Creature> creatureFitnessOrder = new List<Creature>();

    public float simulationTimePerGen = 20f;

    public void MakeInitialPopulation()
    {
        while (this.populationData.Count < CurrentGameConfig.generationSize)
        {
            Creature newCreature = Creature.makeNewRandomCreature();
            this.population.Add(newCreature);
            Creature newerCreature = CreatureManager.CopyCreatureAttributes(newCreature);
            this.populationData.Add(newerCreature);

        }
        StartCoroutine(CheckTheFitnessAndClear(this.simulationTimePerGen));

    }

    public void MakeNewGeneration()
    {
        while (this.populationData.Count < CurrentGameConfig.generationSize)
        {
            Creature newCreature = Creature.makeNewCreatureFromData(this.populationData[0], this.populationData[1]);
            this.population.Add(newCreature);
            Creature newerCreature = CreatureManager.CopyCreatureAttributes(newCreature);
            this.populationData.Add(newerCreature);

        }
        StartCoroutine(CheckTheFitnessAndClear(this.simulationTimePerGen));

    }



    void Awake()
    {
        Physics.IgnoreLayerCollision(8,8);
    }

    public IEnumerator CheckTheFitnessAndClear(float duration)
    {
        yield return new WaitForSeconds(duration);
        //Checks each creatures fitness after the simulation
        for(int i = 0; i < this.population.Count; i++)
        {
            if(Evolution.GenerationCount > 1)
            {
                float startingX = this.populationData[i+2].transform.position.x;
                float startingY = this.populationData[i+2].transform.position.y;
                //The absolute value as large negative values means it travels as far as large positive values
                float EndX = Math.Abs(this.population[i].body.transform.position.x);
                float EndY = Math.Abs(this.population[i].body.transform.position.y);
                float ChangeX = startingX - EndX;
                float ChangeY = startingY - EndY;
                //Uses pythagoras' theorem to find the vector from start to end point, this is the fitness
                this.populationData[i+2].fitness = Mathf.Sqrt((ChangeX * ChangeX) + (ChangeY * ChangeY));
            }
            else
            {
                float startingX = this.populationData[i].transform.position.x;
                float startingY = this.populationData[i].transform.position.y;
                //The absolute value as large negative values means it travels as far as large positive values
                float EndX = Math.Abs(this.population[i].body.transform.position.x);
                float EndY = Math.Abs(this.population[i].body.transform.position.y);
                float ChangeX = startingX - EndX;
                float ChangeY = startingY - EndY;
                //Uses pythagoras' theorem to find the vector from start to end point, this is the fitness
                this.populationData[i].fitness = Mathf.Sqrt((ChangeX * ChangeX) + (ChangeY * ChangeY));
                
            }

        }
        foreach (Creature creature in this.population)
        {

            Destroy(creature.gameObject);
        }
        this.population.Clear();

        FindParents();

    }

    public void sortFitness()
    {
        foreach (Creature creature in this.populationData)
        {
            //If no other creatures add it to the list
            if (creatureFitnessOrder.Count == 0)
            {
                creatureFitnessOrder.Add(creature);
            }
            else
            {
                for (int i = 0; i < creatureFitnessOrder.Count; i++)
                {
                    //Adds this creature behind the compared creature in the list
                    if (creature.selectionChance <= creatureFitnessOrder[i].selectionChance)
                    {
                        creatureFitnessOrder.Insert(i, creature);
                        break;
                    }
                    //Adds this creature to the end of the list if there are no other creatures to compare, this means it is the largest in the current list
                    if (creature.selectionChance > creatureFitnessOrder[creatureFitnessOrder.Count - 1].selectionChance)
                    {
                        creatureFitnessOrder.Insert((creatureFitnessOrder.Count), creature);
                        break;
                    }
                }
            }

        }
    }

    public void ChooseParent()
    {

        float total = 0;
        //Creates a random variable
        var probablity = UnityEngine.Random.Range(1f,0f);
        //Starts with the lowest fitness value and works its way up
        total = creatureFitnessOrder[0].selectionChance;
        for (int i = 0; i < creatureFitnessOrder.Count; i++)
        {
            //If the random number is less than the probability add it as a parent
            if(probablity <= total)
            {
                this.populationData.Add(creatureFitnessOrder[i]);
                creatureFitnessOrder.Remove(creatureFitnessOrder[i]);
                break;
            }
            //If this is the last creature left in the list add it as a parent
            if(i == creatureFitnessOrder.Count - 1)
            {

                this.populationData.Add(creatureFitnessOrder[i]);
                creatureFitnessOrder.Remove(creatureFitnessOrder[i]);
                break;
            }
            else
            {
                //Add this to the previous chance of selection
                total += creatureFitnessOrder[i + 1].selectionChance;
            }
        }
    }

    public void FindParents()
    {
        float fitnessSum = 0;
        //Gets the sum of all creatures fitness'
        foreach(Creature creature in this.populationData)
        {
            
            fitnessSum += Math.Abs(creature.fitness);
        }
        //Assigns each creature with a probability of selection this is their fitness/sum of all creatures fitness
        foreach(Creature creature in this.populationData)
        {
            
            creature.selectionChance = creature.fitness / fitnessSum;
            fitnessValues.Add(creature.selectionChance);
        }
        sortFitness();
        this.populationData.Clear();
        ChooseParent();
        ChooseParent();
        //Destroys all creatures that were not chosen as parents
        foreach(Creature creature in creatureFitnessOrder)
        {
            Destroy(creature.gameObject);
        }
        creatureFitnessOrder.Clear();
        //Adds one to the generation as this generation is complete
        Evolution.GenerationCount += 1;
        MakeNewGeneration();

    }
}
