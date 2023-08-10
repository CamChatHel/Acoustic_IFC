using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc4.Interfaces;
using System.Windows.Media.Media3D;
using System.IO;
using Xbim.Common.Geometry;
using Xbim.Ifc;
using Xbim.Common.XbimExtensions;
using Xbim.ModelGeometry.Scene;

namespace Prototype01
{
    public class GetSendingRoom
    {
        //public IIfcSpace spaceSR { get; set; } //sending room
        //public IIfcSpace spaceRR { get; set; } //receiving room

        public double GetAreaOnSelectedElement(Xbim3DModelContext context, IIfcSpace space, Vector3D directionSE)
        {
            context.CreateContext();
            Point3D PointMin = new Point3D(0, 0, 0);
            Point3D PointMax = new Point3D(0, 0, 0);


            List<XbimShapeInstance> productshapeSE = context.ShapeInstancesOf(space).Where(s => s.RepresentationType != XbimGeometryRepresentationType.OpeningsAndAdditionsExcluded).ToList();
            foreach (XbimShapeInstance shapeInstance in productshapeSE)
            {
                //Do BoundingBox and transform to global coordinates
                XbimRect3D boundingbox = shapeInstance.BoundingBox;
                boundingbox = boundingbox.Transform(((XbimShapeInstance)shapeInstance).Transformation);

                PointMax.X = Math.Round(boundingbox.Max.X, 2);
                PointMax.Y = Math.Round(boundingbox.Max.Y, 2);
                PointMax.Z = Math.Round(boundingbox.Max.Z, 2);
                PointMin.X = Math.Round(boundingbox.Min.X, 2);
                PointMin.Y = Math.Round(boundingbox.Min.Y, 2);
                PointMin.Z = Math.Round(boundingbox.Min.Z, 2);

            }


            if (directionSE == new Vector3D(1, 0, 0) || directionSE == new Vector3D(-1, 0, 0))
            {   //wall, parallel to Y-Z
                return (PointMax.Y - PointMin.Y) * (PointMax.Z - PointMin.Z);
            }
            else if (directionSE == new Vector3D(0, 1, 0) || directionSE == new Vector3D(0, -1, 0))
            {   //wall, parallel to X-Z
                return (PointMax.X - PointMin.X) * (PointMax.Z - PointMin.Z);
            }
            else
            {   //slab, parallel to X-Y
                return (PointMax.X - PointMin.X) * (PointMax.Y - PointMin.Y);
            }

            
        }


    }
}
