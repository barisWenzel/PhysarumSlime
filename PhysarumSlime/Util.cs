using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Rhino.Geometry;

namespace PhysarumSlime
{ 
    public class Util
    {
        static Random random = new Random();

        public static Vector3d GetRandomUnitVectorXY()
        {
            double angle = 2.0 * Math.PI * random.NextDouble();

            double x = Math.Cos(angle);
            double y = Math.Sin(angle);

            return new Vector3d(x, y, 0.0);
        }

        public static Vector3d RotateLeft(Vector3d vec,double degree)
        {
            Vector3d temp = vec;
            temp.Unitize();
            temp.Rotate(Rhino.RhinoMath.ToRadians(degree), Vector3d.ZAxis);
            return temp;
        }

        public static Vector3d RotateRight(Vector3d vec, double degree)
        {
            Vector3d temp = vec;
            temp.Unitize();
            temp.Rotate(Rhino.RhinoMath.ToRadians(-degree), Vector3d.ZAxis);
            return temp;
        }


        public static Color GetColour(int charge,int max)
        {
            // (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
            int val = (charge - 0) * (255 - 0) / (max - 0) + 0;
            //return Color.FromArgb(charge%255, charge%255, charge%255);
            return Color.FromArgb(val, val, val);
        }

        public static Color GetColourGradient(int charge, int max)
        {
            Grasshopper.GUI.Gradient.GH_Gradient gradient = new Grasshopper.GUI.Gradient.GH_Gradient();
            gradient.AddGrip(max, Color.FromArgb(234,28,0));//Red
            gradient.AddGrip(max*0.75, Color.FromArgb(234, 126, 0));//Orange
            gradient.AddGrip(max *0.50, Color.FromArgb(254, 244, 84));//Gelb
            gradient.AddGrip(max*0.25, Color.FromArgb(173, 203, 179));//babyblau
            gradient.AddGrip(0 , Color.FromArgb(75, 107, 169));//blau
            return gradient.ColourAt(charge);
        }
    }
}
