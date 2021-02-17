using System.Collections;
using System.Collections.Generic;
using Application;
using UnityEngine;
using UnityEngine.UI;

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

    //~~~~~~~~~~~~ Local State ~~~~~~~~~~~~~~

    /// The Game board
    Board gameBoard;

    ZoneBoard zoneBoard;

    //~~~~~~~~~~~ GameObjects ~~~~~~~~~~~~~~~~

    /// The spawn button
    public GameObject spawnButton;

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
        //Set spawn button to inactive
        spawnButton.GetComponent<Button>().interactable = false;

        //Retreive Board, instantiates the board geometry
        gameBoard = GameObject.FindWithTag("Board").GetComponent<Board>();

        //Retreive zoneBoard
        zoneBoard = GameObject.FindWithTag("ZoneBoard").GetComponent<ZoneBoard>();

        spawnPiece(1, Player.GREEN, new Vector2Int(5, 5));
        spawnPiece(1, Player.BLUE, new Vector2Int(4, 4));


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //~~~~~~~~~~~~ State Functions ~~~~~~~~~~~~~~~~

    /// Spawn a piece in at a given tile
    public void spawnPiece(int power, Player player, Vector2Int tile)
    {

        //Ensure no pieces are on the current tile
        if (pieces.ContainsKey(tile)) return;

        GameObject newPiece;

        Vector3 absoluteLocation = geometry.getTilePosition(tile);

        //Determione piece
        switch (power)
        {

            case 2:
                newPiece = Instantiate(veternPrefab, absoluteLocation - new Vector3(0, 0, 1), Quaternion.identity);
                break;

            case 3:
                newPiece = Instantiate(knightPrefab, absoluteLocation - new Vector3(0, 0, 1), Quaternion.identity);
                break;

            case 4:
                newPiece = Instantiate(heroPrefab, absoluteLocation - new Vector3(0, 0, 1), Quaternion.identity);
                break;

            case 5:
                newPiece = Instantiate(magePrefab, absoluteLocation - new Vector3(0, 0, 1), Quaternion.identity);
                break;

            default:
                newPiece = Instantiate(pawnPrefab, absoluteLocation - new Vector3(0, 0, 1), Quaternion.identity);
                break;

        }

        //Create piece
        newPiece.GetComponent<SpriteRenderer>().color = player == Player.BLUE ? Color.cyan : Color.green;
        Piece p = newPiece.GetComponent<Piece>();
        p.player = player;

        //Add piece to list
        pieces.Add(tile, newPiece);

        //calculate zones
        zoneBoard.calculateZones(pieces);
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

            Piece currentPiece = piece.GetComponent<Piece>();

            //cannot select opponent pieces
            if (currentPiece.player != player)
            {
                gameBoard.unselectTile();
                return;
            }

            this.mode = GameMode.MOVE;

            List<Vector2Int> possibleMoves = currentPiece.getMovement(tile);

            //prune
            possibleMoves = geometry.pruneTiles(possibleMoves);

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
                        if (pieces[move].GetComponent<Piece>().power < 5 && currentPiece.power < 5)
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
            gameBoard.setMoveHighlights(moves, kills, stacks);
        }

        //Set spawn button to active
        //Debug.Log(zoneBoard.getTileOwner(tile));
        if (zoneBoard.getTileOwner(tile) == player) {
            spawnButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            spawnButton.GetComponent<Button>().interactable = false;
        }
    }

    //Unselects a tile
    public void unselectTile()
    {
        this.mode = GameMode.NONE;
        selectedTile = Vector2Int.zero;

        //Set spawn button to inactive
        spawnButton.GetComponent<Button>().interactable = false;
    }

    // ~~~~~~~~~~~~~ Actions ~~~~~~~~~~~~~~~~

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
                stackAction(selectedTile, tile);
            }
            else
            {
                //Opponent owned, kill
                killAction(selectedTile, tile);
            }
        }
        else
        {
            moveAction(selectedTile, tile);
        }

        //calculate zones
        zoneBoard.calculateZones(pieces);
    }      

    //moves the piece
    public void moveAction(Vector2Int start, Vector2Int dest)
    {
        GameObject piece = pieces[start];
        pieces.Remove(start);
        piece.transform.position = geometry.getTilePosition(dest) - new Vector3(0, 0, 1);
        pieces[dest] = piece;
    }

    public void killAction(Vector2Int start, Vector2Int dest)
    {
        GameObject piece = pieces[start];
        GameObject opponentPiece = pieces[dest];
        Destroy(opponentPiece);
        pieces.Remove(start);
        piece.transform.position = geometry.getTilePosition(dest) - new Vector3(0, 0, 1);
        pieces[dest] = piece;
    }

    public void stackAction(Vector2Int start, Vector2Int dest)
    {
        GameObject piece = pieces[start];
        GameObject otherPiece = pieces[dest];

        Piece p = piece.GetComponent<Piece>();

        //determine player
        Player piecePlayer = p.player;

        //determine combinded power
        int power = p.power + otherPiece.GetComponent<Piece>().power;
        power = Mathf.Clamp(power, 1, 5);

        Destroy(piece);
        Destroy(otherPiece);

        pieces.Remove(start);
        pieces.Remove(dest);

        spawnPiece(power, piecePlayer, dest);
    }

    // ~~~~~~~~~~~~~ Events ~~~~~~~~~~~~~~~~

    /// Run when the span button is pressed
    public void OnSpawnPress()
    {
        //TODO: restrict turns
        spawnPiece(1, player, selectedTile);
        gameBoard.unselectTile();
    }

    //~~~~~~~~~~~~~ Getters ~~~~~~~~~~~~~~~~~

    public BoardGeometry geometry { get {
            return gameBoard.geometry;  
    } }
}
