using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desactivarNiebla : MonoBehaviour
{ 
    public Collider collider;

    // Start is called before the first frame update
    void Awake()
    {
        collider.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            Debug.Log("entra");
        }
    }
}
