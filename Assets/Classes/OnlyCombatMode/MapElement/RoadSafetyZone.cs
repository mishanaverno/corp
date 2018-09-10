using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RoadSafetyZone : MapElement
    {
        char axis;
        public RoadSafetyZone(RCT rct, char axis) : base(rct)
        {
            this.axis = axis;
            surface = "Sidewalk";
        }
    }
}

