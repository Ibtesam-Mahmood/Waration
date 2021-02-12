using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{   

    //Game state single ton
    public static GameStateManager instance;

    //~~~~~~~~~~~ State ~~~~~~~~~~~~~~~~~
    public Vector2 selectedTile = Vector2.zero;

    //~~~~~~~~~~~ GameObjects ~~~~~~~~~~~~~~~~

    ///The board to be rendered
    public GameObject gameBoard;

    // Create instance on wake
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate Board, instantiates the board geometry
        gameBoard = Instantiate(gameBoard, Global.BOARD_POS, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //~~~~~~~~~~~~ State ~~~~~~~~~~~~~~~~

    //Selects a a tile, highlighting any pieces
    public void selectTile(Vector2 tile)
    {
        selectedTile = tile;
        //Select peice
    }

    //Unselects a tile
    public void unselectTile()
    {
        selectedTile = Vector2.zero;
    }

    //~~~~~~~~~~~~~ Getters ~~~~~~~~~~~~~~~~~

    public BoardGeometry geometry { get {
            return gameBoard.GetComponent<Board>().geometry;  
    } }
}
