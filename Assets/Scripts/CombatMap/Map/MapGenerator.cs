using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Units;

namespace Map{
	//скрипт отвечающий за генерацию карты 
public class MapGenerator : MonoBehaviour {
		private int[,,] incomingMap; // получаемая извне карта высот типа int
		public Node[,,] map; // непосредственно игровая карта клеток, с ней в последствии будут проводится все логические операции
		void Start () {
			incomingMap = GameManager.instance.incomingMap; // получение карты высот из Game Managera
			CameraMoving.instance.SetMapLengths (incomingMap.GetLength (1), incomingMap.GetLength (2));// передаем в скрипт камеры размерность игрового поля
			map = new Node[incomingMap.GetLength (0), incomingMap.GetLength (1), incomingMap.GetLength (2)]; // инициализация массива с клетками с размерностью карты высот
 			//RenderGamefield (incomingMap); // вызов методда отображения игровых клеток в сцене
			MapManager.instance.map = map; // передает полученую карту в MapManager
			MapManager.instance.enabled = true; // делает MapManager актвным
			Destroy (this); // уничтожает себя
		}
	/*
		private void RenderGamefield(int[,,] incomingMap){// метод отображения карты, позже будет переделан
			int levelCount = incomingMap.GetLength (0);
			int sizeY = incomingMap.GetLength (2);
			int sizeX = incomingMap.GetLength (1);
			for (int z = 0; z < levelCount; z++) {
				for (int x = 0; x < sizeX; x++) {
					for (int y = 0; y < sizeY; y++) {
						if (incomingMap [z, x, y] == 0) {
							GenerateCell (GameManager.instance.floorPrefab, new Vector3 (x, z*3, y), true);
						} else if (incomingMap [z, x, y] == 1) {
							GenerateCell (GameManager.instance.halfShelterPrefab, new Vector3 (x, z*3, y), false);
						} else if (incomingMap [z, x, y] == 2) {
							GenerateCell (GameManager.instance.fullShelterPrefab, new Vector3 (x, z*3, y), false); 
						} else if (incomingMap [z, x, y] == 3) {
							GenerateCell (GameManager.instance.WallPrefab, new Vector3 (x, z*3, y), false);
						} else if (incomingMap [z, x, y] == 4) {
							GenerateCell (GameManager.instance.portalPrefab, new Vector3 (x, z*3, y), true);
						}

					}
				}
			}
		}

		private void GenerateCell(GameObject prefab, Vector3 position, bool movable){// метод генерации клетки
			GameObject cellInstance = Instantiate (prefab, position, Quaternion.identity, this.transform) as GameObject; 
			cellInstance.transform.name = "cell-[" + position.x + "," + position.z + "]:" + position.y;
			Node node = new Node(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z), Mathf.RoundToInt(position.y), movable);
			node.Cell = cellInstance;
			CellController cellController = cellInstance.GetComponent<CellController> ();
			cellController.node = node;
			if (node.isWalkable) {
				node.InitShelters (incomingMap);
				cellController.SetShelters ();
			}

			this.map[node.level,node.x,node.y] = node;
		}
        */
	
		public static MapGenerator instance;
		void Awake(){
			instance = this;
		}
		public static MapGenerator GetInstance(){
			return instance;
		}
	}
}
