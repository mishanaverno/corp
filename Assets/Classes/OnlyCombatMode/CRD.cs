using System;
using UnityEngine;

namespace Map
{   [Serializable]
    public class CRD // координата в логической структуре карты
    {
        
        public int x, z;
        
        public CRD(int x, int z)
        {
            this.x = x;
            this.z = z;
        }
        public void StepLT () //смещение точки в лево и верх
        {
            x--;
            z--;
        }
        public void StepRB() //смещение в право и вниз
        {
            x++;
            z++;
        }
        public static bool operator==(CRD crd1, CRD crd2)
        {
            if(crd1.x == crd2.x && crd1.z == crd2.z)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator!=(CRD crd1, CRD crd2)
        {
            if (crd1.x == crd2.x && crd1.z == crd2.z)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

}
