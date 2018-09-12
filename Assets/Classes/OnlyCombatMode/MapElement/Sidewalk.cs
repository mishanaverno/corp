using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Sidewalk : MapElement
    {
        char axis;
        public Sidewalk(RCT rct, char axis) : base(rct)
        {
            this.axis = axis;
            surface = "Road";
            AddLayer(new NodeLayer(0,"Surface","Sidewalk"));
        }
    }
}

