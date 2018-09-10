using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RoadStripe : MapElement
    {
        char axis;
        public RoadStripe(RCT rct, char axis) : base(rct)
        {
            this.axis = axis;
            surface = "Road";
        }
    }
}
