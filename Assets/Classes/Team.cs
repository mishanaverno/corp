using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс описывающий модель данных команды
namespace Units{
	public class Team {
		public string title; //название
		public string controll;  // переменная хранящая данные об управлении командой, пока что стринг, позже возможно реализуем через другой класс

		public Team(string title, string controll){ // метод конструктор команды
			this.title = title;
			this.controll = controll;
		}
		public bool IsPlayer(){ // метод проверяющий управляется ли команда игроком
			if (controll == "Player") 
				return true;
			return false;
		}
		public bool IsAI(){ // метод проверяющий управляется ли команда AI
			if (controll == "AI")
				return true;
			return false;
		}
	}
}
