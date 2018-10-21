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
        public Room CreateSubRoom(RCT rct)
        {
            Room room = new Room(rct);
            room.parentElement = parentElement;
            room.OnAddToChildElements();
            room.moveNodesFromMapElementToThis(this);
            parentElement.childElements.Add(room);
            return room;
        }
       
        public Door CreateDoor(RCT rct)
        {
            Door door = new Door(rct);
            addNewElement(door);
            return door;
        }
        public override void OnAddToChildElements()
        {
            NodeLayer floor = new NodeLayer(getPrefabNuber(), "Premetives/Surface", "Floor");
            floor.hasMesh = false;
            AddLayer(floor);
            base.OnAddToChildElements();
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
