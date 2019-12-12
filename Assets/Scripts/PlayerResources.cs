using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour
{
    [HideInInspector]
    public int wood;
    [HideInInspector]
    public int stone;
    [HideInInspector]
    public int gold;
    [HideInInspector]
    public int food;

    private CreateGameGrid gameGrid;

    public Text textWood; 
    public Text textGold; 
    public Text textStone; 
    public Text textFood; 
    
    // Start is called before the first frame update
    void Start()
    {
        gameGrid = GameObject.FindGameObjectWithTag("GridManager").GetComponent<CreateGameGrid>();
        
        wood = 100;
        stone = 100;
        gold = 10;
        food = 10;

        textFood.text = food.ToString();
        textWood.text = wood.ToString();
        textGold.text = gold.ToString();
        textStone.text = stone.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateResources(){

        foreach(GameObject casilla in gameGrid.casillasArray){
            bool recolectando = false;
            
            if (casilla.transform.childCount >= 8)
            {
                if(casilla.transform.GetChild(7).tag == "jornalero")
                recolectando = casilla.transform.GetChild(7).GetComponent<JornaleroScript>().recolectando;
            }
            
            if (casilla.GetComponent<SquareUnit>() != null && casilla.GetComponent<Resources>() != null &&
            (casilla.GetComponent<SquareUnit>().nextToCityPlayer || casilla.GetComponent<Resources>().building == Resources.Building.City || recolectando == true)){
                wood += casilla.GetComponent<Resources>().wood;
                stone += casilla.GetComponent<Resources>().stone;
                gold += casilla.GetComponent<Resources>().gold;
                food += casilla.GetComponent<Resources>().food;

            }
        }

        textFood.text = food.ToString();
        textWood.text = wood.ToString();
        textGold.text = gold.ToString();
        textStone.text = stone.ToString();

        /*print (wood);
        print(stone);
        print(gold);
        print(food);*/

    }
}
