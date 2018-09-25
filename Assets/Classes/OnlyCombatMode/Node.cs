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
		public Node parentNode; // ссылка на клетку из которой пришел Pathfinder, при поиске пути
        [SerializeField]
        public bool isWalkable; // проходима ли клетка для юнитов
        [SerializeField]
        public bool busy; // занята ли клетка каким-либо юнитом
        [SerializeField]
        public GameObject Cell; // ссылка на объект в сцене, представляющий клетку
        [SerializeField]
        public List<NodeLink> links;
        [SerializeField]
        public Floor floor;
        public string order = "Default";
        public string surface = "Ground";
        public int prefabNumber = 0;
        public string direction = "r";
        public List<NodeLayer> Layers; 
 
        public Node(int x, int z, Floor floor, bool movable)
        {
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
        }
        public void refreshFCost(){ // метод вычисляющий fCost, должен вызыватся при изменении hCost или gCost
			fCost = hCost + gCost;
		}
        public void GenerateCell(){// метод генерации клетки
            GenerateSurface();
            GenerateLayers();
        }
        private Vector3 GetRotation()
        {
            Vector3 rotation;
            switch (this.direction)
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
                default:
                    rotation = new Vector3(0, 0, 0);
                    break;
            }
            return rotation;
        }
        private void GenerateSurface()
        {
            Vector3 rotation = GetRotation();
            Vector3 position = new Vector3(crd.x, floor.number, crd.z);
            GameObject cellInstance = GameObject.Instantiate (Resources.Load("Stage/" + this.floor.stage.DesignName + "/Premetives/Surface/" + this.surface + "/" + this.order + "/" + this.prefabNumber), position, Quaternion.Euler(rotation)) as GameObject;
            cellInstance.transform.name = "cell-[" + position.x + "," + position.z + "]:" + position.y;
            Cell = cellInstance;
        }
        private void GenerateLayers()
        {
            float pPY = 0;
            float pSY = 1;
            float cSY;
            for(int i = 0; i < Layers.Count; i++)
            {
                GameObject Instance = this.CreateLayer(Layers[i]);
                Instance.transform.parent = Cell.transform;
                cSY = Instance.transform.localScale.y;
                float cPY = pPY + pSY + ((cSY - pSY) / 2);
                
                Vector3 cPos = Instance.transform.localPosition;
                cPos.y = cPY;
                Instance.transform.localPosition = cPos;
                pPY = cPY;
                pSY = cSY;
            }
        }
        private GameObject CreateLayer(NodeLayer layer)
        {
            Vector3 rotation = GetRotation();
            UnityEngine.Object prefab = Resources.Load("Stage/" + this.floor.stage.DesignName + "/Premetives/" + layer.premitive + "/" + layer.name + "/" + this.order + "/" + layer.prefabNumber);
            Vector3 position = new Vector3(crd.x, floor.number, crd.z);
            GameObject LayerInstance;
            return LayerInstance = GameObject.Instantiate(prefab ,position, Quaternion.Euler(rotation)) as GameObject;
        }
        public void ChangeSurface(string surface)
        {
                this.surface = surface;
        }
        //ПЕРЕПИСАТЬ->

       
		public Shelters shelters; // поле для хранения данных об укрытиях
		public void InitShelters(int[,,] incomingMap){ //  метод инициирующий, проверяющий соседние клетки, и сохраняющий данные об укрытиях в переменную shelters
			shelters = new Shelters (); // инициализация экземпляра класса
			if (this.x < incomingMap.GetLength (1)-1) {  
				if (incomingMap [this.level, this.x + 1, this.z] < 3) 
					this.shelters.bot = incomingMap [this.level, this.x + 1, this.z];
				
			}
			if (this.x > 0) {
				if (incomingMap [this.level, this.x - 1, this.z] < 3)
					this.shelters.top = incomingMap [this.level, this.x - 1, this.z];
			}
			if (this.y < incomingMap.GetLength (2)-1) {
				if (incomingMap [this.level, this.x, this.z + 1] < 3)
					this.shelters.right = incomingMap [this.level, this.x, this.z + 1];
			}
			if (this.y > 0) {
				if (incomingMap [this.level, this.x, this.z - 1] < 3)
					this.shelters.left = incomingMap [this.level, this.x, this.z - 1];
			}
			

		}
        //<-ПЕРЕПИСАТЬ
        public void LinkNode(Node node, float w) {
            NodeLink link = new NodeLink(this, node, w);
            if (!this.links.Contains(link))
            {
                this.links.Add(link);
            }
        }
        public void UnlinkNode(Node node)
        {
        }
        public void OccupyNode(){ // метод осуществляющий занятие клетки юнитом, должен вызываться при остановке юнита в клетке 
	    	busy = true; // не дает другим юнитам стать сюда
	    	Cell.GetComponent<CellController> ().ShowCellShelters (); // отображает значки укрытий в сцене
	    }
	    public void FreeNode(){ // метод осуществляющий освобождение клетки юнитом, должен вызываться при начале движения, или смерти
	    	busy = false; // разрешает занимать клетку
	    	Cell.GetComponent<CellController> ().HideCellShelters (); // скрывает значки укрытий
	    }
	}
    public class Shelters
    { // класс описывающий модель данных укрытий для юнита находящегося в текущей клетке
        public int top; // верх по X
        public int bot; // низ по X
        public int left; // верх по Y
        public int right; // низ по Y
    }
}
