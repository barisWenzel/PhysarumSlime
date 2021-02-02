using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace PhysarumSlime
{
    public class PhysarumSlimeInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "PhysarumSlime";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("66b3c6a4-c4f7-4da5-b66c-b005fdd488a4");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
