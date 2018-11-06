using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RoadSafetyZone : MapElement//островок безопасности
    {
        public char axis;
        public RoadSafetyZone(RCT rct, int floor, char axis) : base(rct,floor)
        {
            this.axis = axis;
            
        }
        public override void HookAddToChildElements()
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
            if (parentElement.parentElement.GetType() != typeof(BackgroundStreet))
            {
                AddLayer(new NodeLayer(0, "Main", "ControllQuad"));
            }
            
        }
        public override List<NodeLayer> HookAddLayersToNode(List<NodeLayer> layers, Node node)
        {
            List<NodeLayer> nodeLayers = new List<NodeLayer>();
            if (parentElement.GetType() == typeof(CrossroadRoad))
            {
                if (node.BorderWidthType(typeof(Intersection)))
                {
                    //nodeLayers.Add(new NodeLayer(0, "Premetives/Surface", "Sidewalk"));
                    if (parentElement.parentElement.GetType() != typeof(BackgroundStreet))
                    {
                        nodeLayers.Add(new NodeLayer(0, "Main", "ControllQuad"));
                    }
                    
                }
                else
                {
                    NodeLayer layer = new NodeLayer(getPrefabNuber(), "Additions/SafetyZone", "End");
                    layer.direction = GetParentByClass(typeof(Crossroad)).rct.GetDirection(node.crd);
                    layer.InvertDirection();
                    nodeLayers.Add(layer);
                    if (parentElement.parentElement.GetType() != typeof(BackgroundStreet))
                    {
                        nodeLayers.Add(new NodeLayer(0, "Main", "ControllQuad"));
                    }
                }
               
            }
            else
            {
                nodeLayers = layers;
                
            }
            
            return base.HookAddLayersToNode(nodeLayers, node);
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

