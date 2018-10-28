using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class SidewalkParkingOuterCorner : MapElement //внешний угол парковочного кармана
    {
        public string direction; 
        public SidewalkParkingOuterCorner(RCT rct, string direction) : base(rct)
        {
            this.direction = direction;
        }
        public override void OnAddToChildElements()
        {
            surface = "Road";
            NodeLayer layer = new NodeLayer(getPrefabNuber(), "Additions/Sidewalk", "OuterCorner");
            layer.direction = direction;
            AddLayer(layer);
            AddLayer(new NodeLayer(0, "Main", "ControllQuad"));
        }
        public override List<NodeLayer> BeforeProcessLayers(List<NodeLayer> layers)
        {
            return new List<NodeLayer>(this.layers);
        }
    }
}
