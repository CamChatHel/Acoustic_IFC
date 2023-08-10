using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Xbim.Common.Geometry;
using Xbim.Common.Step21;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.Common.XbimExtensions;
using Xbim.ModelGeometry.Scene;
using System.Windows.Media.Media3D;  //braucht als Verweis "Presentation Core" , im Projektmappen-Explorer unter Verweise hinzufuegen!

using Xbim.Ifc4.UtilityResource;

using Xbim.Ifc4.MeasureResource;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.ExternalReferenceResource;

using Xbim.Ifc2x3.Kernel;
using Xbim.Ifc2x3.ExternalReferenceResource;
using Xbim.Ifc2x3.Interfaces;

using Newtonsoft.Json;
using Prototype01.JSONexport;
using static Prototype01.TransmissionPath;
using System.Reflection;
using System.Xml.Linq;


namespace Prototype01
{
    class Program
    {


        static void Main(string[] args)
        {
            // string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\23_20200604CamilleExport2.ifc";   // -> DATEI 1
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\09_XStoss_horizontal_1.ifc";  //Verbindungen mit RelConnects -> DATEI 2
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\23_20200604 Camille.ifc"; // Datei 3 -> reduziertes Model
            // string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\23_20200604CamilleExport4.ifc"; // Datei 4 -> neuer export
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\24_PrototypeTest - Kopie.ifc";   // -> DATEI 5
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\26_WandmitSchichten04_loadBearingHinzugefuegt.ifc";   // -> DATEI 6
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\24_PrototypeTest02.ifc";   // -> DATEI 7
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\24_PrototypeTest_mitFassade04_HealingAllTypes.ifc";   // -> DATEI 8 (Version 05 hat für alle Elemente Types
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\26_WandmitSchichten02 - Kopie.ifc"; //-> DAtei9
            //string filename = @"\\141.60.202.49\private\01_FuE\08_SchalluSchwing\99_Messungen_Modelle_Anderes\ReferenzProzess\20211405_Referenzmodell_RefBuild_v_01\2021_fhRo_RefBuild_v_03_coreLayer.ifc";   // -> DATEI 10, REferenzmodell v01
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\Referenzmodelle\2021_fhRo_RefBuild_v_04_ifcCoverings.ifc";   // -> DATEI 11, REferenzmodell v01
            //string filename = @"C:\Users\chca426\Documents\SchalluSchwing\IFC-Beispiele\buildingSMARTBeispielWallElementedCase.ifc"; //Datei 13, Wand elementiert
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\Referenzmodelle\2021_07_29_Tsts_H4\2021_0830_fhRo_H4_bearbeitet_JournalPaper02.ifc"; // Datei 14: H4 Use Journal Paper
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\27_TStoss_horizontal_Varainten02.ifc";  // Datei 15: einzelen stossstellen
            //string filename = @"H:\Promotion\Veröffentlichungen\2022_JournalPaperAdvancedEngineeringInformatics\useCase H4\2021_0830_fhRo_H4_JournalPaper02_02ohneBalkon.ifc"; //Datei 16 -> H4 fuer journal paper
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\28_VerschobeneRaeume_IFC4RV_1stLevel_HealingAllTypes.ifc";   // -> DATEI 17 
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\29_StossParalleleWand.ifc"; //Datei18
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\Referenzmodelle\20210921_Stocker_WoodBuilding.ifc";   // -> DATEI 12, Trennwand: 17o_XuNmTCJQJLid67NoQX
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\Referenzmodelle\20210721_Lauschke_HolzbauIfc4_bearbeitet.ifc";   // DATEI 20 : Test mit Realizingelements
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\UseCases\KIT_OfficeBuilding\KIT_PhantasyOfficeBuilding_IFC4_HealingAllTypes.ifc"; //Datei 21
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\revit\29_StossParalleleWand.ifc"; //Datei 22
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\UseCases\2021_fhRo_RefBuild_v_03_stosstellen_Healing.ifc"; //Datei 23 Test mit Stossstellen
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\UseCases\2021_fhRo_RefBuild_v_02_stosstellenRefView_Healing02_Healing.ifc";
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\UseCases\THRo_H4\2021_0830_fhRo_H4_JournalPaper02_02ohneBalkon.ifc"; //Trennwand: 3zj1mBwqfAdeKX2u8dRXtn , Decke: 0swLdnuPnEPRBPhAKBX9MS
           // string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\UseCases\THRo_H4\Variante02\2021_06_fhRo_H4_Tst4_MitSpaceBoundaries-IFC4AddedSB03.ifc";
            //string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\UseCases\28_VerschobeneRaeume20230612.ifc";
            string filename = @"C:\Users\chca426\Documents\GitHub\Dissertation\Modelle\UseCases\KIT_OfficeBuilding\KIT_PhantasyOfficeBuilding_IFC4.ifc";


            string SaveConsoleToTxtFile = DateTime.Now.ToString() ;

            Console.WriteLine("Welcome to Junction Analysis!");
            SaveConsoleToTxtFile = SaveConsoleToTxtFile +  "\n" + "Welcome to Junction Analysis!";
            Console.WriteLine("Please choose the path and filename you want to analyse.");
            SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Please choose the path and filename you want to analyse.";
            Console.WriteLine("Selected File: " + filename);
            SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Selected File: " + filename;
            Console.WriteLine("Continue with this file? Yes (y) or No (n)");
            if (Console.ReadLine() == "n")
            {
                Console.Write("New Paths and ifc file:");
                filename = Console.ReadLine();
            }


            List<Xbim.Ifc4.Interfaces.IIfcElement> ListofPossibleFlankingelements = new List<Xbim.Ifc4.Interfaces.IIfcElement>();
            List<Xbim.Ifc4.Interfaces.IIfcElement> ListofFlankingelements = new List<Xbim.Ifc4.Interfaces.IIfcElement>();
            List<Xbim.Ifc4.Interfaces.IIfcElement> ListofFlankingelementsSpaceBoundaries = new List<Xbim.Ifc4.Interfaces.IIfcElement>();
            List<MyElement> ListofFlankingMyelements = new List<MyElement>();
            List<Xbim.Ifc4.Interfaces.IIfcElement> ListofFlankingelementsSecondDegree = new List<Xbim.Ifc4.Interfaces.IIfcElement>();
            List<MyElement> ListofFlankingMyelementsSecondDegree = new List<MyElement>();
            MyElement selectedMyElement = new MyElement();
            List<Xbim.Ifc4.Interfaces.IIfcElement> ListofRealizingElements = new List<Xbim.Ifc4.Interfaces.IIfcElement>();
                    //Junction Fastener from selected Element to Flanking Element //TODO: Matrix?
            //List<IIfcElement> ListRealizingElementsToFE1;
            //List<IIfcElement> ListRealizingElementsToFE2;
            //List<IIfcElement> ListRealizingElementsToFE3;

            bool myOwnDebugConsole = true; //true = alle Zwischenergebnisse anzeigen
            MyDebugging MyOwnDebug = new MyDebugging();

            bool UseOnlyCorelayer = false; //true= yes, check for corelayers, false= ignore corelayer stuff

            ////DataTable to save flanking elements
            //DataTable tableFlankingElements = new DataTable();
            //tableFlankingElements.Columns.Add("ID", typeof(int));
            //tableFlankingElements.Columns.Add("EntityLabel", typeof(string));
            //tableFlankingElements.Columns.Add("Myelement", typeof(MyElement));

            //Selected Element
            //string elementID = "11hR$VkvbCy9dUxCK$EsVu";  // 11hR$VkvbCy9dUxCK$EsVu = Trennwand (#1145)  // 2liQ0BYiL3QwLQLcRDpIgN = slab (#405) // -> zu DATEI 1
            //string elementID = "0T1$TlhUf8_f2ueO1CDdzG";  // -> zu DATEI 2
            //string elementID = "11hR$VkvbCy9dUxCK$EsVu";  // (11hR$VkvbCy9dUxCK$EsVu  -> wall) -> zu DATEI 3, #598
            //string elementID = "11hR$VkvbCy9dUxCK$EsVu"; // -> zu Datei 4
            //string elementID = "1fvduOu4LCIxab6Glf9OVH";  // Trenndecke (#1118): 1fvduOu4LCIxab6Glf9OVH // Trennwand(#968):  11hR$VkvbCy9dUxCK$EsVu // -> zu DATEI 5
            //string elementID = "2_0FU55uL7_PwPLl9SywKX";  // Wand (#229): 2_0FU55uL7_PwPLl9SywKX  -> zu DATEI 6
            //string elementID = "11hR$VkvbCy9dUxCK$EsVu";  // Trenndecke (#1118): 1fvduOu4LCIxab6Glf9OVH // Trennwand(#1540):  11hR$VkvbCy9dUxCK$EsVu // -> zu DATEI 7
            // string elementID = "11hR$VkvbCy9dUxCK$EsVu";  // Trenndecke (#1789): 1fvduOu4LCIxab6Glf9OVH // Trennwand(#1546):  11hR$VkvbCy9dUxCK$EsVu // -> zu DATEI 8
            //string elementID = "2_0FU55uL7_PwPLl9SywKX";  //CLT-Wand mit Schichten zu 26_WandmitSchichten03.ifc zu Datei 9
            //string elementID = "0i8nVeTTf6ox2YVT2SRFFG"; //zu Datei 10 -> Wall #2530 (0i8nVeTTf6ox2YVT2SRFFG) und Datei 11
            //string elementID = "2ucZRLBGP4uxZW$9i1VAZ8"; //zu Datei 13 nur eine Wand
            //string elementID = "3Gu$FppSz81v_$kmG_xEpz"; // zu Datei 14: Trennwand 3Gu$FppSz81v_$kmG_xEpz
            //string elementID = "0XQ0wCR094wOEfFSEbZNrh";  // zu Datei 15
            //string elementID = "0swLdnuPnEPRBPhAKBX9MS";  // zu Datei 16 -Y H4 Decke fuer Journal Paper
            //string elementID = "2iiXkdgjbBg8WeQhDmT02m"; //zu Datei 17  Trennwand mit 2 Räumen: 2iiXkdgjbBg8WeQhDmT02m ; Trennwand mit 3 Räumen: 2iiXkdgjbBg8WeQhDmT02m
            //string elementID = "2356lOatvCag54nFb8uly1"; //zu Datei 18
            //string elementID = "2wXoso8_r4TfvwrFGaJp1J"; //zu Datei 12, 06.07.2021: Trennwand: 3rYQh0hp15IfC5Nwvt7z6P, trenndecke: / Datei vom07.07.2021: 3M$RbC7_r3LQJZgyqFMOhx
            //string elementID = "1FTcjgX0T7t9VWuk$4y3aS"; //zu Datei 20
            //string elementID = "15DaMtku1EowtkGqYps1Sn"; // zu Datei 21
            //string elementID = "2356lOatvCag54nFb8uly1"; // zu Datei 22
            string elementID = "3zj1mBwqfAdeKX2u8dRXtn"; // Wand: 3zj1mBwqfAdeKX2u8dRXtn Decke:0swLdnuPnEPRBPhAKBX9MS Variante02: Trennwand: 3zj1mBwqfAdeKX2u8dRXtn
            // ************ Open IFC-model ************ 
            

            using (IfcStore model = IfcStore.Open(filename))
            {
                Xbim3DModelContext context = new Xbim3DModelContext(model);
                context.CreateContext();

                Console.WriteLine("Select Element with 2 Rooms? Yes (y) or No (n)");
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine("select room 1: ");
                    var room1ID = Console.ReadLine();
                    Console.WriteLine("select room 2: ");
                    var room2ID = Console.ReadLine();

                    Xbim.Ifc4.Interfaces.IIfcSpace room1 = model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcSpace>(d => d.GlobalId == room1ID);
                    Xbim.Ifc4.Interfaces.IIfcSpace room2 = model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcSpace>(d => d.GlobalId == room2ID);
                    RoomSelection UseRoomForSelection = new   RoomSelection();
                    UseRoomForSelection.GetSeparatingElement(room1, room2, context);
                    elementID = UseRoomForSelection.separatingElementID;
                }

                //Console.Write("new seleted Element GUID:");
                //elementID = Console.ReadLine();
                //Console.WriteLine("Choose the selected element:");
                Console.WriteLine("Selected Element GUID: " + elementID);
                
                Console.WriteLine("Continue with this element? Yes (y) or No (n)");
                
                if (Console.ReadLine() == "n")
                {
                    Console.Write("new seleted Element GUID:");
                    elementID = Console.ReadLine();
                }

                

                Xbim.Ifc4.Interfaces.IIfcElement selectedElement = model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(d => d.GlobalId == elementID);

                while (selectedElement == null) 
                {
                    Console.WriteLine("Error: No selected Element with GUID " + elementID + " found in data model");
                    Console.WriteLine("Choose another GUID for selected element:");
                    elementID = Console.ReadLine();
                    selectedElement = model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(d => d.GlobalId == elementID);
                }

                var propertiesSelectedElement = selectedElement.IsDefinedBy
                        .Where(r => r.RelatingPropertyDefinition is Xbim.Ifc4.Interfaces.IIfcPropertySet)
                        .SelectMany(r => ((Xbim.Ifc4.Interfaces.IIfcPropertySet)r.RelatingPropertyDefinition).HasProperties)
                        .OfType<Xbim.Ifc4.Interfaces.IIfcPropertySingleValue>();
                foreach (var property in propertiesSelectedElement)
                {
                    if (property.Name == "IsExternal" && property.NominalValue.ToString() == "true")
                    {
                        Console.WriteLine("Error: Selected Element with GUID " + elementID + " is external element");
                        Console.ReadKey();
                        return;
                    }
                }

                
                Console.WriteLine("ausgewähltes Element: ");
                Console.WriteLine(selectedElement);
                Console.WriteLine("------------------------------------------------------------------------------ ");
                SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Selected Element GUID: " + elementID;
                SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "------------------------------------------------------------------------------ ";


                selectedMyElement.Nr = selectedElement.EntityLabel;
                selectedMyElement.GetElementType(selectedElement.GetType().Name);
                selectedMyElement.GUID = selectedElement.GlobalId;

                //Possible flanking elemnts
                List<Xbim.Ifc4.Interfaces.IIfcWall> AllWalls = model.Instances.OfType<Xbim.Ifc4.Interfaces.IIfcWall>().Where(d => !(d.GlobalId == elementID)).ToList();
                List<Xbim.Ifc4.Interfaces.IIfcSlab> AllSlabs = model.Instances.OfType<Xbim.Ifc4.Interfaces.IIfcSlab>().Where(d => !(d.GlobalId == elementID)).ToList();
                List<Xbim.Ifc4.Interfaces.IIfcCurtainWall> AllCurtainWalls = model.Instances.OfType<Xbim.Ifc4.Interfaces.IIfcCurtainWall>().ToList();

                //Element Types - to find Core Layer per Type and not per Element
                List<Xbim.Ifc4.Interfaces.IIfcTypeObject> ListofAllTypes = new List<Xbim.Ifc4.Interfaces.IIfcTypeObject>();
                Xbim.Ifc4.Interfaces.IIfcTypeObject elementtype;
                List<CheckLayers> ListofAllTypesLayers = new List<CheckLayers>();
                CheckLayers elementtypeLayers = new CheckLayers();

                List<Xbim.Ifc4.Interfaces.IIfcElement> templist = new List<Xbim.Ifc4.Interfaces.IIfcElement>();

                #region Check for Connection
                // --------------------------------------------------------------------------------------------------------------------------------- 

                // --------------------------------                Check for Connection Relation               ----------------------------------------

                // --------------------------------------------------------------------------------------------------------------------------------- 

                //RelConnection suchen
                List<Xbim.Ifc4.Interfaces.IIfcRelConnectsElements> connectionFrom = selectedElement.ConnectedFrom.ToList();
                List<Xbim.Ifc4.Interfaces.IIfcRelConnectsElements> connectionTo = selectedElement.ConnectedTo.ToList();

                templist = CheckConnection.GetConnections(connectionFrom, connectionTo);

                if (templist.Count == 0)
                {
                    //No IfcBuildingStoreys avaiable in the model
                    Console.WriteLine("WARNING: No IfcRelConnects exists for the selected element");
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "WARNING: No IfcRelConnects exists for the selected element";
                }
                else
                {
                    
                    ListofPossibleFlankingelements.AddRange(templist);
                    // remove duplicates
                    ListofPossibleFlankingelements = ListofPossibleFlankingelements.Distinct().ToList();
                    ClearList.RemoveFromWalls(ref AllWalls, ListofPossibleFlankingelements);
                    ClearList.RemoveFromSlabs(ref AllSlabs, ListofPossibleFlankingelements);
                }
                
                templist.Clear();
                //Zwischenergebnisse in Console anzeigen? 
                MyOwnDebug.IntermediateResults(myOwnDebugConsole, "Flankierende Elemente nach Connection-Check:", ListofPossibleFlankingelements, SaveConsoleToTxtFile);
                

                #endregion

                #region Check for Building Storey
                // --------------------------------------------------------------------------------------------------------------------------------- 

                // --------------------------------                Check for building Storey                ----------------------------------------

                // --------------------------------------------------------------------------------------------------------------------------------- 


                //All BuildingStoreys
                List <Xbim.Ifc4.Interfaces.IIfcBuildingStorey> AllStoreys = model.Instances.OfType<Xbim.Ifc4.Interfaces.IIfcBuildingStorey>().ToList();

                templist = CheckBuildingStorey.GetBuildingStorey(AllStoreys, selectedElement, AllWalls, AllSlabs, AllCurtainWalls,  context);
                if (templist is null)
                {
                    //No IfcBuildingStoreys avaiable in the model
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Invalid Model: No IfcBuildingStorey";
                    Console.WriteLine("Invalid Model: No IfcBuildingStorey. Program will stop now.");
                    Console.ReadKey();
                    return;
                }
                
                //Add to List
                ListofPossibleFlankingelements.AddRange(templist);
                // remove duplicates
                ListofPossibleFlankingelements = ListofPossibleFlankingelements.Distinct().ToList();
                ClearList.RemoveFromWalls(ref AllWalls, ListofPossibleFlankingelements);
                ClearList.RemoveFromSlabs(ref AllSlabs, ListofPossibleFlankingelements);
                ClearList.RemoveFromCurtainWalls(ref AllCurtainWalls, ListofPossibleFlankingelements);
                templist.Clear();

                //Zwischenergebnisse in Console anzeigen? 
                MyOwnDebug.IntermediateResults(myOwnDebugConsole, "Flankierende Elemente nach Storey-Check:", ListofPossibleFlankingelements, SaveConsoleToTxtFile);

                #endregion

                #region Check for Space Boundary Elements
                // --------------------------------------------------------------------------------------------------------------------------------- 

                // --------------------------------                Check for Space Boundary Elements               ----------------------------------------

                // --------------------------------------------------------------------------------------------------------------------------------- 

                List<Xbim.Ifc4.Interfaces.IIfcSpace> ListOfBoundarySpaces = new List<Xbim.Ifc4.Interfaces.IIfcSpace>();
                List<Xbim.Ifc4.Interfaces.IIfcRelSpaceBoundary> spaceBoundaries = selectedElement.ProvidesBoundaries.ToList();
                bool prefilterSpaceBoundaries = false; //false = no prefilter for space boundaries, true = prefilter with space boundaries
                if (spaceBoundaries.Count == 0)
                {   //No possibility to filter, only distance
                    Console.WriteLine("WARNING: No space Boundaries related to the selected element");
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "WARNING: No space Boundaries related to the selected element";
                    List<Xbim.Ifc4.Interfaces.IIfcSpace> AllSpaces = model.Instances.OfType<Xbim.Ifc4.Interfaces.IIfcSpace>().ToList();
                    if (AllSpaces.Count == 0) Console.WriteLine("WARNING: No IfcSpaces in the model");
                }
                else if (spaceBoundaries.Count > 2)
                {
                    Console.WriteLine("WARNING: More then 2 SpaceBoundaries are related to the separating element.");
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "WARNING: More then 2 SpaceBoundaries are related to the separating element.";
                    prefilterSpaceBoundaries = true;
                }
                else
                {
                    Console.WriteLine("Some SpaceBoundaries are related to the separating element.");
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Some SpaceBoundaries are related to the separating element.";
                }
                Console.ReadKey();

                //Add to list
                CheckSpaceBoundary FilteredSpaces = new CheckSpaceBoundary();
                templist = FilteredSpaces.GetSpaceBoundary(spaceBoundaries, selectedElement);
                
                ListofPossibleFlankingelements.AddRange(templist);
                // remove duplicates
                ListofPossibleFlankingelements = ListofPossibleFlankingelements.Distinct().ToList();
                ClearList.RemoveFromWalls(ref AllWalls, ListofPossibleFlankingelements);
                ClearList.RemoveFromSlabs(ref AllSlabs, ListofPossibleFlankingelements);
                templist.Clear();

                //Zwischenergebnisse in Console anzeigen? 
                MyOwnDebug.IntermediateResults(myOwnDebugConsole, "Flankierende Elemente nach Space-Boundary-Check:", ListofPossibleFlankingelements, SaveConsoleToTxtFile);

                #endregion

                #region pre-filter Space boundaries
                //if more then 0 space boundaries exists, a filter can be done to reduce the number of possible flanking elements
                //this is only possible, if the space definitions are correct in the model

                if (prefilterSpaceBoundaries == true)
                {
                    templist = FilteredSpaces.FilterSpaceBoundary(spaceBoundaries, selectedElement);
                    //Zwischenergebnisse in Console anzeigen? 
                    MyOwnDebug.IntermediateResults(myOwnDebugConsole, "Flankierende Elemente nach Space-Boundary-Filter:", templist, SaveConsoleToTxtFile);
                    Console.WriteLine("Only take those elements for further inspection? yes (y) or no (n) ?");
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Only take those elements for further inspection? yes (y) or no (n) ?";
                    string lineYesNo = Console.ReadLine();
                    if (lineYesNo == "y")
                    {
                        SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "y";
                        //Add to List
                        ListofFlankingelementsSpaceBoundaries.AddRange(templist);
                        ListofPossibleFlankingelements.AddRange(templist);
                        // remove duplicates
                        ListofPossibleFlankingelements = ListofPossibleFlankingelements.Distinct().ToList();
                        ClearList.RemoveFromWalls(ref AllWalls, ListofPossibleFlankingelements);
                        ClearList.RemoveFromSlabs(ref AllSlabs, ListofPossibleFlankingelements);
                        ClearList.RemoveFromCurtainWalls(ref AllCurtainWalls, ListofPossibleFlankingelements);
                        templist.Clear();
                    }

                        
                }


                #endregion


                #region Create Bounding Boxes
                // --------------------------------------------------------------------------------------------------------------------------------- 

                // --------------------------------               Define BoundingBoxes                ----------------------------------------

                // --------------------------------------------------------------------------------------------------------------------------------- 

                //Shape of selectedElement
                ShapeBoundingBox FindShapeBox = new ShapeBoundingBox(); 
                FindShapeBox.GetShape(selectedElement, selectedMyElement, context);

                //Get Area and direction (normalvector)
                Plane planeofelementSE = new Plane();
                planeofelementSE.GetPlane(selectedMyElement);
                selectedMyElement.AreaVector = planeofelementSE;
                
                #region Element Thickness
                
                //Thickness of selected element
                selectedMyElement.Thickness = CheckLayers.GetThicknessFromLayers(selectedElement);
                if (selectedMyElement.Thickness == 0)
                {
                    selectedMyElement.Thickness = ShapeBoundingBox.GetThicknessfromShape(selectedMyElement);
                }

                //Abfrage: Core anschauen oder ignorieren?
                Console.WriteLine("");
                Console.WriteLine("#########        ???????????????       #########");
                Console.WriteLine("Should we consider only the core layers? yes(y) or no(n)");
                SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Should we consider only the core layers? yes(y) or no(n)";
                string line = Console.ReadLine();
                if (line == "y")
                {
                    UseOnlyCorelayer = true;
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "y";
                }
                else SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "n";
                CoreLayerDefinition SearchCoreLayer = new CoreLayerDefinition();
                try
                {
                    elementtype = selectedElement.IsTypedBy.First().RelatingType;
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} ERROR: No IfcType for the selected Element. Please do Model Healing first.", e);
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "{0} ERROR: No IfcType for the selected Element. Please do Model Healing first.";
                    Console.WriteLine("Junction analysis will be closed now.");
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Junction analysis will be closed now.";
                    Console.ReadKey();
                    return;

                }
                elementtype = selectedElement.IsTypedBy.First().RelatingType; //TODO: Element has no Type?
                //selectedelement is first element -> elementtype not analyzed
                ListofAllTypes.Add(elementtype);
                elementtypeLayers.typeNr = elementtype.GlobalId;
                if (UseOnlyCorelayer == true)
                {
                    elementtypeLayers.GetCoreFromType(elementtype); //CoreLayer is searched fr the ElementType of selectedElement
                }
                else  //No Core Layer, use Min and Max from boundingBox
                {
                    selectedMyElement.CoreMax = selectedMyElement.Max;
                    selectedMyElement.CoreMin = selectedMyElement.Min;
                }


