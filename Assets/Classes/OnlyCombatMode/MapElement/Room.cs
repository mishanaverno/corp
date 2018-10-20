using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Room : MapElement
    {
        public Room(RCT rct) : base(rct)
        {

        }
        public override void OnAddToChildElements()
        {
            AddLayer(new NodeLayer(getPrefabNuber(), "Premetives/Surface", "Floor"));
            base.OnAddToChildElements();
        }
        public override List<NodeLayer> BeforeAddLayersToNode(List<NodeLayer> layers, Node node)
        {
            List<NodeLayer> nodeLayers = new List<NodeLayer>(layers);
            if(node.IsOnMapElementBorder())
            {
                if (node.crd.x == rct.Start.x || node.crd.x == rct.End.x || node.crd.z == rct.Start.z || node.crd.z == rct.End.z)
                {
                    NodeLayer innerWall = new NodeLayer(getPrefabNuber(), "Premetives/Wall", "InnerWall");
                    string dir = rct.GetDirection(node.crd);
                    if (dir == "lt" || dir == "rt" || dir == "lb" || dir == "rb")
                    {
                        innerWall.direction = dir[0].ToString();
                        NodeLayer dubl = innerWall.Clone();
                        dubl.direction = dir[1].ToString();
                        dubl.hasMesh = false;
                        nodeLayers.Add(dubl);
                    }
                    nodeLayers.Add(innerWall);
                    
                }
            }
            return base.BeforeAddLayersToNode(nodeLayers, node);
        }
        public override void setNodeDirections()
        {
            for(int i = 0; i < childNodes.Count; i++)
            {
                childNodes[i].direction = rct.GetDirectionNoDiagonals(childNodes[i].crd);
            }
            base.setNodeDirections();
        }
    }
}
