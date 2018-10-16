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
                surface = "Road";
                AddLayer(new NodeLayer(getPrefabNuber(), "Additions/RoadMarker", "Crosswalk"));
            }
            if (parentElement.GetType() == typeof(CrossroadRoad))
            {
                surface = "Road";
                
            }
            if (parentElement.GetType() == typeof(Road))
            {
               
                surface = "Road";
                AddLayer(new NodeLayer(0, "Premetives/Surface", "Sidewalk"));
            }
        }
        public override List<NodeLayer> BeforeAddLayersToNode(List<NodeLayer> layers, Node node)
        {
            List<NodeLayer> nodeLayers = new List<NodeLayer>();
            if (parentElement.GetType() == typeof(CrossroadRoad))
            {
                if (node.BorderWidthType(typeof(Intersection)))
                {
                    //nodeLayers.Add(new NodeLayer(0, "Premetives/Surface", "Sidewalk"));
                }
                else
                {
                    NodeLayer layer = new NodeLayer(getPrefabNuber(), "Additions/SafetyZone", "End");
                    layer.direction = GetParentByClass(typeof(Crossroad)).rct.GetDirection(node.crd);
                    layer.InvertDirection();
                    nodeLayers.Add(layer);
                }
               
            }
            else
            {
                nodeLayers = layers;
            }
            return base.BeforeAddLayersToNode(nodeLayers, node);
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
            
        }
    }
}

