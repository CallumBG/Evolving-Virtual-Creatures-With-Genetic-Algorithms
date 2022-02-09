using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainInputs : MonoBehaviour
{

    public static float[] GetBrainInputs(Limb limb)
    {
        //Gets the values to add as inputs
        float[] inputs = new float[11];
        float Xvelocity = limb.LimbRigidbody.velocity.x;
        float Yvelocity = limb.LimbRigidbody.velocity.y;
        float Zvelocity = limb.LimbRigidbody.velocity.z;
        float XAngularVelocity = limb.LimbRigidbody.angularVelocity.x;
        float YAngularVelocity = limb.LimbRigidbody.angularVelocity.y;
        float ZAngularVelocity = limb.LimbRigidbody.angularVelocity.z;
        float distanceFromGround = limb.transform.position.y;
        float groundedNum;
        if(limb.isGrounded == true)
        {
            groundedNum = 1f;
        }
        else
        {
            groundedNum = 0f;
        }
        float Xrotation = limb.transform.rotation.x;
        float Yrotation = limb.transform.rotation.y;
        float Zrotation = limb.transform.rotation.z;
        

        //Adds the values to the inputs array
        inputs[0] = Xvelocity;
        inputs[1] = Yvelocity;
        inputs[2] = Zvelocity;
        inputs[3] = XAngularVelocity;
        inputs[4] = YAngularVelocity;
        inputs[5] = ZAngularVelocity;
        inputs[6] = distanceFromGround;
        inputs[7] = groundedNum;
        inputs[8] = Xrotation;
        inputs[9] = Yrotation;
        inputs[10] = Zrotation;
        return inputs;


    }

    //Gets and returns the specific limb position from the floor
    public float DistanceFromGround(Limb limb)
    {
        return limb.transform.position.y;
    }
}
