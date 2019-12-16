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

    public GameObject explorerPrefab;
    public GameObject warriorPrefab;
    public GameObject rangerPrefab;
    public GameObject jornaleroPrefab;

    public LayerMask UIMask;
    Color normalColor;
    //private Collider coll;
    private Renderer rend;
    private Transform _selection;
    private int fingerID = -1;
    private Transform selectedTile;
    public bool menuOpen = false;

    [Tooltip("Oro")]
    public int costeCiudad = 30;
    [Tooltip("Piedra")]
    public int costeFuerte = 80;
    [Tooltip("Madera")]
    public int costeAcademia = 80;
    [Tooltip("Madera")]
    public int costePueblo = 80;
    [Tooltip("Comida")]
    public int costeGuerrero = 7;
    [Tooltip("Comida")]
    public int costeArquero = 5;
    [Tooltip("Comida")]
    public int costeTanque = 9;
    [Tooltip("Comida")]
    public int costeJornalero = 3;
    [Tooltip("Comida")]
    public int costeExplorador = 5;

    public Canvas canvasBuilding;
    public Canvas canvasCities;
    public Canvas canvasFortin;
    public Canvas canvasPueblo;
    public Canvas canvasAcademia;
    public Canvas canvasOpcionesConstruccion;
    public Canvas canvasPrecios;

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
                canvasOpcionesConstruccion.gameObject.SetActive(false);
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

                        canvasOpcionesConstruccion.transform.GetChild(7).gameObject.SetActive(true);
                        canvasOpcionesConstruccion.transform.GetChild(9).gameObject.SetActive(true);
                        canvasOpcionesConstruccion.transform.GetChild(8).gameObject.SetActive(true);

                        if (!selection.GetComponent<SquareUnit>().nextToFortPlayer){
                            canvasOpcionesConstruccion.transform.GetChild(7).gameObject.SetActive(false);
                        }

                        if (!selection.GetComponent<SquareUnit>().nextToAcademyPlayer){
                            canvasOpcionesConstruccion.transform.GetChild(9).gameObject.SetActive(false);
                        }

                        if (!selection.GetComponent<SquareUnit>().nextToTownPlayer){
                            canvasOpcionesConstruccion.transform.GetChild(8).gameObject.SetActive(false);
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
                        if (Input.GetMouseButtonDown(0) && selection.GetComponent<SquareUnit>() != null && selection.GetComponent<SquareUnit>().unit && (selection.GetComponentInChildren<Unit>().gameObject.tag == "Player" || selection.GetComponentInChildren<Unit>().gameObject.tag == "jornalero")  && selection.GetComponentInChildren<combatStats>().puedeMoverse){
                            startSquare = selection;
                            moving = true;

                        }
                    }

                    else if (moving && Vector3.Distance(startSquare.transform.position, selection.transform.position) <= startSquare.GetComponentInChildren<combatStats>().rango){
                        if (Input.GetMouseButtonDown(0) && selection.GetComponent<SquareUnit>() != null && !selection.GetComponent<SquareUnit>().unit && (selection.gameObject.tag == "SelectableBridge" || selection.GetComponent<Resources>().building == Resources.Building.Empty))
                        {
                            startSquare.GetComponentInChildren<combatStats>().puedeMoverse = false;
                            startSquare.GetComponentInChildren<Unit>().Pathing(startSquare, selection.transform);

                            startSquare.GetComponentInChildren<Unit>().transform.parent = selection.transform;
                            selection.GetComponent<SquareUnit>().unit = true;
                            startSquare.GetComponent<SquareUnit>().unit = false; 
                            
                            startSquare = null;
                            moving = false; 

                        } else if(Input.GetMouseButtonDown(0) && selection.GetComponent<SquareUnit>() != null && selection.GetComponent<SquareUnit>().unit && (selection.GetComponentInChildren<Unit>().gameObject.tag == "AI_Explorer" || selection.GetComponentInChildren<Unit>().gameObject.tag == "AI_Labourer" || selection.GetComponentInChildren<Unit>().gameObject.tag == "AI_Ranger" || selection.GetComponentInChildren<Unit>().gameObject.tag == "AI_Warrior" || selection.GetComponentInChildren<Unit>().gameObject.tag == "AI_Troop")){
                            startSquare.GetComponentInChildren<combatStats>().puedeMoverse = false;
                            startSquare.GetComponentInChildren<Unit>().Pathing(startSquare, selection.transform);

                            startSquare.GetComponentInChildren<combatStats>().combate(selection.GetComponentInChildren<combatStats>().gameObject);

                            if (startSquare.GetComponentInChildren<Unit>() != null){
                                startSquare.GetComponentInChildren<Unit>().transform.parent = selection.transform;
                            }
                            selection.GetComponent<SquareUnit>().unit = true;
                            startSquare.GetComponent<SquareUnit>().unit = false;
                            
                            startSquare = null;
                            moving = false; 

                        } else if (Input.GetMouseButtonDown(0) && selection.GetComponent<Resources>() != null && selection.GetComponent<Resources>().building == Resources.Building.EnemyCity){
                            startSquare.GetComponentInChildren<Unit>().Pathing(startSquare, selection.transform);
                            startSquare.GetComponentInChildren<combatStats>().atacarCiudad(GameObject.FindGameObjectWithTag("EdificioIA"));

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
        if (selectedTile.GetComponent<Resources>().building == Resources.Building.Empty && GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().wood >= costeAcademia){
            Instantiate(academyPrefab, selectedTile.position + Vector3.up/1.3f, academyPrefab.transform.rotation);
            selectedTile.GetComponent<Resources>().building = Resources.Building.Academy;
            selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().wood -= costeAcademia;
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().updateText();
            Close();
            UpdateNextToAcademyPlayer(selectedTile.gameObject);
        } 
    }

    public void instantiateCity(){
        if (selectedTile.GetComponent<Resources>().building == Resources.Building.Empty && GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().gold >= costeCiudad && selectedTile.GetComponentInChildren<desactivarNiebla>() == null){
            Instantiate(cityPrefab, selectedTile.position + Vector3.up/2f, cityPrefab.transform.rotation);
            selectedTile.GetComponent<Resources>().building = Resources.Building.City;
            selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().gold -=costeCiudad;
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().updateText();
            Close();
            GetComponent<CreateGameGrid>().UpdateNextToCityPlayer(selectedTile.gameObject);
        }
    }

    public void instantiateTown(){
        if (selectedTile.GetComponent<Resources>().building == Resources.Building.Empty && GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().wood >= costePueblo)
        {
            Instantiate(townPrefab, selectedTile.position + Vector3.up / 2f, townPrefab.transform.rotation);
            selectedTile.GetComponent<Resources>().building = Resources.Building.Town;
            selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().wood -= costePueblo;
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().updateText();
            Close();
            UpdateNextToTownPlayer(selectedTile.gameObject);
        }
    }

    public void instantiateFort(){
        if (selectedTile.GetComponent<Resources>().building == Resources.Building.Empty && GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().stone >= costeFuerte){
            Instantiate(fortPrefab, selectedTile.position + Vector3.up/2f, fortPrefab.transform.rotation);
            selectedTile.GetComponent<Resources>().building = Resources.Building.Fort;
            selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().stone -= costeFuerte;
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().updateText();
            Close();
            UpdateNextToFortPlayer(selectedTile.gameObject);
        }
    }

    public void instantiateTank(){
        if (!selectedTile.GetComponent<SquareUnit>().unit && GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().food >= costeTanque){
            Instantiate(fortPrefab, selectedTile.position + Vector3.up/2f, fortPrefab.transform.rotation);
            selectedTile.GetComponent<Resources>().building = Resources.Building.Fort;
            selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().food -= costeTanque;
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().updateText();
            Close();
        }
    }

    public void instantiateExplorer(){
        if (!selectedTile.GetComponent<SquareUnit>().unit && GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().food >= costeExplorador){
            GameObject unitInstanced = Instantiate(explorerPrefab, selectedTile.position + Vector3.up/2f, explorerPrefab.transform.rotation);
            
            /*selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);*/   
                   
            unitInstanced.transform.parent = selectedTile.transform;
            selectedTile.GetComponent<SquareUnit>().unit = true;
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().food -= costeExplorador;
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().updateText();
            Close();
        }
    }

    public void instantiateRanger(){
        if (!selectedTile.GetComponent<SquareUnit>().unit && GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().food >= costeArquero)
        {
            GameObject unitInstanced = Instantiate(rangerPrefab, selectedTile.position + Vector3.up / 2f, rangerPrefab.transform.rotation);

            /*selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);*/

            unitInstanced.transform.parent = selectedTile.transform;
            selectedTile.GetComponent<SquareUnit>().unit = true;
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().food -= costeArquero;
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().updateText();
            Close();
        }
    }

    public void instantiateWarrior()
    {
        if (!selectedTile.GetComponent<SquareUnit>().unit && GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().food >= costeGuerrero)
        {
            GameObject unitInstanced = Instantiate(warriorPrefab, selectedTile.position + Vector3.up / 2f, warriorPrefab.transform.rotation);

            /*selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);*/

            unitInstanced.transform.parent = selectedTile.transform;
            selectedTile.GetComponent<SquareUnit>().unit = true;
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().food -= costeGuerrero;
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().updateText();
            Close();
        }
    }

    public void instantiateJornalero()
    {
        if (!selectedTile.GetComponent<SquareUnit>().unit && GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().food >= costeJornalero)
        {
            GameObject unitInstanced = Instantiate(jornaleroPrefab, selectedTile.position + Vector3.up / 2f, jornaleroPrefab.transform.rotation);

            /*selectedTile.GetChild(0).gameObject.SetActive(false);
            selectedTile.GetChild(1).gameObject.SetActive(false);
            selectedTile.GetChild(2).gameObject.SetActive(false);
            selectedTile.GetChild(3).gameObject.SetActive(false);*/

            unitInstanced.transform.parent = selectedTile.transform;
            selectedTile.GetComponent<SquareUnit>().unit = true;
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().food -= costeJornalero;
            GameObject.FindGameObjectWithTag("TurnManager").GetComponent<PlayerResources>().updateText();
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
            canvasOpcionesConstruccion.gameObject.SetActive(false);
            selectedTile.GetComponent<Resources>().selected = false;
            selectedTile.GetComponent<Renderer>().material.color = whiteColor;

        }
        canvasPrecios.gameObject.SetActive(false);

    }

    void UpdateNextToFortPlayer(GameObject casillaFort){

        GameObject casillaCiudad = new GameObject();

        Collider[] hitColliders = Physics.OverlapSphere(casillaFort.transform.position, 2f);

        for (int i = 0; i < hitColliders.Length; i++){
            if (hitColliders[i].GetComponent<Resources>() != null && hitColliders[i].GetComponent<Resources>().building == Resources.Building.City){
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

        Collider[] hitColliders = Physics.OverlapSphere(casillaAcademy.transform.position, 2f);

        for (int i = 0; i < hitColliders.Length; i++){
            if (hitColliders[i].GetComponent<Resources>() != null && hitColliders[i].GetComponent<Resources>().building == Resources.Building.City){
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
        Collider[] hitColliders = Physics.OverlapSphere(casillaTown.transform.position, 2f);

        for (int i = 0; i < hitColliders.Length; i++){
            if (hitColliders[i].GetComponent<Resources>() != null && hitColliders[i].GetComponent<Resources>().building == Resources.Building.City){
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

    public void showPricesCanvas()
    {
        canvasPrecios.gameObject.SetActive(true);
    }
}
