using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Looks for the player agent,
   returns true if finds, else false */
[CreateAssetMenu (menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    // checks if the target is visible
    private bool Look (StateController controller)
    {        
        RaycastHit hit;

        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookDistance, Color.yellow);        

        if (Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereRadius, 
            controller.eyes.forward, out hit, controller.enemyStats.lookDistance)
            && hit.collider.CompareTag("Player"))
        {
            controller.chaseTarget = hit.transform;
            return true;
        }
        else
        {
            return false;
        }        
    }
    
}
