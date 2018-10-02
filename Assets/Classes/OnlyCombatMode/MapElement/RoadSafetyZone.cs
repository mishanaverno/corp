using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RoadSafetyZone : MapElement//островок безопасности
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
        public override void Upgrade()
        {
            List<MapElement> newElements = new List<MapElement>();
            if (parentElement.GetType() == typeof(Road))
            {
                if (axis == 'v')
                {
                    if (rct.Start.x > 0)
                    {
                        newElements.Add(new RoadSafetyZoneEnd(new RCT(rct.Start, rct.Start), "t"));
                    }
                    if (rct.End.x < Stage.GetStage().rct.End.x)
                    {
                        newElements.Add(new RoadSafetyZoneEnd(new RCT(rct.End, rct.End), "b"));
                    }

                }
                else
                {
                    if (rct.Start.z > 0)
                    {
                        newElements.Add(new RoadSafetyZoneEnd(new RCT(rct.Start, rct.Start), "l"));
                    }
                    if (rct.End.z < Stage.GetStage().rct.End.z)
                    {
                        newElements.Add(new RoadSafetyZoneEnd(new RCT(rct.End, rct.End), "r"));
                    }
                }
            }
            addNewElements(newElements);
        }
    }
}

