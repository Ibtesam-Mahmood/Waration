using System.Collections;
using System.Collections.Generic;
using Application;
using UnityEngine;

public class Board : MonoBehaviour
{
    //Highlight game object
    public GameObject highlightPrefab;

    //Select game object
    public GameObject selectPrefab;

    //Kill highlight game object
    public GameObject killHighlightPrefab;

    // ~~~~~ STATE ~~~~~~~~~~

    //Player cursor
    private GameObject highlight;
    private GameObject select;

    //Geometry of the board
    public BoardGeometry geometry { get; private set; }

    /// Contains the highlights if the GameMode == MOVE
    private Dictionary<Vector2Int, GameObject> highlights = new Dictionary<Vector2Int, GameObject>();

    //Creates the boardGeomety on the GameStateManager
    void Awake()
    {   
        //Instaniate board geomotry
        geometry = new BoardGeometry(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //create highlight and select
        highlight = Instantiate(highlightPrefab, Vector3.zero, Quaternion.identity);
        select = Instantiate(selectPrefab, Vector3.zero, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///Instantiates a highlight on the board
    void OnMouseEnter()
    {
        //Make highlight visible
        highlight.SetActive(true);

    }

    ///Called to update highlights
    void OnMouseOver()
    {
        Vector3 tilePosition = geometry.getTilePositionFromAbsolute(Input.mousePosition);

        //Move highlight
        highlight.transform.position = tilePosition - new Vector3(0, 0, 2);
    }

    //Remove the highlight
    void OnMouseExit()
    {
        //Make highlight invisible
        highlight.SetActive(false);
    }

    void OnMouseDown()
    {
        //Selects a tile
        if(GameStateManager.instance.winner == Player.NONE)
        {
            Vector2Int tile = geometry.getTileFromAbsolute(Input.mousePosition);
            selectTile(tile);
        }
        
    }

    public void setMoveHighlights(List<Vector2Int> moves, List<Vector2Int> kills, List<Vector2Int> stacks)
    {

        //Set move highlights
        foreach(Vector2Int move in moves)
        {
            Vector3 highlightPos = geometry.getTilePosition(move) - new Vector3(0, 0, 2);
            GameObject moveHighlight = Instantiate(selectPrefab, highlightPos, Quaternion.identity);
            moveHighlight.SetActive(true);
            highlights.Add(move, moveHighlight);
        }

        //Set kill highlights
        foreach (Vector2Int kill in kills)
        {
            Vector3 highlightPos = geometry.getTilePosition(kill) - new Vector3(0, 0, 2);
            GameObject killHighlightObject = Instantiate(killHighlightPrefab, highlightPos, Quaternion.identity);
            killHighlightObject.SetActive(true);
            highlights.Add(kill, killHighlightObject);
        }

        //Set stack highlights
        foreach (Vector2Int stack in stacks)
        {
            Vector3 highlightPos = geometry.getTilePosition(stack) - new Vector3(0, 0, 2);
            GameObject stackHighlight = Instantiate(highlightPrefab, highlightPos, Quaternion.identity);
            stackHighlight.SetActive(true);
            highlights.Add(stack, stackHighlight);
        }

    }

    //enables the select. unselects if the type is retapped
    public void selectTile(Vector2Int tile)
    {

        if (tile == GameStateManager.instance.selectedTile)
        {
            unselectTile();
        }
        //Checks if a highlighted tile is selected
        //and the player is trying to move
        else if (GameStateManager.instance.mode == GameMode.MOVE && highlights.ContainsKey(tile))
        {
            //Send action command for the piece
            GameStateManager.instance.action(tile);
            unselectTile();

        }
        else
        {
            if(GameStateManager.instance.mode == GameMode.MOVE)
            {
                unselectTile();
            }

            //select tile
            Vector3 tilePosition = geometry.getTilePosition(tile);

            //Move select
            select.transform.position = tilePosition - new Vector3(0, 0, 2);
            select.SetActive(true);

            GameStateManager.instance.selectTile(tile);
        }
    }

    //Disables the select
    public void unselectTile() 
    {

        //Remove all highlights
        foreach (KeyValuePair<Vector2Int, GameObject> entry in highlights)
        {
            Destroy(entry.Value);
        }
        highlights.Clear();

        select.SetActive(false);
        GameStateManager.instance.unselectTile();
    }
}
