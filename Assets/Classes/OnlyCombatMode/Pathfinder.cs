using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{	// класс объекта отвечающего за поиск пути на карте, реализует метод поиска А* в двухмерном массиве
	public class Pathfinder{
		public List<Node> FindPath(Node startNode, Node targetNode){//непосредственно метод поиска пути на карте от начальной точки до коненчо 
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
				foreach(Node node in GetNeigbours(currentNode)){// получаем точки соседние с проверяемой
					if(closeSet.Contains(node)) // если сосед уже есть в списке провереных, пропускаем итерацию и переходим к следующему
						continue;
					if (node.isWalkable && !node.busy) { // если сосед доступен для передвижения и не занят другим юнитом
                        
                        if (openSet.Contains (node)) {// если сосед уже есть в списке на проверку
 							if (node.gCost > currentNode.gCost + currentNode.links.Find(x => x.To == node).w) {// если расстояние от старта до клетки в списке больше чем до соседа
								node.parentNode = currentNode;//устанавливаем ей ссылку на проверяемую клетку
								node.gCost = currentNode.gCost + currentNode.links.Find(x => x.To == node).w; // задаем расстояние от старта соседа, так как оно меньше 
                                node.hCost = GetHeuristicPathLength(node, targetNode);
                                node.refreshFCost(); // пересчитываем общий путь
							}
						} else {// если соседа нет в списке на проверку
                            node.gCost = currentNode.gCost + currentNode.links.Find(x => x.To == node).w; // увеличиваем расстояние от стартовой точки до соседа на 1
                            node.hCost = GetHeuristicPathLength(node, targetNode); // вычисляем приблизительное расстояние от соседа до конечной точки
                            node.refreshFCost(); //пересчитываем общее расстояние
                            
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
			return Mathf.Max(Mathf.Abs(startNode.crd.x - targetNode.crd.x), Mathf.Abs(startNode.crd.z - targetNode.crd.z), Mathf.Abs(startNode.floor.number - targetNode.floor.number));
		}
		private List<Node> GetNeigbours(Node node){// метод получения соседних точек
			List<Node> result = new List<Node> ();
			for(int i = 0; i < node.links.Count; i++)
            {
                result.Add(node.links[i].To);
            }
            return result;
		}


	}
}