using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Crossroad : MapElement
    {
        public int sidewalk;
        public Crossroad(RCT rct, int sidewalk) : base(rct)
        {
            this.sidewalk = sidewalk;
        }

    }

}
