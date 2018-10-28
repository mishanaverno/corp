using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;

namespace Map{
	// класс отвечающий за прорисовку на сцене путей движения юнитов
	public class Painter : MonoBehaviour {

        //рисует путь 
        private LineRenderer renderer;
        public void Start()
        {
            renderer = GetComponent<LineRenderer>();
        }
        public void ActivatePath(List<Node> path){
			/*if (path.Count == 1) {
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
			}*/
            if(path.Count == 1)
            {

            }
            else
            {
                renderer.positionCount = path.Count;
                for(int p = 0; p < path.Count; p++)
                {
                    renderer.SetPosition(p, new Vector3(path[p].crd.x, path[p].floor.number + 0.2f, path[p].crd.z));
                }
            }
		}
		//стираем путь
		public void DeactivatePath(List<Node> path){
			/*foreach (Node node in path) {
				node.Cell.GetComponentInChildren<ControllZoneController> ().Erase();
			}*/
		}
        public static Painter instance;
		void Awake(){
			instance = this;
		}
		public static Painter GetInstance(){
			return instance;
		}
	}

}
