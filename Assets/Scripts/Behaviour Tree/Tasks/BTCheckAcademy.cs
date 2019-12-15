using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour Tree Node/Tasks/Check Academy")]
public class BTCheckAcademy : BTNode
{
    public override BTNodeState Evaluate(BehaviourController controller)
    {
        if (controller.hasAcademy) return BTNodeState.SUCCESS;

        Collider[] colliders = Physics.OverlapSphere(controller.mainCiyt_AI.transform.position, controller.buildingRadius);
        foreach (Collider collider in colliders)
        {
            Resources resources = collider.GetComponent<Resources>();
            if (resources != null && resources.building == Resources.Building.Empty)
            {
                Transform selectedTile = collider.transform;
                Instantiate(controller.academy_AIPrefab, selectedTile.position + Vector3.up / 1.44f, controller.academy_AIPrefab.transform.rotation);
                selectedTile.GetComponent<Resources>().building = Resources.Building.Academy;
                selectedTile.GetChild(0).gameObject.SetActive(false);
                selectedTile.GetChild(1).gameObject.SetActive(false);
                selectedTile.GetChild(2).gameObject.SetActive(false);
                selectedTile.GetChild(3).gameObject.SetActive(false);

                controller.hasAcademy = true;
                break;
            }
        }

        return BTNodeState.FAILURE;
    }
}
