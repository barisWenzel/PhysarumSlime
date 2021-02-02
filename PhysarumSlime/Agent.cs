using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace PhysarumSlime
{
    public class Agent
    {
        #region Properties
        public Point3d Position;
        public Vector3d Heading;
        public Molde Molde;
        public Grid grid;
        public Point3d SensorCenter = new Point3d();
        public Point3d SensorLeft = new Point3d();
        public Point3d SensorRight = new Point3d();
        public int F;
        public int L;
        public int R;
        public Random random = new Random();
        #endregion


        #region Constructor
        public Agent() { }

        public Agent(Point3d position, Vector3d heading)
        {
            this.Position = position;
            this.Heading = heading;
        }
        #endregion

        #region Methods

        #region sensorstage
        public void Sensorstage()
        {
            SampleTrailMapValues();
            Rotate();
        }

        public void SampleTrailMapValues()
        {
            UpdateSensors();
            F = Molde.grid.ActualVoxel(SensorCenter).Charge;
            R = Molde.grid.ActualVoxel(SensorRight).Charge;
            L = Molde.grid.ActualVoxel(SensorLeft).Charge;
        }

        //Deciding Direction and rotate
        public void Rotate()
        {

            if (F > L && F > R) return;//Continue in the same direction

            else if (F < L && F < R)//Rotate randomly left of right
            {
                int dice = random.Next(1, 100);
                Heading = dice> 50 ? Util.RotateLeft(Heading, Molde.AgentRotAngle): Util.RotateRight(Heading, Molde.AgentRotAngle);
            }

            else if (L < R)//Rotate Right
            {
                Heading = Util.RotateRight(Heading, Molde.AgentRotAngle);
            }

            else if (R < L)//Rotate Left
            {
                Heading = Util.RotateLeft(Heading, Molde.AgentRotAngle);
            }
            else return;
        }

        public void UpdateSensorCenter()
        {
            Heading.Unitize();
            SensorCenter = Position + Heading * Molde.SensorOffsetDistance;
        }

        public void UpdateSensorLeft()
        {
            SensorLeft=Position+ Util.RotateLeft(Heading, Molde.AgentRotAngle)*Molde.SensorOffsetDistance;
        }

        public void UpdateSensorRight()
        {
            SensorRight = Position + Util.RotateRight(Heading, Molde.AgentRotAngle) * Molde.SensorOffsetDistance;
        }

        public void UpdateSensors()
        {
            UpdateSensorCenter();
            UpdateSensorLeft();
            UpdateSensorRight();
        }

        #endregion

        #region Motorstage
        public void KeepAgentInBox()
        {
            double tol = 0.001;
            double minX = tol;
            double maxX = 1 - tol;
            double minY = tol;
            double maxY = 1 - tol;

            if (Position.X < minX)
            {
                Position = new Point3d(minX, Position.Y, Position.Z);
                Heading = Heading * -1;
            }

            else if (Position.X > maxX)
            {
                Position = new Point3d(maxX, Position.Y, Position.Z);
                Heading = Heading * -1;
            }

            if (Position.Y < minY)
            {
                Position = new Point3d(Position.X, minY, Position.Z);
                Heading = Heading * -1;
            }

            else if (Position.Y > maxY)
            {
                Position = new Point3d(Position.X, maxY, Position.Z);
                Heading = Heading * -1;
            }

        }

        public void Motorstage()
        {

            if (!Molde.grid.ActualVoxel(Position + Heading*Molde.Time).IsOccupied)
            {
                Molde.grid.ActualVoxel(Position).IsOccupied = false;
                Position += (Heading * Molde.Time);//the actual move
                KeepAgentInBox();
                Molde.grid.ActualVoxel(Position).Charge++;
                Molde.grid.ActualVoxel(Position).IsOccupied = true;
            }
            else
            {
                Heading = Util.GetRandomUnitVectorXY();
            }
            
        }
        #endregion

        #endregion
    }
}
