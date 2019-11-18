using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameGrid : MonoBehaviour
{
    
    public GameObject casillaInstanced;
    private Vector3 pos;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = new Vector3 (-7.5f, -1f, 7.5f);
        
        createGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createGrid(){
        for (int i = 0; i < 16; i++){
            for (int j = 0; j < 16; j++){
                if(i == 0 && j == 0){
                    pos.x = -7.5f;
                    pos.z = 7.5f;
                    pos.y = -0.5f;
                }
                Instantiate(casillaInstanced, pos, Quaternion.identity);
                pos.x += 1f;
            }
            pos.x = -7.5f;
            pos.z-= 1f;
        }


    }
}
