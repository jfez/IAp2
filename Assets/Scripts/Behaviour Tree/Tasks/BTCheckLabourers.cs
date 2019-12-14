using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckLabourers : BTNode
{
    public override BTNodeState Evaluate(BehaviourController controller)
    {
        if (controller.numLabourers > 1) return BTNodeState.SUCCESS;

        Collider[] colliders = Physics.OverlapSphere(controller.town_AIPrefab.transform.position, controller.instancingRadius);
        foreach (Collider collider in colliders)
        {
            Transform selectedTile = collider.transform;
            if (!selectedTile.GetComponent<SquareUnit>().unit)
            {
                GameObject unitInstantiated = Instantiate(controller.labourer_AIPrefab, selectedTile.position + Vector3.up / 2f, controller.labourer_AIPrefab.transform.rotation);
                unitInstantiated.transform.parent = selectedTile.transform;
                selectedTile.GetComponent<SquareUnit>().unit = true;
                controller.numLabourers++;
                break;
            }
        }

        return BTNodeState.FAILURE;
    }
}
