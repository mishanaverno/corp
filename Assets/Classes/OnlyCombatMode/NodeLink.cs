﻿
using UnityEngine;

namespace Map {
    public class NodeLink//ссылка от одной ноды в другую
    {
        public float w;
        private Node from;
        private Node to;

        public NodeLink(Node from, Node to, float w)
        {
            this.w = w;
            this.from = from;
            this.to = to;
           // Debug.Log("link from:" + from.crd.x + "," + from.crd.z + " to:" + to.crd.x + "," + to.crd.z + " w:"+ w);
        }

        public Node To
        {
            get
            {
                return to;
            }
        }

        public bool LinkedTo(Node node)
        {
            if(node == to)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void UpdateW(float extra)
        {
            w += extra;
        }
        
    }
}
