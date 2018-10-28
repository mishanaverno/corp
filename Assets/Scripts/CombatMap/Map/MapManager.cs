using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;
using Game;

namespace Map
{
	// скрипт управления игровым полем
public class MapManager : MonoBehaviour {
		public Node[,,] map;
		public Pathfinder pathfinder;
		private Node activeUnitNode;

		void OnEnabled(){
			map = new Node[MapGenerator.instance.map.GetLength (0), MapGenerator.instance.map.GetLength (1), MapGenerator.instance.map.GetLength (2)];
			map = MapGenerator.instance.map;
		}
	// Use this for initialization
		void Start () {
			pathfinder = new Pathfinder();
			activeUnitNode = Stage.GetNode(new CRD(0,0));
			UnitsGenerator.instance.enabled = true;	
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