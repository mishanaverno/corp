using UnityEngine;

namespace Map
{
    public class CRD : Object
    {
        public int x, z, level;
        
        public CRD(int x, int z, int level)
        {
            this.x = x;
            this.z = z;
            this.level = level;
        } 
    }

}