                if (selectedElement.Material is Xbim.Ifc4.Interfaces.IIfcMaterialLayerSetUsage)
                {
                    Xbim.Ifc4.Interfaces.IIfcMaterialLayerSetUsage AllLayerSetsUsage = (Xbim.Ifc4.Interfaces.IIfcMaterialLayerSetUsage)selectedElement.Material;
                    selectedMyElement.SetCoreMinMax(elementtypeLayers.dinterior, elementtypeLayers.dexterior, AllLayerSetsUsage.DirectionSense);
                }
                else
                {
                    //No MaterialLayerSetUsage -> DirectionSense = POSITIVE
                    selectedMyElement.SetCoreMinMax(elementtypeLayers.dinterior, elementtypeLayers.dexterior, IfcDirectionSenseEnum.POSITIVE);
                }

                ListofAllTypesLayers.Add(elementtypeLayers); //selected element is the first object to be checked for types and therefore the first on ein the list


                #endregion

                //need definition of direction of selected element to know the relevant area
                Xbim.Ifc4.Interfaces.IIfcSpace spaceSendingRoom = null;
                Xbim.Ifc4.Interfaces.IIfcSpace spaceReceivingRoom = null;
                if (prefilterSpaceBoundaries == true)
                {
                    spaceSendingRoom = FilteredSpaces.space1;
                    spaceReceivingRoom = FilteredSpaces.space2;
                    GetSendingRoom FindSmallerRoom = new GetSendingRoom();
                    double spaceAarea = FindSmallerRoom.GetAreaOnSelectedElement(context, FilteredSpaces.space1, selectedMyElement.AreaVector.NormalVector);
                    double spaceBarea = FindSmallerRoom.GetAreaOnSelectedElement(context, FilteredSpaces.space2, selectedMyElement.AreaVector.NormalVector);
                    if (spaceAarea > spaceBarea)
                    {
                        spaceSendingRoom = FilteredSpaces.space2;
                        spaceReceivingRoom = FilteredSpaces.space1;
                    }
                }

