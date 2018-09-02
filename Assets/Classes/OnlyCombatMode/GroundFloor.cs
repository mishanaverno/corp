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
            GenerateCells();
            Area area = new Area(rct);
            area.moveNodesFromMapElementToThis(this.stage);
            this.stage.childElements.Add(area);
        }
        
    }
}
