using UnityEngine;

namespace Map
{
    public class BasementFloor : Floor
    {
        public BasementFloor(int number, Stage stage) : base(number, stage)
        {
            /*this.number = number;
            this.stage = stage;
            this.map = new Node[stage.height,stage.width];*/
            this.isBasement = true;
        }
    }
}
