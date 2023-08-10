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
using Xbim.ModelGeometry.Scene;

namespace xbimModelHealing01
{
    public class ShapeHandler
    {
        public Point3D elementBoundingBoxMin = new Point3D(0, 0, 0);
        public Point3D elementBoundingBoxMax = new Point3D(0, 0, 0);
        public double height = new double();




        public void GetShape(IIfcElement element, Xbim3DModelContext context)
            {
                //Shape of selectedElement
                List<XbimShapeInstance> productshapeSE = context.ShapeInstancesOf(element).Where(s => s.RepresentationType != XbimGeometryRepresentationType.OpeningsAndAdditionsExcluded).ToList();


            if (productshapeSE.Count == 0) //no Shapeelement 
            {
                //TODO: Rekursion einbauen, falls es mehrere Schichten gibt für die aggregations!! siehe AggregationHandler!
                //Search if element is decomposed
                    List<IIfcRelAggregates> decomposionElements = element.IsDecomposedBy.ToList();
                    foreach (IIfcRelAggregates aggregateElement in decomposionElements)
                    {
                        List<Point3D> ListOfBBPoints = new List<Point3D>(); //TODO: sind 2 Listen notwendig? eine mit Minimum und eine mit Maximum?? Falls nicht Mehr Axis Aligned BoundingBox
                        var Listelements = aggregateElement.RelatedObjects.ToList();

                        foreach (IIfcElement e in Listelements)
                        {
                            Point3D elementPointMin;
                            Point3D elementPointMax;
                            List<XbimShapeInstance> elementshape = context.ShapeInstancesOf(e).Where(s => s.RepresentationType != XbimGeometryRepresentationType.OpeningsAndAdditionsExcluded).ToList();

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

                        //minimum BoundingBox Point
                        elementBoundingBoxMin.X = (from p in ListOfBBPoints select p).Min(e => e.X);
                        elementBoundingBoxMin.Y = (from p in ListOfBBPoints select p).Min(e => e.Y);
                        elementBoundingBoxMin.Z = (from p in ListOfBBPoints select p).Min(e => e.Z);
                        elementBoundingBoxMax.X = (from p in ListOfBBPoints select p).Max(e => e.X);
                        elementBoundingBoxMax.Y = (from p in ListOfBBPoints select p).Max(e => e.Y);
                        elementBoundingBoxMax.Z = (from p in ListOfBBPoints select p).Max(e => e.Z);

                    }


            }
            else
            {
                foreach (XbimShapeInstance shapeInstance in productshapeSE) //TODO: Falsch! Geht nur bei einschichtigen Bauteilen!!
                {
                    //Do BoundingBox and transform to global coordinates
                    XbimRect3D boundingbox = shapeInstance.BoundingBox;
                    boundingbox = boundingbox.Transform(((XbimShapeInstance)shapeInstance).Transformation);
                    elementBoundingBoxMax.X = Math.Round(boundingbox.Max.X, 2);
                    elementBoundingBoxMax.Y = Math.Round(boundingbox.Max.Y, 2);
                    elementBoundingBoxMax.Z = Math.Round(boundingbox.Max.Z, 2);
                    elementBoundingBoxMin.X = Math.Round(boundingbox.Min.X, 2);
                    elementBoundingBoxMin.Y = Math.Round(boundingbox.Min.Y, 2);
                    elementBoundingBoxMin.Z = Math.Round(boundingbox.Min.Z, 2);
                    
                }

            }
        }


        public double GetHeight(IIfcElement element, Xbim3DModelContext context)
        {
            GetShape(element, context);
            height= elementBoundingBoxMax.Z - elementBoundingBoxMin.Z;
            return height;
        }

        
    }


}


