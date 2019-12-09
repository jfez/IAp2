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
    public Canvas canvasCities;
    public Canvas canvasFortin;
    public Canvas canvasPueblo;
    public Canvas canvasAcademia;
    public Canvas canvasOpcionesConstruccion;

    private bool moving;
    private Transform startSquare;

    private Color whiteColor;
    private Color woodColor;

    private TurnManager turnManager;    
    
    private void Awake()
    {
        #if !UNITY_EDITOR
        fingerID = 0; 
        #endif
    }
    
    // Start is called before the first frame update
    void Start()
    {
        turnManager = GameObject.FindGameObjectWithTag("TurnManager").GetComponent<TurnManager>();
        whiteColor = Color.white;
        woodColor = new Color (0.56f, 0.37f, 0.09f, 1f);
        //coll = GetComponent<Collider>();
        //rend = GetComponent<Renderer>();
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {       
        if (!turnManager.playerTurn){
            return;
        }
        
        if (_selection != null){
            rend = _selection.GetComponent<Renderer>();
            rend.material.color = normalColor;
            _selection = null;

        }

        if (Input.GetKey(KeyCode.Escape)){
            if (selectedTile != null){
                canvasBuilding.gameObject.SetActive(false);
                canvasCities.gameObject.SetActive(false);
                canvasFortin.gameObject.SetActive(false);
                canvasPueblo.gameObject.SetActive(false);
                canvasAcademia.gameObject.SetActive(false);
                menuOpen = false;
                selectedTile.GetComponent<Resources>().selected = false;
                selectedTile.GetComponent<Renderer>().material.color = whiteColor;
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
                    if (Input.GetMouseButtonDown(0) && selection.CompareTag("Selectable") && selection.GetComponent<Resources>().building == Resources.Building.Empty && !selection.GetComponent<SquareUnit>().unit && !moving && !menuOpen){
                        /*Instantiate(cityPrefab, selection.position + Vector3.up/2f, cityPrefab.transform.rotation);
                        selection.GetComponent<Resources>().building = true;*/

                        selectedTile = hitInfo.transform;

                        if (!canvasBuilding.gameObject.activeInHierarchy && selectedTile.gameObject.GetComponent<SquareUnit>().nextToCityPlayer) {
                            canvasBuilding.gameObject.SetActive(true);
                            menuOpen = true;
                            selectedTile.GetComponent<Resources>().selected = true;
                            selectedTile.GetComponent<Renderer>().material.color = highlightColor;
                        }

                        else if (!canvasCities.gameObject.activeInHierarchy && !selectedTile.gameObject.GetComponent<SquareUnit>().nextToCityPlayer 
                        && !selectedTile.gameObject.GetComponent<SquareUnit>().nextToCityAI && !selectedTile.gameObject.GetComponent<SquareUnit>().nextToFortPlayer && !selectedTile.gameObject.GetComponent<SquareUnit>().nextToTownPlayer && !selectedTile.gameObject.GetComponent<SquareUnit>().nextToAcademyPlayer) {
                            canvasCities.gameObject.SetActive(true);
                            //Debug.Log("Activar");
                            menuOpen = true;
                            selectedTile.GetComponent<Resources>().selected = true;
                            selectedTile.GetComponent<Renderer>().material.color = highlightColor;
                        }
                    }

                    if ((Input.GetMouseButtonDown(0) && selection.CompareTag("Selectable") && !selection.GetComponent<SquareUnit>().unit && !moving && !menuOpen)
                    && (selection.GetComponent<SquareUnit>().nextToFortPlayer && selection.GetComponent<SquareUnit>().nextToTownPlayer
                    || selection.GetComponent<SquareUnit>().nextToFortPlayer && selection.GetComponent<SquareUnit>().nextToAcademyPlayer
                    || selection.GetComponent<SquareUnit>().nextToTownPlayer && selection.GetComponent<SquareUnit>().nextToAcademyPlayer) ){  
                        selectedTile = hitInfo.transform;

                        canvasOpcionesConstruccion.gameObject.SetActive(true);
                        menuOpen = true;
                        selectedTile.GetComponent<Resources>().selected = true;
                        selectedTile.GetComponent<Renderer>().material.color = highlightColor;

                        if (!selection.GetComponent<SquareUnit>().nextToFortPlayer){
                            canvasOpcionesConstruccion.transform.GetChild(0).gameObject.SetActive(false);
                        }

                        if (!selection.GetComponent<SquareUnit>().nextToAcademyPlayer){
                            canvasOpcionesConstruccion.transform.GetChild(2).gameObject.SetActive(false);
                        }

                        if (!selection.GetComponent<SquareUnit>().nextToTownPlayer){
                            canvasOpcionesConstruccion.transform.GetChild(1).gameObject.SetActive(false);
                        }
                    }

                    else if (!canvasFortin.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0) && selection.CompareTag("Selectable") && selection.GetComponent<SquareUnit>().nextToFortPlayer && !selection.GetComponent<SquareUnit>().unit && !moving && !menuOpen){
                        
                        selectedTile = hitInfo.transform;

                        canvasFortin.gameObject.SetActive(true);
                        menuOpen = true;
                        selectedTile.GetComponent<Resources>().selected = true;
                        selectedTile.GetComponent<Renderer>().material.color = highlightColor;
                    }

                    else if (!canvasPueblo.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0) && selection.CompareTag("Selectable") && selection.GetComponent<SquareUnit>().nextToTownPlayer && !selection.GetComponent<SquareUnit>().unit && !moving && !menuOpen){
                        
                        selectedTile = hitInfo.transform;

                        canvasPueblo.gameObject.SetActive(true);
                        menuOpen = true;
                        selectedTile.GetComponent<Resources>().selected = true;
                        selectedTile.GetComponent<Renderer>().material.color = highlightColor;
                    }

                    else if (!canvasAcademia.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0) && selection.CompareTag("Selectable") && selection.GetComponent<SquareUnit>().nextToAcademyPlayer && !selection.GetComponent<SquareUnit>().unit && !moving && !menuOpen){
                        
                        selectedTile = hitInfo.transform;

                        canvasAcademia.gameObject.SetActive(true);
                        menuOpen = true;
                        selectedTile.GetComponent<Resources>().selected = true;
                        selectedTile.GetComponent<Renderer>().material.color = highlightColor;
                    }

                    else if (!moving){
                        if (Input.GetMouseButtonDown(0) && selection.GetComponent<SquareUnit>() != null && selection.GetComponent<SquareUnit>().unit){
                            startSquare = selection;
                            moving = true;

                        }
                    }

                    else if (moving){
                        if (Input.GetMouseButtonDown(0) && selection.GetComponent<SquareUnit>() != null && !selection.GetComponent<SquareUnit>().unit){
                            startSquare.GetComponentInChildren<Unit>().Pathing(startSquare, selection.transform);

                            startSquare.GetComponentInChildren<Unit>().transform.parent = selection.transform;
                            selection.GetComponent<SquareUnit>().unit = true;
                            startSquare.GetComponent<SquareUnit>().unit = false;

                            
                            startSquare = null;
                            moving = false;
                            
                            
                        }

                    }
                }
                if (selection == null || (selection.GetComponent<Resources>() != null && !selection.GetComponent<Resources>().selected) || selection.CompareTag("SelectableBridge")){
                    _selection = selection;
                }
                
            }
        }
    }

    public void instantiateAcademy(){
        if (selectedTile.GetComponent<Resources>().building == Resources.Building.Empty){
            Instantiate(academyPrefab, selectedTile.position + Vector3.up/1.3f, academyPrefab.transform.rotation);
            selectedTile.GetComponent<Resources>().building = Resources.Building.Academy;
            selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);
            Close();
            UpdateNextToAcademyPlayer(selectedTile.gameObject);
        } 
    }

    public void instantiateCity(){
        if (selectedTile.GetComponent<Resources>().building == Resources.Building.Empty){
            Instantiate(cityPrefab, selectedTile.position + Vector3.up/2f, cityPrefab.transform.rotation);
            selectedTile.GetComponent<Resources>().building = Resources.Building.City;
            selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);
            Close();
            GetComponent<CreateGameGrid>().UpdateNextToCityPlayer(selectedTile.gameObject);
        }
    }

    public void instantiateTown(){
        if (selectedTile.GetComponent<Resources>().building == Resources.Building.Empty)
        {
            Instantiate(townPrefab, selectedTile.position + Vector3.up / 2f, townPrefab.transform.rotation);
            selectedTile.GetComponent<Resources>().building = Resources.Building.Town;
            selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);
            Close();
            UpdateNextToTownPlayer(selectedTile.gameObject);
        }
    }

    public void instantiateFort(){
        if (selectedTile.GetComponent<Resources>().building == Resources.Building.Empty){
            Instantiate(fortPrefab, selectedTile.position + Vector3.up/2f, fortPrefab.transform.rotation);
            selectedTile.GetComponent<Resources>().building = Resources.Building.Fort;
            selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);
            Close();
            UpdateNextToFortPlayer(selectedTile.gameObject);
        }
    }

    public void instantiateTank(){
        if (selectedTile.GetComponent<Resources>().building == Resources.Building.Empty){
            Instantiate(fortPrefab, selectedTile.position + Vector3.up/2f, fortPrefab.transform.rotation);
            selectedTile.GetComponent<Resources>().building = Resources.Building.Fort;
            selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);
            Close();
        }
    }

    public void Close()
    {
        if (selectedTile!= null){
            menuOpen = false;
            canvasBuilding.gameObject.SetActive(false);
            canvasCities.gameObject.SetActive(false);
            canvasFortin.gameObject.SetActive(false);
            canvasPueblo.gameObject.SetActive(false);
            canvasAcademia.gameObject.SetActive(false);
            selectedTile.GetComponent<Resources>().selected = false;
            selectedTile.GetComponent<Renderer>().material.color = whiteColor;

        }
        
    }

    void UpdateNextToFortPlayer(GameObject casillaFort){

        GameObject casillaCiudad = new GameObject();

        Collider[] hitColliders = Physics.OverlapSphere(casillaFort.transform.position, 1.5f);

        for (int i = 0; i < hitColliders.Length; i++){
            if (hitColliders[i].GetComponent<Resources>().building == Resources.Building.City){
                casillaCiudad = hitColliders[i].gameObject;
                break;
            }
        }


        foreach(GameObject casilla in GetComponent<CreateGameGrid>().casillasArray){
            if (Vector3.Distance(casilla.transform.position, casillaCiudad.transform.position) < 2.5 && casilla != casillaCiudad && !casilla.GetComponent<SquareUnit>().nextToCityPlayer
            && casilla.GetComponent<Resources>().building != Resources.Building.City){
                if(casilla.GetComponent<SquareUnit>() != null){
                    casilla.GetComponent<SquareUnit>().nextToFortPlayer = true;

                }
                
            }
        }
    }

    void UpdateNextToAcademyPlayer(GameObject casillaAcademy){

        GameObject casillaCiudad = new GameObject();

        Collider[] hitColliders = Physics.OverlapSphere(casillaAcademy.transform.position, 1.5f);

        for (int i = 0; i < hitColliders.Length; i++){
            if (hitColliders[i].GetComponent<Resources>().building == Resources.Building.City){
                casillaCiudad = hitColliders[i].gameObject;
                break;
            }
        }


        foreach(GameObject casilla in GetComponent<CreateGameGrid>().casillasArray){
            if (Vector3.Distance(casilla.transform.position, casillaCiudad.transform.position) < 2.5 && casilla != casillaCiudad && !casilla.GetComponent<SquareUnit>().nextToCityPlayer
            && casilla.GetComponent<Resources>().building != Resources.Building.City){
                if(casilla.GetComponent<SquareUnit>() != null){
                    casilla.GetComponent<SquareUnit>().nextToAcademyPlayer = true;

                }
                
            }
        }
    }

    void UpdateNextToTownPlayer(GameObject casillaTown){

        GameObject casillaCiudad = new GameObject();
        Collider[] hitColliders = Physics.OverlapSphere(casillaTown.transform.position, 1.5f);

        for (int i = 0; i < hitColliders.Length; i++){
            if (hitColliders[i].GetComponent<Resources>().building == Resources.Building.City){
                casillaCiudad = hitColliders[i].gameObject;
                break;
            }
        }

        foreach(GameObject casilla in GetComponent<CreateGameGrid>().casillasArray){
            if (Vector3.Distance(casilla.transform.position, casillaCiudad.transform.position) < 2.5 && casilla != casillaCiudad && !casilla.GetComponent<SquareUnit>().nextToCityPlayer
            && casilla.GetComponent<Resources>().building != Resources.Building.City){
                if(casilla.GetComponent<SquareUnit>() != null){
                    casilla.GetComponent<SquareUnit>().nextToTownPlayer = true;

                }
            }
        }
    }

    public void showFortCanvas(){
        canvasOpcionesConstruccion.gameObject.SetActive(false);
        canvasFortin.gameObject.SetActive(true);
    }

    public void showAcademyCanvas(){
        canvasOpcionesConstruccion.gameObject.SetActive(false);
        canvasAcademia.gameObject.SetActive(true);
    }

    public void showTownCanvas(){
        canvasOpcionesConstruccion.gameObject.SetActive(false);
        canvasPueblo.gameObject.SetActive(true);
    }
}
