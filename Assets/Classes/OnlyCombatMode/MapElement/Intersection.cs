﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Intersection : MapElement//пересечение дорог на перекрестке
    {
        public Intersection(RCT rct) : base(rct)
        {
            
        }
        public override void OnAddToChildElements()
        {
            base.OnAddToChildElements();
            surface = "Road";
           AddLayer(new NodeLayer(0, "Main", "ControllQuad"));
        }
    }
}

