using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{						// класс описывающий модель данных клетки поля, хранит все данные о состоянии клетки
    public class Node {
        [SerializeField]
        public string name;
        public CRD crd;
        public int x; // позиция по оси X
        public int z; // позиция по оси Z
        public int y; // УДАЛИТЬ
		public int level; //этаж
		// переменные необходимые для поиска пути из точки в которой стоит юнит, далее стартовая точка, в ту на которую наведен курсор, даллее конечная точка
		public float hCost; // приблизительное расстояние до конечной точки
		public float gCost; // расстояние от стартовой точки
		public float fCost; // сумма растояний от стартовой, до конечной точек
		public Node parentNode; // удалить ссылка на клетку из которой пришел Pathfinder, при поиске пути
        [SerializeField]
        public bool isWalkable; // проходима ли клетка для юнитов
        [SerializeField]
        public bool busy; // занята ли клетка каким-либо юнитом
        [SerializeField]
        public GameObject Cell; // ссылка на объект в сцене, представляющий клетку
        [SerializeField]
        public List<NodeLink> links;// ссылки на другие узлы
        [SerializeField]
        public Floor floor; // ссылка на этаж
        public string order = "Default"; //ордер архитектуры
        public string surface = "Ground"; //поверхность 
        public int prefabNumber = 0; //номер префаба
        public string direction = "r"; //направление
        public List<NodeLayer> Layers; //список слоев
        public List<Shelter> shelters; //укрытия
        public MapElement mapElement;
        public bool empty = false;
        public static int iterator = 0;
        public int id;
        public Node()
        {
            id = iterator;
            iterator++;
            empty = true;
        }
        public Node(int x, int z, Floor floor, bool movable)
        {
            id = iterator;
            iterator++;
            this.crd = new CRD(x, z);
            this.x = x;
            this.z = z;
            this.y = z;
            this.floor = floor;
            this.busy = false;
            this.isWalkable = movable;
            this.name = "cell-[" + x + "," + z + "]:" + floor.number;
            this.links = new List<NodeLink>();
            this.Layers = new List<NodeLayer>();
            this.shelters = new List<Shelter>();
        }
        public void refreshFCost(){ // метод вычисляющий fCost, должен вызыватся при изменении hCost или gCost
			fCost = hCost + gCost;
		}
        public void GenerateCell(){// метод генерации клетки
            GenerateSurface();
            GenerateLayers();
            UpdateLinks();
        }
        private void UpdateLinks()
        {
            if(mapElement.GetType() == typeof(Furniture))
            {

                Furniture furniture = mapElement as Furniture;
                List<Node> noDiagonalSiblings = GetSiblingsNoDiagonals();
                for (int i = 0; i < noDiagonalSiblings.Count; i++)
                {
                    if (!furniture.rct.isContainCRD(noDiagonalSiblings[i].crd))
                        noDiagonalSiblings[i].UnlinkNode(noDiagonalSiblings);
                }

                isWalkable = furniture.monoFurniture.Walkable;
                UnlinkAllLinksMutually();
                if (furniture.monoFurniture.Walkable)
                {
                    LinkNode(noDiagonalSiblings, 1 + furniture.monoFurniture.ExtraWeightFrom);
                    foreach(Node node in noDiagonalSiblings)
                    {
                        node.LinkNode(this, 1 + furniture.monoFurniture.ExtraWeightTo);
                    }
                }
            }
            if(mapElement.GetType() == typeof(Column))
            {
                Column column = mapElement as Column;
                List<Node> noDiagonalSiblings = GetSiblingsNoDiagonals();
                for (int i = 0; i < noDiagonalSiblings.Count; i++)
                {
                    if (!column.rct.isContainCRD(noDiagonalSiblings[i].crd))
                        noDiagonalSiblings[i].UnlinkNode(noDiagonalSiblings);
                }
                isWalkable = false;
                UnlinkAllLinksMutually();
            }
        }
        private Vector3 GetRotation()//в зависимости от направление задает угол вращения
        {
            return GetRotation(direction);
        }
        private Vector3 GetRotation(string direction)
        {
            Vector3 rotation;
            switch (direction)
            {
                case "r":
                    rotation = new Vector3(0, 0, 0);
                    break;
                case "b":
                    rotation = new Vector3(0, 90, 0);
                    break;
                case "l":
                    rotation = new Vector3(0, 180, 0);
                    break;
                case "t":
                    rotation = new Vector3(0, 270, 0);
                    break;
                case "rt":
                    rotation = new Vector3(0, -45, 0);
                    break;
                case "rb":
                    rotation = new Vector3(0, 45, 0);
                    break;
                case "lt":
                    rotation = new Vector3(0, 225, 0);
                    break;
                case "lb":
                    rotation = new Vector3(0, 135, 0);
                    break;
                default:
                    rotation = new Vector3(0, 0, 0);
                    break;
            }
            return rotation;
        }
        private void GenerateSurface()// генерация поверхности
        {   
            
            Vector3 rotation = GetRotation();
            Vector3 position = new Vector3(crd.x, floor.number * floor.height, crd.z);
            GameObject cellInstance = GameObject.Instantiate(Game.GameManager.instance.Cell, position, Quaternion.Euler(rotation)) as GameObject;
            cellInstance.transform.name = "cell-[" + position.x + "," + position.z + "]:" + position.y;
            Cell = cellInstance;
            Cell.GetComponent<CellController>().node = this;
            Cell.transform.SetParent(MapManager.instance.gameObject.transform);
            GameObject surface = GameObject.Instantiate(Resources.Load("Stage/" + this.floor.stage.DesignName + "/Premetives/Surface/" + this.surface + "/" + this.order + "/" + this.prefabNumber), position, Quaternion.Euler(rotation)) as GameObject;
            surface.transform.SetParent(Cell.transform);
        }
        private void GenerateLayers()// генерация слоев
        {
            float pPY = 0;
            float pSY = 0.05f;
            float cSY;
            for(int i = 0; i < Layers.Count; i++)
            {
                GameObject Instance = this.CreateLayer(Layers[i]);
                Instance.transform.parent = Cell.transform;
                float cPY;
                if (Layers[i].ignorePreviosMesh)
                {
                    cPY = pPY + 0.0001f;
                }
                else
                {
                    cPY = pPY + pSY + 0.0001f;
                }
                
                Vector3 cPos = Instance.transform.localPosition;
                cPos.y = cPY;
                cPos += Layers[i].positionCorrection;
                Instance.transform.localPosition = cPos;
                pPY = cPY;
                if (Layers[i].hasMesh)
                {
                    Mesh mesh = Instance.GetComponent<MeshFilter>().mesh;
                    pSY = mesh.bounds.size.y;
                }
                else
                {
                    pSY = 0;
                }
            }
        }
        private GameObject CreateLayer(NodeLayer layer)//создание слоя
        {
            if (layer.direction == "i")
            {
                layer.direction = direction;
            }
            Vector3 rotation = GetRotation(layer.direction);
            Vector3 position = new Vector3(crd.x, floor.number, crd.z);
            if (layer.premitive == "Main")
            {
                switch (layer.name)
                {
                    case "ControllQuad":
                        return GameObject.Instantiate(Game.GameManager.instance.ControllZone, position, Quaternion.Euler(rotation));
                    default:
                        break;
                }
            }
            
            if (layer.nonWalkable)
            {
                Node node = GetSibling(layer.direction);
                if (!node.empty)
                {
                    UnlinkNode(node);
                    node.UnlinkNode(this);
                }
            }
           
            UnityEngine.Object prefab = Resources.Load("Stage/" + this.floor.stage.DesignName + "/" + layer.premitive + "/" + layer.name + "/" + this.order + "/" + layer.prefabNumber);
            
            return GameObject.Instantiate(prefab ,position, Quaternion.Euler(rotation)) as GameObject;
        }
        public void ChangeSurface(string surface)
        {
                this.surface = surface;
        }
        // МЕТОДЫ СВЯЗАНЫЕ С ВЗАИМОДЕЙСТВИЕМ С MapElement
        public bool BorderWidthType(Type type)//проверяет граничит ли нода с типом MapElement
        {
            List<Node> siblings = this.GetSiblings();
            bool bordered = false;
            for (int i = 0; i < siblings.Count; i++)
            {
                if (siblings[i].mapElement.GetType() == type && siblings[i].mapElement.id != mapElement.id)
                {
                    bordered = true;
                    break;
                }
            }
            return bordered;
        }
        public MapElement GetMapElementBorderWidtType(Type type)
        {
            List<Node> siblings = this.GetSiblings();
            for (int i = 0; i < siblings.Count; i++)
            {
                if (siblings[i].mapElement.GetType() == type && siblings[i].mapElement.id != mapElement.id)
                {
                    return siblings[i].mapElement;
                }
            }
            return mapElement;
        }
        public List<MapElement> GetMapElementBorderWidtClass<T>()
        {
            List<Node> siblings = this.GetSiblings();
            List<MapElement> mapElements = new List<MapElement>();
            for (int i = 0; i < siblings.Count; i++)
            {
                if (siblings[i].mapElement is T && siblings[i].mapElement.id != mapElement.id && !mapElements.Contains(siblings[i].mapElement))
                {
                    mapElements.Add(siblings[i].mapElement);
                }
            }
            return mapElements;
        }
        public List<MapElement> GetMapElementBorderWidtClassNoDiagonals<T>()
        {
            List<Node> siblings = this.GetSiblingsNoDiagonals();
            List<MapElement> mapElements = new List<MapElement>();
            for (int i = 0; i < siblings.Count; i++)
            {
                if (siblings[i].mapElement is T && siblings[i].mapElement.id != mapElement.id && !mapElements.Contains(siblings[i].mapElement))
                {
                    mapElements.Add(siblings[i].mapElement);
                }
            }
            return mapElements;
        }
        public MapElement GetMapElementBorderWidtType(List<Type> list)
        {
            List<Node> siblings = this.GetSiblings();
            for (int i = 0; i < siblings.Count; i++)
            {
                if (list.Contains(siblings[i].mapElement.GetType())  && siblings[i].mapElement.id != mapElement.id)
                {
                    return siblings[i].mapElement;
                }
            }
            return mapElement;
        }
        public bool BorderWidthTypeNoDiagonal(Type type)
        {
            List<Node> siblings = this.GetSiblingsNoDiagonals();
            bool bordered = false;
            for (int i = 0; i < siblings.Count; i++)
            {
                if (siblings[i].mapElement.GetType() == type)
                {
                    bordered = true;
                    break;
                }
            }
            return bordered;
        }
        public List<Node> GetSiblings()// Получение соседних узлов
        {
            List<Node> siblings = new List<Node>();
            RCT rct = Stage.GetStage().rct;
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if ((z == 0 && x == 0) || !rct.isContainCRD(new CRD(crd.x + x, crd.z + z)))
                    {
                        continue;
                    }
                    siblings.Add(Stage.GetNode(crd.x + x, crd.z + z, floor.number));
                }
            }
            return siblings;
        }
        public List<Node> GetSiblingsNoDiagonals()// Получение соседних узлов
        {
            List<Node> siblings = new List<Node>();
            RCT rct = Stage.GetStage().rct;
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if ((z == 0 && x == 0) || !rct.isContainCRD(new CRD(crd.x + x, crd.z + z)) || (z != 0 && x != 0))
                    {
                        continue;
                    }
                    siblings.Add(Stage.GetNode(crd.x + x, crd.z + z, floor.number));
                }
            }
            return siblings;
        }
        public List<Node> GetMapElementSiblingds()
        {
            List<Node> siblings = new List<Node>();
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    Node node = Stage.GetNode(crd.x + x, crd.z + z, floor.number);
                    if ((z == 0 && x == 0) || (!mapElement.childNodes.Contains(node)))
                    {
                        continue;
                    }
                    siblings.Add(node);
                }
            }
            return siblings;
        }
        public List<Node> GetMapElementSiblingdsNoDiagonals()
        {
            List<Node> siblings = new List<Node>();
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    Node node = Stage.GetNode(crd.x + x, crd.z + z, floor.number);
                    if ((z == 0 && x == 0) || (!mapElement.childNodes.Contains(node)) || (z != 0 && x != 0))
                    {
                        continue;
                    }
                    siblings.Add(node);
                }
            }
            return siblings;
        }
        public int GetMapElementSiblingdsCount()
        {
            return GetMapElementSiblingds().Count;
        }
        public int GetMapElementSiblingdsNoDiagonalsCount()
        {
            return GetMapElementSiblingdsNoDiagonals().Count;
        }
        public bool IsOnMapElementBorder()
        {
            if (GetMapElementSiblingdsCount() < 8) return true;
            else return false;
        }
        public bool IsOnMapElementBorderNoDiagonals()
        {
            if (GetMapElementSiblingdsNoDiagonalsCount() < 4) return true;
            else return false;
        }
        public Node GetSibling(string direction)
        {
            Node node;
            switch (direction)
            {
                case "r":
                    node = floor.GetNode(crd.x, crd.z + 1);
                    break;
                case "l":
                    node = floor.GetNode(crd.x, crd.z - 1);
                    break;
                case "t":
                    node = floor.GetNode(crd.x - 1, crd.z);
                    break;
                case "b":
                    node = floor.GetNode(crd.x + 1, crd.z);
                    break;
                case "lt":
                    node = floor.GetNode(crd.x - 1, crd.z - 1);
                    break;
                case "rt":
                    node = floor.GetNode(crd.x - 1, crd.z + 1);
                    break;
                case "lb":
                    node = floor.GetNode(crd.x + 1, crd.z - 1);
                    break;
                case "rb":
                    node = floor.GetNode(crd.x + 1, crd.z + 1);
                    break;
                default:
                    node = new Node();
                    break;
            }
            return node;
        }
       
        public List<string> GetOuterCorners()
        {
            List<string> list = new List<string>();
            Node lNode = GetSibling("l"), rNode = GetSibling("r"), tNode = GetSibling("t"), bNode = GetSibling("b");
            Node ltNode = GetSibling("lt"), rtNode = GetSibling("rt"), lbNode = GetSibling("lb"), rbNode = GetSibling("rb");
            List<Node> nodes = GetMapElementSiblingds();
            List<Node> lnodes = new List<Node>() { rtNode, rbNode, ltNode, lbNode };
            for (int i = 0; i < lnodes.Count; i++)
            {
                if (!lnodes[i].empty)
                {
                    if (lnodes[i].mapElement.GetType() == typeof(Column))
                    {
                        Column column = lnodes[i].mapElement as Column;
                        if (!column.AsInterior) nodes.Add(lnodes[i]);
                    }
                }
            }
            
            if (nodes.Contains(lNode) && nodes.Contains(tNode) && !nodes.Contains(ltNode)) list.Add("lt");
            if (nodes.Contains(rNode) && nodes.Contains(tNode) && !nodes.Contains(rtNode)) list.Add("rt");
            if (nodes.Contains(lNode) && nodes.Contains(bNode) && !nodes.Contains(lbNode)) list.Add("lb");
            if (nodes.Contains(rNode) && nodes.Contains(bNode) && !nodes.Contains(rbNode)) list.Add("rb");
            return list;
        }
       
        public List<string> GetWall()
        {
            List<string> list = new List<string>();
            List<Node> nodes = GetMapElementSiblingdsNoDiagonals();
            Node lNode = GetSibling("l"), rNode = GetSibling("r"), tNode = GetSibling("t"), bNode = GetSibling("b");
            List<Node> lnodes = new List<Node>() { lNode, rNode, tNode, bNode };
            for(int i = 0; i < lnodes.Count; i++)
            {
                if (!lnodes[i].empty)
                {
                    if (lnodes[i].mapElement.GetType() == typeof(Column))
                    {
                        Column column = lnodes[i].mapElement as Column;
                        if (!column.AsInterior) nodes.Add(lnodes[i]);
                    }
                }
            }
            if (!nodes.Contains(lNode)) list.Add("l");
            if (!nodes.Contains(rNode)) list.Add("r");
            if (!nodes.Contains(tNode)) list.Add("t");
            if (!nodes.Contains(bNode)) list.Add("b");
            return list;
        }

        // МЕТОДЫ СВЯЗАННЫЕ СО СВЯЗЯМИ УЗЛОВ  TODO добавить брэйки в циклы
        public void LinkNode(List<Node> list, float w)
        {
            for(int i = 0; i < list.Count; i++)
            {
                LinkNode(list[i], w);
            }
        }
        public void LinkNode(Node node, float w) //создание ссылки из этого узла в указанный
        {
            if (node.empty) return;
            if (!IsLinked(node))
            {
                this.links.Add(new NodeLink(this, node, w));
            }
        }
        public void UnlinkNode(List<Node> list)
        {
            for(int i = 0; i < list.Count; i++)
            {
                UnlinkNode(list[i]);
            }
        }
        public void UnlinkNode(Node node)//удаление ссылки
        {
            if (node.empty) return;
            for (int i = 0; i < links.Count; i++)
            {
                if (links[i].LinkedTo(node))
                {
                    links.RemoveAt(i);
                    return;
                }
            }
        }
        public void UnlinkAllLinks()
        {
            links.Clear();
        }
        public void UnlinkAllLinksMutually()
        {
            for (int i = 0; i < links.Count; i++)
            {
                links[i].To.UnlinkNode(this);
            }
            UnlinkAllLinks();
        }
        public void UnlinkAllSiblingsLinks()
        {
            List<Node> siblings = GetSiblings();
            for (int i = 0; i < siblings.Count; i++)
            {
                if (IsLinked(siblings[i])) UnlinkNode(siblings[i]);
            }
        }
        public void UnlinkAllSiblingsLinksMutually()
        {
            List<Node> siblings = GetSiblings();
            for (int i = 0; i < siblings.Count; i++)
            {
                if (IsLinked(siblings[i])) {
                    UnlinkNode(siblings[i]);
                    if (siblings[i].IsLinked(this)) siblings[i].UnlinkNode(this);
                }
            }
        }
        public bool IsLinked(Node node)
        {
            bool linked = false;
            for(int i = 0;i < links.Count; i++)
            {
                if (links[i].LinkedTo(node)) linked = true;
            }
            return linked;
        }
       
        public void UpdateLinkTo(Node node, float extra)
        {
            for (int i = 0; i < links.Count; i++)
            {
                if (links[i].LinkedTo(node)) links[i].UpdateW(extra);
            }
        }
        public void UpdateLinkedSiblingsLinks(float extra)
        {
            List<Node> siblings = GetSiblings();
            for(int i = 0; i < siblings.Count; i++)
            {
                for (int l = 0; l < siblings[i].links.Count; l++)
                {
                    if (siblings[i].links[l].LinkedTo(this))
                    {
                        siblings[i].links[l].UpdateW(extra);
                    }
                }
            }
        }
        public void UpdateLinkedSiblingsLinksMutually(float extraTo, float extraFrom, bool ignoreSiblingsInSelfMapElement = false)
        {
            List<Node> siblings = GetSiblings();
            for (int i = 0; i < siblings.Count; i++)
            {
                if (ignoreSiblingsInSelfMapElement && mapElement.rct.isContainCRD(siblings[i].crd)) continue;
                for (int l = 0; l < siblings[i].links.Count; l++)
                {
                    if (siblings[i].links[l].LinkedTo(this))
                    {
                        
                        siblings[i].links[l].UpdateW(extraTo);
                        this.UpdateLinkTo(siblings[i], extraFrom);
                    }
                }
            }
        }

        /// МЕТОДЫ ВЗАИМОДЕЙСТВИЯ С ЮНИТАМИ
        public void OccupyNode(){ // метод осуществляющий занятие клетки юнитом, должен вызываться при остановке юнита в клетке 
	    	busy = true; // не дает другим юнитам стать сюда
	    	Cell.GetComponent<CellController> ().ShowCellShelters (); // отображает значки укрытий в сцене
	    }
	    public void FreeNode(){ // метод осуществляющий освобождение клетки юнитом, должен вызываться при начале движения, или смерти
	    	busy = false; // разрешает занимать клетку
	    	Cell.GetComponent<CellController> ().HideCellShelters (); // скрывает значки укрытий
	    }
        // МЕТОДЫ РАБОТЫ УКРЫТИЙ
        public void AddShelter(Shelter shelter)
        {
            for(int i = 0; i < shelters.Count; i++)
            {
                if (shelters[i] == shelter) return;
            }
            shelters.Add(shelter);
        }
        public void RemoveShelter(Shelter shelter)
        {
            for (int i = 0; i < shelters.Count; i++)
            {
                if (shelters[i] == shelter)
                {
                    shelters.RemoveAt(i);
                    return;
                }
            }
        }
        public void AddSheltersToSiblings(int height)
        {
            Node node = floor.GetNode(crd.x, crd.z - 1);
            if (!node.empty) node.AddShelter(new Shelter(height, 'r'));
            node = floor.GetNode(crd.x, crd.z + 1);
            if (!node.empty) node.AddShelter(new Shelter(height, 'l'));
            node = floor.GetNode(crd.x - 1, crd.z);
            if (!node.empty) node.AddShelter(new Shelter(height, 'b'));
            node = floor.GetNode(crd.x + 1, crd.z);
            if (!node.empty) node.AddShelter(new Shelter(height, 't'));
        }
	}
    public class Shelter
    { // класс описывающий модель данных укрытий для юнита находящегося в текущей клетке
        public int height; // высота
        public char direction;
        public Shelter(int height, char direction)
        {
            this.height = height;
            this.direction = direction;
        }
        public static bool operator ==(Shelter sh1, Shelter sh2)
        {
            if (sh1.height == sh2.height && sh1.direction == sh2.direction) return true;
            else return false;
        }
        public static bool operator !=(Shelter sh1, Shelter sh2)
        {
            if (sh1.height == sh2.height && sh1.direction == sh2.direction) return false;
            else return true;
        }
    }
}
