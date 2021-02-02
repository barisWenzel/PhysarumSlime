using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using System.Drawing;

namespace PhysarumSlime
{
    public class Molde
    {

        #region Properites
        public List<Agent> Agents = new List<Agent>();
        public Grid grid;
        public double Time;
        public double SensorOffsetDistance;
        public double AgentRotAngle;
        public double SensorRotation;
        public List<Queue<Point3d>> Trails = new List<Queue<Point3d>>();
        public List<Queue<Color>> Colours = new List<Queue<Color>>();
        #endregion

        #region Constructor
        public Molde(List<Point3d> positions, int columns, int rows)
        {
            grid = new Grid(columns, rows);
            for (int i = 0; i < positions.Count; i++)
            {

                var temp = new Queue<Point3d>();
                var tempColour = new Queue<Color>();

                Agent agent = new Agent(positions[i], Util.GetRandomUnitVectorXY());
                temp.Enqueue(positions[i]);
                tempColour.Enqueue(Color.Empty);

                agent.Molde = this;
                Agents.Add(agent);

                Trails.Add(temp);
                Colours.Add(tempColour);
                
            }
        }
        #endregion

        #region Methods
        public void Update()
        {
            for (int i = 0; i < Agents.Count; i++)
            {
                Agents[i].Motorstage();
                Agents[i].Sensorstage();
            }
            GetTrails();
        }

        public void GetTrails()
        {
            int max = grid.GetMaxCharge();

            for (int i = 0; i < Agents.Count; i++)
            {

                if (Trails[i].Count > 10)
                {
                    Trails[i].Enqueue(Agents[i].Position);
                    Colours[i].Enqueue(Util.GetColourGradient((grid.ActualVoxel(Agents[i].Position).Charge),max));
                    Trails[i].Dequeue();
                    Colours[i].Dequeue();
                }
                else
                {
                    Trails[i].Enqueue(Agents[i].Position);
                    Colours[i].Enqueue(Util.GetColourGradient((grid.ActualVoxel(Agents[i].Position).Charge), max));
                }
            }
        }

        

        #endregion
    }
}
