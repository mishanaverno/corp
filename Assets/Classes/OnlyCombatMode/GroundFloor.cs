using System;
using UnityEngine;

namespace Map
{

    public class GroundFloor : Floor//первый этаж
    {
        public GroundFloor(int number, Stage stage) : base(number, stage)
        {
            defaultIsWalkable = true;
        }
        public override void Init()
        {
            GenerateNodes();
            stage.addNewElement(new Area(rct, number));
            LinkNodes();
           
        }
        
    }
}
