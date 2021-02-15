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
    public Vector2Int selectedTile = Vector2Int.zero;

    /// All the pieces in the game
    public Dictionary<Vector2Int, GameObject> pieces = new Dictionary<Vector2Int, GameObject>();

    /// The current player
    public Player player = Player.BLUE;

    /// The mode of the game
    public GameMode mode { get; private set; }

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
        mode = GameMode.NONE;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate Board, instantiates the board geometry
        gameBoard = Instantiate(gameBoard, Global.BOARD_POS, Quaternion.identity);
        spawnPiece(1, Player.BLUE, new Vector2Int(5, 5));


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //~~~~~~~~~~~~ State Functions ~~~~~~~~~~~~~~~~

    /// Spawn a piece in at a given tile
    public void spawnPiece(int level, Player player, Vector2Int tile)
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
        newPiece = Instantiate(newPiece, absoluteLocation - new Vector3(0, 0, 1), Quaternion.identity);
        Piece p = newPiece.GetComponent<Piece>();
        p.player = player;

        //Add piece to list
        pieces.Add(tile, newPiece);
    }

    //Selects a a tile, highlighting any pieces
    public void selectTile(Vector2Int tile)
    {
        selectedTile = tile;

        this.mode = GameMode.NONE;

        if (pieces.ContainsKey(tile))
        {

            //Select peice
            GameObject piece = pieces[tile];

            this.mode = GameMode.MOVE;

            List<Vector2Int> possibleMoves = piece.GetComponent<Piece>().getMovement(tile);

            //Organize moves into 3 lists
            List<Vector2Int> moves = new List<Vector2Int>();
            List<Vector2Int> kills = new List<Vector2Int>();
            List<Vector2Int> stacks = new List<Vector2Int>();

            foreach (Vector2Int move in possibleMoves)
            {

                if (pieces.ContainsKey(move))
                {

                    //checks if the piece is player owned
                    //or opponent owned
                    Player peicePlayer = pieces[move].GetComponent<Piece>().player;

                    if(peicePlayer == player)
                    {
                        //Player owned
                        //TODO: Check requirements for stack
                        stacks.Add(move);
                    }
                    else
                    {
                        //Opponent owned
                        kills.Add(move);
                    }
                }
                else
                {
                    //Add to moves
                    moves.Add(move);
                }
            }

            //Set the move highlights
            gameBoard.GetComponent<Board>().setMoveHighlights(moves, kills, stacks);
        }
    }

    //Unselects a tile
    public void unselectTile()
    {
        this.mode = GameMode.NONE;
        selectedTile = Vector2Int.zero;
    }

    //Uses the selected piece and the action tile to move the piece.
    //Determines move type by destination piece occupation
    public void action(Vector2Int tile)
    {
        if (pieces.ContainsKey(tile))
        {
            //checks if the piece is player owned
            //or opponent owned
            Player peicePlayer = pieces[tile].GetComponent<Piece>().player;

            if (peicePlayer == player)
            {
                //Player owned, stack
                stack(selectedTile, tile);
            }
            else
            {
                //Opponent owned, kill
                kill(selectedTile, tile);
            }
        }
        else
        {
            move(selectedTile, tile);
        }
    }

    //moves the piece
    public void move(Vector2Int start, Vector2Int dest)
    {
        GameObject piece = pieces[start];
        pieces.Remove(start);
        piece.transform.position = geometry.getTilePosition(dest) - new Vector3(0, 0, 1);
        pieces[dest] = piece;
    }

    public void kill(Vector2Int start, Vector2Int dest)
    {

    }

    public void stack(Vector2Int start, Vector2Int dest)
    {

    }

    //~~~~~~~~~~~~~ Getters ~~~~~~~~~~~~~~~~~

    public BoardGeometry geometry { get {
            return gameBoard.GetComponent<Board>().geometry;  
    } }
}
