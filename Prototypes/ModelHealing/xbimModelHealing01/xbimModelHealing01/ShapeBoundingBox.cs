using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows.Media.Media3D;
using Xbim.Common.Geometry;
using Xbim.ModelGeometry.Scene;

using Xbim.Ifc4.Interfaces;


namespace xbimModelHealing01
{
     class ShapeBoundingBox
    {

        public void GetShape(IIfcElement element, Xbim3DModelContext context, Point3D MinPoint, Point3D MaxPoint)
        {
            //Shape of selectedElement
            List<XbimShapeInstance> productshapeSE = context.ShapeInstancesOf(element).Where(s => s.RepresentationType != XbimGeometryRepresentationType.OpeningsAndAdditionsExcluded).ToList();


            if (productshapeSE.Count == 0) //no Shapeelement-> search for aggregation elements and their shape
            {

                List<IIfcElement> allAggregatedElements = SearchAggregates(element);

                Point3D elementBoundingBoxMin = new Point3D(0, 0, 0);
                Point3D elementBoundingBoxMax = new Point3D(0, 0, 0);


                foreach (IIfcElement item in allAggregatedElements)
                {
                    List<Point3D> ListOfBBPoints = new List<Point3D>(); //TODO: sind 2 Listen notwendig? eine mit Minimum und eine mit Maximum?? Falls nicht Mehr Axis Aligned BoundingBox
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

                    //minimum BoundingBox Point
                    elementBoundingBoxMin.X = (from p in ListOfBBPoints select p).Min(e => e.X);
                    elementBoundingBoxMin.Y = (from p in ListOfBBPoints select p).Min(e => e.Y);
                    elementBoundingBoxMin.Z = (from p in ListOfBBPoints select p).Min(e => e.Z);
                    elementBoundingBoxMax.X = (from p in ListOfBBPoints select p).Max(e => e.X);
                    elementBoundingBoxMax.Y = (from p in ListOfBBPoints select p).Max(e => e.Y);
                    elementBoundingBoxMax.Z = (from p in ListOfBBPoints select p).Max(e => e.Z);

                    MinPoint = elementBoundingBoxMin;
                    MaxPoint = elementBoundingBoxMax;
                }


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
                    MaxPoint = tempPoint;
                    tempPoint.X = Math.Round(boundingbox.Min.X, 2);
                    tempPoint.Y = Math.Round(boundingbox.Min.Y, 2);
                    tempPoint.Z = Math.Round(boundingbox.Min.Z, 2);
                    MinPoint = tempPoint;

                }

            }
        }

        public List<IIfcElement> allAggregatedElements = new List<IIfcElement>();

        public List<IIfcElement> SearchAggregates(IIfcElement element)
        {

            List<IIfcRelAggregates> relAggregates = element.IsDecomposedBy.ToList(); //buildingSMART: An object definitions can only be part of a single decomposition
            foreach (IIfcRelAggregates relation in relAggregates)
            {

                List<IIfcObjectDefinition> AggregatesList = relation.RelatedObjects.ToList();
                foreach (IIfcElement aggregate in AggregatesList)
                {
                    List<IIfcRelAggregates> relAggregates2 = aggregate.IsDecomposedBy.ToList();
                    if (relAggregates2.Count() != 0)
                    {
                        SearchAggregates(aggregate);
                    }
                    else allAggregatedElements.Add(aggregate);
                }
            }

            return allAggregatedElements;

        }

    }
}
