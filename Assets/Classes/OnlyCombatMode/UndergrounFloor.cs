using System;
using UnityEngine;

namespace Map
{
   
    public class UndergroundFloor : Floor//подземный этаж
    {
        public UndergroundFloor(int number, Stage stage) : base(number, stage)
        {
            defaultIsWalkable = false;
            extraheight = 0.05f;
        }
        public override void Init()
        {
            GenerateNodes();
            stage.addNewElement(new UndergroundDirt(rct));
               
        }
    }
}
