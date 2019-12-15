using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour Tree Node/Tasks/Check Explorers")]
public class BTCheckExplorers : BTNode
{
    public override BTNodeState Evaluate(BehaviourController controller)
    {
        if (controller.numExplorers != 0) return BTNodeState.SUCCESS;

        Collider[] colliders = Physics.OverlapSphere(controller.academy_AIPrefab.transform.position, controller.instancingRadius);
        foreach (Collider collider in colliders)
        {
            Transform selectedTile = collider.transform;
            Resources resources = selectedTile.GetComponent<Resources>();
            SquareUnit squareUnit = selectedTile.GetComponent<SquareUnit>();
            if (squareUnit != null && resources.building == Resources.Building.Empty && !squareUnit.unit)
            {
                GameObject unitInstantiated = Instantiate(controller.explorer_AIPrefab, selectedTile.position + Vector3.up / 2f, controller.explorer_AIPrefab.transform.rotation);
                unitInstantiated.transform.parent = selectedTile.transform;
                selectedTile.GetComponent<SquareUnit>().unit = true;
                controller.numExplorers++;
                controller.explorers.Add(unitInstantiated.GetComponent<Unit>());
                break;
            }
        }

        return BTNodeState.FAILURE;
    }
}
