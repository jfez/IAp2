using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameGrid : MonoBehaviour
{
    
    public GameObject casillaInstanced;
    private Vector3 pos;

    private GameObject[] casillasArray;
    private int index;
    private int wood, stone, gold, food;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = new Vector3 (-7.5f, -1f, 7.5f);
        
        casillasArray = new GameObject[256];
        
        CreateGrid();

        index = 0;
        
        wood = 32;
        stone = 32;
        gold = 32;
        food = 32;

        //CreateResources();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateGrid(){
        for (int i = 0; i < 16; i++){
            for (int j = 0; j < 16; j++){
                if(i == 0 && j == 0){
                    pos.x = -7.5f;
                    pos.z = 7.5f;
                    pos.y = -0.5f;
                }
                GameObject casilla = Instantiate(casillaInstanced, pos, Quaternion.identity);
                casilla.transform.GetChild(0).gameObject.SetActive(false);
                casilla.transform.GetChild(1).gameObject.SetActive(false);
                casilla.transform.GetChild(2).gameObject.SetActive(false);
                casilla.transform.GetChild(3).gameObject.SetActive(false);
                casillasArray[index] = casilla;
                index++;
                pos.x += 1f;
            }
            pos.x = -7.5f;
            pos.z-= 1f;
        }


    }

    void CreateResources(){
        for (int i = 0; i < 32; i++){
            int fila = Random.Range(0,16);
            int col = Random.Range(0,8);
            int colReflex = 15-col;
        }


    }
}
