using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CrossroadCrosswalk : Road //пешеходный переход
    {
        public CrossroadCrosswalk(RCT rct, int floor, char axis) : base(rct, floor, axis)
        {
           
        }
        public override void HookAddToChildElements()
        {
            surface = "Sidewalk";
        }

    }

}
