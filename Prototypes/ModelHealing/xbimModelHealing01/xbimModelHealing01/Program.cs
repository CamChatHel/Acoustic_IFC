using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MaterialResource;
using Xbim.Ifc4.UtilityResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Common;
using Xbim.Ifc;
using Xbim.IO;
using System.Diagnostics;
using System.IO;
using Xbim.ModelGeometry.Scene;
using System.Windows.Forms;
using Xbim.Ifc4.SharedBldgElements;
using Xbim.Ifc4.Kernel;
using System.Windows.Media.Media3D;   //braucht als Verweis "Presentation Core" , im Projektmappen-Explorer unter Verweise hinzufuegen!
using System.Xml.Linq;
using System.Windows;


namespace xbimModelHealing01
{
    class Program
    {
        private static LogHandler mylog = new LogHandler();

        static void Main(string[] args)
        {
            //string filename = @"C:\Users\chca426\Documents\GitHub\MyXBim-Fun\IFC\TestModelsforHealing\ModelHealingMissingTypes.ifc"; //Model Healing Missing Types
            //string filenameNeu = @"C:\Users\chca426\Documents\GitHub\MyXBim-Fun\IFC\TestModelsforHealing\ModelHealingMissingTypesHealing01.ifc";
            //string filename = @"C:\Users\chca426\Documents\GitHub\MyXBim-Fun\IFC\TestModelsforHealing\ModelHealingTestMissingBuildingStorey.ifc"; // First Model Healing Only adding missing building storey to element
            //string filenameNeu = @"C:\Users\chca426\Documents\GitHub\MyXBim-Fun\IFC\TestModelsforHealing\ModelHealingTestMissingBuildingStoreyHealing02.ifc";
            //string filename = @"C:\Users\chca426\Documents\GitHub\MyXBim-Fun\IFC\TestModelsforHealing\ModelHealingLooseCovering.ifc"; //Loose Covering

            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\UseCases\ModelHealing\20230405_25_PrototypeTest_Coverings.ifc"; 
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\UseCases\ModelHealing\2021_fhRo_RefBuild_v_03_ifcCurtainwall.ifc";
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\UseCases\ModelHealing\2021_fhRo_RefBuild_v_03_ifcCurtainwall_multipleREferences.ifc";
            // string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\UseCases\2021_fhRo_RefBuild_v_02_stosstellenRefView_HealingAllTypes.ifc";
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\2021_fhRo_RefBuild_v_02_stosstellenRefView.ifc";
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\UseCases\ModelHealing\2021_fhRo_RefBuild_v_03_ifcCurtainwall.ifc";
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\UseCases\THRo_H4\2021_0830_fhRo_H4_bearbeitet_JournalPaper02.ifc";
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\UseCases\2021_fhRo_RefBuild_v_03_stosstellen.ifc"
            string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\33_ModelHealingGeschosse.ifc";

            Console.WriteLine("Welcome to Model Healing for Junction Analysis!");
            Console.WriteLine("Please choose the path and filename you want to analyse.");
            Console.WriteLine("Selected File: " + filename);
            Console.WriteLine("Continue with this file? Yes (y) or No (n)");
            if (Console.ReadLine() == "n")
            {
                Console.Write("New Paths and ifc file:");
                filename = Console.ReadLine();
            }
            mylog.WriteInLog("Starting Model Healing for Model: " + filename);

            //Open Model
            using (IfcStore model = IfcStore.Open(filename))
            {
                //for Geometric Analysis create Context
                Xbim3DModelContext context = new Xbim3DModelContext(model);
                context.CreateContext();

                //List of IfcElements
                List<IIfcElement> templist = new List<IIfcElement>();
                List<IIfcWall> AllWalls = model.Instances.OfType<IIfcWall>().ToList();
                List<IIfcSlab> AllSlabs = model.Instances.OfType<IIfcSlab>().ToList();
                List<IIfcCurtainWall> AllCurtainWalls = model.Instances.OfType<IIfcCurtainWall>().ToList();

                //-------------------- Model Healing Part 01: IfcBuildingStorey --------------------
                #region Add IfcBuildingStorey
                //List all BuildingStoreys
                List<IIfcBuildingStorey> AllStoreys = model.Instances.OfType<IIfcBuildingStorey>().ToList();
                //Sort BuildingStoreys by their Elevation
                List<BuildingStorey> ListBuildingStoreys = new List<BuildingStorey>();
                foreach (var storey in AllStoreys)
                { if (storey.Elevation != null) ListBuildingStoreys.Add(new BuildingStorey { GUID = storey.GlobalId, Elevation = (double)storey.Elevation }); } //Only consider IfcBuildingStorey with an Elevation
                ListBuildingStoreys.Sort();

                //01.1. Check if IfcBuildingStorey exists
                if (AllStoreys is null)
                {
                    //No IfcBuildingStoreys avaiable in the model
                    mylog.WriteInLog("Error: No IfcBuildingStorey.");
                    Console.WriteLine("The model don't uses IfcBuildingStorey. No model Healing possible. Program will stop now.");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("IfcBuildingStorey: OK");

                //01.1. Look at Walls, Slabs, Curtainwalls -> Are they contained in IfcBuildingStorey? IfcRelContainedInSpatialStructure
                foreach (IIfcWall element in AllWalls)
                {
                    Boolean hasbuildingstorey = StoreyHandler.ElementHasBuildingStorey(element);
                    if (hasbuildingstorey == false) { templist.Add(element); }
                   
                }
                foreach (IIfcSlab element in AllSlabs)
                {
                    Boolean hasbuildingstorey = StoreyHandler.ElementHasBuildingStorey(element);
                    if (hasbuildingstorey == false) { templist.Add(element); }

                }
                foreach (IIfcCurtainWall element in AllCurtainWalls)
                {
                    Boolean hasbuildingstorey = StoreyHandler.ElementHasBuildingStorey(element);
                    if (hasbuildingstorey == false) { templist.Add(element); }

                }

                

                //01.2. For elements without BuildingStorey: Add to IfcBuildingStorey 
                if (templist.Count > 0) //
                {
                    Console.WriteLine("Elements Contained in IfcBuildingStorey: start model healing");

                    using (var txn = model.BeginTransaction("Add IfcBuildingStorey to elements"))
                    {
                       // StoreyHandler buildingstorey = new StoreyHandler();
                        mylog.WriteInLog("Healing: Add IfcBuildingStorey to elements.");
                        foreach (var element in templist)
                        {                            
                            ShapeHandler GeometryOfElement = new ShapeHandler();
                            GeometryOfElement.GetShape(element, context);
                            string GUIDstorey = StoreyHandler.HealingBuildingStoreyToAdd(element, ListBuildingStoreys, context);
                            IfcBuildingStorey thebuildingStorey = (IfcBuildingStorey)AllStoreys.FirstOrDefault(r => r.GlobalId == GUIDstorey);
                            thebuildingStorey.AddElement((IfcElement)element); //Create Relation between IfcBuildingStorey and IfcElement
                            mylog.WriteInLog("Element GUID: " + element.GlobalId + " added in IfcBuildingStorey" + GUIDstorey);
                            Console.WriteLine("Element GUID: " + element.GlobalId + " added in IfcBuildingStorey" + GUIDstorey);
                        }
                        
                        
                        txn.Commit();
                    }

                    templist.Clear();
                }
                else
                {
                    Console.WriteLine("Elements Contained in IfcBuildingStorey: OK");
                    mylog.WriteInLog("Elements Contained in IfcBuildingStorey: OK");
                }
                #endregion

                #region Add Reference to IfcBuildingStorey
                //-------------------- Model Healing Part 02: Reference in IfcBuildingStorey for longer walls and curtain walls --------------------
                //TODO: NICHT für Stockwerke in denen die Elemente von Contained sind!! ##################### DRINGEND!!! ####################

                //Check if multiple Ifc RefrencedInSpatialStructure Exists
                foreach (var bs in AllStoreys)
                {
                    if (bs.ReferencesElements.Count() > 1)
                    {
                        Console.WriteLine("Correction of Multiple References in IfcBuildingStorey" + bs.GlobalId);
                        List<IIfcRelReferencedInSpatialStructure> listreferences = bs.ReferencesElements.ToList();
                        List<IIfcProduct> listreferencestoAdd = new List<IIfcProduct>();
                        
                        for (int i = 0; i < listreferences.Count(); i++) //first at index 0 stays and get the new list of related elements
                        {
                            foreach (var item in listreferences[i].RelatedElements)
                            {
                                listreferencestoAdd.Add(item);
                            }
                            
                        }
                        using (var txn = model.BeginTransaction("Merge Reference IfcBuildingStorey to only one reference per IfcBuildingStorey"))
                        {
                            foreach (var item in listreferences)
                            {
                                model.Delete(item);
                            }
                            IfcRelReferencedInSpatialStructure newReference = model.Instances.New<IfcRelReferencedInSpatialStructure>();
                            newReference.RelatingStructure = (IfcSpatialElement)bs;
                            foreach (var item in listreferencestoAdd)
                            {
                                newReference.RelatedElements.Add((IfcProduct)model.Instances.OfType<IIfcProduct>().FirstOrDefault(r => r.GlobalId == item.GlobalId));
                            }
                            txn.Commit();
                        }
                        
                    }
                }

                foreach (IIfcCurtainWall cwall in AllCurtainWalls)
                {
                    
                    ShapeHandler GeometryOfElement = new ShapeHandler();
                    GeometryOfElement.GetHeight(cwall, context);
                    double height = GeometryOfElement.height;
                    IIfcBuildingStorey bs = StoreyHandler.FindBuildingStorey(cwall);
                    int indexbs = ListBuildingStoreys.IndexOf(new BuildingStorey { GUID = bs.GlobalId });
                    for (int i = 1; i < ListBuildingStoreys.Count()-1; i++) //Index starts with 0 -> 1 index less then counted elements
                    {
                        if ((indexbs + i) > ListBuildingStoreys.Count()-1) break; //stop when list is done

                        double deltaH = ListBuildingStoreys.ElementAt(indexbs + i).Elevation - ListBuildingStoreys.ElementAt(indexbs).Elevation;
                        if (height > ( deltaH+ 0.5) ) // 0.5m of border before counting to next storey 
                        {
                            IIfcBuildingStorey bsabove = AllStoreys.FirstOrDefault(d => d.GlobalId == ListBuildingStoreys[indexbs + i].GUID);
                            if (StoreyHandler.ElementIsReferencedinBuildingStorey(cwall, bsabove) == false)
                            {
                                using (var txn = model.BeginTransaction("Add Reference IfcBuildingStorey to elements"))
                                {
                                    IfcCurtainWall elementToAdd = model.Instances.FirstOrDefault<IfcCurtainWall>(r => r.GlobalId == cwall.GlobalId);
                                    IfcBuildingStorey thebuildingStorey = (IfcBuildingStorey)bsabove;
                                    //if IfcRelReferencedInSpatialStructure for this building storey already exist take this one
                                    if (thebuildingStorey.ReferencesElements.FirstOrDefault() == null)
                                    {
                                        IfcRelReferencedInSpatialStructure referenceRelation = model.Instances.New<IfcRelReferencedInSpatialStructure>();
                                        referenceRelation.RelatingStructure = thebuildingStorey;
                                        referenceRelation.RelatedElements.Add((IfcElement)cwall);
                                        mylog.WriteInLog("Element GUID: " + cwall.GlobalId + "(#" + cwall.EntityLabel + ")" + " created NEW reference to IfcBuildingStorey " + thebuildingStorey.GlobalId + "(#" + thebuildingStorey.EntityLabel + ")");
                                        Console.WriteLine("Element GUID: " + cwall.GlobalId + "(#" + cwall.EntityLabel + ")" + " created NEW reference to IfcBuildingStorey " + thebuildingStorey.GlobalId + "(#" + thebuildingStorey.EntityLabel + ")");

                                    }
                                    else
                                    {
                                        thebuildingStorey.ReferencesElements.FirstOrDefault().RelatedElements.Add((IfcElement)cwall);
                                        Console.WriteLine("Element GUID: " + cwall.GlobalId + "(#" + cwall.EntityLabel + ")" + " added to old reference to IfcBuildingStorey " + thebuildingStorey.GlobalId + "(#" + thebuildingStorey.EntityLabel + ")");

                                    }


                                    txn.Commit();
                                }
                            }
                        }
                    }
                }
                foreach (IIfcWall wall in AllWalls)
                {
                    ShapeHandler GeometryOfElement = new ShapeHandler();
                    GeometryOfElement.GetHeight(wall, context);
                    double height = GeometryOfElement.height;
                    IIfcBuildingStorey bs = StoreyHandler.FindBuildingStorey(wall);
                    int indexbs = ListBuildingStoreys.IndexOf(new BuildingStorey { GUID = bs.GlobalId });
                    for (int i = 1; i < ListBuildingStoreys.Count(); i++)
                    {
                        if ((indexbs + i) == ListBuildingStoreys.Count()) break; //stop when list is done
                        
                        double deltaH = ListBuildingStoreys.ElementAt(indexbs + i).Elevation - ListBuildingStoreys.ElementAt(indexbs).Elevation;
                        if (height > (deltaH + 0.5)) // 0.5m of border before counting to next storey 
                        {
                            IIfcBuildingStorey bsabove = AllStoreys.FirstOrDefault(d => d.GlobalId == ListBuildingStoreys[indexbs + i].GUID);
                            if (StoreyHandler.ElementIsReferencedinBuildingStorey(wall, bsabove) == false)
                            {
                                using (var txn = model.BeginTransaction("Add Reference IfcBuildingStorey to elements"))
                                {

                                        IfcWall elementToAdd = model.Instances.FirstOrDefault<IfcWall>(r => r.GlobalId == wall.GlobalId);
                                        IfcBuildingStorey thebuildingStorey = (IfcBuildingStorey)bsabove;
                                        //if IfcRelReferencedInSpatialStructure for this building storey already exist take this one
                                        if (thebuildingStorey.ReferencesElements.FirstOrDefault() == null)
                                        {
                                            IfcRelReferencedInSpatialStructure referenceRelation = model.Instances.New<IfcRelReferencedInSpatialStructure>();
                                            referenceRelation.RelatingStructure = thebuildingStorey;
                                            referenceRelation.RelatedElements.Add((IfcElement)wall);
                                            mylog.WriteInLog("Element GUID: " + wall.GlobalId + "(#" + wall.EntityLabel + ")" + " created NEW reference to IfcBuildingStorey " + thebuildingStorey.GlobalId + "(#" + thebuildingStorey.EntityLabel + ")");
                                            Console.WriteLine("Element GUID: " + wall.GlobalId + "(#" + wall.EntityLabel + ")" + " created NEW reference to IfcBuildingStorey " + thebuildingStorey.GlobalId + "(#" + thebuildingStorey.EntityLabel + ")");

                                        }
                                        else
                                        {
                                            thebuildingStorey.ReferencesElements.FirstOrDefault().RelatedElements.Add((IfcElement)wall);
                                            Console.WriteLine("Element GUID: " + wall.GlobalId + "(#" + wall.EntityLabel + ")" + " added to old reference to IfcBuildingStorey " + thebuildingStorey.GlobalId + "(#" + thebuildingStorey.EntityLabel + ")");
                                        }


                                        txn.Commit();
                                    
                                }
                            }
                        }
                    }
                }

                
                #endregion
                Console.WriteLine("Elements Referenced in IfcBuildingStorey: OK");
                //-------------------- Model Healing Part 03: IfcType and IfcMaterialLayerSet --------------------
                //ElementsToChange
                List<IIfcElement> AllElementswithTypeMissing = new List<IIfcElement>();
                List<IIfcTypeObject> AllTypeswithMaterialMissing = new List<IIfcTypeObject>();
                TypeHandler checktypes = new TypeHandler();
                foreach (IIfcWall element in AllWalls)
                {
                    if (checktypes.CheckforElementType(element) == false) AllElementswithTypeMissing.Add(element);
                    else
                    {
                        if (checktypes.CheckforMaterialInType(element) == false) AllTypeswithMaterialMissing.Add(element.IsTypedBy.FirstOrDefault().RelatingType);  
                    }
                }
                foreach (IIfcCurtainWall element in AllCurtainWalls)
                {
                    if (checktypes.CheckforElementType(element) == false) AllElementswithTypeMissing.Add(element);
                    else
                    {
                        if (checktypes.CheckforMaterialInType(element) == false) AllTypeswithMaterialMissing.Add(element.IsTypedBy.FirstOrDefault().RelatingType);
                    }
                }
                foreach (IIfcSlab element in AllSlabs)
                {
                    if (checktypes.CheckforElementType(element) == false) AllElementswithTypeMissing.Add(element);
                    else
                    {
                        if (checktypes.CheckforMaterialInType(element) == false) AllTypeswithMaterialMissing.Add(element.IsTypedBy.FirstOrDefault().RelatingType);
                    }
                }
                if (AllElementswithTypeMissing.Count == 0) mylog.WriteInLog("Healing part 03: No Elements without ElementType.");
                else
                {
                    Console.WriteLine("Elements with Missing Type: START Healing");
                    using (var txn = model.BeginTransaction("Type creation"))
                    {
                        foreach (IIfcElement element in AllElementswithTypeMissing) //all this elements have no type
                        {
                            
                            //Search for MaterialLayer in Element
                            if (element.HasAssociations.OfType<IIfcRelAssociatesMaterial>().FirstOrDefault() == null)
                            {
                                //No Type and no MaterialLayer
                                mylog.WriteInLog("Element " + element.GlobalId + " has no Type and no MaterialLayer");
                                Console.WriteLine("This element has no type and no MaterialLayer: " + element.GlobalId);
                                Console.WriteLine("Please give this elementtype a name:");
                                string typename = Console.ReadLine();
                                TypeHandler.SetupNewTypes(model, element, typename);
                                mylog.WriteInLog("New type create for " + element.Declares.ToString() + "GUID: " + element.GlobalId + "(#" + element.EntityLabel + ")");
                                Console.WriteLine("New type create for " + element.Declares.ToString() + "GUID: " + element.GlobalId + "(#" + element.EntityLabel + ")");
                            }
                            else //Relation to MaterialLayer exist for this element -> search other elements with same MaterialLayerSet
                            {                                
                                
                                MaterialHandler Findmaterials = new MaterialHandler();
                                IIfcMaterialLayerSet materialLayerSet = null;
                                materialLayerSet = Findmaterials.FindMaterialLayerSet(element);
                                IfcElementType type = (IfcElementType)checktypes.FindTypeWithMaterialLayerSet(model, materialLayerSet);
                                if (type != null) //another element has the same materialLayerSet and an elementType
                                {
                                    TypeHandler.SetToExistingType(element, type);
                                    mylog.WriteInLog("Elementtype " + type.GlobalId + "(#" + type.EntityLabel + ")" + " added to element " + element.GlobalId + "(#" + element.EntityLabel + ")");
                                    Console.WriteLine("Elementtype " + type.GlobalId + "(#" + type.EntityLabel + ")" + " added to element " + element.GlobalId + "(#" + element.EntityLabel + ")");

                                    //TODO: create RelassociatesMaterial and MaterialLayerSetUsage für Element, Verbniden SetUsage mit LayerSt aus Type

                                }
                                else //no other elementType with this materialLayerSet exist -> create type +relationtoType + type to materialrelation
                                {
                                    //create type and connect it
                                    mylog.WriteInLog("Element " + element.GlobalId + "(#" + element.EntityLabel + ")" + " has no Type");
                                    Console.WriteLine("This element has no type: " + element.GlobalId);
                                    Console.WriteLine("Please give this elementtype a name:");
                                    string typename = Console.ReadLine();
                                    TypeHandler.SetupNewTypes(model, element, typename);
                                    //connect materialLayerSet to Type
                                    IfcRelAssociatesMaterial relMaterial = model.Instances.New<IfcRelAssociatesMaterial>();
                                    relMaterial.RelatedObjects.Add(type);
                                    relMaterial.RelatingMaterial = materialLayerSet;

                                }

                            }

                        }
                        txn.Commit();

                    }
                }
                if (AllTypeswithMaterialMissing.Count == 0) mylog.WriteInLog("Healing part 03: All Elements has ElementType.");
                else
                {
                    Console.WriteLine("Elements with Type but Missing Material: START Healing");
                    using (var txn = model.BeginTransaction("Add Material to existing Types"))
                    {
                        foreach (IIfcTypeObject elementtype in AllTypeswithMaterialMissing)
                        {
                            
                            if (checktypes.CheckforMaterialInType(elementtype) == false)
                            {
                                mylog.WriteInLog("Element " + elementtype.GlobalId + " has no MaterialLayer");
                                Console.WriteLine("This elementtype " + elementtype.Name + "(" + elementtype.GlobalId + ") has no MaterialLayer: ");

                                var RelDefinesToElement = elementtype.Types.FirstOrDefault();
                                var element = (IIfcElement)RelDefinesToElement.RelatedObjects.FirstOrDefault();
                                MaterialHandler Findmaterials = new MaterialHandler();
                                IIfcMaterialLayerSet materialLayerSet = null;
                                materialLayerSet = Findmaterials.FindMaterialLayerSet(element);

                                //connect materialLayerSet to Type
                                IfcRelAssociatesMaterial relMaterial = model.Instances.New<IfcRelAssociatesMaterial>();
                                relMaterial.RelatedObjects.Add(elementtype);
                                relMaterial.RelatingMaterial = materialLayerSet;

                                mylog.WriteInLog("Added IfcMaterialLayer " + materialLayerSet + " to ElementType " + elementtype.GlobalId);
                                Console.WriteLine("Added MaterialLayerSet to Elementtype " + elementtype.GlobalId);
                            }


                        }
                        txn.Commit();
                    }
                }


                Console.WriteLine("Elements with Type and Material: OK");
                //-------------------- Model Healing Part 04: Loose IfcCovering -> Aggregation  --------------------
                List<IIfcCovering> AllCoverings = model.Instances.OfType<IIfcCovering>().ToList();
                mylog.WriteInLog("ModelHealing 04: ");
                foreach (IIfcCovering covering in AllCoverings)
                {
                    //IFC4 healing: IfcRelCoversBldgElements is deprecated and replaces by IfcRelAggregates.
                    List<IIfcRelCoversBldgElements> relcoverEl = covering.CoversElements.ToList();
                    List<IIfcRelAggregates> relcoverAg = covering.Decomposes.ToList();
                    if (relcoverAg.Count() == 0 && relcoverEl.Count() == 0)
                    {
                        mylog.WriteInLog("Loose IfcCovering with no aggregation or IfcRelCoversBldgElements: " + covering.GlobalId + " (#"+covering.EntityLabel+")");
                        Console.WriteLine("IfcCovering wihtout Aggregation: " + covering.GlobalId + " (#" + covering.EntityLabel + ")");
                        Console.WriteLine("Choose element to bind the covering to (enter GUID): ");
                        string elementID= Console.ReadLine();
                        IIfcElement element = model.Instances.OfType<IIfcElement>().Where(s => s.GlobalId == elementID).First();
                        using (var txn = model.BeginTransaction("Covering Aggregation"))
                        {
                            CoveringHandler.AddCoveringToElement(covering, element, model);
                            CoveringHandler.DeleteSpatialRelationOfCovering(covering, model);
                            txn.Commit();
                        }
                            
                    }
                }
                Console.WriteLine("Loose IfcCovering: OK");


                //-------------------- Model Healing Part 05: Small IfcWall oder IfcSlab ??  --------------------
                //--------------------               OR merge elements                       --------------------

                Console.WriteLine("Do you want to merge some elements? (y) yes or (n) no ");
                string line = Console.ReadLine();
                while (line == "y")
                {
                    Console.WriteLine("Give 2 elements to merge:");
                    Console.WriteLine("element 1 (GUID):  ");
                    string element1ID = Console.ReadLine();
                    Console.WriteLine("element 2 (GUID):  ");
                    string element2ID = Console.ReadLine();

                    IIfcElement element1 = model.Instances.FirstOrDefault<IIfcElement>(s => s.GlobalId == element1ID);
                    IIfcElement element2 = model.Instances.FirstOrDefault<IIfcElement>(s => s.GlobalId == element2ID);

                    if (element1 != null && element2 != null)
                    {
                        //Shape of elements -> find Bounding Boxes
                        Point3D Element1Min = new Point3D(0, 0, 0);
                        Point3D Element1Max = new Point3D(0, 0, 0);
                        ShapeBoundingBox FindShapeBox = new ShapeBoundingBox();
                        FindShapeBox.GetShape(element1, context, Element1Min, Element1Max);

                        Point3D Element2Min = new Point3D(0, 0, 0);
                        Point3D Element2Max = new Point3D(0, 0, 0);
                        ShapeBoundingBox FindShapeBox2 = new ShapeBoundingBox();
                        FindShapeBox2.GetShape(element2, context, Element2Min, Element2Max);

                        using (var txn = model.BeginTransaction("New element"))
                        {
                            if (element1.GetType().Name == "IfcWall" || element1.GetType().Name == "IfcWallStandardCase" || element1.GetType().Name == "IfcWallElementedCase")
                            {
                                IfcWall newelement = model.Instances.New<IfcWall>();
                                IfcRelAggregates newaggregation = model.Instances.New<IfcRelAggregates>();
                                newaggregation.RelatingObject = newelement;
                                newaggregation.RelatedObjects.Add((IfcWall)element1);
                                newaggregation.RelatedObjects.Add((IfcWall)element2);

                                //change spatial relations to new element
                                SpatialRelationHandler relationhandler = new SpatialRelationHandler();
                                List<IIfcRelReferencedInSpatialStructure> newReferenced = element1.ReferencedInStructures.ToList();
                                if (newReferenced.Count() != 0)
                                {
                                    foreach (var item in newReferenced)
                                    {
                                        relationhandler.AddSpatialReferenceToElement(item.RelatingStructure, newelement, model);

                                    }

                                    relationhandler.DeleteSpatialReferences(element1);
                                    relationhandler.DeleteSpatialReferences(element2);
                                }


                                IIfcRelContainedInSpatialStructure newContained = element1.ContainedInStructure.First();
                                if (newContained != null)
                                {
                                    relationhandler.AddSpatialContainmentToElement((IIfcSpatialElement)newContained.RelatingStructure, newelement, model);

                                    relationhandler.DeleteSpatialContainement(element1);
                                    relationhandler.DeleteSpatialContainement(element2);
                                }

                                //add elementtype to new element
                                IfcRelDefinesByType relationtoType = element1.IsTypedBy.OfType<IfcRelDefinesByType>().First();
                                relationtoType.RelatedObjects.Add(newelement);



                                Console.WriteLine("We aggregated element" + element1.GlobalId + " and element " + element2.GlobalId + " to the IfcWall " + newelement.GlobalId);

                            }
                            else if (element1.GetType().Name == "IfcSlab" || element1.GetType().Name == "IfcSlabStandardCase" || element1.GetType().Name == "IfcSlabElementedCase")
                            {
                                IfcSlab newelement = model.Instances.New<IfcSlab>();
                                IfcRelAggregates newaggregation = model.Instances.New<IfcRelAggregates>();
                                newaggregation.RelatingObject = newelement;
                                newaggregation.RelatedObjects.Add((IfcSlab)element1);
                                newaggregation.RelatedObjects.Add((IfcSlab)element2);

                                //change spatial relations to new element
                                SpatialRelationHandler relationhandler = new SpatialRelationHandler();
                                List<IIfcRelReferencedInSpatialStructure> newReferenced = element1.ReferencedInStructures.ToList();
                                relationhandler.AddSpatialReferenceToElement((IIfcSpatialElement)newReferenced, newelement, model);

                                IIfcRelContainedInSpatialStructure newContained = element1.ContainedInStructure.First();
                                relationhandler.AddSpatialContainmentToElement((IIfcSpatialElement)newContained, newelement, model);

                                relationhandler.DeleteSpatialContainement(element1);
                                relationhandler.DeleteSpatialReferences(element1);
                                relationhandler.DeleteSpatialContainement(element2);
                                relationhandler.DeleteSpatialReferences(element2);

                                //add elementtype to new element
                                IfcRelDefinesByType relationtoType = element1.IsTypedBy.OfType<IfcRelDefinesByType>().First();
                                relationtoType.RelatedObjects.Add(newelement);

                                Console.WriteLine("We aggregated element" + element1.GlobalId + " and element " + element2.GlobalId + " to the IfcSlab " + newelement.GlobalId);
                            }
                            else
                            {
                                Console.WriteLine("no merging possible: element is not a wall. element is not a slab.");
                            }


                            txn.Commit();
                        }

                    }
                    else
                    {
                        Console.WriteLine("Elements do not exist.");
                    }
                    Console.WriteLine("Do you want to merge some more elements? (y) yes or (n) no ");
                     line = Console.ReadLine();
                } 
                

                // --------------------             END and SAVE file            --------------------


                string pathname = Path.GetDirectoryName(filename);
                string filename2 = Path.GetFileNameWithoutExtension(filename) + "_Healing.ifc";
                string PathAndFile = pathname + "\\" + filename2;
                model.SaveAs(PathAndFile);

                Console.WriteLine("Model Healing done");
                Console.WriteLine("New file in:" + PathAndFile);
                Console.ReadKey();

            }



            mylog.EndLog();
            
            

            ////Aenderungen durchfuehren
            //using (var txn = model.BeginTransaction("Change Wall to Covering"))
            //{


            //    // commit changes
            //    txn.Commit();
            //}

            

        }

        
        
    }
}
