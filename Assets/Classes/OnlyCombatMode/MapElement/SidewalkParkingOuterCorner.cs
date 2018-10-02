﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class SidewalkParkingOuterCorner : MapElement //внешний угол парковочного кармана
    {
        public SidewalkParkingOuterCorner(RCT rct) : base(rct)
        {

        }
        public override void OnAddToChildElements()
        {
            AddLayer(new NodeLayer(0, "Surface", "Road"));
        }
    }
}