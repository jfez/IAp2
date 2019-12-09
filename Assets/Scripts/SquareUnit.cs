using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareUnit : MonoBehaviour
{
    public bool unit;
    public bool nextToCityPlayer;
    public bool nextToCityAI;
    
    // Start is called before the first frame update
    void Awake()
    {
        unit = false;
        nextToCityPlayer = false;
        nextToCityAI = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
