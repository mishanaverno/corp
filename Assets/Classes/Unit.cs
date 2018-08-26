using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

//Класс описывающий модель данных юнита

namespace Units{
	public class Unit{
		public string unitname; //имя 
		public GameObject model; //объект в сцене
		public Node node; // логическая ячейка на карте
		public Team team; // команда

		public int recoveryAP; // количество восстанавливаемых за ход AP(action-point) - очков действия
		public int AP; //текущее AP
		public int maxAP; //максимально допустимое AP

		public Unit(string name,Team team, int maxAP, int rAP){ // метод конструктор класса
			this.unitname = name;
			this.team = team;
			this.recoveryAP = rAP;
			this.AP = 0;
			this.maxAP = maxAP;
		} 
		public void OnNewTurn(){ //метод обработчик начала хода, здесь выполняются все фоновые действия над юнитом при начале нового хода
			AP += recoveryAP; // восстановление AP
			AP = Mathf.RoundToInt (Mathf.Clamp (AP, 0, maxAP)); // проверка не превышает ли AP максимальное значение
		}
	}
}