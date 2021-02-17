using System;
using System.Collections;
using System.Collections.Generic;
using Application;
using UnityEngine;

public class ZoneBoard : MonoBehaviour
{

    //Map of zone coordinates to gameobjects
    public Dictionary<Vector2Int, Zone> zones = new Dictionary<Vector2Int, Zone>();

    /// Refrence any zones within the children
    private void Awake()
    {
        int zoneNum = 0;
        for(int i = 1; i <= 4; i++)
        {
            for(int j = 1; j <= 4; j++)
            {
                Zone zone = this.transform.GetChild(zoneNum).gameObject.GetComponent<Zone>();
                zones[new Vector2Int(j, i)] = zone;
                //Debug.Log(new Vector2Int(i, j) + " | " + zoneNum);
                zoneNum++;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Player getTileOwner(Vector2Int tile)
    {
        return getOwner(determineZone(tile));
    }

    // Determines the owner of the zone
    public Player getOwner(Vector2Int zone)
    {
        try
        {
            return zones[zone].owner;
        }
        catch (Exception e)
        {
            return Player.NONE;
        }
    }

    public Vector2Int determineZone(Vector2Int tile)
    {
        return new Vector2Int(Mathf.CeilToInt( tile.x / 4f), Mathf.CeilToInt(tile.y / 4f));
    }

    /// Determines and sets the zones based on the pieces map
    /// Detemines if the player can win. outputs null if no win is detected
    public Player calculateZones(Dictionary<Vector2Int, GameObject> pieces)
    {

        //Player.Blue points count as positive points. 
        //Player.Green points count as negative points
        Dictionary<Zone, int> pointsPerZone = new Dictionary<Zone, int>();

        //tally points
        foreach(KeyValuePair<Vector2Int, GameObject> piece in pieces)
        {
            Piece p = piece.Value.GetComponent<Piece>();
            Zone z = zones[determineZone(piece.Key)];
            //Debug.Log("Piece: " + piece.Key + " | Zone: " + determineZone(piece.Key));

            int power = (p.player == Player.GREEN ? -1 : 1) * p.power;

            if (!pointsPerZone.ContainsKey(z))
            {
                //add zone to dict
                pointsPerZone[z] = 0;
            }
            pointsPerZone[z] += power;
        }

        //set points
        foreach(KeyValuePair<Vector2Int, Zone> z in zones)
        {
            if (pointsPerZone.ContainsKey(z.Value))
            {
                z.Value.setZone(pointsPerZone[z.Value]);
            }
            else
            {
                z.Value.setZone(0);
            }
        }


        return determineWin();
    }

    //TODO: determines if there is a win and which player
    public Player determineWin()
    {
        return Player.NONE;
    }

}
