using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour Tree Node/Tasks/Move Troops")]
public class BTMoveTroops : BTNode
{
    public override BTNodeState Evaluate(BehaviourController controller)
    {
        if (!controller.hasToMoveTroops) return BTNodeState.SUCCESS;

        int count = 0;
        float[,] worldInfluences = controller.worldMap.GetWorldInfluences();

        foreach (Unit unit in controller.troops)
        {
            Debug.Log("Moving troops...");
            Vector2I gridPos = controller.worldMap.GetGridPosition(unit.transform.position);

            Vector2I[] neighbors = controller.worldMap.GetNeighbors(gridPos.x, gridPos.y);

            float meanInfluence = 0f;
            for (int i = 0; i < neighbors.Length; i++)
            {
                meanInfluence += worldInfluences[neighbors[i].x, neighbors[i].y];
            }
            meanInfluence /= neighbors.Length;

            float difference = Mathf.Infinity;
            Vector2I meanInfluencePos = gridPos;
            for (int i = 0; i < neighbors.Length; i++)
            {
                float currentDifference = Mathf.Abs(worldInfluences[neighbors[i].x, neighbors[i].y] - meanInfluence);
                if (currentDifference < difference)
                {
                    difference = currentDifference;
                    meanInfluencePos = new Vector2I(neighbors[i].x, neighbors[i].y);
                }
            }

            GameObject newGO = new GameObject();
            Transform target = newGO.GetComponent<Transform>();
            target.position = new Vector3(unit.transform.position.x + (meanInfluencePos.x - gridPos.x), 0.5f, unit.transform.position.z + (meanInfluencePos.y - gridPos.y));

            Collider[] playerCitizen = Physics.OverlapSphere(target.position, 0.5f, controller.playerCitizen);
            if (playerCitizen.Length != 0)
            {
                Debug.Log(playerCitizen[0].name + " detected!");
                Collider[] aiCitizens = Physics.OverlapSphere(target.position, 1.5f, controller.aiCitizen);
                if (aiCitizens.Length == 1 || aiCitizens.Length > 1 && count == 0)
                {
                    unit.GetComponent<combatStats>().puedeMoverse = false;
                    unit.GetComponentInParent<SquareUnit>().unit = false;
                    unit.Pathing(unit.transform, target);
                    unit.GetComponent<combatStats>().combate(playerCitizen[0].gameObject);
                    unit.transform.parent = playerCitizen[0].GetComponentInParent<Transform>();

                    Destroy(newGO);
                    count++;
                    break;
                }
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
            count++;

        }

        controller.hasToMoveTroops = false;

        return BTNodeState.FAILURE;
    }
}
