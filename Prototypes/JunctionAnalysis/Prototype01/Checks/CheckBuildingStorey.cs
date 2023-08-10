using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc4.Interfaces;
using Xbim.ModelGeometry.Scene;


namespace Prototype01
{
    public class CheckBuildingStorey
    {

        public static List<IIfcElement> GetBuildingStorey(List<IIfcBuildingStorey> ListofStoreys, IIfcElement selectedElement, List<IIfcWall> AllWalls, List<IIfcSlab> AllSlabs, List<IIfcCurtainWall> AllCurtainWalls, Xbim3DModelContext context)
        { //TODO: auch CurtainWalls übernehmen
            List<IIfcElement> NewElementsforList = new List<IIfcElement>();
            // Liste aller Geschosse nach Elevation sortieren
            List<BuildingStorey> ListBuildingStoreys = new List<BuildingStorey>();
            List<IIfcSpatialElement> ListOfBuildingStoreys = new List<IIfcSpatialElement>();


            //List with Own storey-Type that can be sort
            foreach (var storey in ListofStoreys)
            {
                if (storey.Elevation != null)
                {
                    ListBuildingStoreys.Add(new BuildingStorey { EntityLabel = storey.EntityLabel, Elevation = (double)storey.Elevation });
                    
                }
                else Console.WriteLine("Error - IfcBuildingStorey with no elevation: " + storey.Name.ToString());
                

            }
            ListBuildingStoreys.Sort();

            // select the buildingstorey of the Selected Element               
            IIfcBuildingStorey BuildingStoreySE = FindBuildingStorey(selectedElement);
            //TODO: What to do if no ifcbuildingstorey is defined?
            //Index des selectedStorey ausgeben
            int anzahlstoreys = ListBuildingStoreys.Count();
            if (anzahlstoreys == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("ERROR: no IfcBuildingStoreys declared in the IFC file. Please heal the model first.");
                Console.ReadKey();
                return null;
            }
            int indexSE = ListBuildingStoreys.IndexOf(new BuildingStorey { EntityLabel = BuildingStoreySE.EntityLabel });
            int indexSEMinusOne = indexSE - 1;
            int indexSEPlusOne = indexSE + 1;
            IIfcBuildingStorey BSMinusOne = null;
            IIfcBuildingStorey BSPlusOne = null;
            if (indexSEMinusOne >= 0) //Wenn SE im Index 0 lag, gibt es keine Stockwerke drunter
            {
                foreach (IIfcBuildingStorey w in ListofStoreys.Where(d => d.EntityLabel == ListBuildingStoreys[indexSEMinusOne].EntityLabel))
                {
                    BSMinusOne = w;     //storey under the storey of the selectedElement
                }
            }
            if (indexSEPlusOne <= anzahlstoreys-1) //Wenn SE im letzten Index liegt, gibt es keine Stockwerke darüber
            {
                foreach (IIfcBuildingStorey w in ListofStoreys.Where(d => d.EntityLabel == ListBuildingStoreys[indexSEPlusOne].EntityLabel))
                {
                    BSPlusOne = w;      //storey above the storey of the selectedElement
                }
            }
            

            //Depending on typeof seletedElement
            string SEtype = selectedElement.GetType().Name;
            if (SEtype == ElementType.IfcWall.ToString() || SEtype == ElementType.IfcWallElementedCase.ToString() || SEtype == ElementType.IfcWallStandardCase.ToString())  // selected element is a Wall
            {
                // -------- look at the walls ---------------
                foreach (var wall in AllWalls)
                {
                    IIfcBuildingStorey wallStorey = FindBuildingStorey(wall);

                    if (wallStorey == BuildingStoreySE)
                    {
                        //possible flanking element -> put in new list
                        NewElementsforList.Add(wall);

                    }
                    else if (BSMinusOne != null && wallStorey == BSMinusOne)
                    {

                        //possible flanking element  -> put in new list
                        NewElementsforList.Add(wall);

                    }
                    else if (BSPlusOne != null && wallStorey == BSPlusOne)
                    {

                        //possible flanking element  -> put in new list
                        NewElementsforList.Add(wall);

                    }
                }

                // -------- look the slabs ---------------
                foreach (var slab in AllSlabs)
                {
                    IIfcBuildingStorey wallStorey = FindBuildingStorey(slab);

                    if (wallStorey == BuildingStoreySE)
                    {
                        //possible flanking element -> put in new list
                        NewElementsforList.Add(slab);

                    }
                    else if (wallStorey == BSPlusOne)
                    {
                        //possible flanking element  -> put in new list
                        NewElementsforList.Add(slab);

                    }

                }

                // -------- look the curtain walls ---------------
                foreach (var curtainwall in AllCurtainWalls)
                {
                    IIfcBuildingStorey curtainwallStorey = FindBuildingStorey(curtainwall);
                    if (curtainwallStorey == BuildingStoreySE)
                    {
                        //possible flanking element -> put in new list
                        NewElementsforList.Add(curtainwall);

                    }

                    List<IIfcBuildingStorey> relatedCurtainwallStoreys = FindRelatedBuildingStorey(curtainwall, BuildingStoreySE, context);
                    foreach (var relatedStorey in relatedCurtainwallStoreys)
                    {
                        if (relatedStorey == BuildingStoreySE)
                        {
                            //possible flanking element -> put in new list
                            NewElementsforList.Add(curtainwall);

                        }
                    }

                }

            }

            else if (SEtype == ElementType.IfcSlab.ToString() || SEtype == ElementType.IfcSlabElementedCase.ToString() || SEtype == ElementType.IfcSlabStandardCase.ToString()) // selected element is a slab
            {
                // -------- look the walls ---------------
                foreach (var wall in AllWalls)
                {
                    IIfcBuildingStorey wallStorey = FindBuildingStorey(wall);

                    if (wallStorey == BuildingStoreySE)
                    {
                        //possible flanking element -> put in new list
                        NewElementsforList.Add(wall);

                    }
                    else if (wallStorey == BSMinusOne)
                    {
                        //possible flanking element  -> put in new list
                        NewElementsforList.Add(wall);

                    }
                }


                // -------- look the slabs ---------------
                foreach (var slab in AllSlabs)
                {
                    IIfcBuildingStorey wallStorey = FindBuildingStorey(slab);

                    if (wallStorey == BuildingStoreySE)
                    {
                        //possible flanking element -> put in new list
                        NewElementsforList.Add(slab);

                    }
                    else if (wallStorey == BSMinusOne)
                    {
                        //possible flanking element  -> put in new list
                        NewElementsforList.Add(slab);
                    }

                }

                // -------- look the curtain walls ---------------
                foreach (var curtainwall in AllCurtainWalls)
                {
                    IIfcBuildingStorey curtainwallStorey = FindBuildingStorey(curtainwall);
                    if (curtainwallStorey == BuildingStoreySE)
                    {
                        //possible flanking element -> put in new list
                        NewElementsforList.Add(curtainwall);

                    }

                    List<IIfcBuildingStorey> relatedCurtainwallStoreys = FindRelatedBuildingStorey(curtainwall, BuildingStoreySE, context);
                    foreach (var relatedStorey in relatedCurtainwallStoreys)
                    {
                        if (relatedStorey == BuildingStoreySE)
                        {
                            //possible flanking element -> put in new list
                            NewElementsforList.Add(curtainwall);

                        }
                    }

                }
            }
            else Console.WriteLine("Error with Check for building Storey: selected element has wrong IfcType (no Wall or Slab).");
            return NewElementsforList;

        }

