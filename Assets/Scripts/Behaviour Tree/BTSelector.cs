using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour Tree Node/Selector")]
public class BTSelector : BTNode
{
    public BTNode[] nodes;

    public override BTNodeState Evaluate(BehaviourController controller)
    {
        foreach (BTNode node in nodes)
        {
            BTNodeState currentNodeState = node.Evaluate(controller);
            switch (currentNodeState)
            {
                case BTNodeState.SUCCESS:
                    return currentNodeState;
                case BTNodeState.FAILURE:
                    continue;
                default:
                    break;
            }
        }

        return BTNodeState.FAILURE;

    }
}
