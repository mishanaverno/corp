using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Скрипт включающий и выключающий отображение значков укрытий
public class ShelterViewer : MonoBehaviour {
	private MeshRenderer mesh;
	void Start () {
		mesh = transform.GetComponent<MeshRenderer>();
	}
	public void ShowShelter(){
		mesh.enabled = true;
	}
	public void HideShelter(){
		mesh.enabled = false;
	}
}
