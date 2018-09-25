using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CrossroadSidewalk : MapElement
    {
        public CRD corner;
        public CrossroadSidewalk(RCT rct, CRD corner) : base(rct)
        {
            this.corner = corner;
        }
        public override void OnAddToChildElements()
        {
            surface = "Road";
            AddLayer(new NodeLayer(0, "Surface", "Sidewalk"));
        }
        public override void setNodeDirections()
        {
           
            for (int i = 0; i < childNodes.Count; i++)
            {

                if (corner.z == rct.Start.z)
                {
                    if(childNodes[i].z >= corner.z)
                    {
                        childNodes[i].direction = "l";
                    }
                }
                else if(corner.z <= rct.End.z)
                {
                    if (childNodes[i].z == corner.z)
                    {
                        childNodes[i].direction = "r";
                    }
                }
                if(corner.x == rct.Start.x)
                {
                    if(childNodes[i].x == corner.x)
                    {
                        childNodes[i].direction = "t";
                    }
                }
                else if(corner.x == rct.End.x)
                {
                    if(childNodes[i].x == corner.x)
                    {
                        childNodes[i].direction = "b";
                    }
                }
            }
            
        }
        public override void Upgrade()
        {
            addNewElement(new CrossroadCorner(new RCT(corner, corner)));
        }
        
    }
}

