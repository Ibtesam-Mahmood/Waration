using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Piece
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override List<Vector2> getMovement()
    {
        //List<Vector2> locations = new List<Vector2>();
        return null;
        //Vector2 move = tile;
        //move.x++;
        //locations.Add(tile);

        //Vector2 move = tile;
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
