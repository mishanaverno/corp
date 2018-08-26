using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
	
	//скрипт управления клеткой 
public class CellController : MonoBehaviour {
	public Node node;
	[SerializeField]
	private GameObject halfShelterPrefab;
	[SerializeField]
	private GameObject fullShelterPrefab;
	public void SetShelters(){ // метод создания на сцене GameObject'ов для отображения значков укрытий
		GameObject shelter;
		if (this.node.shelters.top > 0){
			if (this.node.shelters.top == 1) {
				shelter = Instantiate (halfShelterPrefab, this.transform);
			}else{
				shelter = Instantiate (fullShelterPrefab, this.transform);
			}
			shelter.transform.localPosition = new Vector3 (-0.5f, 5.5f, 0);
			shelter.transform.localRotation = Quaternion.Euler (90, 90, 0);
		}
		if (this.node.shelters.bot > 0){
			if (this.node.shelters.bot == 1) {
				shelter = Instantiate (halfShelterPrefab, this.transform);
			}else{
				shelter = Instantiate (fullShelterPrefab, this.transform);
			}
			shelter.transform.localPosition = new Vector3 (0.5f, 5.5f, 0);
			shelter.transform.localRotation = Quaternion.Euler (90, 90, 0);
		}
		if (this.node.shelters.left > 0){
			if (this.node.shelters.bot == 1) {
				shelter = Instantiate (halfShelterPrefab, this.transform);
			}else{
				shelter = Instantiate (fullShelterPrefab, this.transform);
			}
			shelter.transform.localPosition = new Vector3 (0, 5.5f, -0.5f);
			shelter.transform.localRotation = Quaternion.Euler (90, 0, 0);
		}
		if (this.node.shelters.right > 0){
			if (this.node.shelters.bot == 1) {
				shelter = Instantiate (halfShelterPrefab, this.transform);
			}else{
				shelter = Instantiate (fullShelterPrefab, this.transform);
			}
			shelter.transform.localPosition = new Vector3 (0, 5.5f, 0.5f);
			shelter.transform.localRotation = Quaternion.Euler (90, 0, 0);
		}
	}
	public void ShowCellShelters(){// метод включения отображения укрытий клетки
		transform.BroadcastMessage ("ShowShelter",SendMessageOptions.DontRequireReceiver);
	}
	public void HideCellShelters(){// метод выключения отображения укрытий клетки
		transform.BroadcastMessage ("HideShelter",SendMessageOptions.DontRequireReceiver);
	}

}