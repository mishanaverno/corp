using System;
using UnityEngine;

namespace Map
{   [Serializable]
    public class CRD
    {
        
        public int x, z;
        
        public CRD(int x, int z)
        {
            this.x = x;
            this.z = z;
        } 
    }

}
