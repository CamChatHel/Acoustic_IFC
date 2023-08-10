using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Xbim.Common.Geometry;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.Common.XbimExtensions;
using Xbim.ModelGeometry.Scene;
using System.Windows.Media.Media3D;  //braucht als Verweis "Presentation Core" , im Projektmappen-Explorer unter Verweise hinzufuegen!
using System.Security.Policy;


namespace Prototype01
{
    class ShapeBoundingBox
    {

        public static double GetThicknessfromShape(MyElement CorrespondingMyelement)
        {
            double thickness = 0;
            if (CorrespondingMyelement.AreaVector.NormalVector.X == 1 || CorrespondingMyelement.AreaVector.NormalVector.X == -1 )
            {
                thickness =  CorrespondingMyelement.Max.X - CorrespondingMyelement.Min.X;
            }
            else if (CorrespondingMyelement.AreaVector.NormalVector.Y == 1 || CorrespondingMyelement.AreaVector.NormalVector.Y == -1 )
            {
                thickness = CorrespondingMyelement.Max.Y - CorrespondingMyelement.Min.Y;
            }
            else if (CorrespondingMyelement.AreaVector.NormalVector.Z == 1 || CorrespondingMyelement.AreaVector.NormalVector.Z == -1)
            {
                thickness = CorrespondingMyelement.Max.Z - CorrespondingMyelement.Min.Z;
            }

            return thickness;

        }

        public void GetShape(IIfcElement element, MyElement CorrespondingMyelement, Xbim3DModelContext context)
        {
            //Shape of selectedElement
            List<XbimShapeInstance> productshapeSE = context.ShapeInstancesOf(element).Where(s => s.RepresentationType != XbimGeometryRepresentationType.OpeningsAndAdditionsExcluded).ToList();


            if (productshapeSE.Count == 0 || element is IIfcCovering) //no Shapeelement-> search for aggregation elements and their shape
            {
                GetAggregationLayers searchaggregates = new GetAggregationLayers();
                searchaggregates.SearchAggregates(element);


                Point3D elementBoundingBoxMin = new Point3D(0, 0, 0); 
                Point3D elementBoundingBoxMax = new Point3D(0, 0, 0);
                List<Point3D> ListOfBBPoints = new List<Point3D>();

                foreach (IIfcElement item in searchaggregates.allAggregatedElements)
                {
                    //List<Point3D> ListOfBBPoints = new List<Point3D>(); //TODO: sind 2 Listen notwendig? eine mit Minimum und eine mit Maximum?? Falls nicht Mehr Axis Aligned BoundingBox
                    //var Listelements = aggregateElement.RelatedObjects.ToList();
                    Point3D elementPointMin;
                    Point3D elementPointMax;
                    List<XbimShapeInstance> elementshape = context.ShapeInstancesOf(item).Where(s => s.RepresentationType != XbimGeometryRepresentationType.OpeningsAndAdditionsExcluded).ToList();

                    foreach (XbimShapeInstance shape in elementshape)
                    {
                        //Do BoundingBox and transform to global coordinates
                        XbimRect3D boundingbox = shape.BoundingBox;
                        boundingbox = boundingbox.Transform(((XbimShapeInstance)shape).Transformation);

                        Point3D tempPoint = new Point3D(0, 0, 0);
                        tempPoint.X = Math.Round(boundingbox.Max.X, 2);
                        tempPoint.Y = Math.Round(boundingbox.Max.Y, 2);
                        tempPoint.Z = Math.Round(boundingbox.Max.Z, 2);
                        elementPointMax = tempPoint;
                        tempPoint.X = Math.Round(boundingbox.Min.X, 2);
                        tempPoint.Y = Math.Round(boundingbox.Min.Y, 2);
                        tempPoint.Z = Math.Round(boundingbox.Min.Z, 2);
                        elementPointMin = tempPoint;

                        ListOfBBPoints.Add(elementPointMin);
                        ListOfBBPoints.Add(elementPointMax);

                    }

                }
                //minimum BoundingBox Point from all aggregated elements and all shapes
                elementBoundingBoxMin.X = (from p in ListOfBBPoints select p).Min(e => e.X);
                elementBoundingBoxMin.Y = (from p in ListOfBBPoints select p).Min(e => e.Y);
                elementBoundingBoxMin.Z = (from p in ListOfBBPoints select p).Min(e => e.Z);
                elementBoundingBoxMax.X = (from p in ListOfBBPoints select p).Max(e => e.X);
                elementBoundingBoxMax.Y = (from p in ListOfBBPoints select p).Max(e => e.Y);
                elementBoundingBoxMax.Z = (from p in ListOfBBPoints select p).Max(e => e.Z);

                CorrespondingMyelement.Min = elementBoundingBoxMin;
                CorrespondingMyelement.Max = elementBoundingBoxMax;

            }

            else
            {
                foreach (XbimShapeInstance shapeInstance in productshapeSE) 
                {
                    //Do BoundingBox and transform to global coordinates
                    XbimRect3D boundingbox = shapeInstance.BoundingBox;
                    boundingbox = boundingbox.Transform(((XbimShapeInstance)shapeInstance).Transformation);

                    Point3D tempPoint = new Point3D(0, 0, 0);
                    tempPoint.X = Math.Round(boundingbox.Max.X, 2);
                    tempPoint.Y = Math.Round(boundingbox.Max.Y, 2);
                    tempPoint.Z = Math.Round(boundingbox.Max.Z, 2);
                    CorrespondingMyelement.Max = tempPoint;
                    tempPoint.X = Math.Round(boundingbox.Min.X, 2);
                    tempPoint.Y = Math.Round(boundingbox.Min.Y, 2);
                    tempPoint.Z = Math.Round(boundingbox.Min.Z, 2);
                    CorrespondingMyelement.Min = tempPoint;

                }

            }
        }

        public void GetShape(IIfcSpace element, MyElement CorrespondingMyelement, Xbim3DModelContext context) //For Spaces without Aggregation!!
        {
            //Shape of selectedElement
            List<XbimShapeInstance> productshapeSE = context.ShapeInstancesOf(element).Where(s => s.RepresentationType != XbimGeometryRepresentationType.OpeningsAndAdditionsExcluded).ToList();

                foreach (XbimShapeInstance shapeInstance in productshapeSE)
                {
                    //Do BoundingBox and transform to global coordinates
                    XbimRect3D boundingbox = shapeInstance.BoundingBox;
                    boundingbox = boundingbox.Transform(((XbimShapeInstance)shapeInstance).Transformation);

                    Point3D tempPoint = new Point3D(0, 0, 0);
                    tempPoint.X = Math.Round(boundingbox.Max.X, 2);
                    tempPoint.Y = Math.Round(boundingbox.Max.Y, 2);
                    tempPoint.Z = Math.Round(boundingbox.Max.Z, 2);
                    CorrespondingMyelement.Max = tempPoint;
                    tempPoint.X = Math.Round(boundingbox.Min.X, 2);
                    tempPoint.Y = Math.Round(boundingbox.Min.Y, 2);
                    tempPoint.Z = Math.Round(boundingbox.Min.Z, 2);
                    CorrespondingMyelement.Min = tempPoint;

                }

            
        }


    }
}
