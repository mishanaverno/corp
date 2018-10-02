using System;
using UnityEngine;

namespace Map
{

    public class AbovegroundFloor : Floor // надземный этаж
    {
        public AbovegroundFloor(int number, Stage stage) : base(number, stage)
        {
            defaultIsWalkable = false;

        }
    }
}
