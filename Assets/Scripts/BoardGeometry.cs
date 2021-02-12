using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGeometry
{

    ///The number of tiles in a given column or row
    public static readonly int TILE_SPAN = 16;

    ///The number of tiles in a given column or row or a zone
    public static readonly int ZONE_TILE_SPAN = 4;

    ///The board position
    private Transform boardPosition;

    public Vector3 position { get; private set; }

    public float tile_height { get; private set; }

    public BoardGeometry(GameObject board){
           
        //Extract infromation from baord object
        Collider2D boardCollider = board.GetComponent<Collider2D>();
        Transform boardTransform = board.GetComponent<Transform>();

        this.position = boardTransform.position;
        this.tile_height = boardCollider.bounds.size.x / 16;

        this.boardPosition = boardTransform;
    }


    ///Returns the position of the vector relative to the board
    public Vector3 getRelativePos(Vector3 pos)
    {
        
        float boardSize = size.x;
        pos = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(pos);
        pos.y *= -1;
        return pos + boardPosition.position + new Vector3(boardSize / 2, boardSize / 2, 0);
    }

    /// Converts a board relative position to an absolute position
    public Vector3 getAbsolutePos(Vector3 pos)
    {
        float boardSize = size.x;
        pos = pos - boardPosition.position - new Vector3(boardSize / 2, boardSize / 2, 0);
        pos.y *= -1;
        return pos;
    }

    ///Return the tile the the position is pointing to. 
    ///Must be relative position to the board
    public Vector2 getTile(Vector3 pos)
    {
        float x_val = pos.x;
        float y_pos = pos.y;

        int x_tile = Mathf.CeilToInt(x_val / tile_height);
        int y_tile = Mathf.CeilToInt(y_pos / tile_height);


        x_tile = Mathf.Clamp(x_tile, 1, TILE_SPAN);
        y_tile = Mathf.Clamp(y_tile, 1, TILE_SPAN);

        return new Vector2(x_tile, y_tile);
    }

    ///Retreives a tile from a absolute position
    public Vector2 getTileFromAbsolute(Vector3 pos)
    {
        return getTile(getRelativePos(pos));
    }

    ///Returns the realtive position of a tile from its mapping
    public Vector3 getTileRelativePosition(Vector2 tiles)
    {
        Vector3 tilePosition = getTileCenter(tiles);
        tilePosition.x += tile_height / 2;
        tilePosition.y += tile_height / 2;
        return tilePosition;
    }

    public Vector3 getTilePosition(Vector2 tiles)
    {
        return getAbsolutePos(getTileRelativePosition(tiles));
    }

    ///Returns the realtive position of a tile center from its mapping
    public Vector3 getTileCenter(Vector2 tiles)
    {
        return new Vector3((tiles.x - 1) * tile_height, (tiles.y - 1) * tile_height, 0);
    }

    /// Returns the tile position realtive to an absolute position
    /// Position must be within bounds
    public Vector3 getTilePositionFromAbsolute(Vector3 pos)
    {
        return getTilePosition(getTileFromAbsolute(pos));
    }

    //Gettser for baord size
    public Vector3 size
    {
        get
        {
            return new Vector3(tile_height * TILE_SPAN, tile_height * TILE_SPAN, 0);
        }
    }

}
