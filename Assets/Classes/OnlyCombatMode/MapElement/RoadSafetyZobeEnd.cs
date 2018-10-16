using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RoadSafetyZoneEnd : MapElement //конец островока безопасности на дороге
    {
        private string direction;
        public RoadSafetyZoneEnd(RCT rct, string direction) : base(rct)
        {
            this.direction = direction;
        }
        public override void OnAddToChildElements()
        {
            surface = "Road";
            AddLayer(new NodeLayer(0, "Premetives/Surface", "Sidewalk"));
        }
        public override List<NodeLayer> BeforeProcessLayers(List<NodeLayer> layers)
        {
            return this.layers;
        }
        public override void setNodeDirections()
        {
            childNodes[0].direction = direction;
        }
 
    }
}
