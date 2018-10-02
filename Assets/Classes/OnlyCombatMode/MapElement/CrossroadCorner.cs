using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CrossroadCorner : MapElement//угол на перекрестке
    {
        public CrossroadCorner(RCT rct) : base(rct)
        {
        
        }
        public override void OnAddToChildElements()
        {
            surface = "Road";
            AddLayer(new NodeLayer(0, "Surface", "Road"));
        }

    }
}
