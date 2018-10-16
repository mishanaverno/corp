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
	
	public void ShowCellShelters(){// метод включения отображения укрытий клетки
		transform.BroadcastMessage ("ShowShelter",SendMessageOptions.DontRequireReceiver);
	}
	public void HideCellShelters(){// метод выключения отображения укрытий клетки
		transform.BroadcastMessage ("HideShelter",SendMessageOptions.DontRequireReceiver);
	}

}