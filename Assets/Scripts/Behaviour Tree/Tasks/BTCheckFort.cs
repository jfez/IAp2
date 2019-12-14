using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour Tree Node/Tasks/Check Fort")]
public class BTCheckFort : BTNode
{
    public override BTNodeState Evaluate(BehaviourController controller)
    {
        if (controller.hasFort) return BTNodeState.SUCCESS;

        Collider[] colliders = Physics.OverlapSphere(controller.mainCiyt_AI.transform.position, controller.buildingRadius);
        foreach (Collider collider in colliders)
        {
            Resources resources = collider.GetComponent<Resources>();
            if (resources != null && resources.building == Resources.Building.Empty)
            {
                Transform selectedTile = collider.transform;
                Instantiate(controller.fort_AIPrefab, selectedTile.position + Vector3.up / 2f, controller.fort_AIPrefab.transform.rotation);
                selectedTile.GetComponent<Resources>().building = Resources.Building.Fort;
                selectedTile.GetChild(0).gameObject.SetActive(false);
                selectedTile.GetChild(1).gameObject.SetActive(false);
                selectedTile.GetChild(2).gameObject.SetActive(false);
                selectedTile.GetChild(3).gameObject.SetActive(false);

                controller.hasFort = true;
                break;
            }
        }

        return BTNodeState.FAILURE;
    }
}
