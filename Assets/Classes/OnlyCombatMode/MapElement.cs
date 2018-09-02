using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapElement{
        public List<MapElement> childElements;
        public MapElement parentElement;
        public List<Node> childNodes;
        public RCT rct;
        public static int iterator = 0;
        public int id;

        public MapElement(RCT rct)
        {
            childElements = new List<MapElement>();
            childNodes = new List<Node>();
            this.rct = rct;
            id = iterator;
            iterator++;
        }
        public MapElement GetFirstParent()
        {
            MapElement element = this;
            MapElement parent = this.parentElement;
            while (parentElement != null)
            {
                element = parent;
                parent = parent.parentElement;
            }
            return element;
        }
        public MapElement GetParent()
        {
            return this.parentElement;
        }
        public void moveNodesFromMapElementToThis(MapElement from) 
        {
            Debug.Log("MOVING:");
            for (int i = 0; i < from.childNodes.Count; i++)
            {
                Node node = from.childNodes[i];
                if (rct.isContainCRD(node.crd))
                {  
                    moveNode(i, from, this);
                    i--;
                }
            }
        }
        public void moveNode(int index, MapElement from, MapElement to)
        {
            to.childNodes.Add(from.childNodes[index]);
            from.childNodes.RemoveAt(index);
        }
        public void moveNode(Node node, MapElement from, MapElement to)
        {
            int index = from.childNodes.IndexOf(node);
            to.childNodes.Add(from.childNodes[index]);
            from.childNodes.RemoveAt(index);
        }
        public bool isEmpty()
        {
            if (this.rct.Width == 0 || this.rct.Height == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void NodesToConsole()
        {
            Debug.Log("ELEMENT " + this.ToString() + " ID:" + this.id + " w: " + this.rct.Width + " h: " + this.rct.Height + " sq: " + this.rct.sq);
            for(int i = 0; i < this.childNodes.Count; i++)
            {
                Debug.Log(this.childNodes[i].name);
            }

        }
    }
   
   

}
