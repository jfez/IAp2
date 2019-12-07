using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourController : MonoBehaviour
{
    public BTNode behaviourTree;

    // Update is called once per frame
    void Update()
    {
        behaviourTree.Evaluate();
    }
}