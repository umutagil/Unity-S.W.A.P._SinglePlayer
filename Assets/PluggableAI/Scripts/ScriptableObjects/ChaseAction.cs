using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/ChaseAction")]
public class ChaseAction : ActionAI{

    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    // Set next destination of ai to player's position
    private void Chase(StateController controller)
    {
        controller.navMeshAgent.destination = controller.chaseTarget.position;
        controller.navMeshAgent.Resume();
    }

}

