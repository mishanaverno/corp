using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class SidewalkParking : MapElement
    {
        public SidewalkParking(RCT rct) : base(rct)
        {

        }
        public override void OnAddToChildElements()
        {
            surface = "Road";
        }
        public override List<NodeLayer> BeforeProcessLayers(List<NodeLayer> layers)
        {
            return new List<NodeLayer>();
        }

    }
}