                #endregion

                #region Define Flanking Elements by Distance

                bool parallelElementsfound = false;
                int parallelCheckWiederholung = 0;
                do 
                {
                    
                    //Shape of Possible flanking elements
                    foreach (Xbim.Ifc4.Interfaces.IIfcElement element in ListofPossibleFlankingelements)
                    {

                        MyElement FE = new MyElement();
                        FE.Nr = element.EntityLabel;
                        FE.GUID = element.GlobalId;

                        //Check for IfcCovering: Search the main element IfcWall or IfcSlab or IfcCurtainwall and take this shape!
                        if (element.GetType().Name == ElementType.IfcCovering.ToString())
                        {
                            List<Xbim.Ifc4.Interfaces.IIfcRelAggregates> relAggregates2 = element.Decomposes.ToList();
                            foreach (Xbim.Ifc4.Interfaces.IIfcRelAggregates relation in relAggregates2)
                            {
                                Xbim.Ifc4.Interfaces.IIfcObjectDefinition aggregate = relation.RelatingObject;
                                FE.GetElementType(aggregate.GetType().Name);
                                if (FE.Type == TypeOfElement.Wall || FE.Type == TypeOfElement.Slab) break;

                            }
                        }
                        else FE.GetElementType(element.GetType().Name);




                        //Shape of selectedElement
                        FindShapeBox.GetShape(element, FE, context);

                        //GEt Direction of Element
                        Plane planeofelement = new Plane();
                        planeofelement.GetPlane(FE);
                        FE.AreaVector = planeofelement;

                        //Thickness of flanking elements
                        FE.Thickness = CheckLayers.GetThicknessFromLayers(element);
                        if (FE.Thickness == 0)
                        {
                            FE.Thickness = ShapeBoundingBox.GetThicknessfromShape(FE);
                        }


                        //Calculate the distance to decided if it is a flanking element
                        // TODO: Abstaende anpassen je nachdem wo das Element genau liegt, z.B. bei X-Stoss mit dem Abstand des Bauteils dazwischen
                        MyDistance abstand = new MyDistance();


                        abstand.CalculateDistance(selectedMyElement, FE, false);


                        if (abstand.Distance <= 0.5 && abstand.Distance >= -0.5)
                        {
                            //real flanking element
                            FE.DistanceDirection = abstand;
                            ListofFlankingelements.Add(model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(d => d.EntityLabel == FE.Nr));
                            ListofFlankingMyelements.Add(FE);


                            if (UseOnlyCorelayer == true)
                            {
                                //Core Layer only if required
                                //ModelHealing before: make sure there is an elementtype and material related to it!
                                elementtype = element.IsTypedBy.FirstOrDefault().RelatingType;
                                //Define CoreLayers for each Elementtype, use this thickness again for every element from the same type
                                if (elementtype != null)
                                {
                                    CheckLayers typeLayers = ListofAllTypesLayers.FirstOrDefault(n => n.typeNr == elementtype.GlobalId);
                                    //search if this elementtype already exists -> Define Corethickness just one time for new elementtype and safe it
                                    //If ElementType already exists, use existing Layer-Thickness-information
                                    // If ElementType is not known, search for Material and MaterialLayerSet
                                    if (typeLayers != null)
                                    {
                                        //Calculate CoreLayer for specific element
                                        if (element.Material is Xbim.Ifc4.Interfaces.IIfcMaterialLayerSetUsage)
                                        {
                                            Xbim.Ifc4.Interfaces.IIfcMaterialLayerSetUsage AllLayerSetsUsage = (Xbim.Ifc4.Interfaces.IIfcMaterialLayerSetUsage)element.Material;
                                            FE.SetCoreMinMax(typeLayers.dinterior, typeLayers.dexterior, AllLayerSetsUsage.DirectionSense);

                                        }
                                        else
                                        {
                                            //No MaterialLayerSetUsage -> DirectionSense = POSITIVE
                                            FE.SetCoreMinMax(typeLayers.dinterior, typeLayers.dexterior, IfcDirectionSenseEnum.POSITIVE);
                                        }

                                    }
                                    else
                                    {
                                        //new elementtype: add to list and do analysis
                                        ListofAllTypes.Add(elementtype);
                                        typeLayers = new CheckLayers();
                                        typeLayers.typeNr = elementtype.GlobalId;
                                        typeLayers.GetCoreFromType(elementtype); //CoreLayer is searched for the ElementType
                                        ListofAllTypesLayers.Add(typeLayers);

                                        if (element.Material is Xbim.Ifc4.Interfaces.IIfcMaterialLayerSetUsage && element.Material != null)
                                        {
                                            Xbim.Ifc4.Interfaces.IIfcMaterialLayerSetUsage AllLayerSetsUsage = (Xbim.Ifc4.Interfaces.IIfcMaterialLayerSetUsage)element.Material;
                                            FE.SetCoreMinMax(typeLayers.dinterior, typeLayers.dexterior, AllLayerSetsUsage.DirectionSense);
                                        }
                                        else
                                        {
                                            //No MaterialLayerSetUsage -> DirectionSense = POSITIVE
                                            FE.SetCoreMinMax(typeLayers.dinterior, typeLayers.dexterior, IfcDirectionSenseEnum.POSITIVE);
                                        }


                                    }
                                }
                                else
                                {
                                    Console.WriteLine("ERROR: No Elementtype, Material was not found.");
                                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "ERROR: No Elementtype, Material was not found.";
                                }

                            }
                            else
                            {
                                //No Core Layer, use Min and Max from boundingBox
                                FE.CoreMax = FE.Max;
                                FE.CoreMin = FE.Min;
                            }

                        }


                    }  // ************ Ende von: foreach element in ListofPossibleFlankingelements ************ 

                    parallelCheckWiederholung = parallelCheckWiederholung + 1;
                    if (parallelCheckWiederholung > 1) break;
                    // Search for elements parallel to selected Element -> If some are found -> new bounding boxes and new flanking element search is needed!
                    if (parallelElementsfound == false) //Do the loop only once if needed!
                    {
                        CheckParallel ParallelTest = new CheckParallel();
                        foreach (var element2 in ListofFlankingMyelements)
                        {
                            if (ParallelTest.ParallelToElement(selectedMyElement, element2) == true) //element is paralel if true
                            {
                                //merge elements and create new bounding boxes
                                ParallelTest.MergeElements(selectedMyElement, element2, UseOnlyCorelayer);
                                parallelElementsfound = true;
                               
                            }
                        }
                        if (parallelElementsfound == true)
                        {
                            ListofFlankingelements.Clear();
                            ListofFlankingMyelements.Clear();
                        }
                    }
                    
                    
                } while (parallelElementsfound == true) ;
                
                
                

