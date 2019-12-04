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

        if (Input.GetKey(KeyCode.Escape)){
            if (selectedTile != null){
                selectedTile.GetChild(4).gameObject.SetActive(false);
                menuOpen = false;
            }
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
                    if (Input.GetMouseButtonDown(0)&& !selection.GetComponent<Resources>().building && !menuOpen){
                        /*Instantiate(cityPrefab, selection.position + Vector3.up/2f, cityPrefab.transform.rotation);
                        selection.GetComponent<Resources>().building = true;*/

                        selectedTile = hitInfo.transform;
                        if (selection.GetChild(4).gameObject.activeInHierarchy){
                            selection.GetChild(4).gameObject.SetActive(false);
                            menuOpen = false;
                        } else {
                            selection.GetChild(4).gameObject.SetActive(true);
                            Debug.Log("Activar");
                            menuOpen = true;
                        }
                    }
                }
                _selection = selection;
            }
        }
    }

    

    public void instantiateAcademy(){
        if (selectedTile.GetComponent<Resources>().building == false){
            Instantiate(academyPrefab, selectedTile.position + Vector3.up/1.3f, academyPrefab.transform.rotation);
<<<<<<< Updated upstream
            selectedTile.GetComponent<Resources>().building = true;
=======
            selectedTile.GetComponent<Resources>().building = Resources.Building.Academy;
            selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);
>>>>>>> Stashed changes
            Close();
        } 
    }

    public void instantiateCity(){
        if (selectedTile.GetComponent<Resources>().building == false){
            Instantiate(cityPrefab, selectedTile.position + Vector3.up/2f, cityPrefab.transform.rotation);
<<<<<<< Updated upstream
            selectedTile.GetComponent<Resources>().building = true;
=======
            selectedTile.GetComponent<Resources>().building = Resources.Building.City;
            selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);
>>>>>>> Stashed changes
            Close();
        }
    }

    public void instantiateTown(){
        if (selectedTile.GetComponent<Resources>().building == false)
        {
            Instantiate(townPrefab, selectedTile.position + Vector3.up / 2f, townPrefab.transform.rotation);
<<<<<<< Updated upstream
            selectedTile.GetComponent<Resources>().building = true;
=======
            selectedTile.GetComponent<Resources>().building = Resources.Building.Town;
            selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);
>>>>>>> Stashed changes
            Close();
        }
    }

    public void instantiateFort(){
        if (selectedTile.GetComponent<Resources>().building == false){
            Instantiate(fortPrefab, selectedTile.position + Vector3.up/2f, fortPrefab.transform.rotation);
<<<<<<< Updated upstream
            selectedTile.GetComponent<Resources>().building = true;
=======
            selectedTile.GetComponent<Resources>().building = Resources.Building.Fort;
            selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);
>>>>>>> Stashed changes
            Close();
        }
    }

    void Close()
    {
        menuOpen = false;
        selectedTile.GetChild(4).gameObject.SetActive(false);
    }
}
