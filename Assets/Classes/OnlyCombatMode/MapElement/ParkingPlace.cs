using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class ParkingPlace : MapElement //Парковочное место в парковочном кармане
    {
        public char axis;
        public ParkingPlace(RCT rct, char axis): base(rct)
        {
            this.axis = axis;
        }
        public override void OnAddToChildElements()
        {
            surface = "Road";
        }
    }
}