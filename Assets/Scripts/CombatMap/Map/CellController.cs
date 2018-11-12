using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
	
	//скрипт управления клеткой 
public class CellController : MonoBehaviour {
    [SerializeField]
    public Node node;
	[SerializeField]
	private GameObject halfShelterPrefab;
	[SerializeField]
	private GameObject fullShelterPrefab;
    private bool hidden = false;
	
	public void ShowCellShelters(){// метод включения отображения укрытий клетки
		transform.BroadcastMessage ("ShowShelter",SendMessageOptions.DontRequireReceiver);
	}
	public void HideCellShelters(){// метод выключения отображения укрытий клетки
		transform.BroadcastMessage ("HideShelter",SendMessageOptions.DontRequireReceiver);
	}
    public void ChangeActiveFloor()
    {
        int activefloorNumber = Game.GameController.instance.activFloorNumber;
        if(node.floor.number > activefloorNumber && !hidden)
        {
            if (activefloorNumber == 0)
            {
                if(Stage.GetNode(node.crd, 0).isWalkable) ToggleHidden();
            }
            else
            {
                ToggleHidden();
            }
        }
        if(node.floor.number <= activefloorNumber && hidden)
        {
            ToggleHidden();
        }
        
    }
    public void ToggleHidden()
    {
       
        Transform[] childs = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in childs)
        {

            ControllZoneController script = child.GetComponent<ControllZoneController>();
            if (script != null) script.enabled = hidden;
            MeshRenderer mesh = child.GetComponent<MeshRenderer>();
            if (mesh != null && script == null) mesh.enabled = hidden;
            Light light = child.GetComponent<Light>();
            if (light != null) light.enabled = hidden;
            BoxCollider boxColider = child.GetComponent<BoxCollider>();
            if (boxColider != null) boxColider.enabled = hidden;
            MeshCollider meshCollider = child.GetComponent<MeshCollider>();
            if (meshCollider != null) meshCollider.enabled = hidden;
        }
        hidden = !hidden;
        /*foreach(MeshRenderer childMesh in childs)
        {
            childMesh.enabled = !hidden;

        }
        Light[] childLights = gameObject.GetComponentsInChildren<Light>();
        foreach(Light light in childLights)
        {
            light.enabled = !hidden;
        }*/
    }

}