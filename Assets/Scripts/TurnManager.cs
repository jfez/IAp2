using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool playerTurn;
    private TileMouseOver gameGrid;
    private PlayerResources playerResources;
    
    void Start()
    {
        //Start the coroutine we define below named ExampleCoroutine.
        //StartCoroutine(ExampleCoroutine());
        playerTurn = true;
        gameGrid = GameObject.FindGameObjectWithTag("GridManager").GetComponent<TileMouseOver>();
        playerResources = GetComponent<PlayerResources>();
    }

    public void ClickButton(){
        if (playerTurn){
            gameGrid.Close();
            StartCoroutine(TurnCoroutine());
            

        }

        
    }

    IEnumerator TurnCoroutine()
    {
        playerTurn = false;
        
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        playerTurn = true;
        playerResources.UpdateResources();
    }
}
