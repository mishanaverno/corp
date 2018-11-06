using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Portal : MapElement
    {
        protected string innername, outsidename;
        public string name;
        protected bool isExit;
        public Portal Brother { get; set; }
        public Portal TwinBrother { get; set; }
        public MonoPortal monoPortal;
        public Portal(RCT rct, int floor, string innername, string outsidename, bool isExit) : base(rct,floor)
        {
            this.innername = innername;
            this.outsidename = outsidename;
            this.isExit = isExit;
        }
        public void Init()
        {
            if (parentElement is Room)
            {
                name = innername;
            }
            else
            {
                name = outsidename;
            }
            GameObject prefab = Resources.Load("Stage/" + Stage.GetStage().DesignName + "/Premetives/Portal/" + name + "/" + order + "/" + getPrefabNuber()) as GameObject;
            MonoPortal script = prefab.GetComponent < MonoPortal>();
            monoPortal = script;
        }
        public override List<NodeLayer> HookAddLayersToNode(List<NodeLayer> layers, Node node)
        {
            NodeLayer door = new NodeLayer(getPrefabNuber(), "Premetives/Portal", name);
            if (isExit)
            {
                door.direction = parentElement.rct.GetDirection(node.crd);
            }
            else
            {
                RCT dirRCT = Brother.parentElement.rct.Clone();
                dirRCT.Grow();
                door.direction = dirRCT.GetDirection(node.crd);
                door.InvertDirection();
            }
            door.hasMesh = false;
            NodeLayer wall = node.Layers.Find(x => x.direction == door.direction && (x.name == "InnerWall" || x.name == "OuterWall"));
            node.Layers.Remove(wall);
            if (IsOpened() && isExit)
            {
               
                node.UnlinkNode(Brother.childNodes[0]);
                Brother.childNodes[0].UnlinkNode(node);
                node.LinkNode(Brother.childNodes[0], 1 + monoPortal.ExtraWeight);
                Brother.childNodes[0].LinkNode(node, 1 + monoPortal.ExtraWeight);
                
            }
            return new List<NodeLayer>() { door };
        }
        
        public bool IsOpened()
        {
            if (monoPortal.Opened && Brother.monoPortal.Opened)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Bind(Portal p1, Portal p2)
        {
            p1.Brother = p2;
            p2.Brother = p1;
        }
        public static void BindTwin(Portal p1, Portal p2)
        {
            p1.TwinBrother = p2;
            p2.TwinBrother = p1;
        }
    }
}
