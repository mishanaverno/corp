﻿using System;
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
        public void CreateColumn(CRD crd)
        {
            addNewElement(new Column(crd));
        }

        public Room CreateSubRoom(RCT rct)
        {
            Room room = new Room(rct);
            room.parentElement = parentElement;
            room.OnAddToChildElements();
            room.moveNodesFromMapElementToThis(this);
            parentElement.childElements.Add(room);
            return room;
        }
        public void CreateDoor(CRD crd, string order = "Default")
        {
            if (rct.isContainCRD(crd))
            {
                MapElement.SetOrder(CreateOpenablePortal(new RCT(crd, crd), "InnerDoor", "InnerPortal", "OuterPortal"), order);
            }
        }
        
        public void CreateWindow(CRD crd, string order = "Default")
        {
            if (rct.isContainCRD(crd))
            {
                MapElement.SetOrder(CreateNonOpenablePortal(new RCT(crd, crd), "InnerWindow", "OuterWindow"), order);
            }
        }
        public void CreateDoorway(CRD crd, string order = "Default")
        {
            if (rct.isContainCRD(crd))
            {
                MapElement.SetOrder(CreateNonOpenablePortal(new RCT(crd, crd), "InnerPortal", "OuterPortal"), order);
            }
        }
        public void CreateDoubleDoor(CRD crd, string order = "Default")
        {
            if (rct.isContainCRD(crd))
            {
                MapElement.SetOrder(CreateDoublePortal(crd, true), order);
            }
        }
        public void CreateDoubleDoorway(CRD crd, string order = "Default")
        {
            if (rct.isContainCRD(crd))
            {
                MapElement.SetOrder(CreateDoublePortal(crd, false), order);
            }
        }
        public List<MapElement> CreateDoublePortal(CRD crd, bool openable = false)
        {
            string crdDirection = rct.GetDirection(crd), twinNode;
            switch (crdDirection)
            {
                case "l":
                    twinNode = "b";
                    break;
                case "r":
                    twinNode = "t";
                    break;
                case "t":
                    twinNode = "l";
                    break;
                case "b":
                    twinNode = "r";
                    break;
                default:
                    twinNode = "t";
                    break;
            }
            CRD twinCRD = Stage.GetNode(crd).GetSibling(twinNode).crd;
            List<MapElement> RightPortals;
            List<MapElement> LeftPortals;
            if (openable)
            {
                RightPortals = CreateOpenablePortal(new RCT(crd, crd), "InnerDoubleDoorR", "InnerDoublePortalL", "OuterDoublePortalL");
                LeftPortals = CreateOpenablePortal(new RCT(twinCRD, twinCRD), "InnerDoubleDoorL", "InnerDoublePortalR", "OuterDoublePortalR");
            }
            else
            {
                RightPortals = CreateOpenablePortal(new RCT(crd, crd), "InnerDoublePortalR", "InnerDoublePortalL", "OuterDoublePortalL");
                LeftPortals = CreateOpenablePortal(new RCT(twinCRD, twinCRD), "InnerDoublePortalL", "InnerDoublePortalR", "OuterDoublePortalR");
            }
            List<MapElement> list = new List<MapElement>();
            for (int i = 0; i < 2; i++)
            {
                Portal p1 = RightPortals[i] as Portal;
                Portal p2 = LeftPortals[i] as Portal;
                Portal.BindTwin(p1, p2);
            }
            list.AddRange(RightPortals);
            list.AddRange(LeftPortals);
            return list;
        }
        public RCT CreatePortal(Portal portal)
        {
            portal.parentElement = this;
            portal.OnAddToChildElements();
            for (int i = 0; i < childNodes.Count; i++)
            {
                if (portal.rct.isContainCRD(childNodes[i].crd)) portal.childNodes.Add(childNodes[i]);
            }
            childElements.Add(portal);
            List<Node> nodesSecondPortal = new List<Node>();
            for(int i = 0; i < portal.childNodes.Count; i++)
            {
                List<Node> siblings = portal.childNodes[i].GetSiblingsNoDiagonals();
                for (int s = 0; s < siblings.Count; s++)
                {
                    if(siblings[s].mapElement.id != id)
                    {
                        nodesSecondPortal.Add(siblings[s]);
                        break;
                    }
                }
            }
            return new RCT(nodesSecondPortal);
        }
        public List<MapElement> CreateNonOpenablePortal(RCT rct, string insidename, string outsidename) {
            return CreateOpenablePortal(rct, insidename, insidename, outsidename);
        }
        public List<MapElement> CreateOpenablePortal(RCT rct, string innername, string outername, string outsidename)
        {
            Portal exit = new Portal(rct, innername, outername, true);
            RCT doorRCT = CreatePortal(exit);
            MapElement mapElement = Stage.GetNode(doorRCT.Start, GetFloorNumber()).mapElement;
            Portal entrence;
            if (mapElement.GetType() == typeof(Building))
            {
                entrence = new Portal(doorRCT, innername, outsidename, false);
                Building building = mapElement as Building;
                building.CreatePortal(entrence);
            }
            else
            {
                entrence = new Portal(doorRCT, outername, outsidename, false);
                Room room = mapElement as Room;
                room.CreatePortal(entrence);
            }
            Portal.Bind(exit, entrence);
            return new List<MapElement>() { exit, entrence };
        }
       
        public int GetFloorNumber()
        {
            if (childNodes.Count > 0)
            {
                return childNodes[0].floor.number;
            }
            else
            {
                return -1;
            }
        }
        public override void OnAddToChildElements()
        {
            base.OnAddToChildElements();
            NodeLayer floor = new NodeLayer(getPrefabNuber(), "Premetives/Surface", "Floor");
            floor.hasMesh = false;
            AddLayer(floor);
            
            NodeLayer controllQuad = new NodeLayer(0, "Main", "ControllQuad");
            controllQuad.positionCorrection = new Vector3(0, 0.05f, 0);
            AddLayer(controllQuad);
        }
        public override List<NodeLayer> BeforeAddLayersToNode(List<NodeLayer> layers, Node node)
        {
            List<NodeLayer> nodeLayers = new List<NodeLayer>(layers);
            
            if (node.IsOnMapElementBorder())
            {
                
                List<string> walls = node.GetWall();
                for(int w = 0; w < walls.Count; w++)
                {
                    NodeLayer wall = new NodeLayer(getPrefabNuber(), "Premetives/Wall", "InnerWall");
                    wall.direction = walls[w];
                    wall.hasMesh = false;
                    nodeLayers.Add(wall);
                }
                List<string> outerCorners = node.GetOuterCorners();
                for (int c = 0; c < outerCorners.Count; c++)
                {
                    NodeLayer outerCorner = new NodeLayer(getPrefabNuber(), "Premetives/Wall", "innerWallOuterCorner");
                    outerCorner.direction = outerCorners[c];
                    outerCorner.hasMesh = false;
                    nodeLayers.Add(outerCorner);
                }
            }
            
            return base.BeforeAddLayersToNode(nodeLayers, node);
        }
        public override List<NodeLayer> BeforeProcessLayers(List<NodeLayer> layers)
        {
            int oldControllQuad = layers.FindIndex(x => x.name == "ControllQuad");
            if (oldControllQuad >= 0) layers.RemoveAt(oldControllQuad);
            return base.BeforeProcessLayers(layers);
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