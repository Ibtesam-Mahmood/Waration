using System;
using UnityEngine;

namespace Application
{
    public enum Player
    {
        GREEN,
        BLUE,
        NONE
    }

    public enum GameMode
    {
        NONE,
        MOVE,
        OPPONENT
    }

    public static class Global
    {

        public static readonly Vector3 BOARD_POS = new Vector3(-3, 0, 0);

    }


}
