using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareUnit : MonoBehaviour
{
    public bool unit;
    public bool nextToCityPlayer;
    public bool nextToCityAI;
    public bool nextToFortPlayer;
    public bool nextToTownPlayer;
    public bool nextToAcademyPlayer;
    
    // Start is called before the first frame update
    void Awake()
    {
        unit = false;
        nextToCityPlayer = false;
        nextToCityAI = false;
        nextToFortPlayer = false;
        nextToFortPlayer = false;
        nextToTownPlayer = false;
        nextToAcademyPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
