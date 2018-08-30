using System;
using UnityEngine;

namespace Map
{
    public class Floor
    {
        [SerializeField]
        public int number;
        public Node[,] map;
        protected Stage stage;
        protected RCT rct;
        [SerializeField]
        protected bool defaultIsWalkable;
        public Floor(int number, Stage stage)
        {
            this.number = number;
            this.stage = stage;
            this.rct = new RCT(new CRD(0, 0), stage.height, stage.width);
            map = new Node[stage.height,stage.width];
        }
        public virtual void Init()
        {
            GenerateNodes();
        }
        protected void GenerateNodes()
        {
            //+++++++++++++++++++++++++++++++++++++
            Debug.Log("Generate floor " + number);
            //+++++++++++++++++++++++++++++++++++++
            for(int x = 0; x < stage.height; x++)
            {
                for(int z = 0; z < stage.width; z++)
                {
                    Node node = new Node(x, z, this, defaultIsWalkable);
                    map[x, z] = node;
                    string json = JsonUtility.ToJson(node);
                    Debug.Log(json);
                }
            }
        }
        protected void LinkNodes()
        {
            //+++++++++++++++++++++++++++++++++++++
            Debug.Log("Generate floor " + number);
            //+++++++++++++++++++++++++++++++++++++
            for (int fx = 0; fx < stage.height; fx++)
            {
                for(int fz = 0; fz < stage.width; fz++)
                {
                    Node currentNode = GetNode(fx, fz);
                    Debug.Log("Node -------- " + currentNode.crd.x + "," + currentNode.crd.z);
                    for (int x = -1; x <= 1; x++)
                    {
                        for(int z = -1; z <= +1; z++)
                        {

                            if ((z == 0 && x == 0) || !isInFloor(new CRD(fx + x, fz + z)))
                            {
                                continue;
                            }
                            float w;
                            if (z == 0 || x == 0)
                            {
                               w = 1;
                            }
                            else
                            {
                               w = 1.5f;
                            }
                            Node sibblingnode = GetNode(fx + x, fz + z);
                            currentNode.LinkNode(sibblingnode, w);
                        }
                    }
                }
            }
        }
        public Node GetNode(int x, int z)
        {
            return map[x, z];
        }
        public bool isInFloor(CRD crd)
        {
            return this.rct.isContainCRD(crd);
        }

    }
}
