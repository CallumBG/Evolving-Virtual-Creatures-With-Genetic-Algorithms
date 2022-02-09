using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Limb : MonoBehaviour
{

    public List<int> potentialLimbs = new List<int>();

    public int LimbAttachType;

    public ConfigurableJoint joint;

    public Rigidbody LimbRigidbody;

    public IDictionary<string, float> limbDimensions = new Dictionary<string, float>();

    public bool isGrounded;

    //Default limb for holding the limb data
    public static Limb newLimb(Creature creature, Limb limb)
    {
        //Initialise limb
        GameObject baseLimb = new GameObject("Limb");
        Limb newLimb = baseLimb.AddComponent<Limb>();
        baseLimb.layer = 8;
        //Copy limb dimensions
        float tempXScale = limb.limbDimensions["X_Scale"];
        newLimb.limbDimensions.Add("X_Scale", tempXScale);
        float tempYScale = limb.limbDimensions["Y_Scale"];
        newLimb.limbDimensions.Add("Y_Scale", tempXScale);
        float tempZScale = limb.limbDimensions["Z_Scale"];
        newLimb.limbDimensions.Add("Z_Scale", tempXScale);
        //copy potentialLimbList
        newLimb.potentialLimbs = new List<int>(limb.potentialLimbs);
        //Copy the attachment type
        newLimb.LimbAttachType = limb.LimbAttachType;
        //Make it child of creature object
        newLimb.transform.parent = creature.transform;
        return newLimb;

    }

    public static Limb MakeNewBaseLimb(Creature creature)
    {
        GameObject baseLimb = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Limb newLimb = baseLimb.AddComponent<Limb>();
        baseLimb.layer = 8;
        Rigidbody LimbRigidbody = baseLimb.AddComponent<Rigidbody>();
        newLimb.transform.parent = creature.transform;
        List<int> tempPotentialLimbs = new List<int>();
        newLimb.LimbRigidbody = LimbRigidbody;
        ConfigurableJoint newJoint = baseLimb.AddComponent<ConfigurableJoint>();
        newLimb.joint = newJoint;
        return newLimb;
    }

    //New random limb for the initial population or mutation
    public static void MakeNewRandomLimb(Creature creature, bool isMutation = false)
    {
        //Randomise where limb will be added if there is not too many limbs
        int LimbSlotToAddTo;
        if (creature.limbSlot1Limbs.Count + creature.limbSlot2Limbs.Count + creature.limbSlot3Limbs.Count + creature.limbSlot4Limbs.Count >= creature.maxLimbCount && isMutation == false)
        {
            return;
        }
        else
        {
            LimbSlotToAddTo = UnityEngine.Random.Range(1, 5);
        }

        Limb newLimb = MakeNewBaseLimb(creature);
        LimbManager.ScaleLimbRandomly(newLimb.gameObject, creature, newLimb);

        newLimb.LimbRigidbody.mass =  newLimb.gameObject.transform.localScale.x * newLimb.gameObject.transform.localScale.y * newLimb.gameObject.transform.localScale.z;

        //Test to see setup with no gravity
        //LimbRigidbody.isKinematic = true;
        List<Limb> currentLimbSlot = creature.limbSlot1Limbs;

        switch (LimbSlotToAddTo)
        {
            case 1:
                currentLimbSlot = creature.limbSlot1Limbs;
                break;
            case 2:
                currentLimbSlot = creature.limbSlot2Limbs;
                break;
            case 3:
                currentLimbSlot = creature.limbSlot3Limbs;
                break;
            case 4:
                currentLimbSlot = creature.limbSlot4Limbs;
                break;
            default:
                Debug.Log("Limb slot not in range 1-4");
                break;

        }
        int limbAttachmentType;

        if(LimbSlotToAddTo == 1)
        {
            limbAttachmentType = UnityEngine.Random.Range(1, 5);
        }
        else if(LimbSlotToAddTo == 2)
        {
            limbAttachmentType = UnityEngine.Random.Range(5, 10);
        }
        else if(LimbSlotToAddTo == 3)
        {
            limbAttachmentType = UnityEngine.Random.Range(10, 14);
        }
        else
        {
            limbAttachmentType = UnityEngine.Random.Range(14, 19);
        }

        if (currentLimbSlot.Count == 0)
        {
            newLimb.LimbAttachType = limbAttachmentType;
            newLimb.potentialLimbs = LimbAttacher.randomLimbSetup(newLimb.gameObject, creature.body.bodyRigidbody, limbAttachmentType);
        }
        else
        {
            int randomAttachType = UnityEngine.Random.Range(0, currentLimbSlot.Last().potentialLimbs.Count - 1);
            limbAttachmentType = currentLimbSlot.Last().potentialLimbs[randomAttachType];
            newLimb.LimbAttachType = limbAttachmentType;
            newLimb.potentialLimbs = LimbAttacher.randomLimbSetup(newLimb.gameObject, currentLimbSlot.Last().LimbRigidbody, limbAttachmentType);
            LimbManager.addLimbJoint(creature, newLimb.LimbRigidbody, currentLimbSlot.Last().joint);
        }
        currentLimbSlot.Add(newLimb);
    }


    public static void MakeLimb(Creature creature, Creature parent1, Creature parent2, int limbSlotToEdit)
    {
        List<Limb> limbSlotToAddTo = creature.limbSlot1Limbs;
        List<Limb> parent1ListToInherit = parent1.limbSlot1Limbs;
        List<Limb> parent2ListToInherit = parent2.limbSlot1Limbs;
        switch (limbSlotToEdit)
        {
            case 1:
                parent1ListToInherit = parent1.limbSlot1Limbs;
                parent2ListToInherit = parent2.limbSlot1Limbs;
                limbSlotToAddTo = creature.limbSlot1Limbs;
                break;
            case 2:
                parent1ListToInherit = parent1.limbSlot2Limbs;
                parent2ListToInherit = parent2.limbSlot2Limbs;
                limbSlotToAddTo = creature.limbSlot2Limbs;
                break;
            case 3:
                parent1ListToInherit = parent1.limbSlot3Limbs;
                parent2ListToInherit = parent2.limbSlot3Limbs;
                limbSlotToAddTo = creature.limbSlot3Limbs;
                break;
            case 4:
                parent1ListToInherit = parent1.limbSlot4Limbs;
                parent2ListToInherit = parent2.limbSlot4Limbs;
                limbSlotToAddTo = creature.limbSlot4Limbs;
                break;
            default:
                break;
        }

        int newLimbListSize = Mathf.Max(parent1ListToInherit.Count, parent2ListToInherit.Count);

        while (limbSlotToAddTo.Count < newLimbListSize)
        {
            Limb newLimb = MakeNewBaseLimb(creature);

            ScaleLimbFromParents(newLimb.gameObject, parent1ListToInherit, parent2ListToInherit, limbSlotToAddTo.Count);

            int parentToChooseFrom = UnityEngine.Random.Range(0, 2);

            if (limbSlotToAddTo.Count == 0)
            {
                //TODO
                //Add Mutation

                if (parentToChooseFrom == 0)
                {
                    try
                    {
                        newLimb.LimbAttachType = parent1ListToInherit[limbSlotToAddTo.Count].LimbAttachType;
                    }
                    catch
                    {
                        newLimb.LimbAttachType = parent2ListToInherit[limbSlotToAddTo.Count].LimbAttachType;
                    }
                }
                else
                {
                    try
                    {
                        newLimb.LimbAttachType = parent2ListToInherit[limbSlotToAddTo.Count].LimbAttachType;
                    }
                    catch
                    {
                        newLimb.LimbAttachType = parent1ListToInherit[limbSlotToAddTo.Count].LimbAttachType;
                    }
                }
                newLimb.potentialLimbs = LimbAttacher.randomLimbSetup(newLimb.gameObject, creature.body.bodyRigidbody, newLimb.LimbAttachType, true);
            }
            else
            {
                //TODO
                //Add mutation

                if (parentToChooseFrom == 0)
                {
                    try
                    {
                        newLimb.LimbAttachType = parent1ListToInherit[limbSlotToAddTo.Count].LimbAttachType;
                        newLimb.potentialLimbs = LimbAttacher.randomLimbSetup(newLimb.gameObject, limbSlotToAddTo.Last().LimbRigidbody, newLimb.LimbAttachType, true);
                        LimbManager.addLimbJoint(creature, newLimb.LimbRigidbody, limbSlotToAddTo.Last().joint);
                    }
                    catch
                    {
                        newLimb.LimbAttachType = parent2ListToInherit[limbSlotToAddTo.Count].LimbAttachType;
                        newLimb.potentialLimbs = LimbAttacher.randomLimbSetup(newLimb.gameObject, limbSlotToAddTo.Last().LimbRigidbody, newLimb.LimbAttachType, true);
                        LimbManager.addLimbJoint(creature, newLimb.LimbRigidbody, limbSlotToAddTo.Last().joint);
                    }
                }
                else
                {
                    try
                    {
                        newLimb.LimbAttachType = parent2ListToInherit[limbSlotToAddTo.Count].LimbAttachType;
                        newLimb.potentialLimbs = LimbAttacher.randomLimbSetup(newLimb.gameObject, limbSlotToAddTo.Last().LimbRigidbody, newLimb.LimbAttachType, true);
                        LimbManager.addLimbJoint(creature, newLimb.LimbRigidbody, limbSlotToAddTo.Last().joint);
                    }
                    catch
                    {
                        newLimb.LimbAttachType = parent1ListToInherit[limbSlotToAddTo.Count].LimbAttachType;
                        newLimb.potentialLimbs = LimbAttacher.randomLimbSetup(newLimb.gameObject, limbSlotToAddTo.Last().LimbRigidbody, newLimb.LimbAttachType, true);
                        LimbManager.addLimbJoint(creature, newLimb.LimbRigidbody, limbSlotToAddTo.Last().joint);
                    }
                }
            }
            limbSlotToAddTo.Add(newLimb);

        }


    }

    public static void ScaleLimbFromParents(GameObject limb, List<Limb> parent1List, List<Limb> parent2List, int limbPosition)
    {
        int[] attributeDistribution = new int[5];
        for (int i = 0; i < attributeDistribution.Length; i++)
        {
            attributeDistribution[i] = UnityEngine.Random.Range(0, 2);
        }

        float Xscale;
        float Yscale;
        float Zscale;

        //Get a random value between 0 and 1
        float newRandomX = UnityEngine.Random.Range(0f, 1f);
        //Mutate if value is less than mutation rate, between a 1% and 20% chance
        if (newRandomX <= CurrentGameConfig.mutationRate)
        {
            float newRandomXscale = UnityEngine.Random.Range(0.25f, 0.5f);
            Xscale = newRandomXscale;
        }
        else
        {
            //If 0 take attributes from first parent
            if (attributeDistribution[0] == 0)
            {
                try
                {
                    Xscale = parent1List[limbPosition].limbDimensions["X_scale"];
                }
                catch
                {
                    Xscale = parent2List[limbPosition].limbDimensions["X_scale"];
                }
            }
            //If 1 take attributes from second parent
            else
            {
                try
                {
                    Xscale = parent2List[limbPosition].limbDimensions["X_scale"];
                }
                catch
                {
                    Xscale = parent1List[limbPosition].limbDimensions["X_scale"];
                }
            }
        }
        //Get a random value between 0 and 1
        float newRandomY = UnityEngine.Random.Range(0f, 1f);
        //Mutate if value is less than mutation rate, between a 1% and 20% chance
        if (newRandomY <= CurrentGameConfig.mutationRate)
        {
            float newRandomYscale = UnityEngine.Random.Range(0.25f, 0.5f);
            Yscale = newRandomYscale;
        }
        else
        {
            //If 0 take attributes from first parent
            if (attributeDistribution[1] == 0)
            {
                try
                {
                    Yscale = parent1List[limbPosition].limbDimensions["Y_scale"];
                }
                catch
                {
                    Yscale = parent2List[limbPosition].limbDimensions["Y_scale"];
                }
            }
            //If 1 take attributes from second parent
            else
            {
                try
                {
                    Yscale = parent2List[limbPosition].limbDimensions["Y_scale"];
                }
                catch
                {
                    Yscale = parent1List[limbPosition].limbDimensions["Y_scale"];
                }
            }
        }
        //Get a random value between 0 and 1
        float newRandomZ = UnityEngine.Random.Range(0f, 1f);
        //Mutate if value is less than mutation rate, between a 1% and 20% chance
        if (newRandomZ <= CurrentGameConfig.mutationRate)
        {
            float newRandomZscale = UnityEngine.Random.Range(0.25f, 0.5f);
            Zscale = newRandomZscale;
        }
        else
        {
            //If 0 take attributes from first parent
            if (attributeDistribution[2] == 0)
            {
                try
                {
                    Zscale = parent1List[limbPosition].limbDimensions["Z_scale"];
                }
                catch
                {
                    Zscale = parent2List[limbPosition].limbDimensions["Z_scale"];
                }
            }
            //If 1 take attributes from second parent
            else
            {
                try
                {
                    Zscale = parent2List[limbPosition].limbDimensions["Z_scale"];
                }
                catch
                {
                    Zscale = parent1List[limbPosition].limbDimensions["Z_scale"];
                }
            }
        }

        //Set the size of the limb using the values decided above
        limb.transform.localScale += new Vector3(Xscale, Yscale, Zscale);
        Limb limbComponent = limb.GetComponent<Limb>();
        limbComponent.limbDimensions.Add("X_Scale", Xscale);
        limbComponent.limbDimensions.Add("Y_Scale", Yscale);
        limbComponent.limbDimensions.Add("Z_Scale", Zscale);


    }


    // Update is called once per frame
    void Update()
    {

    }
}
