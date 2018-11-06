using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class SidewalkParkingOuterCorner : MapElement //внешний угол парковочного кармана
    {
        public string direction; 
        public SidewalkParkingOuterCorner(RCT rct, int floor, string direction) : base(rct, floor)
        {
            this.direction = direction;
        }
        public override void HookAddToChildElements()
        {
            surface = "Road";
            NodeLayer layer = new NodeLayer(getPrefabNuber(), "Additions/Sidewalk", "OuterCorner");
            layer.direction = direction;
            AddLayer(layer);
            AddLayer(new NodeLayer(0, "Main", "ControllQuad"));
        }
        public override List<NodeLayer> HookProcessLayers(List<NodeLayer> layers)
        {
            return new List<NodeLayer>(this.layers);
        }
    }
}
