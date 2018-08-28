using UnityEngine;

namespace Map
{
      //Клас конструирующий уровень(карту)
    public class StageConstructor : Object
    {
        public Stage stage;
        public StageConstructor()
        {
            stage = new Stage(2, 5, 5, false);
        }
    }
   
}
