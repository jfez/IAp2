using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRandomizer : BTNode
{
    public override BTNodeState Evaluate(BehaviourController controller)
    {
        if (Random.value >= 0.5f) return BTNodeState.SUCCESS;
        return BTNodeState.FAILURE;
    }
}
