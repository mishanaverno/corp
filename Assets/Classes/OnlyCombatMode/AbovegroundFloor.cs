using System;
using UnityEngine;

namespace Map
{

    public class AbovegroundFloor : Floor
    {
        public AbovegroundFloor(int number, Stage stage) : base(number, stage)
        {
            defaultIsWalkable = false;

        }
    }
}
