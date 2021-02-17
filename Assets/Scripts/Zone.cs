using UnityEngine;
using System.Collections;
using Application;

public class Zone : MonoBehaviour
{

    public Player owner = Player.NONE;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Determines the player based on the zone
    // < 0, Player.blue
    // > 0, Player.Green
    public void setZone(int score)
    {

        if(score > 0)
        {
            this.owner = Player.BLUE;
        }
        else if(score < 0)
        {
            this.owner = Player.GREEN;
        }
        else
        {
            this.owner = Player.NONE;
        }

        updateUI();
    }

    //Updates the zone UI to reflect the owner
    private void updateUI()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        switch (owner)
        {
            case Player.BLUE:
                renderer.enabled = true;
                renderer.color = new Color(0, 0, 1f, 0.5f);
                break;
            case Player.GREEN:
                renderer.enabled = true;
                renderer.color = new Color(0, 1f, 0, 0.5f);
                break;
            default:
                renderer.enabled = false;
                break;
        }
    }
}
