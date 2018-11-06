using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Intersection : MapElement//пересечение дорог на перекрестке
    {
        public Intersection(RCT rct, int floor) : base(rct,floor)
        {
            
        }
        public override void HookAddToChildElements()
        {
            base.HookAddToChildElements();
            surface = "Road";
           AddLayer(new NodeLayer(0, "Main", "ControllQuad"));
        }
    }
}

