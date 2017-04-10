using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Looks for the player agent while chasing it, returns true if finds, else false. 
   Since the agent chases the player, we assume it knows the position of the player */

[CreateAssetMenu (menuName = "PluggableAI/Decisions/LookChaserDecision")]
public class LookChaserDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    // check if any object is between the ai and player
    // or the player is in look distance
    private bool Look (StateController controller)
    {
        RaycastHit hit;

        Vector3 directionToPlayer = (controller.chaseTarget.position - controller.eyes.position).normalized;
        if(Physics.Raycast(controller.eyes.position, directionToPlayer, out hit, controller.enemyStats.lookDistance)
            && hit.collider.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
