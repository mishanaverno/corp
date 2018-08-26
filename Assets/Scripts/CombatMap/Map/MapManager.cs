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
		public Painter painter;
		public Pathfinder pathfinder;
		private Node activeUnitNode;
		private Node testNode;
		void OnEnabled(){
			map = new Node[MapGenerator.instance.map.GetLength (0), MapGenerator.instance.map.GetLength (1), MapGenerator.instance.map.GetLength (2)];
			map = MapGenerator.instance.map;
		}
	// Use this for initialization
		void Start () {
			painter = new Painter (map);
			pathfinder = new Pathfinder();
			activeUnitNode = GetNode (0, 0, 0);
			UnitsGenerator.instance.enabled = true;	
		}

		public List<Node> GetPathFromActivUnit(Node targetNode){ // метод получения кратчайшего пути
			return pathfinder.FindPath (UnitsController.instance.activeUnit.node, targetNode, this.map);
		}
		public void ClearAllPathFromActivNode(){ // метод вызова метода отчистки кэшированых путей у всех клеток карты
			transform.BroadcastMessage ("CleatPathFromActivUnit");
		}
		public Node GetNode(int level, int x, int y){ //метод получения клетки по кординатам
			if (level < 0 || level >= map.GetLength (0))
				return null;
			if (x < 0 || x >= map.GetLength (1))
				return null;
			if (y < 0 || y >= map.GetLength (2))
				return null;
			return map [level, x, y];
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