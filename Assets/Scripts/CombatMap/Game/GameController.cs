using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using Units;

namespace Game{
	// Скрипт управления игрой на тактической карте
	public class GameController : MonoBehaviour {
		public CameraMoving cameraTarget;
		public bool combatMode;
        private string gameMode; 
		public int turnNumber;
		public int activeUnitIndex;
		public List<Unit> allUnits = new List<Unit>();
		public bool pauseMoving = false;
		public bool blockPathfinding = false;
        public int activFloorNumber = 0;
        // Use this for initialization
        void Start () {
			turnNumber = 1;
			combatMode = true;
			activeUnitIndex = 0;
		}
		
		// Update is called once per frame
		void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                ChangeActiveFloor(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeActiveFloor(1);
            }
        }
        public static void ChangeActiveFloor(int floorNumber)
        {
            if (GameController.instance.activFloorNumber != floorNumber)
            {
                Debug.Log(floorNumber);
                GameController.instance.activFloorNumber = floorNumber;
                MapManager.instance.gameObject.transform.BroadcastMessage("ChangeActiveFloor");
            }
        }
		public void EndTurn(){// метод обработки конца хода
			if (activeUnitIndex == allUnits.Count) {
				activeUnitIndex = 0;
				turnNumber++;
			} 
				MapManager.instance.ClearAllPathFromActivNode ();
				UnitsController.instance.activeUnit = allUnits [activeUnitIndex];
				UnitsController.instance.activeUnit.OnNewTurn ();
				activeUnitIndex++;

			blockPathfinding = false;
			Debug.Log ("turn" + turnNumber);
			Debug.Log ("team:"+UnitsController.instance.activeUnit.team.title + " name: " + UnitsController.instance.activeUnit.unitname);
		}

		public void ConsoleLogList(List<Node> list){
			foreach (Node node in list) {
				Debug.Log (node.x + ", " + node.y);
			}
		}
		public void ConsoleLogListU(List<Unit> list){
			foreach (Unit unit in list) {
				Debug.Log (unit.unitname);
			}
		}

		public static GameController instance;
		void Awake(){
			instance = this;
		}
		public static GameController GetInstance(){
			return instance;
		}
	}
}