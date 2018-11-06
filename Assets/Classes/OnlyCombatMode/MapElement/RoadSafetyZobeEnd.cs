using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RoadSafetyZoneEnd : MapElement //конец островока безопасности на дороге
    {
        private string direction;
        public RoadSafetyZoneEnd(RCT rct, int floor, string direction) : base(rct, floor)
        {
            this.direction = direction;
        }
        public override void HookAddToChildElements()
        {
            surface = "Road";
            AddLayer(new NodeLayer(0, "Premetives/Surface", "Sidewalk"));
        }
        public override List<NodeLayer> HookProcessLayers(List<NodeLayer> layers)
        {
            return this.layers;
        }
        public override void setNodeDirections()
        {
            childNodes[0].direction = direction;
        }
 
    }
}
