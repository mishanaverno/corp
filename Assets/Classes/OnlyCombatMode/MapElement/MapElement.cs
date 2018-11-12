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
        public int floorNumber; // номер этажа
        public string surface = "Ground"; // название поверхности передаваемое узлам
        public static int iterator = 0;   //итератор для установки id
        public int id;                    //порядковый номер
        protected List<NodeLayer> layers = new List<NodeLayer>(); //слои передаваемые узлам
        public int prefabNumber = -1;
        public string order = "Default";
        public List<string> furnitureList = new List<string>();
        public float extraHeight = 0;

        public MapElement(RCT rct, int floor)
        {
            childElements = new List<MapElement>();
            childNodes = new List<Node>();
            floorNumber = floor;
            this.rct = rct;
            id = iterator;
            iterator++;
        }
        public void moveNodesFromMapElementToThis(MapElement from) //передает узлы от указаного елемнта текущему
        {
            for (int i = 0; i < from.childNodes.Count; i++)
            {
                Node node = from.childNodes[i];
                if (rct.isContainCRD(node.crd) && (floorNumber == node.floor.number || floorNumber == -1))
                {  
                    moveNode(i, from, this);
                    i--;
                }
            }
            HookAfterAddNodesToMapElement();
        }

        public void moveNode(int index, MapElement from, MapElement to) //передача узла
        {
            from.childNodes[index].ChangeSurface(to.surface);
            from.childNodes[index].mapElement = to;
            to.childNodes.Add(from.childNodes[index]);
            from.childNodes.RemoveAt(index);
        }
        public void moveNode(Node node, MapElement from, MapElement to)
        {
            int index = from.childNodes.IndexOf(node);
            moveNode(index, from, to);
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
        
        public virtual void HookAfterAddNodesToMapElement()// виртуальный метод по необходимости переопределяемый конкретными классами
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
        public void addFurniture(string name, CRD start, string furnitureDirection)//добавление мебели
        {
            Furniture furniture = new Furniture(name, start, floorNumber, furnitureDirection, getPrefabNuber());
            bool collision = false;
            for(int i = 0; i < childElements.Count; i++)
            {
                if (childElements[i].rct.checkCollision(furniture.rct))//проверка столкновений с другими дочерними элементами
                {
                    collision = true;
                    break;
                }
            }
            if (!collision && rct.isContainRCT(furniture.rct))//проверка столкновений и вписывается ли мебель в элемент
            {
                addNewElement(furniture);
            }
        }
        public void AddPeriodicalFurniture(string name, string furnitureDirection, char axis, int offset, int step)
        {
            bool[,] map = StageConstructor.createPeriodicalMap(rct,axis,offset,step);
            for(int x = 0; x < map.GetLength(0); x++)
            {
                for(int z = 0; z < map.GetLength(1); z++)
                {
                    if (map[x, z])
                    {
                        addFurniture(name, new CRD(x + rct.Start.x, z + rct.Start.z), furnitureDirection);
                    }
                }
            }
        }
        public void AddRandomFurniture(string name, string furnitureDirection, int treshold)
        {
            bool[,] map = StageConstructor.createRandomMap(rct, treshold);
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int z = 0; z < map.GetLength(1); z++)
                {
                    if (map[x, z])
                    {
                        addFurniture(name, new CRD(x + rct.Start.x, z + rct.Start.z), furnitureDirection);
                    }
                }
            }
        }
        public void AddRandomFurniture(string furnitureDirection, int treshold)
        {
            int furnitureListCount = furnitureList.Count;
            if (furnitureListCount < 0) return;
            
            bool[,] map = StageConstructor.createRandomMap(rct, treshold);
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int z = 0; z < map.GetLength(1); z++)
                {
                    if (map[x, z])
                    {
                        float rand = Random.Range(0, furnitureListCount * 100) / 100;
                        int index = Mathf.RoundToInt(rand);
                        string name = furnitureList[index];
                        addFurniture(name, new CRD(x + rct.Start.x, z + rct.Start.z), furnitureDirection);
                    }
                }
            }
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
            newMapElement.HookAddToChildElements();
            newMapElement.moveNodesFromMapElementToThis(this);
            childElements.Add(newMapElement);
        }
        public void RemoveElement(MapElement mapElement)
        {
            mapElement.parentElement = Stage.GetStage();
            mapElement.layers.Clear();
            this.moveNodesFromMapElementToThis(mapElement);
            childElements.Remove(mapElement);
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
        public int getPrefabNuber()
        {
            if(prefabNumber >= 0)
            {
                return prefabNumber;
            }
            else
            {
                return parentElement.getPrefabNuber();
            }
        }
        public void ProcessLayersChildElements(List<NodeLayer> layers)// рекурсивная обработка слоев всех елементов  
        {
            List<NodeLayer> newLayers = HookProcessLayers(layers);
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
                List<NodeLayer> newLayers = HookAddLayersToNode(layers, childNodes[i]);
                childNodes[i].Layers.AddRange(newLayers);
            }
        }
        public virtual List<NodeLayer> HookAddLayersToNode(List<NodeLayer> layers, Node node)// виртуальный метод по необходимости переопределяемый конкретными классами
        {                                                                                      // обработка слоев перед добавлением узлу
            List<NodeLayer> nodeLayers = new List<NodeLayer>(layers);
            for (int i = 0; i < nodeLayers.Count; i++)
            {
                if (nodeLayers[i].mapping)
                {
                    if(!checkMap(node.crd, nodeLayers[i].map))
                    {
                        nodeLayers.RemoveAt(i);
                    }
                }
            }
            return nodeLayers;
        }
        public bool checkMap(CRD crd, bool[,] map)
        {
            if(map[crd.x - rct.Start.x,crd.z - rct.Start.z])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual List<NodeLayer> HookProcessLayers(List<NodeLayer> layers)// виртуальный метод по необходимости переопределяемый конкретными классами
        {                                                                         // обработка слоев перед добавление узлам
            List<NodeLayer> newLayers = new List<NodeLayer>();
            newLayers.AddRange(layers);
            newLayers.AddRange(this.layers);
            return newLayers;
        }
        public virtual void HookAddToChildElements()// виртуальный метод по необходимости переопределяемый конкретными классами
        {                                         // метод вызываемый перед добавлением елемента к дочерним, после него к нему добавляются узлы
            surface = parentElement.surface;
        }
        public MapElement GetParentByClass(System.Type C)
        {
            if (GetType() == C || GetType() == typeof(Stage)) 
            {
                return this;
            }
            else
            {
                return parentElement.GetParentByClass(C);
            }
        }
        public MapElement GetChildMapElementById(int id)
        {
            for(int i = 0; i < childElements.Count; i++)
            {
                if (childElements[i].id == id)
                {
                    return childElements[i];
                }
            }
            for (int i = 0; i < childElements.Count; i++)
            {
                MapElement childFind = childElements[i].GetChildMapElementById(id);
                if(childFind.id == id)
                {
                    return childFind;
                }

            }
            return this;
            
        }
        public static void SetOrder(List<MapElement> list, string order)
        {
            for(int i = 0; i < list.Count; i++)
            {
                list[i].order = order;
            }
        }
        public void DebugParents()
        {
            MapElement elem = this; 
            string log = "";
            do
            {
                log += elem.ToString() + " > ";
                elem = elem.parentElement;
            } while (elem.GetType() != typeof(Stage));
            log += elem.ToString();

            Debug.Log("PARENTS LOG " + log);
        }
        public void DebugNodes()
        {
            Debug.Log("ELEMENT " + this.ToString() + " ID:" + this.id + " w: " + this.rct.Width + " h: " + this.rct.Height + " sq: " + this.rct.sq);
            for (int i = 0; i < this.childNodes.Count; i++)
            {
                Debug.Log(this.childNodes[i].name);
            }

        }
        public void DebugChildrens()
        {
            Debug.Log("PARENT ELEMENT " + this.ToString() + " ID:" + this.id + " w: " + this.rct.Width + " h: " + this.rct.Height + " sq: " + this.rct.sq);
            foreach(MapElement el in childElements)
            {
                Debug.Log("CHILD ELEMENT " + el.ToString() + " ID:" + el.id + " w: " + el.rct.Width + " h: " + el.rct.Height + " sq: " + el.rct.sq);
            }

        }

    }
   
   

}
