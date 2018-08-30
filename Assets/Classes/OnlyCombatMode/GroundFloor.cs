using System;
using UnityEngine;

namespace Map
{

    public class GroundFloor : Floor
    {
        public GroundFloor(int number, Stage stage) : base(number, stage)
        {
            defaultIsWalkable = true;
        }
        public override void Init()
        {
            GenerateNodes();
            LinkNodes();
        }
    }
}
