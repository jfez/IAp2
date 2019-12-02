using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileMouseOver : MonoBehaviour
{
    public Color highlightColor;
    public GameObject cityPrefab;
    public GameObject academyPrefab;
    public GameObject townPrefab;
    public GameObject fortPrefab;

    public LayerMask UIMask;
    Color normalColor;
    //private Collider coll;
    private Renderer rend;
    private Transform _selection;
    private int fingerID = -1;
    private Transform selectedTile;
    public bool menuOpen = false;

    public Canvas canvasBuilding;

    
    
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

        if (Input.GetKey(KeyCode.Escape)){
            if (selectedTile != null){
                canvasBuilding.gameObject.SetActive(false);
                menuOpen = false;
            }
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
                    if (Input.GetMouseButtonDown(0)&& selection.CompareTag("Selectable") && selection.GetComponent<Resources>().building == Resources.Building.Empty){
                        /*Instantiate(cityPrefab, selection.position + Vector3.up/2f, cityPrefab.transform.rotation);
                        selection.GetComponent<Resources>().building = true;*/

                        selectedTile = hitInfo.transform;
                        if (canvasBuilding.gameObject.activeInHierarchy){
                            canvasBuilding.gameObject.SetActive(false);
                            menuOpen = false;
                            selectedTile.GetComponent<Resources>().selected = false;
                            selectedTile.GetComponent<Renderer>().material.color = normalColor;
                        } 
                        else {
                            canvasBuilding.gameObject.SetActive(true);
                            //Debug.Log("Activar");
                            menuOpen = true;
                            selectedTile.GetComponent<Resources>().selected = true;
                            selectedTile.GetComponent<Renderer>().material.color = highlightColor;
                        }
                    }
                }
                if (selection == null || (selection.GetComponent<Resources>() != null && !selection.GetComponent<Resources>().selected)){
                    _selection = selection;
                }
                
            }
        }
    }

    public void instantiateAcademy(){
        if (selectedTile.GetComponent<Resources>().building == Resources.Building.Empty){
            Instantiate(academyPrefab, selectedTile.position + Vector3.up/1.3f, academyPrefab.transform.rotation);
            selectedTile.GetComponent<Resources>().building = Resources.Building.Academy;
            Close();
        } 
    }

    public void instantiateCity(){
        if (selectedTile.GetComponent<Resources>().building == Resources.Building.Empty){
            Instantiate(cityPrefab, selectedTile.position + Vector3.up/2f, cityPrefab.transform.rotation);
            selectedTile.GetComponent<Resources>().building = Resources.Building.City;
            Close();
        }
    }

    public void instantiateTown(){
        if (selectedTile.GetComponent<Resources>().building == Resources.Building.Empty)
        {
            Instantiate(townPrefab, selectedTile.position + Vector3.up / 2f, townPrefab.transform.rotation);
            selectedTile.GetComponent<Resources>().building = Resources.Building.Town;
            Close();
        }
    }

    public void instantiateFort(){
        if (selectedTile.GetComponent<Resources>().building == Resources.Building.Empty){
            Instantiate(fortPrefab, selectedTile.position + Vector3.up/2f, fortPrefab.transform.rotation);
            selectedTile.GetComponent<Resources>().building = Resources.Building.Fort;
            Close();
        }
    }

    void Close()
    {
        menuOpen = false;
        canvasBuilding.gameObject.SetActive(false);
        selectedTile.GetComponent<Resources>().selected = false;
        selectedTile.GetComponent<Renderer>().material.color = normalColor;
    }
}
