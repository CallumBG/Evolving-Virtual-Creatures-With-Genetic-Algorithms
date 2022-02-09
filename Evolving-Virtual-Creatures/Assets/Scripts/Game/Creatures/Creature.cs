using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public List<Limb> limbSlot1Limbs = new List<Limb>();
    public List<Limb> limbSlot2Limbs = new List<Limb>();
    public List<Limb> limbSlot3Limbs = new List<Limb>();
    public List<Limb> limbSlot4Limbs = new List<Limb>();

    public int currentLimbCount = 0;

    public int maxLimbCount = 8;
    public Brain brain;

    //public int rating;
    public float fitness;
    public float selectionChance;

    [SerializeField]
    public IDictionary<string, float> bodyDimensions = new Dictionary<string, float>();
    public Body body;

    public bool isCopy;

    //Make base creature container
    public static Creature newCreature()
    {

        GameObject baseCreature = new GameObject("Creature");
        Creature newlyMadeCreature = baseCreature.AddComponent<Creature>();
        return newlyMadeCreature;

    }

    //Initially added to population with random attributes
    public static Creature makeNewRandomCreature()
    {
        GameObject baseCreature = new GameObject("Creature");
        Creature newCreature = baseCreature.AddComponent<Creature>();
        //add body of creature
        newCreature.body = Body.MakeRandomBody(newCreature);
        //Adds a random set of limbs
        CreatureManager.AddInitialLimbs(newCreature);
        //Adds the joints of the body
        BodyManager.addBodyJoints(newCreature.body, newCreature);
        //Moves the creatures into the air so limbs don't clip with ground
        newCreature.transform.position += new Vector3(0, 10f, 0);
        //Removes the joints of the last limb in each list
        LimbManager.RemoveEndJoints(newCreature);
        //Creates the network structure of the brain
        int[] layers = { 11, 15, 15, 15, 15, 15, 15, 3 };
        //Creates the brain based on the structure passed in
        newCreature.brain = new Brain(layers);
        return newCreature;
    }

    public static Creature makeNewCreatureFromData(Creature parent1, Creature parent2)
    {
        GameObject baseCreature = new GameObject("Creature");
        Creature newCreature = baseCreature.AddComponent<Creature>();
        //add body of creature
        newCreature.body = Body.MakeBodyFromParents(newCreature, parent1, parent2);


        //Make limbs using attributes from the parents limbs
        int currentLimbSlotBeingEdited = 1;

        while (currentLimbSlotBeingEdited <= 4)
        {
            Limb.MakeLimb(newCreature, parent1, parent2, currentLimbSlotBeingEdited);
            currentLimbSlotBeingEdited += 1;

        }

        int parent1LimbCount = parent1.limbSlot1Limbs.Count + parent1.limbSlot2Limbs.Count + parent1.limbSlot3Limbs.Count + parent1.limbSlot4Limbs.Count;
        int parent2LimbCount = parent2.limbSlot1Limbs.Count + parent2.limbSlot2Limbs.Count + parent2.limbSlot3Limbs.Count + parent2.limbSlot4Limbs.Count;
        int averageParentLimbCount = (parent1LimbCount + parent2LimbCount) / 2;
        newCreature.maxLimbCount = Random.Range(averageParentLimbCount - 3, averageParentLimbCount + 3);

        //Creature goes through 4 potential mutations (one for each limb)
        int currentLimbSlotBeingMutated = 1;
        while (currentLimbSlotBeingMutated <= 4)
        {
            int mutationCheck = Random.Range(0, 100);
            if (mutationCheck < CurrentGameConfig.mutationRate && (newCreature.limbSlot1Limbs.Count + newCreature.limbSlot2Limbs.Count + newCreature.limbSlot3Limbs.Count + newCreature.limbSlot4Limbs.Count) < 20)
            {
                Limb.MakeNewRandomLimb(newCreature, true);
            }
            else if (mutationCheck < CurrentGameConfig.mutationRate * 2)
            {
                LimbManager.RemoveEndLimb(newCreature);
            }
            currentLimbSlotBeingMutated += 1;

        }


        //Trim number of limbs so it is less than max
        while (newCreature.currentLimbCount > newCreature.maxLimbCount)
        {
            LimbManager.RemoveEndLimb(newCreature);
            currentLimbSlotBeingMutated -= 1;
        }

        //Adds the joints of the body
        BodyManager.addBodyJoints(newCreature.body, newCreature);


        //Removes the joints of the last limb in each list
        LimbManager.RemoveEndJoints(newCreature);


        //Moves the creatures into the air so limbs don't clip with ground
        newCreature.transform.position += new Vector3(0, 5f, 0);

        //Takes brain randomly from parent 1 or parent 2 and mutates
        int brainRandom = Random.Range(1, 3);
        if (brainRandom == 1)
        {
            newCreature.brain = new Brain(parent1.brain);
            newCreature.brain.Mutate();
        }
        else
        {
            newCreature.brain = new Brain(parent2.brain);
            newCreature.brain.Mutate();
        }
        return newCreature;
    }

    /*
     * Get inputs from brainInputs for each limb and feed them through the neural network to get the outputs
     * */
    public void UseBrain()
    {
        //Moves the limb when not grounded with little force, subject to change
        float torqueFactor = 50f;
        //Moves the limb when grounded with lots of force, subject to change
        float torqueFactorGrounded = 1000f;
        //Adds torque to each axis of the limb based on the brain outputs
        foreach (Limb limb in this.limbSlot1Limbs)
        {
            if (limb.LimbRigidbody != null)
            {
                float[] inputs = BrainInputs.GetBrainInputs(limb);
                float[] outputs = this.brain.FeedForward(inputs);
                if (limb.isGrounded == true)
                {

                    limb.LimbRigidbody.AddRelativeTorque(outputs[0] - 0.25f * torqueFactorGrounded * limb.LimbRigidbody.mass, outputs[1] - 0.25f * torqueFactorGrounded * limb.LimbRigidbody.mass, outputs[2] - 0.25f * torqueFactorGrounded * limb.LimbRigidbody.mass);
                }
                else
                {

                    limb.LimbRigidbody.AddRelativeTorque(outputs[0] - 0.25f * torqueFactor * limb.LimbRigidbody.mass, outputs[1] - 0.25f * torqueFactor * limb.LimbRigidbody.mass, outputs[2] - 0.25f * torqueFactor * limb.LimbRigidbody.mass);
                }
            }
        }
        foreach (Limb limb in this.limbSlot2Limbs)
        {
            if (limb.LimbRigidbody != null)
            {
                float[] inputs = BrainInputs.GetBrainInputs(limb);
                float[] outputs = this.brain.FeedForward(inputs);
                if (limb.isGrounded == true)
                {

                    limb.LimbRigidbody.AddRelativeTorque(outputs[0] - 0.25f * torqueFactorGrounded * limb.LimbRigidbody.mass, outputs[1] - 0.25f * torqueFactorGrounded * limb.LimbRigidbody.mass, outputs[2] - 0.25f * torqueFactorGrounded * limb.LimbRigidbody.mass);
                }
                else
                {

                    limb.LimbRigidbody.AddRelativeTorque(outputs[0] - 0.25f * torqueFactor * limb.LimbRigidbody.mass, outputs[1] - 0.25f * torqueFactor * limb.LimbRigidbody.mass, outputs[2] - 0.25f * torqueFactor * limb.LimbRigidbody.mass);
                }
            }
        }
        foreach (Limb limb in this.limbSlot3Limbs)
        {
            if (limb.LimbRigidbody != null)
            {
                float[] inputs = BrainInputs.GetBrainInputs(limb);
                float[] outputs = this.brain.FeedForward(inputs);
                if (limb.isGrounded == true)
                {

                    limb.LimbRigidbody.AddRelativeTorque(outputs[0] - 0.25f * torqueFactorGrounded * limb.LimbRigidbody.mass, outputs[1] - 0.25f * torqueFactorGrounded * limb.LimbRigidbody.mass, outputs[2] - 0.25f * torqueFactorGrounded * limb.LimbRigidbody.mass);
                }
                else
                {

                    limb.LimbRigidbody.AddRelativeTorque(outputs[0] - 0.25f * torqueFactor * limb.LimbRigidbody.mass, outputs[1] - 0.25f * torqueFactor * limb.LimbRigidbody.mass, outputs[2] - 0.25f * torqueFactor * limb.LimbRigidbody.mass);
                }
            }
        }
        foreach (Limb limb in this.limbSlot4Limbs)
        {
            if (limb.LimbRigidbody != null)
            {
                float[] inputs = BrainInputs.GetBrainInputs(limb);
                float[] outputs = this.brain.FeedForward(inputs);
                if (limb.isGrounded == true)
                {

                    limb.LimbRigidbody.AddRelativeTorque(outputs[0] - 0.25f * torqueFactorGrounded * limb.LimbRigidbody.mass, outputs[1] - 0.25f * torqueFactorGrounded * limb.LimbRigidbody.mass, outputs[2] - 0.25f * torqueFactorGrounded * limb.LimbRigidbody.mass);
                }
                else
                {

                    limb.LimbRigidbody.AddRelativeTorque(outputs[0] - 0.25f * torqueFactor * limb.LimbRigidbody.mass, outputs[1] - 0.25f * torqueFactor * limb.LimbRigidbody.mass, outputs[2] - 0.25f * torqueFactor * limb.LimbRigidbody.mass);
                }
            }
        }
    }

    //Runs 20 times per second if there is a brain present
    void FixedUpdate()
    {
        if (isCopy == false)
        {
            UseBrain();

        }
    }
}
