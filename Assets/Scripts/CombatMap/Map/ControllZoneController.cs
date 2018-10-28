using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Units;

namespace Map{
	// скрипт взаимодействия клетки(дочерний объект ControllZone) с курсором
	public class ControllZoneController : MonoBehaviour {

		public MeshRenderer controllZone; // компонент MeshRenderer объекта
		private List<Node> pathFromActiveUnit  = new List<Node>(); // переменная для кэшироваиня пути
		private	CellController parentCellController; // скрипт родительского объекта, самой клетки
		[SerializeField]
		private Material cursor;
		[SerializeField] 
		private Material path;
		[SerializeField] 
		private Material notAvaliable;
		void Start () {
			controllZone = this.GetComponent<MeshRenderer> ();
			parentCellController = this.GetComponentInParent<CellController>();
		}
		//взаимодействие с мышью
		void OnMouseEnter(){// при наведении
            if (GameController.instance.combatMode) {//исли включен боевой режим
                if (!GameController.instance.blockPathfinding) {//если не заблокирован GameController'ом не заблокирован поиск пути
                    
					if (pathFromActiveUnit.Count == 0) {// если в кэше нет пути
						pathFromActiveUnit = MapManager.instance.GetPathFromActivUnit (parentCellController.node);// запрос к MapManager'у на поиск пути
					}
					if (!controllZone.enabled) {// компонент MeshRendere не активен, т.е. путь не отображается
                        Debug.Log("start path");
                        for (int i=0;i< pathFromActiveUnit.Count; i++)
                        {
                            Debug.Log(pathFromActiveUnit[i].crd.x + ":" + pathFromActiveUnit[i].crd.z);
                        }
                        Debug.Log("end path");
						Painter.instance.ActivatePath (pathFromActiveUnit);// отображение пути 
						//parentCellController.ShowCellShelters ();// отображение значков укрытий
					}
				}
			}

		}
		void OnMouseExit(){// при уходе курсора
			if (GameController.instance.combatMode) {//если включен боевой режим
				if (controllZone.enabled) {// если путь отображается
					//MapManager.instance.painter.DeactivatePath (pathFromActiveUnit);// выключить отображение пути(стереть путь)
					if (!parentCellController.node.busy) { // если в клетке нет юнита 
						parentCellController.HideCellShelters ();//то скрыть значки укрытий
					}
				}
			}
		}

		void OnMouseDown(){// по клику
			if (GameController.instance.combatMode) {// если включен боевой режим 
				if (!GameController.instance.blockPathfinding) {// если не заблокирован поиск пути
					if (pathFromActiveUnit.Count > 1) {// если в пути больше одной точки(если в пути только одна точка то пути к конечной точки нет и в списке находится только конечная точка)
						//MapManager.instance.painter.DeactivatePath (pathFromActiveUnit);// выключить отображение пути(стереть путь)
						UnitsController.instance.activeUnit.model.GetComponent<UnitMoveController> ().SetMovingPath (pathFromActiveUnit, parentCellController.node);// передать путь к активному юниту для движения
					}
				}
			}
		}

		public void CleatPathFromActivUnit(){// метод отчиски кэша пути
			pathFromActiveUnit.Clear ();
		}
		public void PaintPath(){
			controllZone.material = path;
			controllZone.enabled = true;
		}
		public void PaintNotAvaliable(){
			controllZone.material = notAvaliable;
			controllZone.enabled = true;
		}
		public void PaintCursor(){
			controllZone.material = cursor;
			controllZone.enabled = true;
		}
		public void Erase(){
			controllZone.material = cursor;
			controllZone.enabled = false;
		}
 
	}
}