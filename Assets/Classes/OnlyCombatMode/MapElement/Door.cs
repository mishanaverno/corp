using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Door : Portal
    {
        public Door(RCT rct, string innername, string outername, bool isExit) : base(rct, innername, outername, isExit)
        {

        }
        
    }
}
