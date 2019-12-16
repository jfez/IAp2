using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourController : MonoBehaviour
{
    public BTNode behaviourTree;
    public InfluenceMapControl worldMap;
    public LayerMask cellLayer;
    public LayerMask playerCitizen;
    public LayerMask aiCitizen;
    public LayerMask playerCity;

    public GameObject mainCiyt_AI;
    public GameObject town_AIPrefab;
    public GameObject fort_AIPrefab;
    public GameObject academy_AIPrefab;

    public GameObject ranger_AIPrefab;
    public GameObject warrior_AIPrefab;
    public GameObject explorer_AIPrefab;
    public GameObject labourer_AIPrefab;

    [HideInInspector] public bool hasFort;
    [HideInInspector] public bool hasTown;
    [HideInInspector] public bool hasAcademy;

    [HideInInspector] public bool hasToMoveTroops;
    [HideInInspector] public bool hasToMoveExplorers;
    [HideInInspector] public bool hasToMoveLabourers;

    [HideInInspector] public float buildingRadius = 2f;
    [HideInInspector] public float instancingRadius = 10f;

    [HideInInspector] public int numTroops;
    [HideInInspector] public int numExplorers;
    [HideInInspector] public int numLabourers;

    [HideInInspector] public List<Unit> troops = new List<Unit>();
    [HideInInspector] public List<Unit> explorers = new List<Unit>();
    [HideInInspector] public List<Unit> labourers = new List<Unit>();

    public void PerformTurn_AI(int actionsPerTurn)
    {
        hasToMoveTroops = true;
        hasToMoveExplorers = true;
        hasToMoveLabourers = true;

        for (int i = 0; i < actionsPerTurn; i++)
        {
            behaviourTree.Evaluate(this);
        }
    }
}