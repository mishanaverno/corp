using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RoadLine : MapElement // линия движения транспорта
    {
        public char axis;
        public RoadLine(RCT rct, char axis) : base(rct)
        {
            this.rct = rct;
            this.axis = axis;
        }
        public override void setNodeDirections()
        {
            for(int i = 0; i < childNodes.Count; i++)
            {
                if(axis == 'v')
                {
                    if (childNodes[i].z == rct.Start.z)
                    {
                        childNodes[i].direction = "l";
                    }
                    else
                    {
                        childNodes[i].direction = "r";
                    }
                }
                else
                {
                    if(childNodes[i].x == rct.Start.x)
                    {
                        childNodes[i].direction = "t";
                    }
                    else
                    {
                        childNodes[i].direction = "b";
                    }
                }
            }
        }
        public override void OnAddToChildElements()
        {
            
            if (parentElement.parentElement is CrossroadCrosswalk)
            {
                surface = "Crosswalk";
            }
            else
            {
                surface = "Road";

            }
        }
    }
}

