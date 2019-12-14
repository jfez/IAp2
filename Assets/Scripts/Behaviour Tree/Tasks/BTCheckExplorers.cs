using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckExplorers : BTNode
{
    public override BTNodeState Evaluate(BehaviourController controller)
    {
        if (controller.numExplorers != 0) return BTNodeState.SUCCESS;

        Collider[] colliders = Physics.OverlapSphere(controller.academy_AIPrefab.transform.position, controller.instancingRadius);
        foreach (Collider collider in colliders)
        {
            Transform selectedTile = collider.transform;
            if (!selectedTile.GetComponent<SquareUnit>().unit)
            {
                GameObject unitInstantiated = Instantiate(controller.explorer_AIPrefab, selectedTile.position + Vector3.up / 2f, controller.explorer_AIPrefab.transform.rotation);
                unitInstantiated.transform.parent = selectedTile.transform;
                selectedTile.GetComponent<SquareUnit>().unit = true;
                controller.numExplorers++;
                break;
            }
        }

        return BTNodeState.FAILURE;
    }
}
