using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysarumSlime
{
    public class Voxel
    {
        #region Properties
        public int I;
        public int J;
        public int Charge = 0;
        public bool IsOccupied = false;
        #endregion

        #region Constructor
        public Voxel (int i, int j)
        {
            this.I = i;
            this.J = j;
        }
        #endregion
    }
}
