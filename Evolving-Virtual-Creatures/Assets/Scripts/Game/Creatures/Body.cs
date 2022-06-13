using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public ConfigurableJoint bodyJointSlot1;
    public ConfigurableJoint bodyJointSlot2;
    public ConfigurableJoint bodyJointSlot3;
    public ConfigurableJoint bodyJointSlot4;

    public List<ConfigurableJoint> bodyJoints;

    public Rigidbody bodyRigidbody;

    public static Body MakeRandomBody(Creature creature)
    {

        Body body = MakeNewBaseBody(creature);

        BodyManager.ScaleBodyRandomly(body.gameObject,creature);
        //newBody.isKinematic = true;
        body.bodyRigidbody.mass = 3*body.gameObject.transform.localScale.x * body.gameObject.transform.localScale.y * body.gameObject.transform.localScale.z;

        return body;
    }

    public static Body MakeNewBaseBody(Creature creature)
    {
        GameObject baseBody = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var cubeRenderer = baseBody.GetComponent<Renderer>();
        cubeRenderer.material.color = Color.green;
        Body body = baseBody.AddComponent<Body>();
        //Sets the body at layer 8, to ensure it doesn't collide with other creatures
        baseBody.layer = 8;
        Rigidbody newBody = baseBody.AddComponent<Rigidbody>();

        body.bodyRigidbody = newBody;

        ConfigurableJoint bodyJointSlot1 = baseBody.AddComponent<ConfigurableJoint>();
        ConfigurableJoint bodyJointSlot2 = baseBody.AddComponent<ConfigurableJoint>();
        ConfigurableJoint bodyJointSlot3 = baseBody.AddComponent<ConfigurableJoint>();
        ConfigurableJoint bodyJointSlot4 = baseBody.AddComponent<ConfigurableJoint>();

        body.bodyJoints = new List<ConfigurableJoint>();
        body.bodyJoints.Add(bodyJointSlot1);
        body.bodyJoints.Add(bodyJointSlot2);
        body.bodyJoints.Add(bodyJointSlot3);
        body.bodyJoints.Add(bodyJointSlot4);
        body.bodyJointSlot1 = bodyJointSlot1;
        body.bodyJointSlot2 = bodyJointSlot2;
        body.bodyJointSlot3 = bodyJointSlot3;
        body.bodyJointSlot4 = bodyJointSlot4;

        body.name = "Body";

        baseBody.transform.parent = creature.transform;

        return body;
    }

    public static Body MakeBodyFromParents(Creature creature, Creature parent1, Creature parent2)
    {
        Body body = MakeNewBaseBody(creature);

        ScaleBodyFromParents(body.gameObject, parent1, parent2);

        body.bodyRigidbody.mass = 2*body.gameObject.transform.localScale.x * body.gameObject.transform.localScale.y * body.gameObject.transform.localScale.z;

        return body;

    }

    public static void ScaleBodyFromParents(GameObject body, Creature parent1, Creature parent2)
    {
        int[] attributeDistribution = new int[5];
        for (int i = 0; i < attributeDistribution.Length; i++)
        {
            attributeDistribution[i] = Random.Range(0, 2);
        }

        float Xscale;
        float Yscale;
        float Zscale;

        //Get a random value between 0 and 1
        float newRandomX = Random.Range(0f, 1f);
        //Mutate if value is less than mutation rate, between a 1% and 20% chance
        if (newRandomX <= CurrentGameConfig.mutationRate)
        {
            float newRandomXscale = Random.Range(1f, 2f);
            Xscale = newRandomXscale;
        }
        else
        {
            //If 0 take attributes from first parent
            if (attributeDistribution[0] == 0)
            {
                Xscale = parent1.bodyDimensions["Xscale"];
            }
            //If 1 take attributes from second parent
            else
            {
                Xscale = parent2.bodyDimensions["Xscale"];
            }
        }
        //Get a random value between 0 and 1
        float newRandomY = Random.Range(0f, 1f);
        //Mutate if value is less than mutation rate, between a 1% and 20% chance
        if (newRandomY <= CurrentGameConfig.mutationRate)
        {
            float newRandomYscale = Random.Range(1f, 2f);
            Yscale = newRandomYscale;
        }
        else
        {
            //If 0 take attributes from first parent
            if (attributeDistribution[1] == 0)
            {
                Yscale = parent1.bodyDimensions["Yscale"];
            }
            //If 1 take attributes from second parent
            else
            {
                Yscale = parent2.bodyDimensions["Yscale"];
            }
        }
        //Get a random value between 0 and 1
        float newRandomZ = Random.Range(0f, 1f);
        //Mutate if value is less than mutation rate, between a 1% and 20% chance
        if (newRandomZ <= CurrentGameConfig.mutationRate)
        {
            float newRandomZscale = Random.Range(1f, 2f);
            Zscale = newRandomZscale;
        }
        else
        {
            //If 0 take attributes from first parent
            if (attributeDistribution[2] == 0)
            {
                Zscale = parent1.bodyDimensions["Zscale"];
            }
            //If 1 take attributes from second parent
            else
            {
                Zscale = parent2.bodyDimensions["Zscale"];
            }
        }

        //Set the size of the body using the values decided above
        body.transform.localScale = new Vector3(Xscale, Yscale, Zscale);

    }
}
