using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public Knight()
    {
        this.power = 3;
    }

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
        for (int i = tile.x - 1; i <= tile.x + 1; i++)
        {

            if(i == tile.x)
            {
                for(int j = 0; j <= 16; j++)
                {
                    Vector2Int tilePos = new Vector2Int(i, j);
                    if (tilePos != tile)
                    {
                        locations.Add(tilePos);
                    }
                }
            }

            else
            {
                for (int j = tile.y - 1; j <= tile.y + 1; j++)
                {

                    locations.Add(new Vector2Int(i, j));
                }
            }

        }

        locations.Add(new Vector2Int(tile.x - 2, tile.y));
        locations.Add(new Vector2Int(tile.x + 2, tile.y));


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
