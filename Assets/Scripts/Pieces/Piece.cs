using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{

    ///The tile position of the piece
    public Vector2 tile;

    public abstract List<Vector2> getMovement();
}
