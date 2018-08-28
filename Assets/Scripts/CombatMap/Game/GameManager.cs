using System.Collections.Generic;
using UnityEngine;
using Map;
using Units;
// скрипт получающий стартовые данные для игры, карту, юнитов, команды, префабы
namespace Game
{
    [System.Serializable]
	public class GameManager : MonoBehaviour {
        
		public int[,,] incomingMap;
		public GameObject floorPrefab;
		public GameObject halfShelterPrefab;
		public GameObject fullShelterPrefab;
		public GameObject WallPrefab;
		public GameObject portalPrefab;
		public GameObject Unit;
		public GameObject EnemyUnit;

		public List<Team> teams = new List<Team> ();
		public List<Unit> playerUnits = new List<Unit>();
		public List<Unit> enemyUnits = new List<Unit>();
		// Use this for initialization
		void Start () {
            StageConstructor SC = new StageConstructor();

            incomingMap = MapLoader.LoadMap ("test.txt");
			teams.Add (new Team ("Master", "Player"));
			teams.Add (new Team ("Enemy", "AI"));
			playerUnits.Add (new Unit ("Igor Svezdov-pezdof", teams.Find (x => x.title == "Master"), 10, 8)); 
			playerUnits.Add (new Unit ("Alexander Sukinsin", teams.Find (x => x.title == "Master"), 10, 8));
			UnitsGenerator.instance.playerUnits = playerUnits;
			enemyUnits.Add (new Unit ("Angry Sailon", teams.Find (x => x.title == "Enemy"), 8, 7));
			UnitsGenerator.instance.enemyUnits = enemyUnits;

		}

		public static GameManager instance;
		void Awake(){
			instance = this;
		}
		public static GameManager GetInstance(){
			return instance;
		}
	}

}
