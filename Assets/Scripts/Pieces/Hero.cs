using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Piece
{

    public Hero()
    {
        this.power = 4;
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

        for (int i = 0; i <= 16; i++)
        {
            Vector2Int tilePos = new Vector2Int(i, tile.y);
            if (tilePos != tile)
            {
                locations.Add(tilePos);
            }
        }

        for (int i = 0; i <= 16; i++)
        {
            Vector2Int tilePos = new Vector2Int(tile.x, i);
            if (tilePos != tile)
            {
                locations.Add(tilePos);
            }
        }

        locations.Add(new Vector2Int(tile.x - 1, tile.y - 1));
        locations.Add(new Vector2Int(tile.x - 1, tile.y + 1));
        locations.Add(new Vector2Int(tile.x + 1, tile.y - 1));
        locations.Add(new Vector2Int(tile.x + 1, tile.y + 1));


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
