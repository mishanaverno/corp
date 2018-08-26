using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// скрипт отвечающий за отображение стен
public class wallController : MonoBehaviour {
	public bool wallHidden = true; // отображаются ли стены или нет
	void Start () {
		toggleWalls (); // временно скрываются при инициализации позже будут переработаны 
	}
	

	public void toggleWalls(){ // медот отображения и скрытия стен
		GameObject wall = transform.Find ("wall").gameObject;
		GameObject wall_min = transform.Find ("wall_min").gameObject;
		if (!wallHidden) {
			wall.SetActive(false);
			wall_min.SetActive(true);
			wallHidden = true;
		} else if (wallHidden) {
			wall.SetActive(true);
			wall_min.SetActive(false);
			wallHidden = false;
		}
	}
}
