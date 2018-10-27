using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Building : MapElement
    {
        public Room BaseRoom { get; protected set; }
        public Building(RCT rct) : base(rct)
        {
        }
        public override void OnAddToChildElements()
        {
            base.OnAddToChildElements();
            surface = "Ground";
            AddLayer(new NodeLayer(getPrefabNuber(), "Premetives/Surface", "Building"));
        }
        public void Init()
        {
            BaseRoom = CreateRoom(new RCT(rct.Start.Clone().ReturnStepRB(), rct.End.Clone().ReturnStepLT()));
            
        }
        public Room CreateRoom(RCT rct)
        {
            Room room = new Room(rct);
            addNewElement(room);
            return room;
        }

        public MapElement AppendRoom(RCT rct)
        {
            RCT growRCT = new RCT(rct.Start.Clone().ReturnStepLT(), rct.End.Clone().ReturnStepRB());
            if (parentElement.rct.isContainRCT(growRCT))
            {
                GrowFor(growRCT);
                Room room = CreateRoom(rct);
                return room;
            }
            return this;
        } 
        public void GrowFor(RCT rct)
        {
            if (parentElement.rct.isContainRCT(rct))
            {
                Area parentArea = parentElement as Area;
                parentArea.RemoveElement(this);
                this.rct = RCT.Addition(this.rct, rct);
                parentArea.addNewElement(this);
            }
        }
        public void CreateColumn(CRD crd)
        {
            addNewElement(new Column(crd));
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
            return portal.rct;
        }
        public void CreateMainEntrance(RCT rct, string innername, string outername)
        {
            BaseRoom.CreateNonOpenablePortal(rct, innername, outername);
        }
        
        public override List<NodeLayer> BeforeAddLayersToNode(List<NodeLayer> layers, Node node)
        {
            List<NodeLayer> nodeLayers = new List<NodeLayer>(layers);
            List<Type> ignoreList = new List<Type>();
            ignoreList.Add(typeof(Column));
            if (node.IsOnMapElementBorder())
            {
                List<string> walls = node.GetWall();
                for (int w = 0; w < walls.Count; w++)
                {
                    if ((node.crd.x == rct.Start.x && walls[w] == "t") || 
                        (node.crd.x == rct.End.x && walls[w] == "b") ||
                        (node.crd.z == rct.Start.z && walls[w] == "l") ||
                        (node.crd.z == rct.End.z && walls[w] == "r"))
                    {
                        walls.RemoveAt(w);
                        w--;
                        continue;
                    }
                    NodeLayer border = new NodeLayer(getPrefabNuber(), "Premetives/Surface", "BuildingBorder");
                    border.direction = walls[w];
                    border.hasMesh = false;
                    nodeLayers.Add(border);
                }
                for (int w = 0; w < walls.Count; w++)
                {
                    NodeLayer wall = new NodeLayer(getPrefabNuber(), "Premetives/Wall", "OuterWall");
                    wall.direction = walls[w];
                    wall.hasMesh = false;
                    nodeLayers.Add(wall);
                }
                List<string> outerCorners = node.GetOuterCorners();
                for (int c = 0; c < outerCorners.Count; c++)
                {
                    NodeLayer border = new NodeLayer(getPrefabNuber(), "Premetives/Surface", "BuildingBorderOuterCorner");
                    border.direction = outerCorners[c];
                    border.hasMesh = false;
                    nodeLayers.Add(border);
                }
                for (int c = 0; c < outerCorners.Count; c++)
                {
                    NodeLayer corner = new NodeLayer(getPrefabNuber(), "Premetives/Wall", "OuterWallOuterCorner");
                    corner.direction = outerCorners[c];
                    corner.hasMesh = false;
                    nodeLayers.Add(corner);
                }
            }
            return base.BeforeAddLayersToNode(nodeLayers, node);
        }

    }
}
