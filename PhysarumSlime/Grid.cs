using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace PhysarumSlime
{
    public class Grid
    {
        #region Properties
        public Voxel[,] Voxels;
        public int Columns;
        public int Rows;
        #endregion

        #region Constructor
        public Grid(int rows, int columns)
        {
            this.Columns = columns;
            this.Rows = rows;

            this.Voxels = new Voxel[columns,rows];

            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    Voxels[i, j] = new Voxel(i, j);
                }
            }
        }
        #endregion

        #region Methods



        public int GetMaxCharge()
        {
            var charges = new List<int>();
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    charges.Add(Voxels[i, j].Charge);
                }
            }
            return charges.Max();

        }


        public Voxel ActualVoxel(Point3d pt)
        {
            int c = (int)(pt.X * Columns);
            int r = (int)(pt.Y *Rows);


            if (c > Columns - 1)
                c = Columns - 1;
            if (c < 0)
                c = 0;

            if (r > Rows - 1)
                r = Rows - 1;
            if (r < 0)
                r = 0;


            return Voxels[c, r];
        }

        public List<Polyline> DrawGrid()
        {
            var pLines = new List<Polyline>();

            double colStep = 1.0 / Columns;
            double rowStep = 1.0 / Rows;

            for (int i = 0; i < Columns; i++)
            {
                Point3d pt1, pt2, pt3, pt4;

                for (int j = 0; j < Rows; j++)
                {
                    pt1 = new Point3d(i * colStep, j * rowStep, 0);
                    pt2 = new Point3d((i + 1) * colStep, j * rowStep, 0);
                    pt3 = new Point3d((i + 1) * colStep, (j + 1) * rowStep, 0);
                    pt4 = new Point3d((i) * colStep, (j + 1) * rowStep, 0);
                    pLines.Add(new Polyline(new List<Point3d>() { pt1, pt2, pt3, pt4, pt1 }));
                }
            }
            return pLines;
        }
        #endregion
    }
}
