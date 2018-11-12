using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class EmptyArea : MapElement
    {
        public EmptyArea(RCT rct, int floor): base(rct, floor)
        {

        }
        public override void HookAddToChildElements()
        {
            surface = "None";
        }
    }
}
