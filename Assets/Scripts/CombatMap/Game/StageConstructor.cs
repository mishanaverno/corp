using UnityEngine;

namespace Map
{
      //Клас конструирующий уровень(карту)
    public class StageConstructor : Object
    {
        public Stage stage;
        public StageConstructor()
        {
            stage = new Stage(2, 3, 3, true);
            string json = JsonUtility.ToJson(stage);
            Debug.Log(json);
        }
    }
   
}
