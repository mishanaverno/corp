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
        public string surface = "Ground";
        public static int iterator = 0;
        public int id;
        private List<NodeLayer> layers = new List<NodeLayer>();

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
            for (int i = 0; i < from.childNodes.Count; i++)
            {
                Node node = from.childNodes[i];
                if (rct.isContainCRD(node.crd))
                {  
                    moveNode(i, from, this);
                    i--;
                    //Debug.Log("MOVE NODE: "+node.name);
                }
            }
            setNodeDirections();
            //Debug.Log("Nodes moving from: "+from.ToString()+" to: "+this.ToString());
        }
        public void moveNode(int index, MapElement from, MapElement to)
        {
            from.childNodes[index].ChangeSurface(to.surface);
            to.childNodes.Add(from.childNodes[index]);
            from.childNodes.RemoveAt(index);
        }
        public void moveNode(Node node, MapElement from, MapElement to)
        {
            int index = from.childNodes.IndexOf(node);
            from.childNodes[index].ChangeSurface(to.surface);
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
        
        public virtual void setNodeDirections()
        {

        }
        public void UpgradeChildElements()
        {
            for(int i = 0; i < childElements.Count; i++)
            {
                childElements[i].Upgrade();
                childElements[i].UpgradeChildElements();
            }
        }
        public virtual void Upgrade()
        {
           
        }
        public void addNewElements(List<MapElement> newMapElements)
        {
            for (int i = 0; i < newMapElements.Count; i++)
            {
                addNewElement(newMapElements[i]);
            }
            
        }
        public void addNewElement(MapElement newMapElement)
        {
            newMapElement.parentElement = this;
            newMapElement.OnAddToChildElements();

            newMapElement.moveNodesFromMapElementToThis(this);
            childElements.Add(newMapElement);
        }
        public void AddLayer(NodeLayer layer)
        {
            this.layers.Add(layer);
        }
        public void RemoveLayer(NodeLayer layer)
        {
            this.layers.Remove(layer);
        }
        public void RemoveLayer(int index)
        {
            this.layers.RemoveAt(index);
        }
        
        public void ProcessLayersChildElements(List<NodeLayer> layers)
        {
            List<NodeLayer> newLayers = BeforeProcessLayers(layers);
            ProcessLayers(newLayers);
            for (int i = 0; i < childElements.Count; i++)
            {
                childElements[i].ProcessLayersChildElements(newLayers);
                
            }

        }
        public void ProcessLayers(List<NodeLayer> layers)
        {

            for (int i = 0; i < childNodes.Count; i++)
            {
                childNodes[i].Layers.AddRange(layers);
            }
        }
        public virtual List<NodeLayer> BeforeProcessLayers(List<NodeLayer> layers)
        {
            List<NodeLayer> newLayers = new List<NodeLayer>();
            newLayers.AddRange(layers);
            newLayers.AddRange(this.layers);
            return newLayers;
        }
        public virtual void OnAddToChildElements()
        {
           
        }
        
        public void DebugParents()
        {
            MapElement elem = this; 
            string log = "";
            do
            {
                log += elem.ToString() + " > ";
                elem = elem.parentElement;
            } while (!(elem is Stage));
            log += elem.ToString();

            Debug.Log("PARENTS LOG " + log);
        }
        public void NodesToConsole()
        {
            Debug.Log("ELEMENT " + this.ToString() + " ID:" + this.id + " w: " + this.rct.Width + " h: " + this.rct.Height + " sq: " + this.rct.sq);
            for (int i = 0; i < this.childNodes.Count; i++)
            {
                Debug.Log(this.childNodes[i].name);
            }

        }

    }
   
   

}
