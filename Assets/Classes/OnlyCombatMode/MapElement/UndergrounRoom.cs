using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class UndergrounRoom : Room
    {
        public UndergrounRoom(RCT rct) : base(rct, 0)
        {

        }
        public override void HookAddToChildElements()
        {
            surface = "Ground";
        }
    }
}
