using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;

namespace Map{
	// класс отвечающий за прорисовку на сцене путей движения юнитов
	public class Painter {
		//хз зачем, может потом удалю
		private Node[,,] map;
		public Painter(Node[,,] map){
			this.map = map;
		}
		//рисует путь 
		public void ActivatePath(List<Node> path){
			if (path.Count == 1) {
				path [0].Cell.GetComponentInChildren<ControllZoneController> ().PaintNotAvaliable();
			} else {
				int AP = UnitsController.instance.activeUnit.AP+1;
				foreach (Node node in path) {
					if (AP > 0)
						node.Cell.GetComponentInChildren<ControllZoneController> ().PaintPath ();
					else
						node.Cell.GetComponentInChildren<ControllZoneController> ().PaintNotAvaliable ();
					AP--;
				}
				if (AP >= 0)
					path [path.Count - 1].Cell.GetComponentInChildren<ControllZoneController> ().PaintCursor ();
			}
		}
		//стираем путь
		public void DeactivatePath(List<Node> path){
			foreach (Node node in path) {
				node.Cell.GetComponentInChildren<ControllZoneController> ().Erase();
			}
		}
	}

}
