using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
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
        List<Vector2Int> locations = new List<Vector2Int>();
        for(int i = tile.x - 1; i <= tile.x + 1; i++)
        {
            for (int j = tile.y - 1; j <= tile.y + 1; j++)
            {
                Vector2Int tilePos = new Vector2Int(i, j);
                if(tilePos != tile)
                {
                    locations.Add(new Vector2Int(i, j));
                }
            }
        }


        return locations;

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
