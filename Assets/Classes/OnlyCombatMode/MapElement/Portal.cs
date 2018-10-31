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
        public Portal(RCT rct, string innername, string outsidename, bool isExit) : base(rct)
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
        public override List<NodeLayer> BeforeAddLayersToNode(List<NodeLayer> layers, Node node)
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
            if (IsOpened())
            {
                node.UnlinkNode(Brother.childNodes[0]);
                node.LinkNode(Brother.childNodes[0], 1 + monoPortal.ExtraWeight);
                foreach(NodeLink link in node.links)
                {
                    Debug.Log(link.To.crd.x + ":" + link.To.crd.z + " -> " + link.w);
                }
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
