using System.Collections;
using System.Collections.Generic;
using Application;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{   

    //Game state single ton
    public static GameStateManager instance;

    //~~~~~~~~~~~ State ~~~~~~~~~~~~~~~~~

    /// The selected tile on the board
    public Vector2 selectedTile = Vector2.zero;

    /// All the pieces in the game
    public Dictionary<Vector2, GameObject> pieces = new Dictionary<Vector2, GameObject>();

    //~~~~~~~~~~~ GameObjects ~~~~~~~~~~~~~~~~

    ///The board to be rendered
    public GameObject gameBoard;

    //pieces and piece types
    public GameObject pawnPrefab;
    public GameObject veternPrefab;
    public GameObject knightPrefab;
    public GameObject heroPrefab;
    public GameObject magePrefab;

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
        spawnPiece(1, Player.BLUE, new Vector2(1, 1));
        spawnPiece(2, Player.BLUE, new Vector2(1, 2));
        spawnPiece(3, Player.BLUE, new Vector2(1, 3));
        spawnPiece(4, Player.BLUE, new Vector2(1, 4));
        spawnPiece(5, Player.BLUE, new Vector2(1, 5));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //~~~~~~~~~~~~ State Functions ~~~~~~~~~~~~~~~~

    /// Spawn a piece in at a given tile
    public void spawnPiece(int level, Player player, Vector2 tile)
    {

        //Ensure no pieces are on the current tile
        if (pieces.ContainsKey(tile)) return;

        GameObject newPiece;

        Vector3 absoluteLocation = geometry.getTilePosition(tile);

        //Determione piece
        switch (level)
        {

            case 2:
                newPiece = veternPrefab;
                break;

            case 3:
                newPiece = knightPrefab;
                break;

            case 4:
                newPiece = heroPrefab;
                break;

            case 5:
                newPiece = magePrefab;
                break;

            default:
                newPiece = pawnPrefab;
                break;

        }

        //Create piece
        newPiece = Instantiate(newPiece, absoluteLocation, Quaternion.identity);
        Piece p = newPiece.GetComponent<Piece>();
        p.player = player;

        //Add piece to list
        pieces.Add(tile, newPiece);
    }

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
