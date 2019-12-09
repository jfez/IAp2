using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    [HideInInspector]
    public int wood;
    [HideInInspector]
    public int stone;
    [HideInInspector]
    public int gold;
    [HideInInspector]
    public int food;
    //public bool building;

    public enum Building {Empty, City, Academy, Fort, Town, EnemyCity, EnemyAcademy, EnemyFort, EnemyTown};

    public Building building;

    public bool selected;

    
    
    // Start is called before the first frame update
    void Awake()
    {
        wood = 0;
        stone = 0;
        gold = 0;
        food = 0;
        building = Building.Empty;
        selected = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateResource(string resource){
        if (resource == "wood"){
            if (wood == 1){
                transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                

            }

            else if (wood == 2){
                transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);

            }

            else if (wood == 3){
                transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);

            }
            

        }

        else if (resource == "stone"){
            if (stone == 1){
                transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);

            }

            else if (stone == 2){
                transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);

            }

            else if (stone == 3){
                transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);

            }
            
        }

        else if (resource == "gold"){
            if (gold == 1){
                transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);

            }

            else if (gold == 2){
                transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);

            }

            else if (gold == 3){
                transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(true);

            }
            
        }

        else if (resource == "food"){
            if (food == 1){
                transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(true);

            }

            else if (food == 2){
                transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);

            }

            else if (food == 3){
                transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(true);

            }
            
        }

    }
}
