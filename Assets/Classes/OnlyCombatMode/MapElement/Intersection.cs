using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Intersection : MapElement
    {
        public Intersection(RCT rct) : base(rct)
        {
            surface = "Road";
        }
    }
}

