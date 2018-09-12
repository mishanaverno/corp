using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RoadLine : MapElement
    {
        public char axis;
        public RoadLine(RCT rct, char axis) : base(rct)
        {
            this.rct = rct;
            this.axis = axis;
            surface = "Road";
        }
    }
}

