using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LimbManager : MonoBehaviour
{
    public static void RemoveEndJoints(Creature creature)
    {
        if (creature.limbSlot1Limbs.Count > 0)
        {
            Destroy(creature.limbSlot1Limbs.Last().joint);
        }
        if (creature.limbSlot2Limbs.Count > 0)
        {
            Destroy(creature.limbSlot2Limbs.Last().joint);
        }
        if (creature.limbSlot3Limbs.Count > 0)
        {
            Destroy(creature.limbSlot3Limbs.Last().joint);
        }
        if (creature.limbSlot4Limbs.Count > 0)
        {
            Destroy(creature.limbSlot4Limbs.Last().joint);
        }

    }

    public static void ScaleLimbRandomly(GameObject baseLimb, Creature creature, Limb limb)
    {

        float myRandomXScale = Random.Range(0.25f, 0.5f);
        float myRandomYScale = Random.Range(0.25f, 0.5f);
        float myRandomZScale = Random.Range(0.25f, 0.5f);
        limb.limbDimensions.Add("X_Scale", myRandomXScale);
        limb.limbDimensions.Add("Y_Scale", myRandomYScale);
        limb.limbDimensions.Add("Z_Scale", myRandomZScale);
        baseLimb.transform.localScale += new Vector3(myRandomXScale, myRandomYScale, myRandomZScale);

    }

    public static List<Limb> copyLimbs(List<Limb> limbListToCopy, Creature creature)
    {
        List<Limb> newList = new List<Limb>();
        foreach (Limb limb in limbListToCopy)
        {
            Limb copiedLimb = Limb.newLimb(creature, limb);
            newList.Add(copiedLimb);
        }
        return newList;
    }

    public static void RemoveEndLimb(Creature creature)
    {
        List<Limb> limbSlotToRemoveFrom = creature.limbSlot1Limbs;
        int randomLimbSlotToRemoveFrom = Random.Range(1, 5);
        switch (randomLimbSlotToRemoveFrom)
        {
            case 1:
                limbSlotToRemoveFrom = creature.limbSlot1Limbs;
                break;
            case 2:
                limbSlotToRemoveFrom = creature.limbSlot2Limbs;
                break;
            case 3:
                limbSlotToRemoveFrom = creature.limbSlot3Limbs;
                break;
            case 4:
                limbSlotToRemoveFrom = creature.limbSlot4Limbs;
                break;
            default:
                break;

        }
        if(limbSlotToRemoveFrom.Count != 0)
        {
            Limb limbToDestroy = limbSlotToRemoveFrom.Last();
            limbSlotToRemoveFrom.Remove(limbSlotToRemoveFrom.Last());
            Destroy(limbToDestroy.gameObject);
        }


    }

    public static ConfigurableJoint addLimbJoint(Creature creature, Rigidbody limbToAttach, ConfigurableJoint joint, ConfigurableJoint oldJoint = null)
    {
        if (oldJoint == null)
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
            XStandarddrive.positionSpring = 10000;
            newJoint.xDrive = XStandarddrive;

            JointDrive Ydrive = newJoint.yDrive;
            Ydrive.positionSpring = 10000;
            newJoint.yDrive = Ydrive;

            JointDrive Zdrive = newJoint.zDrive;
            Zdrive.positionSpring = 10000;
            newJoint.zDrive = Zdrive;

            //Adds angular limit springs in X,Y and Z directions
            SoftJointLimitSpring Xlimit = newJoint.angularXLimitSpring;
            Xlimit.spring = 300;
            newJoint.angularXLimitSpring = Xlimit;
            SoftJointLimitSpring YZlimit = newJoint.angularYZLimitSpring;
            YZlimit.spring = 300;
            newJoint.angularYZLimitSpring = YZlimit;

            // Adds linear limit spring to the joint
            SoftJointLimitSpring limit = newJoint.linearLimitSpring;
            limit.spring = 150;
            newJoint.linearLimitSpring = limit;
            return newJoint;

        }
        else
        {
            ConfigurableJoint newJoint = joint;
            newJoint.connectedBody = limbToAttach;
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

    // Update is called once per frame
    void Update()
    {

    }
}
