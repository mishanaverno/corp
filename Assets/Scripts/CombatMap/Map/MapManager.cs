using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;
using Game;

namespace Map
{
	// скрипт управления игровым полемSS
public class MapManager : MonoBehaviour {
		public Node[,,] map;
		public Pathfinder pathfinder;
		public Node activeUnitNode;
        public Transform stageObject;
		void OnEnabled(){
            stageObject = GetComponent<Transform>();
		}
	// Use this for initialization
		void Start () {
            CameraMoving.instance.SetMapLengths(Stage.GetStage().rct.Height, Stage.GetStage().rct.Width);
			pathfinder = new Pathfinder();
			activeUnitNode = Stage.GetNode(new CRD(0,0));
			//UnitsGenerator.instance.enabled = true;	
		}

		public List<Node> GetPathFromActivUnit(Node targetNode){ // метод получения кратчайшего пути
            return pathfinder.FindPath(activeUnitNode, targetNode);
		}
		public void ClearAllPathFromActivNode(){ // метод вызова метода отчистки кэшированых путей у всех клеток карты
			transform.BroadcastMessage ("CleatPathFromActivUnit");
		}
		
		public static MapManager instance;
		void Awake(){
			instance = this;
		}
		public static MapManager GetInstance(){
			return instance;
		}

	}
}