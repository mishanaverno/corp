using System.Collections;
using System.Collections.Generic;
using UnityEngine;
		
		//скрипт отвечающий за поведение дверей
public class portalController : MonoBehaviour {
	public bool portalTopHidden = true; // отображаются ли дверные косяки, необходим для скрытия и ооражения стен

	void Start () {
		togglePortalTops (); //временно автоматически скрывает косяки позже будет переработан
	}
	

	public void togglePortalTops(){ // медод включающий выключающий отображения косяков
		GameObject portal_top = this.transform.Find ("portal_top").gameObject;
		if (!portalTopHidden) {
			portal_top.SetActive (false);
			portalTopHidden = false;
		} else if (portalTopHidden) {
			portal_top.SetActive (true);
			portalTopHidden = true;
		}
	}
}
