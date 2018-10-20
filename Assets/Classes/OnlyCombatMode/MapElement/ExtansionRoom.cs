using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class ExtansionRoom : Room
    {
        public ExtansionRoom(RCT rct) : base(rct)
        {

        }
        public override List<NodeLayer> BeforeAddLayersToNode(List<NodeLayer> layers, Node node)
        {
            List<NodeLayer> nodeLayers = base.BeforeAddLayersToNode(layers, node);
            NodeLayer innerWall = new NodeLayer(getPrefabNuber(), "Premetives/Wall", "InnerWall");
            MapElement room = node.GetMapElementBorderWidtType(typeof(Room));
            if (room.id != id)
            {
                RCT cloneRCT = room.rct.Clone();
                cloneRCT.Grow();
                string direction = cloneRCT.GetDirection(node.crd);
                innerWall.direction =  direction;
                innerWall.InvertDirection();
                int siblingsCount = node.GetMapElementSiblingdsCount();
                if(siblingsCount == 7)
                {
                    innerWall.name = "innerWallOuterCorner";
                }
                if (node.crd.x == rct.Start.x || node.crd.x == rct.End.x || node.crd.z == rct.Start.z || node.crd.z == rct.End.z) innerWall.ignorePreviosMesh = true;
                nodeLayers.Add(innerWall);
            }
            return nodeLayers;
        }
    }
}
