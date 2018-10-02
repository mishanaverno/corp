using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    abstract public class MapElement// основной абстрактный логический элемент карты
    {
        public List<MapElement> childElements; //дочерние конкретные элементы
        public MapElement parentElement;  //конкретный родительский элемент
        public List<Node> childNodes;     //узлы карты пренадлежащие этому элементу
        public RCT rct;                   //прямоугольник на карте
        public string surface = "Ground"; // название поверхности передаваемое узлам
        public static int iterator = 0;   //итератор для установки id
        public int id;                    //порядковый номер
        private List<NodeLayer> layers = new List<NodeLayer>(); //слои передаваемые узлам

        public MapElement(RCT rct)
        {
            childElements = new List<MapElement>();
            childNodes = new List<Node>();
            this.rct = rct;
            id = iterator;
            iterator++;
        }
        public void moveNodesFromMapElementToThis(MapElement from) //передает узлы от указаного елемнта текущему
        {
            for (int i = 0; i < from.childNodes.Count; i++)
            {
                Node node = from.childNodes[i];
                if (rct.isContainCRD(node.crd))
                {  
                    moveNode(i, from, this);
                    i--;
                }
            }
            setNodeDirections();
        }
        public void moveNode(int index, MapElement from, MapElement to) //передача узла
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
        public bool isEmpty()//проверяет прямоугольник элемента на пустоту
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
        
        public virtual void setNodeDirections()// виртуальный метод по необходимости переопределяемый конкретными классами
        {                                       // устанавливает узлам направление параметр direction

        }
        public void UpgradeChildElements()//Усложняет дочерние элементы рекурсивно
        {
            for(int i = 0; i < childElements.Count; i++)
            {
                childElements[i].Upgrade();
                childElements[i].UpgradeChildElements();
            }
        }
        public virtual void Upgrade()// виртуальный метод по необходимости переопределяемый конкретными классами
        {                            // Усложняет елемент, т.е. добавляет более мелкие дочерние элементы и передает им узлы 
           
        }
        public void addNewElements(List<MapElement> newMapElements)// добавляет елементу список дочерних
        {
            for (int i = 0; i < newMapElements.Count; i++)
            {
                addNewElement(newMapElements[i]);
            }
            
        }
        public void addNewElement(MapElement newMapElement)// добавляет елементу один дочерний
        {
            newMapElement.parentElement = this;
            newMapElement.OnAddToChildElements();
            newMapElement.moveNodesFromMapElementToThis(this);
            childElements.Add(newMapElement);
        }
        public void AddLayer(NodeLayer layer)// добавляет елементу объект слой узла
        {
            this.layers.Add(layer);
        }
        public void RemoveLayer(NodeLayer layer)// удаляет у елемента объект слой узла
        {
            this.layers.Remove(layer);
        }
        public void RemoveLayer(int index)
        {
            this.layers.RemoveAt(index);
        }
        
        public void ProcessLayersChildElements(List<NodeLayer> layers)// рекурсивная обработка слоев всех елементов  
        {
            List<NodeLayer> newLayers = BeforeProcessLayers(layers);
            ProcessLayers(newLayers);
            for (int i = 0; i < childElements.Count; i++)
            {
                childElements[i].ProcessLayersChildElements(newLayers);
                
            }

        }
        public void ProcessLayers(List<NodeLayer> layers)// добавление слоев узлам
        {

            for (int i = 0; i < childNodes.Count; i++)
            {
                childNodes[i].Layers.AddRange(layers);
            }
        }
        public virtual List<NodeLayer> BeforeProcessLayers(List<NodeLayer> layers)// виртуальный метод по необходимости переопределяемый конкретными классами
        {                                                                         // обработка слоев перед добавление узлам
            List<NodeLayer> newLayers = new List<NodeLayer>();
            newLayers.AddRange(layers);
            newLayers.AddRange(this.layers);
            return newLayers;
        }
        public virtual void OnAddToChildElements()// виртуальный метод по необходимости переопределяемый конкретными классами
        {                                         // метод вызываемый перед добавлением елемента к дочерним, после него к нему добавляются узлы
           
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
