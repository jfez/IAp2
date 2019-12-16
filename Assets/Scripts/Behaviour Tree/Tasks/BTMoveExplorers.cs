using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour Tree Node/Tasks/Move Explorers")]
public class BTMoveExplorers : BTNode
{
    public override BTNodeState Evaluate(BehaviourController controller)
    {
        if (!controller.hasToMoveExplorers) return BTNodeState.SUCCESS;

        float[,] worldInfluences = controller.worldMap.GetWorldInfluences();

        foreach (Unit unit in controller.explorers)
        {
            Debug.Log("Moving explorers...");
            Vector2I gridPos = controller.worldMap.GetGridPosition(unit.transform.position);

            Vector2I[] neighbors = controller.worldMap.GetNeighbors(gridPos.x, gridPos.y);

            float minInfluence = -Mathf.Infinity;
            Vector2I minInfluencePos = gridPos;
            for (int i = 0; i < neighbors.Length; i++)
            {
                Vector2I[] nextNeighbors = controller.worldMap.GetNeighbors(neighbors[i].x, neighbors[i].y);
                for (int j = 0; j < nextNeighbors.Length; j++)
                {
                    if (worldInfluences[nextNeighbors[j].x, nextNeighbors[j].y] > minInfluence)
                    {
                        minInfluence = worldInfluences[nextNeighbors[j].x, nextNeighbors[j].y];
                        minInfluencePos = new Vector2I(nextNeighbors[j].x, nextNeighbors[j].y);
                    }
                }

                if (worldInfluences[neighbors[i].x, neighbors[i].y] > minInfluence)
                {
                    minInfluence = worldInfluences[neighbors[i].x, neighbors[i].y];
                    minInfluencePos = new Vector2I(neighbors[i].x, neighbors[i].y);
                }
            }

            GameObject newGO = new GameObject();
            Transform target = newGO.GetComponent<Transform>();
            target.position = new Vector3(unit.transform.position.x + (minInfluencePos.x - gridPos.x), 0.5f, unit.transform.position.z + (minInfluencePos.y - gridPos.y));

            Collider[] playerCitizens = Physics.OverlapSphere(target.position, 2f, controller.playerCitizen);
            if (playerCitizens.Length != 0)
            {
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
                target.position = new Vector3(unit.transform.position.x + (maxInfluencePos.x - gridPos.x), 0.5f, unit.transform.position.z + (maxInfluencePos.y - gridPos.y));
            }

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

        controller.hasToMoveExplorers = false;

        return BTNodeState.FAILURE;
    }
}
