using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CrossroadCrosswalk : Road
    {
        public CrossroadCrosswalk(RCT rct, char axis) : base(rct, axis)
        {
           
        }
        public override void OnAddToChildElements()
        {
            surface = "Sidewalk";
        }

    }

}
