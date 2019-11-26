using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileMouseOver : MonoBehaviour
{
    public Color highlightColor;
    public GameObject cityPrefab;

    public LayerMask UIMask;
    Color normalColor;
    //private Collider coll;
    private Renderer rend;
    private Transform _selection;
    private int fingerID = -1;
    
    private void Awake()
    {
        #if !UNITY_EDITOR
        fingerID = 0; 
        #endif
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //normalColor = Color.white;
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

        if (EventSystem.current.IsPointerOverGameObject(fingerID))    // is the touch on the GUI
        {
            // GUI Action
            return;
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, ~UIMask)){
            Transform selection = hitInfo.transform;
            if (selection.CompareTag("Selectable") || selection.CompareTag("SelectableBridge")){
                rend = selection.GetComponent<Renderer>();
                if (rend != null){
                    normalColor = rend.material.color;
                    rend.material.color = highlightColor;
                    if (Input.GetMouseButtonDown(0)&& !selection.GetComponent<Resources>().building){
                        Instantiate(cityPrefab, selection.position + Vector3.up/2f, cityPrefab.transform.rotation);
                        selection.GetComponent<Resources>().building = true;

                    }

                }
                _selection = selection;
                
            }
            


        }

        

        
    }
}
