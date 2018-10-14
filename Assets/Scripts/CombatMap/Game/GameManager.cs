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
            StageConstructor Constructor = new StageConstructor();
            Constructor.CreateStage(37, 40, false, "Dev");
            Constructor.AddStreet(1, 'h', 3, 5);
            Constructor.AddStreet(20, 'h', 1, 3);
            Constructor.AddStreet(1, 'v', 7, 5);
            //Constructor.AddStreet(16, 'v', 6, 4);
            Constructor.Upgrade();
            Constructor.RenderStage();
            Constructor.RenderBackground();
            /*RCT rct = new RCT(new CRD(2, 3), 6, 4);
            bool[,] nmap = StageConstructor.createPeraiodicalMap(rct, 'h', 1, 2);
            for (int x = 0; x < nmap.GetLength(0); x++)
            {
                string row = "x: " + x + " ";
                for (int z = 0; z < nmap.GetLength(1); z++)
                {
                    row += nmap[x, z].ToString() + " + ";
                }
                Debug.Log(row);
            }*/
            

                    //teams.Add (new Team ("Master", "Player"));
                    //teams.Add (new Team ("Enemy", "AI"));
                    //playerUnits.Add (new Unit ("Igor Svezdov-pezdof", teams.Find (x => x.title == "Master"), 10, 8)); 
                    //playerUnits.Add (new Unit ("Alexander Sukinsin", teams.Find (x => x.title == "Master"), 10, 8));
                    //UnitsGenerator.instance.playerUnits = playerUnits;
                    //enemyUnits.Add (new Unit ("Angry Sailon", teams.Find (x => x.title == "Enemy"), 8, 7));
                    //UnitsGenerator.instance.enemyUnits = enemyUnits;

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
