using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class UndergrounRoom : Room
    {
        public UndergrounRoom(RCT rct) : base(rct, 0)
        {

        }
        public override void HookAddToChildElements()
        {
            surface = "None";
            NodeLayer floor = new NodeLayer(getPrefabNuber(), "Premetives/Surface", "Floor");
            floor.hasMesh = true;
            AddLayer(floor);

            NodeLayer controllQuad = new NodeLayer(0, "Main", "ControllQuad");
            controllQuad.positionCorrection = new Vector3(0, 0.05f, 0);
            controllQuad.hasMesh = false;
            AddLayer(controllQuad);
        }
        public override void HookAfterAddNodesToMapElement()
        {
            foreach (Node node in childNodes)
            {
                node.isWalkable = true;
                for (int x = -1; x <= 1; x++)
                {
                    for(int z = -1; z <= 1; z++)
                    {
                        CRD crd = new CRD(node.x + x, node.z + z);
                        if ((x!=0 || z!=0) && rct.isContainCRD(crd)){
                            float w = 1;
                            if (x != 0 && z != 0) w = 1.5f;
                            node.LinkNode(Stage.GetNode(crd, floorNumber), w);
                        }
                    }
                }
            }
        }
    }
}
