using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class BackgroundStreet : Street
    {
        private List<Node> nodeList;
        public BackgroundStreet(RCT rct, char axis, int sidewalk) : base(rct, axis, sidewalk)
        {
            parentElement =  Stage.GetStage();
            Init();
        }
        public void Init()
        {
            CreateNodes();
            base.Upgrade();
            UpgradeChildElements();
            base.ProcessLayersChildElements(layers);
            RenderNodes();
        }
        public void CreateNodes()
        {
            
            Stage stage = Stage.GetStage();
            for (int x = rct.Start.x; x <= rct.End.x; x++)
            {
                for(int z = rct.Start.z; z <= rct.End.z; z++)
                {
                    Node node = new Node(x, z, stage.floors.Find(g => g.number == stage.groundFloor), false);
                    node.mapElement = this;
                    childNodes.Add(node);
                }
            }
            nodeList = new List<Node>(childNodes);
        }
        public void RenderNodes()
        {
            for(int i = 0; i< nodeList.Count; i++)
            {
                nodeList[i].GenerateCell();
            }
        }
    }
}

