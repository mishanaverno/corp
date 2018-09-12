using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RoadSafetyZone : MapElement
    {
        public char axis;
        public RoadSafetyZone(RCT rct, char axis) : base(rct)
        {
            this.axis = axis;
            surface = "Road";
            AddLayer(new NodeLayer(0, "Surface", "Sidewalk"));
            AddLayer(new NodeLayer(0, "Surface", "Sidewalk"));
        }
    }
}