                #endregion

            } // ************ Ende von: Open IFC-Model ************ 



            #region Zwischenergebnisse anzeigen
            Console.WriteLine();
            Console.WriteLine("########################################### ");
            Console.WriteLine("Echte Flanken: ");

            SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "########################################### ";
            SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Echte Flanken: ";

            foreach (Xbim.Ifc4.Interfaces.IIfcElement element in ListofFlankingelements)
            {
                Console.WriteLine(element);
                SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + element;
            }

            foreach (MyElement e in ListofFlankingMyelements)
            {

                Console.WriteLine($"#{e.Nr}: {e.Type}, Distance to SE: {e.DistanceDirection.Distance} in {e.DistanceDirection.Direction}-direction");
                SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + $"#{e.Nr}: {e.Type}, Distance to SE: {e.DistanceDirection.Distance} in {e.DistanceDirection.Direction}-direction";
            }

            Console.ReadKey();



            #endregion

            #region JunctionElements definieren



            #region JunctionBoxen erstellen
            JunctionBox SEjunctionBoxes = new JunctionBox();
            SEjunctionBoxes.GetJBoxes(selectedMyElement.Min, selectedMyElement.Max, selectedMyElement.AreaVector.NormalVector);

            //Zwischenergebnisse in Console anzeigen?
            MyOwnDebug.IntermediateResults(myOwnDebugConsole, SEjunctionBoxes, SaveConsoleToTxtFile);
            
            #endregion

            Box[] ListOfJunctionBoxes = new Box[6];
            ListOfJunctionBoxes[0] = SEjunctionBoxes.JBox1;
            ListOfJunctionBoxes[1] = SEjunctionBoxes.JBox2;
            ListOfJunctionBoxes[2] = SEjunctionBoxes.JBox3;
            ListOfJunctionBoxes[3] = SEjunctionBoxes.JBox4;
            ListOfJunctionBoxes[4] = SEjunctionBoxes.JBox5;
            ListOfJunctionBoxes[5] = SEjunctionBoxes.JBox6;

            //selectedElement in jeder JunctionBox anlegen
            foreach (Box item in ListOfJunctionBoxes)
            {
                item.selectedElement = selectedMyElement;
            }


