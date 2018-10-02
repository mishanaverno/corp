using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class SidewalkParkingInnerCorner : MapElement//внутренний угол парковочного кармана
    {
        public string direction;
        public SidewalkParkingInnerCorner(RCT rct, string direction) : base(rct)
        {
            this.direction = direction;
        }
        public override void OnAddToChildElements()
        {
            surface = "Ground";
        }
        public override List<NodeLayer> BeforeProcessLayers(List<NodeLayer> layers)
        {
            return new List<NodeLayer>();
        }
    }
}
