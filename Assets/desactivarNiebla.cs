using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desactivarNiebla : MonoBehaviour
{ 
    //public Collider myCollider;

    // Start is called before the first frame update
    void Awake()
    {
        //myCollider.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Edificio")
        {
            gameObject.SetActive(false);
            //Debug.Log("entra");
        }
    }
}
