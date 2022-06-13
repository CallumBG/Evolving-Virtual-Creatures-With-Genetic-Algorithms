using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{

    public static Creature CopyCreatureAttributes(Creature creature)
    {
        //Initialize new creature to hold data
        Creature newCopy = Creature.newCreature();
        newCopy.isCopy = true;
        //Copy old creature
        newCopy.bodyDimensions = creature.bodyDimensions;
        newCopy.limbSlot1Limbs = LimbManager.copyLimbs(creature.limbSlot1Limbs, newCopy);
        newCopy.limbSlot2Limbs = LimbManager.copyLimbs(creature.limbSlot2Limbs, newCopy);
        newCopy.limbSlot3Limbs = LimbManager.copyLimbs(creature.limbSlot3Limbs, newCopy);
        newCopy.limbSlot4Limbs = LimbManager.copyLimbs(creature.limbSlot4Limbs, newCopy);
        //copy brain
        newCopy.brain = new Brain(creature.brain);
        newCopy.currentLimbCount = newCopy.limbSlot1Limbs.Count + newCopy.limbSlot2Limbs.Count;

        return newCopy;
    }

    public static void AddInitialLimbs(Creature creature)
    {
        //Makes a new limb while the number of limbs is less than the defined number of maximum limbs
        while (creature.currentLimbCount < creature.maxLimbCount)
        {
            Limb.MakeNewRandomLimb(creature);
        }
    }

}
