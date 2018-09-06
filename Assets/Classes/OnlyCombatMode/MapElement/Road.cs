using UnityEngine;

namespace Map
{
     public class Road : MapElement//класс описывает объект дорога
     {
        public char axis;  
         public Road(RCT rct, char axis) : base(rct)
         {

         }
           
     }
}
