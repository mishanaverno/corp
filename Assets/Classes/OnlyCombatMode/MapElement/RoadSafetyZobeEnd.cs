using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RoadSafetyZoneEnd : MapElement
    {
        private string direction;
        public RoadSafetyZoneEnd(RCT rct, string direction) : base(rct)
        {
            this.direction = direction;
        }
        public override void OnAddToChildElements()
        {
            AddLayer(new NodeLayer(0, "Surface", "Road"));
        }
        public override void setNodeDirections()
        {
            childNodes[0].direction = direction;
        }
 
    }
}
