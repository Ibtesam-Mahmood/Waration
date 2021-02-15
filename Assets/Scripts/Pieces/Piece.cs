﻿using System.Collections;
using System.Collections.Generic;
using Application;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{

    /// The power of the peice
    public int power { get; protected set; }

    public Player player;

    public abstract List<Vector2> getMovement();

    public abstract Piece stack(Piece peice);

    public abstract bool isStackable();
}