        public static IIfcBuildingStorey FindBuildingStorey(IIfcElement Element)
        {
            // Find the IfcBuildingStorey related to the Element
            // first: all spatialStructure related to the Element
            // then: ifcBuilingStoreys related to it

            List<IIfcRelContainedInSpatialStructure> SpatialStructures = Element.ContainedInStructure.ToList();
            //List<IIfcSpatialElement> ListOfBuildingStoreys = new List<IIfcSpatialElement>();
            //IIfcSpatialElement BuildingStorey = new IIfcSpatialElement();

            foreach (var structure in SpatialStructures)
            {

                IIfcSpatialElement structureElement = structure.RelatingStructure;
                if (structureElement is IIfcBuildingStorey)
                {
                    IIfcBuildingStorey bs = (IIfcBuildingStorey)structureElement;
                    return bs;
                }

            }

            return null;

        }

        public static List<IIfcBuildingStorey> FindRelatedBuildingStorey(IIfcElement Element, IIfcBuildingStorey BuildingStoreySE, Xbim3DModelContext context)
        {

            List<IIfcRelReferencedInSpatialStructure> SpatialStructures = Element.ReferencedInStructures.ToList();
            
            //IIfcSpatialElement BuildingStorey = new IIfcSpatialElement();
            List<IIfcBuildingStorey> ListBs = new List<IIfcBuildingStorey>();


            if (SpatialStructures.Count == 0)
            {
                //No Relation to SpatialStructures found -> Found Min and Maximum of element
                // Compare to Elevation of Selected Element
                // If selected Element is in the same range -> count as possible flanking element
                //TODO: Model Healing : Add Relation to IfcBuildingStorey
                MyElement newMyElement = new MyElement();
                ShapeBoundingBox GeometryOfElement = new ShapeBoundingBox();
                GeometryOfElement.GetShape(Element, newMyElement, context );
                double elevation = (double)BuildingStoreySE.Elevation;
                if (elevation >= newMyElement.Min.Z && elevation < newMyElement.Max.Z)
                {
                    //Element is at the same level as the elevation of the selected element
                    ListBs.Add(BuildingStoreySE);
                }


            }
            else
            {
                foreach (var structure in SpatialStructures)
                {

                    IIfcSpatialElement structureElement = structure.RelatingStructure;

                    if (structure.RelatingStructure.GetType().Name == "IfcBuildingStorey")
                    {
                        ListBs.Add((IIfcBuildingStorey)structureElement);

                    }

                }
            }
            

            return ListBs;

        }

    }
}
