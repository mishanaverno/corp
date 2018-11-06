using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class ParkingDelimetr : MapElement //Разделитель парковочных мест в парковочных карманах
    {
        public char axis;
        public ParkingDelimetr(RCT rct, int floor, char axis) : base(rct,floor)
        {
            this.axis = axis;
        }
        public override void HookAddToChildElements()
        {
            surface = "Road";
            AddLayer(new NodeLayer(getPrefabNuber(), "Additions/RoadMarker", "CenterLine"));
            AddLayer(new NodeLayer(0, "Main", "ControllQuad"));
        }
    }
}
