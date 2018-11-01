﻿using System.Collections;
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
            NodeLayer corner = new NodeLayer(getPrefabNuber(), "Additions/Sidewalk", "OuterCorner");
            string layerDirection = parentElement.rct.GetDirection(this.rct.Start);
            corner.direction = layerDirection;
            AddLayer(corner);
            AddLayer(new NodeLayer(0, "Main", "ControllQuad"));
            NodeLayer trafficLight = new NodeLayer(getPrefabNuber(), "Additions/Pilars", "TrafficLight");
            trafficLight.direction = layerDirection;
            trafficLight.hasMesh = false;
            trafficLight.nonWalkable = true;
            AddLayer(trafficLight);
        }
        public override List<NodeLayer> BeforeProcessLayers(List<NodeLayer> layers)
        {
            return new List<NodeLayer>(this.layers); 
        }

    }
}
