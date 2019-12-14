using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourController : MonoBehaviour
{
    public BTNode behaviourTree;

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

    [HideInInspector] public float buildingRadius = 2f;
    [HideInInspector] public float instancingRadius = 3f;

    [HideInInspector] public int numTroops;
    [HideInInspector] public int numExplorers;
    [HideInInspector] public int numLabourers;

    public void PerformTurn_AI(int actionsPerTurn)
    {
        for (int i = 0; i < actionsPerTurn; i++)
        {
            behaviourTree.Evaluate(this);
        }
    }
}