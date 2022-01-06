using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{

    public static void ScaleBodyRandomly(GameObject body, Creature creature)
    {
        //Creates random variables to rescale body by
         float myRandomXScale = Random.Range(1f, 3f);
         float myRandomYScale = Random.Range(1f, 3f);
         float myRandomZScale = Random.Range(1f, 3f);

         //Saves these to the creature data so they can be copied late if required
         creature.bodyDimensions.Add("X_Scale", myRandomXScale);
         creature.bodyDimensions.Add("Y_Scale", myRandomYScale);
         creature.bodyDimensions.Add("Z_Scale", myRandomZScale);
        
        body.transform.localScale += new Vector3(myRandomXScale, myRandomYScale, myRandomZScale);
    }

    public static void addBodyJoints(Body body, Creature creature, Body oldBody = null)
    {
        //If there is no body to take values from set random values
        if(oldBody == null)
        {
            if(creature.limbSlot1Limbs.Count > 0)
            {
                body.bodyJointSlot1 = addBodyJoints(creature, creature.limbSlot1Limbs[0].LimbRigidbody, body.bodyJointSlot1);
            }
            if(creature.limbSlot2Limbs.Count > 0)
            {
                body.bodyJointSlot2 = addBodyJoints(creature, creature.limbSlot2Limbs[0].LimbRigidbody, body.bodyJointSlot2);
            }
            if(creature.limbSlot3Limbs.Count > 0)
            {
                body.bodyJointSlot3 = addBodyJoints(creature, creature.limbSlot3Limbs[0].LimbRigidbody, body.bodyJointSlot3);
            }
            if(creature.limbSlot4Limbs.Count > 0)
            {
                body.bodyJointSlot4 = addBodyJoints(creature, creature.limbSlot4Limbs[0].LimbRigidbody, body.bodyJointSlot4);
            }
        }
        //Take values from the previous joints
        else
        {

            body.bodyJointSlot1 = addBodyJoints(creature, creature.limbSlot1Limbs[0].LimbRigidbody, body.bodyJointSlot1, oldBody.bodyJointSlot1);
            body.bodyJointSlot2 = addBodyJoints(creature, creature.limbSlot2Limbs[0].LimbRigidbody, body.bodyJointSlot2, oldBody.bodyJointSlot2);
            body.bodyJointSlot1 = addBodyJoints(creature, creature.limbSlot3Limbs[0].LimbRigidbody, body.bodyJointSlot3, oldBody.bodyJointSlot3);
            body.bodyJointSlot2 = addBodyJoints(creature, creature.limbSlot4Limbs[0].LimbRigidbody, body.bodyJointSlot4, oldBody.bodyJointSlot4);
        }
       
    }


    public static ConfigurableJoint addBodyJoints(Creature creature, Rigidbody limbToAttach, ConfigurableJoint joint, ConfigurableJoint oldJoint = null)
    {
        if(oldJoint == null)
        {
            ConfigurableJoint newJoint = joint;
            newJoint.connectedBody = limbToAttach;
            //Sets the movement limits to limited in X,Y and Z directions
            newJoint.xMotion = ConfigurableJointMotion.Free;
            newJoint.yMotion = ConfigurableJointMotion.Free;
            newJoint.zMotion = ConfigurableJointMotion.Free;


            newJoint.autoConfigureConnectedAnchor = true;

            //Adds the angular drives and dampers in X,Y and Z directions
            JointDrive Xdamper = newJoint.angularXDrive;
            Xdamper.positionDamper = 5;
            newJoint.angularXDrive = Xdamper;
            JointDrive Xdrive = newJoint.angularXDrive;
            Xdrive.positionSpring = 150;
            newJoint.angularXDrive = Xdrive;
            JointDrive YZdamper = newJoint.angularYZDrive;
            YZdamper.positionDamper = 5;
            newJoint.angularYZDrive = YZdamper;
            JointDrive YZdrive = newJoint.angularYZDrive;
            YZdrive.positionSpring = 150;
            newJoint.angularYZDrive = YZdrive;

            //Adds Direction Drives
            JointDrive XStandarddrive = newJoint.xDrive;
            XStandarddrive.positionSpring = 20000;
            newJoint.xDrive = XStandarddrive;

            JointDrive Ydrive = newJoint.yDrive;
            Ydrive.positionSpring = 20000;
            newJoint.yDrive = Ydrive;

            JointDrive Zdrive = newJoint.zDrive;
            Zdrive.positionSpring = 20000;
            newJoint.zDrive = Zdrive;

            //Adds angular limit springs in X,Y and Z directions
            SoftJointLimitSpring Xlimit = newJoint.angularXLimitSpring;
            Xlimit.spring = 1000;
            newJoint.angularXLimitSpring = Xlimit;
            SoftJointLimitSpring YZlimit = newJoint.angularYZLimitSpring;
            YZlimit.spring = 1000;
            newJoint.angularYZLimitSpring = YZlimit;

            // Adds linear limit spring to the joint
            SoftJointLimitSpring limit = newJoint.linearLimitSpring;
            limit.spring = 1000;
            newJoint.linearLimitSpring = limit;
            return newJoint;

        }
        else
        {
            ConfigurableJoint newJoint = joint;
            newJoint.connectedBody = limbToAttach;
            newJoint.xMotion = ConfigurableJointMotion.Locked;
            newJoint.yMotion = ConfigurableJointMotion.Locked;
            newJoint.zMotion = ConfigurableJointMotion.Locked;

            newJoint.autoConfigureConnectedAnchor = true;

            //Adds the angular drives and dampers in X,Y and Z directions
            JointDrive Xdamper = newJoint.angularXDrive;
            Xdamper.positionDamper = 5;
            newJoint.angularXDrive = Xdamper;
            JointDrive Xdrive = newJoint.angularXDrive;
            Xdrive.positionSpring = 150;
            newJoint.angularXDrive = Xdrive;
            JointDrive YZdamper = newJoint.angularYZDrive;
            YZdamper.positionDamper = 5;
            newJoint.angularYZDrive = YZdamper;
            JointDrive YZdrive = newJoint.angularYZDrive;
            YZdrive.positionSpring = 150;
            newJoint.angularYZDrive = YZdrive;

            //Adds Direction Drives
            JointDrive XStandarddrive = newJoint.xDrive;
            XStandarddrive.positionSpring = 150;
            newJoint.xDrive = XStandarddrive;

            JointDrive Ydrive = newJoint.yDrive;
            Ydrive.positionSpring = 500;
            newJoint.yDrive = Ydrive;

            JointDrive Zdrive = newJoint.zDrive;
            Zdrive.positionSpring = 150;
            newJoint.zDrive = Zdrive;

            //Adds angular limit springs in X,Y and Z directions
            SoftJointLimitSpring Xlimit = newJoint.angularXLimitSpring;
            Xlimit.spring = 150;
            newJoint.angularXLimitSpring = Xlimit;
            SoftJointLimitSpring YZlimit = newJoint.angularYZLimitSpring;
            YZlimit.spring = 150;
            newJoint.angularYZLimitSpring = YZlimit;

            // Adds linear limit spring to the joint
            SoftJointLimitSpring limit = newJoint.linearLimitSpring;
            limit.spring = 150;
            newJoint.linearLimitSpring = limit;
            return newJoint;

        }
        
    }
}
