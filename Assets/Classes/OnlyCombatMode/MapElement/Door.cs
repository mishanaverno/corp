using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Door : Portal
    {
        public Door(RCT rct) : base(rct)
        {

        }
        public override List<NodeLayer> BeforeAddLayersToNode(List<NodeLayer> layers, Node node)
        {
            List<NodeLayer> nodeLayers = new List<NodeLayer>(layers);
            NodeLayer door = new NodeLayer(getPrefabNuber(), "Premitives/Portal", "InnerPortal");
            nodeLayers.Add(door);
            return base.BeforeAddLayersToNode(nodeLayers, node);
        }
    }
}
