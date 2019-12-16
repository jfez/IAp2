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
            GameObject newGO = new GameObject();
            Transform target = newGO.GetComponent<Transform>();

            Collider[] playerCityCollider = Physics.OverlapSphere(unit.transform.position, 1.4f, controller.playerCity);
            if (playerCityCollider.Length != 0)
            {
                target.position = new Vector3(playerCityCollider[0].transform.position.x, 0.5f, playerCityCollider[0].transform.position.z);

                unit.Pathing(unit.transform, target);
                unit.GetComponent<combatStats>().atacarCiudad(playerCityCollider[0].gameObject);
                unit.GetComponentInParent<SquareUnit>().unit = false;
                unit.transform.parent = null;

                Destroy(newGO);
                count++;
                continue;
            }

            Transform playerCitizenCell = null;
            Collider[] surroundingCells = Physics.OverlapSphere(unit.transform.position, 1.4f, controller.cellLayer);
            foreach (Collider col in surroundingCells)
            {
                if (col.GetComponentInChildren<Unit>() != null && col.GetComponentInChildren<Unit>().gameObject.CompareTag("Player"))
                {
                    playerCitizenCell = col.transform;
                }
            }

            if (playerCitizenCell != null)
            {
                target.position = new Vector3(playerCitizenCell.position.x, 0.5f, playerCitizenCell.position.z);

                Collider[] aiCitizens = Physics.OverlapSphere(target.position, 1.4f, controller.aiCitizen);
                List<Collider> colliderFilter = new List<Collider>();
                foreach (Collider col in aiCitizens)
                {
                    if (!col.isTrigger) colliderFilter.Add(col);
                }

                if (colliderFilter.Count == 1 || colliderFilter.Count > 1 && count == 0)
                {
                    Debug.Log("attacking!");
                    unit.GetComponent<combatStats>().puedeMoverse = false;
                    unit.GetComponentInParent<SquareUnit>().unit = false;
                    unit.Pathing(unit.transform, target);
                    unit.GetComponent<combatStats>().combate(playerCitizenCell.GetComponentInChildren<Unit>().gameObject);
                    unit.transform.parent = playerCitizenCell;

                    Destroy(newGO);
                    count++;
                    continue;
                }
            }

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

            target.position = new Vector3(unit.transform.position.x + (meanInfluencePos.x - gridPos.x), 0.5f, unit.transform.position.z + (meanInfluencePos.y - gridPos.y));

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
