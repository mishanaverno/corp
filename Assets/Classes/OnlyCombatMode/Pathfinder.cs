using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{	// класс объекта отвечающего за поиск пути на карте, реализует метод поиска А* в двухмерном массиве
	public class Pathfinder{
		public List<Node> FindPath(Node startNode, Node targetNode, Node[,,] map){//непосредственно метод поиска пути на карте от начальной точки до коненчо 
			List<Node> openSet = new List<Node> ();// список клеток которые нужно проверить
			List<Node> closeSet = new List<Node> ();// список уже проверенных клеток
			startNode.parentNode = null; // инициализация параметров стартовой клетки
			startNode.gCost = 0; //расстояние от начала 0
			startNode.hCost = GetHeuristicPathLength (startNode, targetNode); // получение расстояния до конечнной клетки без учета препятствий
			startNode.refreshFCost (); // пересчет суммы расстояний
			openSet.Add (startNode); // добавление в список стартовой точки

			while (openSet.Count > 0) { // цикл проверки клеток пока окрытый список не будет пуст
				Node currentNode = GetMinFCost (openSet); // получение клетки с минимальным общим расстоянем до конечной точки из открытого списка
				if (currentNode == targetNode) { // если проверяемая клетка и есть конечная 
					return GetPathFromNode (currentNode);		// то получаем путь и выходим из функции
				}
				openSet.Remove (currentNode); //удаляем из списка ожидающих проверку проверяемую клетку 
				closeSet.Add (currentNode); //и записываем ее в список провереных
				foreach(Node node in GetNeigbours(currentNode,targetNode,map)){// получаем точки соседние с проверяемой
					if(closeSet.Contains(node)) // если сосед уже есть в списке провереных, пропускаем итерацию и переходим к следующему
						continue;
					if (node.isWalkable && !node.busy) { // если сосед доступен для передвижения и не занят другим юнитом
						node.gCost = currentNode.gCost + 1; // увеличиваем расстояние от стартовой точки до соседа на 1
						node.hCost = GetHeuristicPathLength (node, targetNode); // вычисляем приблизительное расстояние от соседа до конечной точки
						node.refreshFCost (); //пересчитываем общее расстояние
						if (openSet.Contains (node)) {// если сосед уже есть в списке на проверку
							Node nodeAlredyInList = openSet.Find (n => n == node); //получаем клетку из открытого списка которая и есть сосед
 							if (nodeAlredyInList.gCost > node.gCost) {// если расстояние от старта до клетки в списке больше чем до соседа
								nodeAlredyInList.parentNode = currentNode;//устанавливаем ей ссылку на проверяемую клетку
								nodeAlredyInList.gCost = node.gCost; // задаем расстояние от старта соседа, так как оно меньше 
								nodeAlredyInList.refreshFCost(); // пересчитываем общий путь
							}
						} else {// если соседа нет в списке на проверку
							node.parentNode = currentNode; // то сохраняем ссылку на проверяемую клетку
							openSet.Add (node); // и добавляем соседа в список на проверку
						}
					}
				}
			}
			//если цикл не нашел путь до конечной точки
			List<Node> empt = new List<Node> ();// создаем пустой список точек(клеток)
			empt.Add (targetNode); // добавляем в него только конечную точку, понадобится для подсветки клетки к которой нет пути
			return empt; // возвращаем список
		}
		private List<Node> GetPathFromNode (Node node){// восспроизводит путь по ссылкам от точки к точке из которой в нее перешел  pathfinder, 
			List<Node> result = new List<Node> ();
			Node currentNode = node;
			while (currentNode != null) {
				result.Add (currentNode);
				currentNode = currentNode.parentNode;
			}
			result.Reverse ();
			return result;
		}
		private Node GetMinFCost(List<Node> list){// метод получения из мписка клетки с минимальным значением общего пути
			float min = float.MaxValue;
			Node minNode = null;
			foreach (Node node in list) {
				if(node.fCost<min){
					min = node.fCost;
					minNode = node;
				}
			}

			return minNode;
		}
		private float GetHeuristicPathLength(Node startNode, Node targetNode){// метод получения пути до конечной точки игнорируя препятствия
			return Mathf.Max(Mathf.Abs(startNode.x - targetNode.x),Mathf.Abs(startNode.y - targetNode.y));
		}
		private List<Node> GetNeigbours(Node node, Node target,Node[,,] map){// метод получения соседних точек
			List<Node> result = new List<Node> ();
			bool nx = false;
			bool ny = false;
			Node neighbour = null;

			if (node.x > 0) {
				neighbour = MapManager.instance.GetNode (node.level, node.x - 1, node.y);
				result.Add (neighbour);
				nx = neighbour.isWalkable;
			}
			if (node.y > 0) {
				neighbour = MapManager.instance.GetNode (node.level, node.x, node.y - 1);
				result.Add (neighbour);
				ny = neighbour.isWalkable;
			}
			if(nx && ny) result.Add(MapManager.instance.GetNode(node.level,node.x-1,node.y-1));
			nx = false;
			if (node.x < map.GetLength (1) - 1) {
				neighbour = MapManager.instance.GetNode (node.level, node.x + 1, node.y);
				result.Add (neighbour);
				nx = neighbour.isWalkable;
			}
			if(nx && ny) result.Add(MapManager.instance.GetNode(node.level,node.x+1,node.y-1));
			ny = false;
			if (node.y < map.GetLength (2) - 1) {
				neighbour = MapManager.instance.GetNode (node.level, node.x, node.y + 1);
				result.Add (neighbour);
				ny = neighbour.isWalkable;
			}
			if(nx && ny) result.Add(MapManager.instance.GetNode(node.level,node.x+1,node.y+1));	
			nx = false;
			if (node.x > 0) {
				neighbour = MapManager.instance.GetNode (node.level, node.x - 1, node.y);
				nx = neighbour.isWalkable;
			}
			if(nx && ny) result.Add(MapManager.instance.GetNode(node.level,node.x-1,node.y+1));	
			return result;
		}


	}
}