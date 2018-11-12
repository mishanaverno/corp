using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class UndergroundDirt : MapElement
    {
        public UndergroundDirt(RCT rct) : base(rct, 0)
        {

        }
        public override void HookAddToChildElements()
        {
            this.surface = "None";
        }
        
    }
}
