using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Map;

	// скрипт управления юнитами в процессе игры
namespace Units
{
	public class UnitsController : MonoBehaviour {
		public Unit activeUnit;

		void Start () {
			GameController.instance.EndTurn ();
		}

		public static UnitsController instance;
		void Awake(){
			instance = this;
		}
		public static UnitsController GetInstance(){
			return instance;
		}
	}
}