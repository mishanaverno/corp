using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using Game;
	// скрипт генерирующий юнитов 
namespace Units{
	public class UnitsGenerator : MonoBehaviour {
		public List<Unit> playerUnits = new List<Unit>();
		public List<Unit> enemyUnits = new List<Unit>();
		private List<Unit> allUnits = new List<Unit>();
		Node node; 
		void Start () {
			int startx = 0;
			int starty = 0;
			foreach (Unit playerUnit in playerUnits) {
				GeneratePlayerUnit (GameManager.instance.Unit, startx, starty, MapManager.instance.map, playerUnit);
				startx++;
				starty++;
			}
			GenerateEnemyUnit (GameManager.instance.EnemyUnit, 3, 6, 0, MapManager.instance.map, enemyUnits [0]);

			UnitsController.instance.enabled = true;
			GameController.instance.allUnits = allUnits;
			Destroy (this);
		}

		private void GenerateEnemyUnit(GameObject prefab, int posX, int posY, int level, Node[,,] map, Unit unit){
			node = map [level, posX, posY];
			node.OccupyNode ();
			Vector3 position = new Vector3 (node.x, node.level + 0.85f, node.y);
			GameObject unitInstance = Instantiate (prefab, position, Quaternion.identity, this.transform);
			unit.model = unitInstance;
			unit.node = node;
			unitInstance.transform.GetComponent<UnitController>().unit = unit;
			allUnits.Add (unit);
		}
		private void GeneratePlayerUnit(GameObject prefab, int posX, int posY, Node[,,] map, Unit unit){
			node = map [0, posX, posY];
			node.OccupyNode ();
			Vector3 position = new Vector3 (node.x, node.level + 0.85f, node.y);
			GameObject unitInstance = Instantiate (prefab, position, Quaternion.identity,this.transform);
			unit.model = unitInstance;
			unit.node = node;
			unitInstance.transform.GetComponent<UnitController> ().unit = unit;
			allUnits.Add (unit);
		}
		public static UnitsGenerator instance;
		void Awake(){
			instance = this;
		}
		public static UnitsGenerator GetInstance(){
			return instance;
		}

	}
}