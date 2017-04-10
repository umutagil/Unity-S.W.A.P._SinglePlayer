using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : ActionAI {

    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    // Attack if ai can see the player and the player is in shoot range
    private void Attack(StateController controller)
    {                

        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.attackDistance, Color.yellow);

        RaycastHit hit;
        if (Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereRadius, 
            controller.eyes.forward, out hit, controller.enemyStats.attackDistance)
            && hit.collider.CompareTag("Player"))
        {            
            controller.enemyAttack.Attack();
        }

    }
}
