using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RoadLine : MapElement // линия движения транспорта
    {
        public char axis;
        public RoadLine(RCT rct, int floor, char axis) : base(rct,floor)
        {
            this.rct = rct;
            this.axis = axis;
        }
        public override void HookAfterAddNodesToMapElement()
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
        public override void HookAddToChildElements()
        {
                surface = "Road";
        }
        public override List<NodeLayer> HookAddLayersToNode(List<NodeLayer> layers, Node node)
        {
            List<NodeLayer> nodeLayers = new List<NodeLayer>(layers);
            if(parentElement.parentElement.GetType() == typeof(Road)){
                if (axis == 'v')
                {
                    if (node.crd.z == parentElement.parentElement.rct.Start.z || node.crd.z == parentElement.parentElement.rct.End.z)
                    {

                        if (node.BorderWidthTypeNoDiagonal(typeof(ParkingPlace)) || node.BorderWidthTypeNoDiagonal(typeof(ParkingDelimetr)))
                        {
                            nodeLayers.Add(new NodeLayer(getPrefabNuber(), "Additions/RoadMarker", "ShortDashedLine"));
                        }
                        else
                        {
                            nodeLayers.Add(new NodeLayer(getPrefabNuber(), "Additions/RoadMarker", "SideLine"));
                        }
                    }
                    else if (node.crd.z == parentElement.rct.Start.z || node.crd.z == parentElement.rct.End.z)
                    {
                        nodeLayers.Add(new NodeLayer(getPrefabNuber(), "Additions/RoadMarker", "SolidLine"));
                    }
                    else
                    {
                        nodeLayers.Add(new NodeLayer(getPrefabNuber(), "Additions/RoadMarker", "DashedLine"));
                    }
                }
                else
                {
                    if (node.crd.x == parentElement.parentElement.rct.Start.x || node.crd.x == parentElement.parentElement.rct.End.x)
                    {
                        if (node.BorderWidthTypeNoDiagonal(typeof(ParkingPlace)) || node.BorderWidthTypeNoDiagonal(typeof(ParkingDelimetr)))
                        {
                            nodeLayers.Add(new NodeLayer(getPrefabNuber(), "Additions/RoadMarker", "ShortDashedLine"));
                        }
                        else
                        {
                            nodeLayers.Add(new NodeLayer(getPrefabNuber(), "Additions/RoadMarker", "SideLine"));
                        }
                    }
                    else if (node.crd.x == parentElement.rct.Start.x || node.crd.x == parentElement.rct.End.x)
                    {
                        nodeLayers.Add(new NodeLayer(getPrefabNuber(), "Additions/RoadMarker", "SolidLine"));
                    }
                    else
                    {
                        nodeLayers.Add(new NodeLayer(getPrefabNuber(), "Additions/RoadMarker", "DashedLine"));
                    }
                }
            }
            else if(parentElement.parentElement.GetType() == typeof(CrossroadCrosswalk))
            {
                nodeLayers.Add(new NodeLayer(getPrefabNuber(), "Additions/RoadMarker", "Crosswalk"));
            }
            else if(parentElement.parentElement.GetType() == typeof(CrossroadRoad))
            {
                if (node.BorderWidthType(typeof(Intersection)))
                {

                }
                else
                {
                    nodeLayers.Add(new NodeLayer(getPrefabNuber(), "Additions/RoadMarker", "CenterLine"));
                }
            }
            if (parentElement.parentElement.parentElement.GetType() != typeof(BackgroundStreet))
            {
                nodeLayers.Add(new NodeLayer(0, "Main", "ControllQuad"));
            }
            return base.HookAddLayersToNode(nodeLayers, node);
        }
    }
}

