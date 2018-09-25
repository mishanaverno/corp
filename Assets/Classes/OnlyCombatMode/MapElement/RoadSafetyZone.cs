using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RoadSafetyZone : MapElement
    {
        public char axis;
        public RoadSafetyZone(RCT rct, char axis) : base(rct)
        {
            this.axis = axis;
            
        }
        public override void OnAddToChildElements()
        {
            if (parentElement.GetType() == typeof(CrossroadCrosswalk))
            {
                surface = "Crosswalk";
            }
            if (parentElement.GetType() == typeof(CrossroadRoad))
            {
                surface = "Road";
            }
            if (parentElement.GetType() == typeof(Road))
            {
               
                surface = "Road";
                AddLayer(new NodeLayer(0, "Surface", "Sidewalk"));
                AddLayer(new NodeLayer(0, "Surface", "Sidewalk"));
            }
        }
        public override void setNodeDirections()
        {
            string direction;
            if (axis == 'v')
            {
                direction = "b";
            }
            else
            {
                direction = "l";
            }
            for (int i = 0; i < childNodes.Count; i++)
            {
                childNodes[i].direction = direction;
            }
        }
    }
}

