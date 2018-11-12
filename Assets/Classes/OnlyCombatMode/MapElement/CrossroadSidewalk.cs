using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class CrossroadSidewalk : MapElement//тратуар в перекрестке
    {
        public CRD corner;
        public CrossroadSidewalk(RCT rct, int floor, CRD corner) : base(rct, floor)
        {
            this.corner = corner;
        }
        public override void HookAddToChildElements()
        {
            surface = "Road";
            AddLayer(new NodeLayer(getPrefabNuber(), "Premetives/Surface", "Sidewalk"));
            AddLayer(new NodeLayer(0, "Main", "ControllQuad"));
        }
        public override void HookAfterAddNodesToMapElement()
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
            addNewElement(new CrossroadCorner(new RCT(corner, corner), floorNumber));
        }
        
    }
}