            #region Elemente den Junctionboxes zuweisen

           

            
            if (selectedMyElement.AreaVector.NormalVector == new Vector3D(1, 0, 0) || selectedMyElement.AreaVector.NormalVector == new Vector3D(-1, 0, 0))
            { //SE = Wall, normalVector = X
                selectedMyElement.Type = TypeOfElement.Wall;
                foreach (MyElement e in ListofFlankingMyelements)
                {
                    if (e.AreaVector.NormalVector == new Vector3D(1, 0, 0) || e.AreaVector.NormalVector == new Vector3D(-1, 0, 0)) //wall, parallel, normalvector X
                    {
                        e.Type = TypeOfElement.Wall;
                        //block1-parallel
                        if (e.DistanceDirection.Direction == DDirections.Xplus || e.DistanceDirection.Direction == DDirections.Xminus)
                        { // Element Parallel 
                            //TODO: Paralleles Element zum selected Element -> bounding Box anpassen und neue Flanking Elements suchen!
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Yplus)
                        {
                            //JB3
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox3, e, 0);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Yminus)
                        {
                            //JB1
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox1, e, 0);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Zplus)
                        {
                            //JB6
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox6, e, 0);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Zminus)
                        {
                            //JB4
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox4, e, 0);
                        }
                    }
                    else if(e.AreaVector.NormalVector == new Vector3D(0, 1, 0) || e.AreaVector.NormalVector == new Vector3D(0, -1, 0)) //wall, 90°, normalvector Y
                    {
                        e.Type = TypeOfElement.Wall;
                        //block2-gelb
                        if (e.DistanceDirection.Direction == DDirections.Zplus || e.DistanceDirection.Direction == DDirections.Zminus)
                        { // fehler
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Xplus || e.DistanceDirection.Direction == DDirections.Xminus)
                        {
                            
                            //JB1,2,3 depending on Y-position
                            if (e.Min.Y >= SEjunctionBoxes.JBox3.Min.Y)
                            
                            {   //JB3
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox3, e, 90);
                            }
                            else if (e.Max.Y <= SEjunctionBoxes.JBox1.Max.Y)
                            { //JB1
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox1, e, 90);
                            }
                            else
                            { // JB2
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox2, e, 90);
                            }
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Yplus)
                        {
                            //JB3
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox3, e, 90);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Yminus)
                        {
                            //JB1
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox1, e, 90);
                        }
                    }
                    else if (e.AreaVector.NormalVector == new Vector3D(0, 0, 1) || e.AreaVector.NormalVector == new Vector3D(0, 0, -1)) //slab, normalvector Z
                    {       // FE = slab
                        e.Type = TypeOfElement.Slab;
                        //block3-orange
                        if (e.DistanceDirection.Direction == DDirections.Yplus || e.DistanceDirection.Direction == DDirections.Yminus)
                        { // fehler
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Xplus || e.DistanceDirection.Direction == DDirections.Xminus)
                        {
                           //JB4,5,6 depending on Z-position
                            if (e.Min.Z >= SEjunctionBoxes.JBox6.Min.Z)
                            {   //JB6
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox6, e, 90);
                            }
                            else if (e.Max.Z <= SEjunctionBoxes.JBox4.Max.Z)
                            { //JB4
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox4, e, 90);
                            }
                            else
                            { // JB5
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox5, e, 90);
                            }
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Zplus)
                        {
                            //JB6
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox6, e, 90);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Zminus)
                        {
                            //JB4
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox4, e, 90);
                        }
                    }

                }
            }
            else if(selectedMyElement.AreaVector.NormalVector == new Vector3D(0, 1, 0) || selectedMyElement.AreaVector.NormalVector == new Vector3D(0, -1, 0))
            { //SE = Wall, normalVector = Y
                selectedMyElement.Type = TypeOfElement.Wall;
                foreach (MyElement e in ListofFlankingMyelements)
                {  
                    if (e.AreaVector.NormalVector == new Vector3D(1, 0, 0) || e.AreaVector.NormalVector == new Vector3D(-1, 0, 0)) //wall, parallel, normalvector X
                    {
                        e.Type = TypeOfElement.Wall;
                        //block1-gelb
                        if (e.DistanceDirection.Direction == DDirections.Zplus || e.DistanceDirection.Direction == DDirections.Zminus)
                        { // fehler
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Yplus || e.DistanceDirection.Direction == DDirections.Yminus)
                        {
                            //JB1,2,3 depending on X-position
                            if (e.Min.X >= SEjunctionBoxes.JBox3.Min.X)
                            {   //JB3
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox3, e, 90);
                            }
                            else if (e.Max.X <= SEjunctionBoxes.JBox1.Max.X)
                            { //JB1
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox1, e, 90);
                            }
                            else
                            { // JB2
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox2, e, 90);
                            }
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Xplus)
                        {
                            //JB3
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox3, e, 90);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Xminus)
                        {
                            //JB1
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox1, e, 90);
                        }

                    }
                    else if (e.AreaVector.NormalVector == new Vector3D(0, 1, 0) || e.AreaVector.NormalVector == new Vector3D(0, -1, 0)) //wall, 90°, normalvector Y
                    {
                        e.Type = TypeOfElement.Wall;
                        //block2-parallel
                        if (e.DistanceDirection.Direction == DDirections.Yplus || e.DistanceDirection.Direction == DDirections.Yminus)
                        { // fehler
                            //Parallele Elemente zum selected ELement einfuegen!! Gilt fuer alle JB
                            foreach (Box item in ListOfJunctionBoxes)
                            {
                                SEjunctionBoxes.FillInBox(item, e, 1);
                            }
                            
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Xplus)
                        {
                            //JB3
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox3, e, 0);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Xminus)
                        {
                            //JB1
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox1, e, 0);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Zplus)
                        {
                            //JB6
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox6, e, 0);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Zminus)
                        {
                            //JB4
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox4, e, 0);
                        }

                    }
                    else if (e.AreaVector.NormalVector == new Vector3D(0, 0, 1) || e.AreaVector.NormalVector == new Vector3D(0, 0, -1)) //slab, normalvector Z
                    {       // FE = slab
                        e.Type = TypeOfElement.Slab;
                        //block3-orange
                        if (e.DistanceDirection.Direction == DDirections.Xplus || e.DistanceDirection.Direction == DDirections.Xminus)
                        { // fehler
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Yplus || e.DistanceDirection.Direction == DDirections.Yminus)
                        {
                            //JB4,5,6 depending on Z-position
                            if (e.Min.Z >= SEjunctionBoxes.JBox6.Min.Z)
                            {   //JB6
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox6, e, 90);
                            }
                            else if (e.Max.Z <= SEjunctionBoxes.JBox4.Max.Z)
                            { //JB4
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox4, e, 90);
                            }
                            else
                            { // JB5
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox5, e, 90);
                            }
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Zplus)
                        {
                            //JB6
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox6, e, 90);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Zminus)
                        {
                            //JB4
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox4, e, 90);
                        }
                    }

                }
            }
            else
            { //SE = slab, normalVector = Z
                selectedMyElement.Type = TypeOfElement.Slab;
                foreach (MyElement e in ListofFlankingMyelements)
                {
                    if (e.AreaVector.NormalVector == new Vector3D(1, 0, 0) || e.AreaVector.NormalVector == new Vector3D(-1, 0, 0)) //wall, normalvector X
                    { //FE = wall
                        e.Type = TypeOfElement.Wall;
                        if (e.DistanceDirection.Direction == DDirections.Yplus || e.DistanceDirection.Direction == DDirections.Yminus)
                        { // fehler
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Zplus || e.DistanceDirection.Direction == DDirections.Zminus)
                        {
                            //JB4,5,6 depending on X-position
                            if (e.Min.X >= SEjunctionBoxes.JBox6.Min.X)
                            {   //JB6
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox6, e, 90);
                            }
                            else if (e.Max.X <= SEjunctionBoxes.JBox4.Max.X)
                            { //JB4
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox4, e, 90);
                            }
                            else
                            { // JB5
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox5, e, 90);
                            }
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Xplus)
                        {
                            //JB6
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox6, e, 90);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Xminus)
                        {
                            //JB4
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox4, e, 90);
                        }
                    }
                    else if (e.AreaVector.NormalVector == new Vector3D(0, 1, 0) || e.AreaVector.NormalVector == new Vector3D(0, -1, 0)) //wall, 90°, normalvector Y
                    { //FE = wall
                        e.Type = TypeOfElement.Wall;
                        //block1-gelb
                        if (e.DistanceDirection.Direction == DDirections.Xplus || e.DistanceDirection.Direction == DDirections.Xminus)
                        { // fehler
                        }
                        
                        else if (e.DistanceDirection.Direction == DDirections.Zplus || e.DistanceDirection.Direction == DDirections.Zminus)
                        {
                            //JB1,2,3 depending on X-position
                            if (e.Min.Y >= SEjunctionBoxes.JBox3.Min.Y)
                            {   //JB3
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox3, e, 90);
                            }
                            else if (e.Max.Y <= SEjunctionBoxes.JBox1.Max.Y)
                            { //JB1
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox1, e, 90);
                            }
                            else
                            { // JB2
                                SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox2, e, 90);
                            }
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Yplus)
                        {
                            //JB3
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox3, e, 90);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Yminus)
                        {
                            //JB1
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox1, e, 90);
                        }

                    }
                    else if (e.AreaVector.NormalVector == new Vector3D(0, 0, 1) || e.AreaVector.NormalVector == new Vector3D(0, 0, -1)) //slab, normalvector Z
                    {       // FE = slab
                        e.Type = TypeOfElement.Slab;
                        //block2-parallel
                        if (e.DistanceDirection.Direction == DDirections.Zplus || e.DistanceDirection.Direction == DDirections.Zminus)
                        { // fehler
                        }
                        
                        else if (e.DistanceDirection.Direction == DDirections.Yplus)
                        {
                            //JB3
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox3, e, 0);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Yminus)
                        {
                            //JB1
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox1, e, 0);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Xplus)
                        {
                            //JB6
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox6, e, 0);
                        }
                        else if (e.DistanceDirection.Direction == DDirections.Xminus)
                        {
                            //JB4
                            SEjunctionBoxes.FillInBox(SEjunctionBoxes.JBox4, e, 0);
                        }
                    }

                }
            }

            //Zwischenergebnisse in Console anzeigen?
            MyOwnDebug.JBconsoleOutput(myOwnDebugConsole, SEjunctionBoxes, SaveConsoleToTxtFile);

            #region ConnectionMatrix auslesen


            #endregion

            //Get Element Direction -> VOR anpassen in JB2 und JB5, sonst nicht mehr möglich. Wird nur an REALEN Bauteilen erstellt.
            foreach (var boxelement in ListOfJunctionBoxes)
            {
                boxelement.getElementDirection();
            }
            
            //SEElement in JB2 und JB5 müssen durchgehend sein -> Selected Element ist per Definition an der Stelle "middle"
            ListOfJunctionBoxes[1].SetSEandFE2();
            ListOfJunctionBoxes[4].SetSEandFE2();


            //Ausgabe ueberruefen
            Console.WriteLine("Vor der zusammenfuehrung der Elemente");
            Console.WriteLine("JB1: Max " + ListOfJunctionBoxes[0].selectedElement.Max.X + "/" + ListOfJunctionBoxes[0].selectedElement.Max.Y + "/" + ListOfJunctionBoxes[0].selectedElement.Max.Z
                                                + "Min" + ListOfJunctionBoxes[0].selectedElement.Min.X + "/" + ListOfJunctionBoxes[0].selectedElement.Min.Y + "/" + ListOfJunctionBoxes[0].selectedElement.Min.Z);

            SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Vor der zusammenfuehrung der Elemente";
            SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "JB1: Max " + ListOfJunctionBoxes[0].selectedElement.Max.X + "/" + ListOfJunctionBoxes[0].selectedElement.Max.Y + "/" + ListOfJunctionBoxes[0].selectedElement.Max.Z
                                                + "Min" + ListOfJunctionBoxes[0].selectedElement.Min.X + "/" + ListOfJunctionBoxes[0].selectedElement.Min.Y + "/" + ListOfJunctionBoxes[0].selectedElement.Min.Z;


            foreach (Box boxelement in ListOfJunctionBoxes)
            {
                foreach (MyElement element in boxelement.getElements())
                {
                    if (element != null)
                    {
                        //Search for other elements in this slot in the junction -> List
                        Int32 index = boxelement.getElementIndex(element);
                        List<MyElement> FElist = boxelement.getElementList(index);
                        if (FElist.Count != 0)
                        {
                            CheckParallel t = new CheckParallel();
                            t.ElementAnalysis(element, FElist, UseOnlyCorelayer);
                        }
                    }
                }
            }

            Console.WriteLine("NACH der zusammenfuehrung der Elemente");
            Console.WriteLine("JB1: Max " + ListOfJunctionBoxes[0].selectedElement.Max.X + "/" + ListOfJunctionBoxes[0].selectedElement.Max.Y + "/" + ListOfJunctionBoxes[0].selectedElement.Max.Z
                                                + "     - Min" + ListOfJunctionBoxes[0].selectedElement.Min.X + "/" + ListOfJunctionBoxes[0].selectedElement.Min.Y + "/" + ListOfJunctionBoxes[0].selectedElement.Min.Z);
           
            SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Nach der zusammenfuehrung der Elemente";
            SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "JB1: Max " + ListOfJunctionBoxes[0].selectedElement.Max.X + "/" + ListOfJunctionBoxes[0].selectedElement.Max.Y + "/" + ListOfJunctionBoxes[0].selectedElement.Max.Z
                                                + "Min" + ListOfJunctionBoxes[0].selectedElement.Min.X + "/" + ListOfJunctionBoxes[0].selectedElement.Min.Y + "/" + ListOfJunctionBoxes[0].selectedElement.Min.Z;


            // fuer jede JBox mit Elementen
            foreach (Box boxelement in ListOfJunctionBoxes)
            {
                MyOwnDebug.IntermediateResults(myOwnDebugConsole, "Zwischenschritte", SaveConsoleToTxtFile);

                MyDistance x = new MyDistance();
                //Abstand von SE zu FE1, zu FE2, zu  FE3 bereits eingetragen in [0,1], [0,2], [0,3]
                //Abstand ausrechnen von FE1 zu SE, zu FE2, zu FE3-> MyDistance  -> in connection Matrix [1,0], [1,2], [1,3]
                //Abstand ausrechnen von FE2 zu SE, zu FE1, zu FE3-> MyDistance  -> in connection Matrix [2,0], [2,1], [2,3]
                //Abstand ausrechnen von FE3 zu SE, zu FE1, zu FE2-> MyDistance  -> in connection Matrix [3,0], [3,1], [3,2]
                 
                if (boxelement.Max != null && boxelement.Min != null) //Box vorhanden?
                {
                    //boxelement.selectedElement = selectedMyElement;  //schon frueher??

                    foreach (MyElement element in boxelement.getElements())
                    {
                        // no calculation possible if no element, matrix stays with null
                        if (element != null)
                        {
                            foreach (MyElement secondElement in boxelement.getElements())
                            {
                                if (secondElement != null)
                                {
                                    if(element != secondElement) //[0,0], [1,1], [2,2], [3,3] are not relevant
                                    {
                                        //calculate Distance and direction and write it in connection matrix
                                        //CoreMin und CoreMax muesssen schon vorhanden sein
                                        MyDistance xNew = boxelement.CalculateDistance(element, secondElement, true); 
  
                                        if (myOwnDebugConsole ==true)
                                        {
                                            SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n";
                                            Console.WriteLine("Box:" + boxelement.Nr);
                                            SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Box:" + boxelement.Nr;
                                            Console.WriteLine("[" + boxelement.getElementIndex(element) + ","
                                            + boxelement.getElementIndex(secondElement) + "] " + xNew.Direction + ". " + xNew.Distance);
                                            SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "[" + boxelement.getElementIndex(element) + ","
                                            + boxelement.getElementIndex(secondElement) + "] " + xNew.Direction + ". " + xNew.Distance;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            //Zwischenergebnisse in Console anzeigen?
            MyOwnDebug.IntermediateResultsConnectionMatrix(myOwnDebugConsole, ListOfJunctionBoxes, SaveConsoleToTxtFile);

            int boxnumber = 0;

            //Auswertung zu short / middle / border
            foreach (Box boxelement in ListOfJunctionBoxes)
            {
                boxnumber = boxnumber + 1;
                if (boxelement.Max != null && boxelement.Min != null) //check if boundingBox exist
                { 
                    ////boxelement.getElementDirection(); //Put n, m, o inside ElementDirection  ->> AENDERUNG: muss statt finden BEVOR parallele Bauteile in der Conenction Zone Matrix ergänzt wurden!!

                    foreach (MyElement element in boxelement.getElements())
                    {
                        foreach (MyElement secondElement in boxelement.getElements())
                        {
                            if (secondElement != null && element != null)
                            {
                                if (element != secondElement) //[0,0], [1,1], [2,2], [3,3] are not relevant
                                {
                                    //Fuer [0,2], [1,3], [2,0], [3,1] //Elemente sollten sich gegenueber liegen
                                    if ((element == boxelement.selectedElement & secondElement == boxelement.FE2)
                                        || (element == boxelement.FE1 & secondElement == boxelement.FE3)
                                        || (element == boxelement.FE2 & secondElement == boxelement.selectedElement)
                                        || (element == boxelement.FE3 & secondElement == boxelement.FE1))
                                    {

                                        double? angle = new double?();
                                        angle = CheckAngle.GetAngle(element.AreaVector.NormalVector, secondElement.AreaVector.NormalVector);
                                        if (angle == 0)
                                        {
                                            boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)] = ConnectionZones.Short;
                                        }
                                        
                                    }
                                    else
                                    {
                                        double? angle = new double?();
                                        angle = CheckAngle.GetAngle(element.AreaVector.NormalVector, boxelement.ConnectionMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)].Direction);
                                        if (angle == 90) // Element 2 in Abstandsrichtung wie Element 2 Direction (== element1 Richtung ist 90° zu Abstandsrichtung): Element 1 ist short 
                                        {
                                            boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)] = ConnectionZones.Short;
                                        }
                                        else if (angle == 0) //Element 2 in Abstandsrichtung wie Element 1 Direction:  Element 1 kann border oder middle sein -> extra test
                                        {
                                            //Abfrage Min / Max von Element 1 in Richtung SecondElement-NormalVector
                                            if (secondElement.AreaVector.NormalVector == new Vector3D(1, 0, 0) || secondElement.AreaVector.NormalVector == new Vector3D(-1, 0, 0))
                                            {   //x-Richtung
                                                if (secondElement.Max.X >= element.Min.X + 0.5 && secondElement.Min.X <= element.Max.X - 0.5)
                                                {
                                                    boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)] = ConnectionZones.Middle;

                                                }
                                                else
                                                { boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)] = ConnectionZones.Border; }
                                            }
                                            else if (secondElement.AreaVector.NormalVector == new Vector3D(0, 1, 0) || secondElement.AreaVector.NormalVector == new Vector3D(0, -1, 0))
                                            {   //y-Richtung
                                                if (secondElement.Max.Y >= element.Min.Y + 0.5 && secondElement.Min.Y <= element.Max.Y - 0.5)
                                                {
                                                    boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)] = ConnectionZones.Middle;

                                                }
                                                else
                                                { boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)] = ConnectionZones.Border; }
                                            }
                                            if (secondElement.AreaVector.NormalVector == new Vector3D(0, 0, 1) || secondElement.AreaVector.NormalVector == new Vector3D(0, 0, -1))
                                            {   //z-Richtung
                                                if (secondElement.Max.Z >= element.Min.Z + 0.5 && secondElement.Min.Z <= element.Max.Z - 0.5)
                                                {
                                                    boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)] = ConnectionZones.Middle;

                                                }
                                                else
                                                { boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)] = ConnectionZones.Border; }
                                            }
                                        }

                                    }
                                }

                            }

                        }

                    }

                    //check for elements with short-short touch if there is an element between. If yes convert to zero.
                    foreach (MyElement element in boxelement.getElements())
                    {
                        foreach (MyElement secondElement in boxelement.getElements())
                        {

                            if (boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)] == ConnectionZones.Short
                        && boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(secondElement), boxelement.getElementIndex(element)] == ConnectionZones.Short)
                            { //DIASTANCE NICHT BEREHCNET??!?!
                                if (boxelement.ConnectionMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)].Distance > 0)
                                {
                                    if (boxelement.getNextElement(element) != null)
                                    {
                                        if ((boxelement.ConnectionZoneMatrix[boxelement.getNextElementIndex(element), boxelement.getElementIndex(element)] == ConnectionZones.Middle
                                        || boxelement.ConnectionZoneMatrix[boxelement.getNextElementIndex(element), boxelement.getElementIndex(element)] == ConnectionZones.Border)
                                        || (boxelement.ConnectionZoneMatrix[boxelement.getNextElementIndex(element), boxelement.getElementIndex(secondElement)] == ConnectionZones.Middle
                                        || boxelement.ConnectionZoneMatrix[boxelement.getNextElementIndex(element), boxelement.getElementIndex(secondElement)] == ConnectionZones.Border))
                                        {
                                            // Distance between element and secondelement should be at least the thickness of the element in between

                                            if (boxelement.getNextElement(element).Thickness <= boxelement.ConnectionMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)].Distance)
                                            {
                                                //change short-short to null-null
                                                boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)] = ConnectionZones.Zero;
                                                boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(secondElement), boxelement.getElementIndex(element)] = ConnectionZones.Zero;
                                            }
                                            else
                                            {
                                                //Problem?? //TODO: what todo if distance is too small? Elements are then overlapping
                                                //change short-short to null-null ??
                                                boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)] = ConnectionZones.Zero;
                                                boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(secondElement), boxelement.getElementIndex(element)] = ConnectionZones.Zero;
                                            }

                                        }
                                    }
                                    else if (boxelement.getNextElement(secondElement) != null)
                                    {
                                        if ((boxelement.ConnectionZoneMatrix[boxelement.getNextElementIndex(secondElement), boxelement.getElementIndex(secondElement)] == ConnectionZones.Middle
                                        || boxelement.ConnectionZoneMatrix[boxelement.getNextElementIndex(secondElement), boxelement.getElementIndex(secondElement)] == ConnectionZones.Border)
                                        && (boxelement.ConnectionZoneMatrix[boxelement.getNextElementIndex(secondElement), boxelement.getElementIndex(element)] == ConnectionZones.Middle
                                        || boxelement.ConnectionZoneMatrix[boxelement.getNextElementIndex(secondElement), boxelement.getElementIndex(element)] == ConnectionZones.Border))
                                        {
                                            // Distance between element and secondelement should be at least the thickness of the element in between
                                            if (boxelement.getNextElement(secondElement).Thickness <= boxelement.ConnectionMatrix[boxelement.getElementIndex(secondElement), boxelement.getElementIndex(element)].Distance)
                                            {
                                                //change short-short to null-null
                                                boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(element), boxelement.getElementIndex(secondElement)] = ConnectionZones.Zero;
                                                boxelement.ConnectionZoneMatrix[boxelement.getElementIndex(secondElement), boxelement.getElementIndex(element)] = ConnectionZones.Zero;
                                            }
                                            else
                                            {
                                                //Problem?? //TODO: what todo if distance is too small? Elements are then overlapping
                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }

                    //check for elements with middle : the oppposite element must be the same element! ADD FE Element to List!
                    //Possible only at position [0,3], [1,0], [3,2] and [2,3], [2,1], [3,0]
                    if (boxelement.ConnectionZoneMatrix[3, 0] == ConnectionZones.Middle)
                    {
                        if (boxelement.ConnectionZoneMatrix[1, 0] == ConnectionZones.None)
                        {
                            boxelement.ConnectionZoneMatrix[1, 0] = ConnectionZones.Middle;
                            boxelement.ConnectionZoneMatrix[0, 1] = boxelement.ConnectionZoneMatrix[0, 3];
                            if (boxelement.FE1 == null)
                            {
                                boxelement.FE1 = boxelement.FE3;
                                //boxelement.ElementDirection[1] = boxelement.ElementDirection[3];
                            }
                        }
                    }
                    if (boxelement.ConnectionZoneMatrix[0, 1] == ConnectionZones.Middle)
                    {
                        if (boxelement.ConnectionZoneMatrix[2, 1] == ConnectionZones.None)
                        {
                            boxelement.ConnectionZoneMatrix[2, 1] = ConnectionZones.Middle;
                            boxelement.ConnectionZoneMatrix[1,2] = boxelement.ConnectionZoneMatrix[1,0];
                            if (boxelement.FE2 == null)
                            {
                                boxelement.FE2 = boxelement.selectedElement;
                                //boxelement.ElementDirection[2] = boxelement.ElementDirection[0];
                            }
                        }
                    }
                    if (boxelement.ConnectionZoneMatrix[0,3] == ConnectionZones.Middle)
                    {
                        if (boxelement.ConnectionZoneMatrix[2,3] == ConnectionZones.None)
                        {
                            boxelement.ConnectionZoneMatrix[2,3] = ConnectionZones.Middle;
                            boxelement.ConnectionZoneMatrix[3,2] = boxelement.ConnectionZoneMatrix[3,0];
                            if (boxelement.FE2 == null)
                            {
                                boxelement.FE2 = boxelement.selectedElement;
                                //boxelement.ElementDirection[2] = boxelement.ElementDirection[0];
                            }

                        }
                    }
                    if (boxelement.ConnectionZoneMatrix[3,2] == ConnectionZones.Middle)
                    {
                        if (boxelement.ConnectionZoneMatrix[1, 2] == ConnectionZones.None)
                        {
                            boxelement.ConnectionZoneMatrix[1, 2] = ConnectionZones.Middle;
                            boxelement.ConnectionZoneMatrix[2,1] = boxelement.ConnectionZoneMatrix[2, 3];
                            if (boxelement.FE1 == null)
                            {
                                boxelement.FE1 = boxelement.FE3;
                                //boxelement.ElementDirection[1] = boxelement.ElementDirection[3];
                            }

                        }
                    }
                    if (boxelement.ConnectionZoneMatrix[1, 0] == ConnectionZones.Middle)
                    {
                        if (boxelement.ConnectionZoneMatrix[3, 0] == ConnectionZones.None)
                        {
                            boxelement.ConnectionZoneMatrix[3, 0] = ConnectionZones.Middle;
                            boxelement.ConnectionZoneMatrix[0, 3] = boxelement.ConnectionZoneMatrix[0, 1];
                            if (boxelement.FE3 == null)
                            {
                                boxelement.FE3 = boxelement.FE1;
                                //boxelement.ElementDirection[3] = boxelement.ElementDirection[1];
                            }

                        }
                    }
                    if (boxelement.ConnectionZoneMatrix[1,2] == ConnectionZones.Middle)
                    {
                        if (boxelement.ConnectionZoneMatrix[3,2] == ConnectionZones.None)
                        {
                            boxelement.ConnectionZoneMatrix[3,2] = ConnectionZones.Middle;
                            boxelement.ConnectionZoneMatrix[2,3] = boxelement.ConnectionZoneMatrix[2,1];
                            if (boxelement.FE3 == null)
                            {
                                boxelement.FE3 = boxelement.FE1;
                               //boxelement.ElementDirection[3] = boxelement.ElementDirection[1];
                            }

                        }
                    }
                }
            }


            //AUSGABE IN CONSOLE: ConnectionZoneMatrix Zeilenweise auslesen
            foreach (Box boxelement in ListOfJunctionBoxes)
            {
                boxelement.Junction = JunctionType.JunctionTypeError;
                if (boxelement.Max != null && boxelement.Min != null)
                {
                    
                    CheckJunctionType Junciontype2 = new CheckJunctionType();
                    boxelement.Junction = Junciontype2.GetJunctionType(boxelement.ConnectionZoneMatrix, boxelement.ElementDirection);

                    Console.WriteLine(" ");
                    Console.WriteLine("Connection Zone Matrix:");
                    Console.WriteLine(boxelement.ConnectionZoneMatrix[0,0] + "," + boxelement.ConnectionZoneMatrix[0, 1] + ","+ boxelement.ConnectionZoneMatrix[0, 2] + "," + boxelement.ConnectionZoneMatrix[0, 3]);
                    Console.WriteLine(boxelement.ConnectionZoneMatrix[1, 0] + "," + boxelement.ConnectionZoneMatrix[1, 1] + "," + boxelement.ConnectionZoneMatrix[1, 2] + "," + boxelement.ConnectionZoneMatrix[1, 3]);
                    Console.WriteLine(boxelement.ConnectionZoneMatrix[2, 0] + "," + boxelement.ConnectionZoneMatrix[2, 1] + "," + boxelement.ConnectionZoneMatrix[2, 2] + "," + boxelement.ConnectionZoneMatrix[2, 3]);
                    Console.WriteLine(boxelement.ConnectionZoneMatrix[3, 0] + "," + boxelement.ConnectionZoneMatrix[3, 1] + "," + boxelement.ConnectionZoneMatrix[3, 2] + "," + boxelement.ConnectionZoneMatrix[3, 3]);
                    Console.WriteLine("DirectionElements:");
                    Console.WriteLine(boxelement.ElementDirection[0] + "," + boxelement.ElementDirection[1] + "," + boxelement.ElementDirection[2] + "," + boxelement.ElementDirection[3]);
                    Console.WriteLine("Junction Type:");
                    Console.WriteLine(boxelement.Junction);

                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Connection Zone Matrix: ";
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + boxelement.ConnectionZoneMatrix[0, 0] + "," + boxelement.ConnectionZoneMatrix[0, 1] + "," + boxelement.ConnectionZoneMatrix[0, 2] + "," + boxelement.ConnectionZoneMatrix[0, 3];
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + boxelement.ConnectionZoneMatrix[1, 0] + "," + boxelement.ConnectionZoneMatrix[1, 1] + "," + boxelement.ConnectionZoneMatrix[1, 2] + "," + boxelement.ConnectionZoneMatrix[1, 3];
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + boxelement.ConnectionZoneMatrix[2, 0] + "," + boxelement.ConnectionZoneMatrix[2, 1] + "," + boxelement.ConnectionZoneMatrix[2, 2] + "," + boxelement.ConnectionZoneMatrix[2, 3];
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + boxelement.ConnectionZoneMatrix[3, 0] + "," + boxelement.ConnectionZoneMatrix[3, 1] + "," + boxelement.ConnectionZoneMatrix[3, 2] + "," + boxelement.ConnectionZoneMatrix[3, 3];
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "DirectionElements:";
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + boxelement.ElementDirection[0] + "," + boxelement.ElementDirection[1] + "," + boxelement.ElementDirection[2] + "," + boxelement.ElementDirection[3];
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Junction Type:";
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + boxelement.Junction;

                }
            }

            
            List<Junction> allJunctions = new List<Junction>();

            using (IfcStore model = IfcStore.Open(filename))
            {
                //TODO: Export in Junction-elements in JSON-File
                
                foreach (Box boxelement in ListOfJunctionBoxes)
                {
                    Junction junctionToSave = new Junction();
                    junctionToSave.SeparatingElementID = selectedMyElement.GUID.ToString();

                    if ((boxelement.Junction != JunctionType.JunctionTypeError) && (boxelement.Junction != JunctionType.ErrorNoCorrectDirection)) //nur speichern, wenn auch ein sinnvoller JunctionType vorhanden ist
                    {
                        
                        junctionToSave.TypeOfJunction = boxelement.Junction;
                        boxelement.SetCommonLength(ListOfJunctionBoxes);
                        junctionToSave.CommonLength = boxelement.CommonLength;
                        junctionToSave.TypeOfJunction = boxelement.Junction;

                        //Save flanking elements and transform elements to BuildingElementJSON
                        RelAggregatesJunction flankingElementsaggregation = new RelAggregatesJunction();
                        flankingElementsaggregation.RelatingObject = junctionToSave;
                        BuildingElementJSON newelement= new BuildingElementJSON();
                        BuildingElementJSON newelementFE1 = new BuildingElementJSON();
                        BuildingElementJSON newelementFE2 = new BuildingElementJSON();
                        BuildingElementJSON newelementFE3 = new BuildingElementJSON();
                        newelement.TransformToBuildingElementJSON(model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(d => d.GlobalId == boxelement.selectedElement.GUID));
                        flankingElementsaggregation.RelatedObjects.Add(newelement);
                        if (boxelement.FE1 != null)
                        {
                            newelementFE1.TransformToBuildingElementJSON(model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(d => d.GlobalId == boxelement.FE1.GUID));
                            flankingElementsaggregation.RelatedObjects.Add(newelementFE1);
                        }
                        if (boxelement.FE2 != null)
                        {
                            newelementFE2.TransformToBuildingElementJSON(model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(d => d.GlobalId == boxelement.FE2.GUID));
                            flankingElementsaggregation.RelatedObjects.Add(newelementFE2);
                        }
                        if (boxelement.FE3 != null)
                        {
                            newelementFE3.TransformToBuildingElementJSON(model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(d => d.GlobalId == boxelement.FE3.GUID));
                            flankingElementsaggregation.RelatedObjects.Add(newelementFE3);
                        }
                        junctionToSave.IsDecomposedBy = flankingElementsaggregation;


                        //Create Paths
                        if ((boxelement.FE1 != null && boxelement.FE2 == null && boxelement.FE3 == null) 
                            || (boxelement.FE1 != null && boxelement.FE2 != null && boxelement.FE3 == null))
                        { 
                            TransmissionPath path1 = new TransmissionPath();
                            path1.Name = TransmissionPath.PathsName.Fd;
                            path1.Is_i = model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(p => p.GlobalId ==(IfcGloballyUniqueId)boxelement.FE1.GUID);
                            path1.Is_j = model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(p => p.GlobalId == (IfcGloballyUniqueId)boxelement.selectedElement.GUID);
                            
                            RelAssociatesPaths VerbindungJunctionToPaths = new RelAssociatesPaths();
                            VerbindungJunctionToPaths.RelatingJunction = junctionToSave;
                            VerbindungJunctionToPaths.RelatedPath.Add(path1);
                            List<RelAssociatesPaths> PathsList = new List<RelAssociatesPaths>();
                            PathsList.Add(VerbindungJunctionToPaths);
                            junctionToSave.Transmits = PathsList;
                        }
                        else if ((boxelement.FE1 != null && boxelement.FE2  == null && boxelement.FE3 != null)
                            || (boxelement.FE1 != null && boxelement.FE2 != null && boxelement.FE3 != null))
                        {
                            TransmissionPath path1 = new TransmissionPath();
                            path1.Name = TransmissionPath.PathsName.Df;
                            path1.Is_i = model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(p => p.GlobalId == (IfcGloballyUniqueId)boxelement.selectedElement.GUID);
                            path1.Is_j = model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(p => p.GlobalId == (IfcGloballyUniqueId)boxelement.FE1.GUID);
                            TransmissionPath path2 = new TransmissionPath();
                            path2.Name = TransmissionPath.PathsName.Fd;
                            path2.Is_i = model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(p => p.GlobalId == (IfcGloballyUniqueId)boxelement.FE1.GUID);
                            path2.Is_j = model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(p => p.GlobalId == (IfcGloballyUniqueId)boxelement.selectedElement.GUID);
                            TransmissionPath path3 = new TransmissionPath();
                            path3.Name = TransmissionPath.PathsName.Ff;
                            path3.Is_i = model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(p => p.GlobalId == (IfcGloballyUniqueId)boxelement.FE1.GUID);
                            path3.Is_j = model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(p => p.GlobalId == (IfcGloballyUniqueId)boxelement.FE3.GUID);

                            RelAssociatesPaths VerbindungJunctionToPaths = new RelAssociatesPaths();
                            VerbindungJunctionToPaths.RelatingJunction = junctionToSave;
                            VerbindungJunctionToPaths.RelatedPath.Add(path1);
                            VerbindungJunctionToPaths.RelatedPath.Add(path2);
                            VerbindungJunctionToPaths.RelatedPath.Add(path3);
                            List<RelAssociatesPaths> PathsList = new List<RelAssociatesPaths>();
                            PathsList.Add(VerbindungJunctionToPaths);
                            junctionToSave.Transmits = PathsList;

                        }

                        allJunctions.Add(junctionToSave);
                    }
                }

                
                Console.Write("Do you want to safe the junctions in a JSON-file?");
                Console.WriteLine("Continue with this element? Yes (y) or No (n)");
                if (Console.ReadLine() == "y")
                {
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Write JSON-file";

                    ConvertToJSONstring stringforjson = new ConvertToJSONstring();
                    JsonElementwithAllJunctions junctionstoconvert = stringforjson.GetJsonString(allJunctions);
                    JsonSerializer serializer = new JsonSerializer();
                    string pathname = Path.GetDirectoryName(filename);
                    string filename2 = Path.GetFileNameWithoutExtension(filename) + "_JunctionInpuData_" + elementID.ToString() + ".json";
                    string jsonpathfileName = pathname + "\\" + filename2; ;
                    using (StreamWriter sw = new StreamWriter(jsonpathfileName))
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, junctionstoconvert);
                    }
                    Console.WriteLine("JSON with junction was saved in: " + jsonpathfileName);
                    SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "JSON with junction was saved in: " + jsonpathfileName;

                   Console.Write("Add JSON-file to the selected element? Yes(y) or No(n)");
                    if (Console.ReadLine() == "y")
                    {
                        SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "Add JSON-file to the selected element? Yes(y)";
                        //Add json file to seleted element with IfcRelAssociatesDocument
                        using (var txn = model.BeginTransaction("Add IfcRelAssociatesDocument"))
                        {

                            //Check IFC Version before associating document
                            if (model.SchemaVersion == Xbim.Common.Step21.XbimSchemaVersion.Ifc4)
                            {
                                Xbim.Ifc4.ExternalReferenceResource.IfcDocumentInformation documentinfo = model.Instances.New<Xbim.Ifc4.ExternalReferenceResource.IfcDocumentInformation>(r =>
                                {
                                    r.Location = jsonpathfileName;
                                    r.Name = "_JunctionInpuData";
                                    r.Description = "JunctionInpuData for VBAcoustic";
                                });

                                Xbim.Ifc4.Kernel.IfcRelAssociatesDocument relAssociates = model.Instances.New<Xbim.Ifc4.Kernel.IfcRelAssociatesDocument>(rela =>
                                {
                                    rela.RelatedObjects.Add(model.Instances.FirstOrDefault<Xbim.Ifc4.Interfaces.IIfcElement>(d => d.GlobalId == elementID));
                                    rela.RelatingDocument = documentinfo;
                                });
                            }
                            else if (model.SchemaVersion == Xbim.Common.Step21.XbimSchemaVersion.Ifc2X3)
                            {
                                Xbim.Ifc2x3.ExternalReferenceResource.IfcDocumentReference documentinfo = model.Instances.New<Xbim.Ifc2x3.ExternalReferenceResource.IfcDocumentReference>(r =>
                                {
                                    r.Location = jsonpathfileName;
                                    r.Name = "_JunctionInpuData";

                                });

                                Xbim.Ifc2x3.Kernel.IfcRelAssociatesDocument relAssociates = model.Instances.New<Xbim.Ifc2x3.Kernel.IfcRelAssociatesDocument>(rela =>
                                {
                                    rela.RelatedObjects.Add((Xbim.Ifc2x3.Kernel.IfcRoot)model.Instances.FirstOrDefault<Xbim.Ifc2x3.Interfaces.IIfcElement>(d => d.GlobalId == elementID));
                                    rela.RelatingDocument = documentinfo;
                                });
                            }
                            else {
                                Console.WriteLine("IFC Version not known. Impossible to add JSON file.");
                                SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "IFC Version not known. Impossible to add JSON file.";
                            }
                            

                            txn.Commit();
                        }

                        //save IFC file

                        string filename3 = Path.GetFileNameWithoutExtension(filename) + "_mitAnhangJunctionInpuData.ifc";
                        string PathAndFile = pathname + "\\" + filename3;
                        model.SaveAs(PathAndFile);

                        SaveConsoleToTxtFile = SaveConsoleToTxtFile + "\n" + "JSON-file: " + PathAndFile;
                    }

                }

                
            }

            string SaveConsoleToTxtFilePATH = Path.GetDirectoryName(filename) + "\\" + Path.GetFileNameWithoutExtension(filename) + "_protocol_" + elementID.ToString() + ".txt";
            
            File.WriteAllText(SaveConsoleToTxtFilePATH, SaveConsoleToTxtFile);

            Console.WriteLine("## Ausgabe beendet ## ");
            Console.ReadKey();

            #endregion




        } // **************** Ende von Static void Main() ************

    }
}
