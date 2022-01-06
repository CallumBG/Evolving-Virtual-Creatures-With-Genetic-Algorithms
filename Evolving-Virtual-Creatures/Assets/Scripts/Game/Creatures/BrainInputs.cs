using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainInputs : MonoBehaviour
{

     public static float[] GetBrainInputs(Limb limb)
    {
        //Gets the values to add as inputs
        float[] inputs = new float[6];
        float Xvelocity = limb.LimbRigidbody.velocity.x;
        float Yvelocity = limb.LimbRigidbody.velocity.y;
        float Zvelocity = limb.LimbRigidbody.velocity.z;
        float XAngularVelocity = limb.LimbRigidbody.angularVelocity.x;
        float YAngularVelocity = limb.LimbRigidbody.angularVelocity.y;
        float ZAngularVelocity = limb.LimbRigidbody.angularVelocity.z;



        //Adds the values to the inputs array
        inputs[0] = Xvelocity;
        inputs[1] = Yvelocity;
        inputs[2] = Zvelocity;
        inputs[3] = XAngularVelocity;
        inputs[4] = YAngularVelocity;
        inputs[5] = ZAngularVelocity;
        return inputs;
        

    }

    //Gets and returns the specific limb position from the floor
    public float DistanceFromGround(Limb limb)
    {
        return limb.transform.position.y;
    }
}
