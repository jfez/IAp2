using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckTroops : BTNode
{
    public override BTNodeState Evaluate(BehaviourController controller)
    {
        if (controller.numTroops > 1) return BTNodeState.SUCCESS;

        Collider[] colliders = Physics.OverlapSphere(controller.fort_AIPrefab.transform.position, controller.instancingRadius);
        foreach (Collider collider in colliders)
        {
            Transform selectedTile = collider.transform;
            if (!selectedTile.GetComponent<SquareUnit>().unit)
            {
                GameObject unitInstantiated;
                if (Random.value >= 0.5f) unitInstantiated = Instantiate(controller.ranger_AIPrefab, selectedTile.position + Vector3.up / 2f, controller.ranger_AIPrefab.transform.rotation);
                else unitInstantiated = Instantiate(controller.warrior_AIPrefab, selectedTile.position + Vector3.up / 2f, controller.warrior_AIPrefab.transform.rotation);

                unitInstantiated.transform.parent = selectedTile.transform;
                selectedTile.GetComponent<SquareUnit>().unit = true;
                controller.numTroops++;
                break;
            }
        }

        return BTNodeState.FAILURE;
    }
}
