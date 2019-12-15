using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool playerTurn;
    public GameObject ticTac;
    private TileMouseOver gameGrid;
    private PlayerResources playerResources;
    private BehaviourController behaviourController;
    
    void Start()
    {
        //Start the coroutine we define below named ExampleCoroutine.
        //StartCoroutine(ExampleCoroutine());
        playerTurn = true;
        gameGrid = GameObject.FindGameObjectWithTag("GridManager").GetComponent<TileMouseOver>();
        playerResources = GetComponent<PlayerResources>();
        behaviourController = GetComponent<BehaviourController>();
        ticTac.SetActive(false);
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
        ticTac.SetActive(true);

        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        behaviourController.PerformTurn_AI(Random.Range(3,8));

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        playerTurn = true;
        playerResources.UpdateResources();
        ticTac.transform.rotation = Quaternion.identity;
        ticTac.SetActive(false);
    }
}
