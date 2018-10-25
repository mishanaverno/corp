using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Portal : MapElement
    {
        protected string innername, outsidename;
        protected bool isExit;
        public Portal Brother { get; set; }

        public Portal(RCT rct, string innername, string outsidename, bool isExit) : base(rct)
        {
            this.innername = innername;
            this.outsidename = outsidename;
            this.isExit = isExit;
        }

        public override List<NodeLayer> BeforeAddLayersToNode(List<NodeLayer> layers, Node node)
        {
            string name;
            if(parentElement is Room)
            {
                name = innername;
            }
            else
            {
                name = outsidename;
            }
            NodeLayer door = new NodeLayer(getPrefabNuber(), "Premetives/Portal", name);
            if (isExit)
            {
                door.direction = parentElement.rct.GetDirection(node.crd);
            }
            else
            {
                RCT dirRCT = Brother.parentElement.rct.Clone();
                dirRCT.DebugLog("portal");
                dirRCT.Grow();
                door.direction = dirRCT.GetDirection(node.crd);
                door.InvertDirection();
            }
            door.hasMesh = false;
            NodeLayer wall = node.Layers.Find(x => x.direction == door.direction && (x.name == "InnerWall" || x.name == "OuterWall"));
            node.Layers.Remove(wall);
            return new List<NodeLayer>() { door };
        }
        public static void Bind(Portal p1, Portal p2)
        {
            p1.Brother = p2;
            p2.Brother = p1;
        }
    }
}
