using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class ParkingPlace : MapElement //Парковочное место в парковочном кармане
    {
        public char axis;
        public ParkingPlace(RCT rct, int floor, char axis): base(rct,floor)
        {
            this.axis = axis;
        }
        public override void HookAddToChildElements()
        {
            surface = "Road";
            AddLayer(new NodeLayer(0, "Main", "ControllQuad"));
        }
    }
}