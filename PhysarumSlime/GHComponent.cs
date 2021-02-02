using System;
using System.Collections.Generic;
using GH_IO;
using GH_IO.Serialization;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;



namespace PhysarumSlime
{
    public class SlimeComponent : GH_Component
    {

        public SlimeComponent() : base("Slime", "Slime", "Slime", "Slime", "Slime") { }

        private Molde molde;

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {

            pManager.AddPointParameter("Pts", "Pts", "Pts", GH_ParamAccess.list);
            pManager.AddIntegerParameter("GridXY", "GridXY", "A reasonable value seems to be between 300<1000", GH_ParamAccess.item, 100);
            pManager.AddNumberParameter("SensorOffset", "SensOffset", "A reasonable value for the sensorOffset seems to be > 0 and <0.1", GH_ParamAccess.item, 0.05);
            pManager.AddNumberParameter("SensorRotation", "SensRotation", "SensorRotation, typical values are 22.5 or 45", GH_ParamAccess.item, 45);
            pManager.AddNumberParameter("AgentRotationAngle", "AgeRotAngle", "AgenRotationAngle, typical values are 22.5 or 45", GH_ParamAccess.item, 45);
            pManager.AddNumberParameter("Time", "Time", "A reasonable value seems to be beetween > 0 and <0.5", GH_ParamAccess.item, 0.05);
            pManager.AddColourParameter("Colour", "Colour", "Colour for PointCloud", 0, System.Drawing.Color.Aquamarine);
            pManager.AddIntegerParameter("PointSize", "PtSize", "Pointsize for PointCloud", GH_ParamAccess.item, 1);
        }


        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {

        }

        #region Display and button
        //Display
        protected override void BeforeSolveInstance()
        {
            if (_cloud != null)
                _cloud.Dispose();

            _cloud = new PointCloud();
            _clippingBox = BoundingBox.Empty;
        }
        //Button
        public override void CreateAttributes()
        {
            m_attributes = new CustomAttributes(this);
        }

        public bool Run;

        #endregion

        protected override void SolveInstance(IGH_DataAccess DA)
        {

            #region Get the inputs
            List<Point3d> pts = new List<Point3d>();
            if (!DA.GetDataList(0, pts)) return;

            int gridXY = 0;
            if (!DA.GetData(1, ref gridXY)) return;


            double sensorOffset = 0;
            if (!DA.GetData(2, ref sensorOffset)) return;

            double sensorRotation = 0;
            if (!DA.GetData(3, ref sensorRotation)) return;

            double agentRotAngle = 0;
            if (!DA.GetData(4, ref agentRotAngle)) return;

            double time = 0;
            if (!DA.GetData(5, ref time)) return;

            System.Drawing.Color colour = new System.Drawing.Color();
            if (!DA.GetData(6, ref colour)) return;

            int ptSize = 0;
            if (!DA.GetData(7, ref ptSize)) return;


            #endregion

            #region Run the simulation

            if (Run || molde == null)
                molde = new Molde(pts, gridXY, gridXY);


            else
            {
                molde.SensorOffsetDistance = sensorOffset;
                molde.SensorRotation = sensorRotation;
                molde.Time = time;
                molde.AgentRotAngle = agentRotAngle;
                _ptSize = ptSize;

                molde.Update();

                var tree = new DataTree<Point3d>();



                for (int i = 0; i < molde.Agents.Count; i++)
                    _cloud.AddRange(molde.Trails[i], molde.Colours[i]);

            }
            #endregion

        }


        #region display
        private PointCloud _cloud;
        private BoundingBox _clippingBox;
        private float _ptSize;

        public override BoundingBox ClippingBox
        {
            get { return _clippingBox; }
        }


        public override void DrawViewportWires(IGH_PreviewArgs args)
        {
            base.DrawViewportWires(args);

            if (_cloud != null)
                args.Display.DrawPointCloud(_cloud, _ptSize);

            Plane plane;
            args.Viewport.GetFrustumFarPlane(out plane);

        }


        #endregion

        protected override System.Drawing.Bitmap Icon
        {
            get { return null; }
        }


        public override Guid ComponentGuid
        {
            get { return new Guid("8cdb3632-9d76-486c-99f4-354b6c2200c2"); }
        }
    }
}
