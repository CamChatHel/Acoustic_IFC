using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.ProductExtension;

namespace Prototype01
{
    public class CheckSpaceBoundary
    {
        public  List<IIfcElement> GetSpaceBoundary(List<IIfcRelSpaceBoundary> RelSpaceBoundary, IIfcElement selectedElement)
            //Search for Flanking Elements for Space Boundaries
        {
            List<IIfcElement> NewElementsforList = new List<IIfcElement>();
            List<IIfcSpace> ListOfBoundarySpaces = new List<IIfcSpace>();

            foreach (var spaceboundaryelement in RelSpaceBoundary)
            {
                ListOfBoundarySpaces.Add((IIfcSpace)spaceboundaryelement.RelatingSpace);
            }
            foreach (var space in ListOfBoundarySpaces)
            {
                var spaceBoundaryElements = space.BoundedBy.ToList();

                foreach (IIfcRelSpaceBoundary element in spaceBoundaryElements)
                {
                    
                    var e = element.RelatedBuildingElement;
                    //Check if element is not null, is a wall or a slab
                    if (e != null)
                    {
                        if (e != selectedElement)
                        {
                            MyElement SE = new MyElement();
                            SE.GetElementType(e.GetType().Name);
                            if (SE.Type == TypeOfElement.Slab || SE.Type == TypeOfElement.Wall)
                            {
                                NewElementsforList.Add(e);
                            }
                            
                        }
                    }
                    

                }

            }
            // remove duplicates
            NewElementsforList = NewElementsforList.Distinct().ToList();

            return NewElementsforList;
        }

        public  IIfcSpace space1 { get; set; }
        public  IIfcSpace space2 { get; set; }


        public List<IIfcElement> FilterSpaceBoundary(List<IIfcRelSpaceBoundary> ListRelSpaceBoundary, IIfcElement selectedElement)
        {
            List<IIfcElement> templist = new List<IIfcElement>();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("INFO: Number of Space Boundaries for the selected element: " + ListRelSpaceBoundary.Count);
            Console.WriteLine("Do you want to filter the list of flanking elements for those spaces only before checking for distance? (y) yes or no?");
            string lineYesNo = Console.ReadLine();
            if (lineYesNo == "y")
            {
                if (ListRelSpaceBoundary.Count == 2) //for 2 space boundaries -> Only elements with a connection to this Space Bpundary
                {
                    
                    foreach (IIfcRelSpaceBoundary relSpace in ListRelSpaceBoundary)
                    {
                        IIfcSpaceBoundarySelect space = relSpace.RelatingSpace;
                        if (space is IIfcSpace)
                        {
                            IIfcSpace sp = (IIfcSpace)space;
                            foreach (IIfcRelSpaceBoundary relSB in sp.BoundedBy.ToList())
                            {
                                IIfcElement element = relSB.RelatedBuildingElement;
                                templist.Add(element); //All elements around the space bounded to the separating element
                            }
                        }

                    }


                }
                else
                {

                    //for more than 2 space boundaries: choose relevant spaces first
                    //for 2 space boundaries -> Only elements with a connection to this Space Bpundary
                    List<IIfcSpace> spacesAroundSelectedElement = new List<IIfcSpace>();

                    foreach (IIfcRelSpaceBoundary relSpace in ListRelSpaceBoundary)
                    {
                        IIfcSpaceBoundarySelect space = relSpace.RelatingSpace;
                        if (space is IIfcSpace)
                        {
                            spacesAroundSelectedElement.Add((IIfcSpace)space);
                        }

                    }
                    Console.WriteLine("");
                    Console.WriteLine("All Spaces avaiable:");
                    int countindex = 0;
                    // remove duplicates
                    spacesAroundSelectedElement = spacesAroundSelectedElement.Distinct().ToList();
                    foreach (IIfcSpace space in spacesAroundSelectedElement)
                    {
                        countindex = countindex + 1;
                        Console.WriteLine(countindex + " Space: #" + space.EntityLabel + ", GUID: " + space.GlobalId);
                    }
                    Console.WriteLine("Choose Space 1:");
                    int index = Convert.ToInt32(Console.ReadLine());
                    space1 = spacesAroundSelectedElement.ElementAt(index - 1);

                    Console.WriteLine("Choose Space 2:");
                    index = Convert.ToInt32(Console.ReadLine());
                    space2 = spacesAroundSelectedElement.ElementAt(index - 1);

                    List<IIfcRelSpaceBoundary> ListrelSB1 = space1.BoundedBy.ToList();
                    List<IIfcRelSpaceBoundary> ListrelSB2 = space2.BoundedBy.ToList();

                    //for those 2 space boundaries -> Only elements with a connection to them
                    foreach (var relSB in ListrelSB1)
                    {
                        IIfcElement element = relSB.RelatedBuildingElement;
                        templist.Add(element); //All elements around the space bounded to the separating element

                    }
                    foreach (var relSB in ListrelSB2)
                    {
                        IIfcElement element = relSB.RelatedBuildingElement;
                        templist.Add(element); //All elements around the space bounded to the separating element
                    }

                }

                // remove duplicates
                templist = templist.Distinct().ToList();
                //remove selected element
                templist.Remove(selectedElement);
                //Zwischenergebnisse in Console anzeigen? 
                // MyOwnDebug.IntermediateResults(myOwnDebugConsole, "Flankierende Elemente nach Space-Boundary-Filter:", templist);
            }

            return templist;
        }
        
    }
}
