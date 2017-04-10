using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool chaseTargetActive = controller.chaseTarget.gameObject.activeSelf;
        return chaseTargetActive;
    }
}
