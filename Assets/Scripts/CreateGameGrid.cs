﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateGameGrid : MonoBehaviour
{
    [Tooltip("The grid.")]
    public GameObject grid;

    public GameObject casillaInstanced;
    public GameObject casillaRiver;
    public GameObject casillaBridge;

    public GameObject cityPlayer;

    public GameObject playerPlaceholder;
    private Vector3 pos;

    public GameObject[] casillasArray;
    private int index;
    private int wood, stone, gold, food;

    private GameObject casilla;
    private Grid gridScript;

    private GameObject casillaCityPlayer;
    private GameObject casillaCityAI;
    
    // Start is called before the first frame update
    void Awake()
    {
        gridScript = grid.GetComponent<Grid>();
        Vector3 pos = new Vector3 (-7.5f, -1f, 7.5f);
        
        casillasArray = new GameObject[256];
        
        CreateGrid();
        //gridScript.CreateGrid();

        index = 0;
        
        wood = 32;
        stone = 32;
        gold = 32;
        food = 32;

    }

    private void Start()
    {
        gridScript.CreateGrid();
        CreateResources();
        UpdateNextToCityPlayer(casillaCityPlayer);
        UpdateNextToCityAI(casillaCityAI);

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
                if(i==0 && j == 2 || i==0 && j == 3 || i==1 && j == 3 || i==2 && j == 3 
                    || i==2 && j == 4 || i==4 && j == 5 || i==4 && j == 6 
                    || i==5 && j == 6 || i==6 && j == 6 || i==6 && j == 7 
                    || i==7 && j == 7 || i==8 && j == 8 || i==9 && j == 8 
                    || i==9 && j == 9 || i==10 && j == 9 || i==11 && j == 9 
                    || i==11 && j == 10 || i==13 && j == 11 || i==13 && j == 12
                    || i==14 && j == 12 || i==15 && j == 12 || i==15 && j == 13){
                    casilla = Instantiate(casillaRiver, pos, Quaternion.identity, grid.transform);

                }

                else if(i==3 && j == 4 || i==3 && j == 5 || i==7 && j == 8 || i==8 && j == 7 || i==12 && j == 10 || i==12 && j == 11 ){
                    casilla = Instantiate(casillaBridge, pos, Quaternion.identity,grid.transform);
                }

                else{

                    if (i == 14 && j == 1){ //PLAYER CITY
                        Instantiate(cityPlayer, pos + Vector3.up/2f, cityPlayer.transform.rotation);
                        casilla = Instantiate(casillaInstanced, pos, Quaternion.identity, grid.transform);
                        casilla.gameObject.GetComponent<Resources>().building = Resources.Building.City;
                        casillaCityAI = casilla;
                        

                    }

                    else if (i == 1 && j == 14){    //ENEMY CITY
                        Instantiate(cityPlayer, pos + Vector3.up/2f, cityPlayer.transform.rotation);
                        casilla = Instantiate(casillaInstanced, pos, Quaternion.identity, grid.transform);
                        casilla.gameObject.GetComponent<Resources>().building = Resources.Building.EnemyCity;
                        casillaCityPlayer = casilla;
                        

                    }

                    

                    else if (i == 0 && j == 0){
                        GameObject unitInstanced = Instantiate(playerPlaceholder, pos + Vector3.up/2f, playerPlaceholder.transform.rotation);
                        
                        casilla = Instantiate(casillaInstanced, pos, Quaternion.identity, grid.transform);
                        unitInstanced.transform.parent = casilla.transform;
                        casilla.gameObject.GetComponent<SquareUnit>().unit = true;
                        
                    }

                    else{
                        casilla = Instantiate(casillaInstanced, pos, Quaternion.identity, grid.transform);

                    }

                    casilla.transform.GetChild(0).gameObject.SetActive(false);
                    casilla.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                    casilla.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                    casilla.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);

                    casilla.transform.GetChild(1).gameObject.SetActive(false);
                    casilla.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                    casilla.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                    casilla.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);

                    casilla.transform.GetChild(2).gameObject.SetActive(false);
                    casilla.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
                    casilla.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
                    casilla.transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(false);

                    casilla.transform.GetChild(3).gameObject.SetActive(false);
                    casilla.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false);
                    casilla.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
                    casilla.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);

                }
                
                casillasArray[index] = casilla;
                index++;
                pos.x += 1f;
            }
            pos.x = -7.5f;
            pos.z-= 1f;
        }


    }

    void CreateResources(){ //recursos en las ciudades y alrededores?? 
        
        //WOOD
        for (int i = 0; i < 32; i++){
            int fila = Random.Range(0,16);
            int col = Random.Range(0,16);
            

            while(casillasArray[fila*16+col].GetComponent<Resources>() == null || casillasArray[fila*16+col].GetComponent<Resources>().wood != 0 
            || (fila == 15 && col == 0) || (fila == 0 && col == 15)) 
            //si resources es null es que esa casilla no tiene recursos (puente o río)
            {
                
                fila = Random.Range(0,16);
                col = Random.Range(0,16);
            }
            int filaReflex = 15 - fila;
            int colReflex = 15 - col;

            int resourceAmount = Random.Range(1,4);
            casillasArray[fila*16+col].GetComponent<Resources>().wood = resourceAmount;
            casillasArray[fila*16+col].transform.GetChild(0).gameObject.SetActive(true);
            casillasArray[fila*16+col].GetComponent<Resources>().UpdateResource("wood");

            casillasArray[filaReflex*16+colReflex].GetComponent<Resources>().wood = resourceAmount;
            casillasArray[filaReflex*16+colReflex].transform.GetChild(0).gameObject.SetActive(true);
            casillasArray[filaReflex*16+colReflex].GetComponent<Resources>().UpdateResource("wood");
        }

        //STONE
        for (int i = 0; i < 32; i++){
            int fila = Random.Range(0,16);
            int col = Random.Range(0,16);
            

            while(casillasArray[fila*16+col].GetComponent<Resources>() == null || casillasArray[fila*16+col].GetComponent<Resources>().stone != 0 
            || (fila == 15 && col == 0) || (fila == 0 && col == 15)) 
            //si resources es null es que esa casilla no tiene recursos (puente o río)
            {
                
                fila = Random.Range(0,16);
                col = Random.Range(0,16);
            }
            int filaReflex = 15 - fila;
            int colReflex = 15 - col;

            int resourceAmount = Random.Range(1,4);
            casillasArray[fila*16+col].GetComponent<Resources>().stone = resourceAmount;
            casillasArray[fila*16+col].transform.GetChild(1).gameObject.SetActive(true);
            casillasArray[fila*16+col].GetComponent<Resources>().UpdateResource("stone");

            casillasArray[filaReflex*16+colReflex].GetComponent<Resources>().stone = resourceAmount;
            casillasArray[filaReflex*16+colReflex].transform.GetChild(1).gameObject.SetActive(true);
            casillasArray[filaReflex*16+colReflex].GetComponent<Resources>().UpdateResource("stone");
        }

        //GOLD
        for (int i = 0; i < 32; i++){
            int fila = Random.Range(0,16);
            int col = Random.Range(0,16);
            

            while(casillasArray[fila*16+col].GetComponent<Resources>() == null || casillasArray[fila*16+col].GetComponent<Resources>().gold != 0 
            || (fila == 15 && col == 0) || (fila == 0 && col == 15)) 
            //si resources es null es que esa casilla no tiene recursos (puente o río)
            {
                
                fila = Random.Range(0,16);
                col = Random.Range(0,16);
            }
            int filaReflex = 15 - fila;
            int colReflex = 15 - col;

            int resourceAmount = Random.Range(1,4);
            casillasArray[fila*16+col].GetComponent<Resources>().gold = resourceAmount;
            casillasArray[fila*16+col].transform.GetChild(2).gameObject.SetActive(true);
            casillasArray[fila*16+col].GetComponent<Resources>().UpdateResource("gold");

            casillasArray[filaReflex*16+colReflex].GetComponent<Resources>().gold = resourceAmount;
            casillasArray[filaReflex*16+colReflex].transform.GetChild(2).gameObject.SetActive(true);
            casillasArray[filaReflex*16+colReflex].GetComponent<Resources>().UpdateResource("gold");
        }

        //FOOD
        for (int i = 0; i < 32; i++){
            int fila = Random.Range(0,16);
            int col = Random.Range(0,16);
            

            while(casillasArray[fila*16+col].GetComponent<Resources>() == null || casillasArray[fila*16+col].GetComponent<Resources>().food != 0 
            || (fila == 15 && col == 0) || (fila == 0 && col == 15)) 
            //si resources es null es que esa casilla no tiene recursos (puente o río)
            {
                
                fila = Random.Range(0,16);
                col = Random.Range(0,16);
            }
            int filaReflex = 15 - fila;
            int colReflex = 15 - col;

            int resourceAmount = Random.Range(1,4);
            casillasArray[fila*16+col].GetComponent<Resources>().food = resourceAmount;
            casillasArray[fila*16+col].transform.GetChild(3).gameObject.SetActive(true);
            casillasArray[fila*16+col].GetComponent<Resources>().UpdateResource("food");

            casillasArray[filaReflex*16+colReflex].GetComponent<Resources>().food = resourceAmount;
            casillasArray[filaReflex*16+colReflex].transform.GetChild(3).gameObject.SetActive(true);
            casillasArray[filaReflex*16+colReflex].GetComponent<Resources>().UpdateResource("food");
        }


    }

    void UpdateNextToCityPlayer(GameObject casillaCiudad){
        foreach(GameObject casilla in casillasArray){
            if (Vector3.Distance(casilla.transform.position, casillaCiudad.transform.position) < 1.5 && casilla != casillaCiudad){
                if(casilla.GetComponent<SquareUnit>() != null){
                    casilla.GetComponent<SquareUnit>().nextToCityPlayer = true;
                    //print (casilla.GetComponent<SquareUnit>().nextToCity);

                }
                
            }
        }


    }

    void UpdateNextToCityAI(GameObject casillaCiudad){
        foreach(GameObject casilla in casillasArray){
            if (Vector3.Distance(casilla.transform.position, casillaCiudad.transform.position) < 1.5 && casilla != casillaCiudad){
                if(casilla.GetComponent<SquareUnit>() != null){
                    casilla.GetComponent<SquareUnit>().nextToCityAI = true;
                    //print (casilla.GetComponent<SquareUnit>().nextToCity);

                }
                
            }
        }


    }
}
