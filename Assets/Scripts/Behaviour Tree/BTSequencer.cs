using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour Tree Node/Sequencer")]
public class BTSequencer : BTNode
{
    public BTNode[] nodes;

    public override BTNodeState Evaluate()
    {
        bool nodeRunning = false;

        foreach (BTNode node in nodes)
        {
            BTNodeState currentNodeState = node.Evaluate();
            switch (currentNodeState)
            {
                case BTNodeState.SUCCESS:
                    continue;
                case BTNodeState.RUNNING:
                    nodeRunning = true;
                    continue;
                case BTNodeState.FAILURE:
                    return currentNodeState;
                default:
                    break;
            }
        }

        return nodeRunning? BTNodeState.RUNNING : BTNodeState.SUCCESS;

    }

    
}
