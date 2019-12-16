using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour Tree Node/Tasks/Move Labourers")]
public class BTMoveLabourers : BTNode
{
    public override BTNodeState Evaluate(BehaviourController controller)
    {
        if (!controller.hasToMoveLabourers) return BTNodeState.SUCCESS;

        float[,] worldInfluences = controller.worldMap.GetWorldInfluences();

        foreach (Unit unit in controller.labourers)
        {
            Debug.Log("Moving labourers...");
            Vector2I gridPos = controller.worldMap.GetGridPosition(unit.transform.position);

            Vector2I[] neighbors = controller.worldMap.GetNeighbors(gridPos.x, gridPos.y);

            float maxInfluence = Mathf.Infinity;
            Vector2I maxInfluencePos = gridPos;
            for (int i = 0; i < neighbors.Length; i++)
            {
                if (worldInfluences[neighbors[i].x, neighbors[i].y] < maxInfluence)
                {
                    maxInfluence = worldInfluences[neighbors[i].x, neighbors[i].y];
                    maxInfluencePos = new Vector2I(neighbors[i].x, neighbors[i].y);
                }
            }

            GameObject newGO = new GameObject();
            Transform target = newGO.GetComponent<Transform>();
            target.position = new Vector3(unit.transform.position.x + (maxInfluencePos.x - gridPos.x), 0.5f, unit.transform.position.z + (maxInfluencePos.y - gridPos.y));

            Collider[] cell = Physics.OverlapSphere(target.position, 0.5f, controller.cellLayer);
            foreach (Collider collider in cell)
            {
                if (collider.GetComponent<Resources>() != null && collider.GetComponent<Resources>().building == Resources.Building.Empty && !collider.GetComponent<SquareUnit>().unit)
                {
                    collider.GetComponent<SquareUnit>().unit = true;
                    unit.GetComponentInParent<SquareUnit>().unit = false;
                    unit.Pathing(unit.transform, target);
                    unit.transform.parent = collider.transform;
                    break;
                }
            }

            Destroy(newGO);

        }

        controller.hasToMoveLabourers = false;

        return BTNodeState.FAILURE;
    }
}
