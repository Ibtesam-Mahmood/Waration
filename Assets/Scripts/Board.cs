using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    //Highlight game object
    public GameObject highlight;

    //Select game object
    public GameObject select;

    //Geometry of the board
    public BoardGeometry geometry { get; private set; }

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
        highlight = Instantiate(highlight, geometry.getAbsolutePos(new Vector3(0, 0, 1)), Quaternion.identity);
        select = Instantiate(select, geometry.getAbsolutePos(new Vector3(0, 0, 1)), Quaternion.identity);

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
        highlight.transform.position = tilePosition;
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
        Vector2 tile = geometry.getTileFromAbsolute(Input.mousePosition);
        selectTile(tile);
    }

    //enables the select. unselects if the type is retapped
    void selectTile(Vector2 tile)
    {
        if(tile == GameStateManager.instance.selectedTile)
        {
            unselectTile();
        }
        else
        {
            //select tile
            Vector3 tilePosition = geometry.getTilePosition(tile);

            //Move select
            select.transform.position = tilePosition;
            Debug.Log(tilePosition);
            select.SetActive(true);

            GameStateManager.instance.selectTile(tile);
        }
    }

    //Disables the select
    void unselectTile() 
    {
        select.SetActive(false);
        GameStateManager.instance.unselectTile();
    }
}
