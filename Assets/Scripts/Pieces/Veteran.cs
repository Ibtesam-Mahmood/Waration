using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veteran : Piece
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override List<Vector2Int> getMovement(Vector2Int tile)
    {
        //List<Vector2Int> locations = new List<Vector2Int>();
        return null;
        //Vector2Int move = tile;
        //move.x++;
        //locations.Add(tile);

        //Vector2Int move = tile;
        //tile.x++;
        //locations.Add(tile);
    }

    public override Piece stack(Piece peice)
    {
        throw new System.NotImplementedException();
    }

    public override bool isStackable()
    {
        throw new System.NotImplementedException();
    }
}
