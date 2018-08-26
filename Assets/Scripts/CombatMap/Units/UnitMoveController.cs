using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using Units;
using Game;
	
	// скрипт реализующий движение юнита по карте
public class UnitMoveController : MonoBehaviour {
	private Vector3 actualPosition;// позиция в которой юнит должен находится, актуальная
	private float speed = 2f; // скорость движения
	private List<Node> path; // путь по котрому юнит будет двигаться в случае получения такой команды
	private UnitController unitController;// ссылка на компоненот контроллера юнита
	private bool moving; // двигается ли юнит в данный момент
	public Node queryNode; // точка в которую юнит двигается
	void Start () {
		moving = false;// юнит стоит
		path = new List<Node>();// ему никуда идти не нужно
		unitController = transform.GetComponent<UnitController> ();
		actualPosition = this.transform.position;// позиция верна

	}
		
	void Update () {
		if ((path.Count > 0) && (!moving)) {// если в пути есть точки и юнит не двигается
			MoveToNode (path [0]); // вызов метода изменения актуальной позиции
		}
		//this.transform.Translate(0,0,Time.deltaTime);
		if (!GameController.instance.pauseMoving) {// если двежение не стоит на паузе, в будущем понадобится для реализации некоторых скилов
			if (Vector3.Distance (this.transform.position, actualPosition) > 0.01f) {// если дистанция до актуальной посиции больше 0		
				this.transform.position = Vector3.MoveTowards(this.transform.position, actualPosition, speed * Time.deltaTime); // то двигаться из текущей позиции в актуальную
			} else {// если позиция актуальна, то запомнить что юнит не двигается
				moving = false;

			}
		}
	}
	private void MoveToNode(Node incomingNode){// метод изменения позиции 
			moving = true; // запомнить что юнит уже движется
			actualPosition = new Vector3 (incomingNode.x, this.transform.position.y, incomingNode.y); //установить новую актуальную позицию
			this.transform.LookAt (actualPosition);//повернуться к точке, позже нужно будет переделать чтобы поворот был плавным
			path.RemoveAt (0);// удалить из пути точку к которой пошел юнит
			unitController.unit.AP--;// отнять одно AP 
			if (path.Count == 0) { // если  в пути точек для движения больше нет, то остановить юнит в актуальной позиции
				EndMoving (incomingNode);// вызвать метод обработки конца движения
			}
			if (unitController.unit.AP == 0) {// если у юнита закончились AP
				EndMoving (incomingNode); // вызвать метод обработки конца движения
				GameController.instance.EndTurn (); // закончить ход юнита
			}
	}
	public void EndMoving(Node endNode){// метод обработки остановки юнита
		unitController.unit.node = endNode; // запомнить в какой логической клетке теперь стоит юнит, нужно для корректного взаимодействия с укрытиями и корректного поиска путей
		MapManager.instance.ClearAllPathFromActivNode ();// отчистить кэш путей у ControllZoneController'ов
		GameController.instance.blockPathfinding = false; //разблокировать поиск путей
		path.Clear ();// отчистить путь движения юнита, на тот случай если AP закончились до окончания конечной точки пути
		GameController.instance.cameraTarget.UnchainFromUnit ();// отцепить камеру от юнита
		if (queryNode != endNode) {// если юнит остановился не в той точке в которую шел
			queryNode.FreeNode (); // сказать что конечная точка осталось не занятой
			endNode.OccupyNode (); // занять точку в которой остановился
		} else {
			queryNode.OccupyNode (); // занять конечную точку
		}
		Debug.Log (unitController.unit.AP + "pos: "+unitController.unit.node.x+","+unitController.unit.node.y);
	}
	public void SetMovingPath(List<Node> incomingPath, Node queryNode){// метод задает маршрут движения юнита, и точку в которую он идет
		incomingPath.RemoveAt(0);// удаляет первую точку движения, т.к она является стартовой
		path = incomingPath;// задать путь движения юниту
		this.queryNode = queryNode;// сохранить точку в которую идет юнит
		GameController.instance.cameraTarget.ChainToUnit (this.gameObject);//Закрепить камеру на юните
		GameController.instance.blockPathfinding = true; //заблокировать поиск пути, чтобы пути не искались и не отображались пока юнит движется 
		unitController.unit.node.FreeNode ();// освободить клетку в которой стоит юнит
	}
}
