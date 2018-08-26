using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game{
	public class CameraMoving : MonoBehaviour {
		private float speed = 6.0f;
		public float mapLengthX;
		public float mapLengthY;
		public GameObject targetUnit;
		private bool rotationMode = false;
		private GameObject followfingCM;
		// Use this for initialization
		void Start () {
			targetUnit = this.gameObject;
		}
		
		// Update is called once per frame
		void Update () {
			if (targetUnit == this.gameObject) {
				
				transform.Translate (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis ("Vertical") * speed * Time.deltaTime, Space.Self);
				if (transform.position.x < -0.5f)
					transform.position = new Vector3 (-0.5f, 0.1f, transform.position.z);
				if (transform.position.z < -0.5f)
					transform.position = new Vector3 (transform.position.x, 0.1f, -0.5f);
				if (transform.position.x > mapLengthX)
					transform.position = new Vector3 (mapLengthX, 0.1f, transform.position.z);
				if(transform.position.z >mapLengthY)
					transform.position = new Vector3 (transform.position.x, 0.1f, mapLengthY);
			} else {
				this.transform.position = Vector3.MoveTowards (this.transform.position, targetUnit.transform.position, 10 * Time.deltaTime);
			}
			if (Input.GetMouseButtonDown (2)) {
				rotationMode = true;
			}
			if (Input.GetMouseButtonUp (2)) {
				rotationMode = false;
			}
			if (rotationMode) {
				transform.Rotate (0, Input.GetAxis ("Mouse X") * speed * 2f * Time.deltaTime, 0);
			}
		}
		public void SetMapLengths(float x, float y){
			mapLengthX = x - 0.5f;
			mapLengthY = y - 0.5f;
		}
		public void ChainToUnit(GameObject target){
			targetUnit = target;
		}
		public void UnchainFromUnit(){
			targetUnit = this.gameObject;
		}
		public static CameraMoving instance;
		void Awake(){
			instance = this;
		}
		public static CameraMoving GetInstance(){
			return instance;
		}
	}
}