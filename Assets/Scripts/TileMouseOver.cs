using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMouseOver : MonoBehaviour
{
    public Color highlightColor;
    Color normalColor;
    //private Collider coll;
    private Renderer rend;
    private Transform _selection;
    
    // Start is called before the first frame update
    void Start()
    {
        normalColor = Color.white;
        //coll = GetComponent<Collider>();
        //rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_selection != null){
            rend = _selection.GetComponent<Renderer>();
            rend.material.color = normalColor;
            _selection = null;

        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)){
            Transform selection = hitInfo.transform;
            if (selection.CompareTag("Selectable")){
                rend = selection.GetComponent<Renderer>();
                if (rend != null){
                    rend.material.color = highlightColor;

                }
                _selection = selection;
                
            }
            


        }

        

        
    }
}
