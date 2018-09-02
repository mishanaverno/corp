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
        }
        public void refreshFCost(){ // метод вычисляющий fCost, должен вызыватся при изменении hCost или gCost
			fCost = hCost + gCost;
		}
        public void GenerateCell(){// метод генерации клетки
            Vector3 position = new Vector3(crd.x, floor.number, crd.z);
            GameObject cellInstance = GameObject.Instantiate (Resources.Load("Stage/" + floor.stage.DesignName + "/GroundNode/0/Cell"), position, Quaternion.identity) as GameObject;
            cellInstance.transform.name = "cell-[" + position.x + "," + position.z + "]:" + position.y;
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
