using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    public GameObject canvasPause;
    
    // Start is called before the first frame update
    void Start()
    {
        canvasPause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPause(){
        canvasPause.SetActive(true);

    }

    public void ClosePause(){
        canvasPause.SetActive(false);

    }

    public void ExitMenu(){
        SceneManager.LoadScene("Init");

    }
}
