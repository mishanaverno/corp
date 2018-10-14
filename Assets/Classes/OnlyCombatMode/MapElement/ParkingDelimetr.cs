using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class ParkingDelimetr : MapElement //Разделитель парковочных мест в парковочных карманах
    {
        public char axis;
        public ParkingDelimetr(RCT rct, char axis) : base(rct)
        {
            this.axis = axis;
        }
        public override void OnAddToChildElements()
        {
            surface = "Road";
            AddLayer(new NodeLayer(getPrefabNuber(), "Additions/RoadMarker", "CenterLine"));
        }
    }
}
