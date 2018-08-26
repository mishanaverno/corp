using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour {

	private CinemachineVirtualCamera followingCM;
	void Start () {
		followingCM = transform.GetComponent<CinemachineVirtualCamera> ();
	}
	
	// Update is called once per frame
	void Update () {
		followingCM.m_Lens.FieldOfView = Mathf.Clamp (Input.GetAxis ("Mouse ScrollWheel") + followingCM.m_Lens.FieldOfView, 15f, 50f);

	}
}
