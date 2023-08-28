using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Xbim.Ifc4.Interfaces;

namespace Prototype01
{


   
    public enum TouchingPlane
    {
        borderMax,
        borderMin,
        middle,
        shortMax,
        shortMin
    }

    public class Box //junctionBoxes
    {
        public int Nr;
        public Point3D Min;
        public Point3D Max;

        //maximum of 3 flanking elements per junction box
        //one element is always the separating box
        public MyElement FE1;
        public MyElement FE2;
        public MyElement FE3;
        public MyElement selectedElement;

        //for more elements
        public List<MyElement> FE1List = new List<MyElement>();
        public List<MyElement> FE2List = new List<MyElement>();
        public List<MyElement> FE3List = new List<MyElement>();
        public List<MyElement> selectedElementList = new List<MyElement>();

        //JunctionType in this Box
        public JunctionType Junction;



        public MyElement[] getElements()
        {
            return new MyElement[] {selectedElement, FE1, FE2, FE3 };
        }

        public List<MyElement>[] getElementLists()
        {
            return new List<MyElement>[] { selectedElementList, FE1List, FE2List, FE3List };
        }

        public int getElementIndex(MyElement elementToCheck)
        {
            //fixed position of elements depending on the index in the list
            return (elementToCheck == selectedElement) ? 0 : (elementToCheck == FE1) ? 1 : (elementToCheck == FE2) ? 2 : 3;
        }

        public List<MyElement> getElementList(int index)
        {
            
            return (index == 0) ? selectedElementList : (index == 1) ? FE1List : (index == 2) ? FE2List : FE3List;
        }

        public int getNextElementIndex(MyElement elementToCheck)
        {
            //find the next index in the list
            int index = getElementIndex(elementToCheck);
            index = index + 1;
            if (index == 4) index = 0;
            return index;
        }

        public MyElement getNextElement(MyElement elementToCheck)
        {
            int NextIndex = getNextElementIndex(elementToCheck);
            return (NextIndex == 0) ? selectedElement : (NextIndex == 1) ? FE1 : (NextIndex == 2) ? FE2 : FE3;
        }

        //ConnectionMatrix with element 0 for selected element, flanking elements in 1,2,3
        public MyDistance[,] ConnectionMatrix = new MyDistance[4,4];

        //ConnectionRealizingElement in Matrix with element 0 for selected element, flanking elements in 1,2,3
        public List<IIfcElement>[,] RealizingElementsMatrix = new List<IIfcElement>[4, 4];

        //ConnectionZones in Matrix with element 0 for selected element, flanking elements in 1,2,3
        public ConnectionZones[,] ConnectionZoneMatrix = new ConnectionZones[4,4];

        public MyDistance CalculateDistance(MyElement currentElement, MyElement element2)
        {
            MyDistance x = new MyDistance();
            x.CalculateDistance(currentElement, element2, false);
            this.ConnectionMatrix[getElementIndex(currentElement), getElementIndex(element2)] = x;

            return x;
        }

        public MyDistance CalculateDistance(MyElement currentElement, MyElement element2, bool coreYes)
        {
            MyDistance x = new MyDistance();
            x.CalculateDistance(currentElement, element2, coreYes);
            this.ConnectionMatrix[getElementIndex(currentElement), getElementIndex(element2)] = x;

            return x;
        }

        //ElementDirection compared to selected element
        public string[] ElementDirection = new string[4];

        public void getElementDirection()
        {  
            //Standard values
            this.ElementDirection[0] = "None";
            this.ElementDirection[1] = "None";
            this.ElementDirection[2] = "None";
            this.ElementDirection[3] = "None";
            //Set Values depending on element and his direction
            if (this.selectedElement.Type == TypeOfElement.Wall)
            {
                this.ElementDirection[0] = "n";

                if (this.FE1!= null && this.FE1.Type == TypeOfElement.Wall)
                {
                    this.ElementDirection[1] = "m";
                }
                if (this.FE1 != null && this.FE1.Type == TypeOfElement.Slab)
                {
                    this.ElementDirection[1] = "o";
                }
                if (this.FE2 != null)
                {
                    this.ElementDirection[2] = "n";
                }
                if (this.FE3 != null && this.FE3.Type == TypeOfElement.Wall)
                {
                    this.ElementDirection[3] = "m";
                }
                if (this.FE3 != null && this.FE3.Type == TypeOfElement.Slab)
                {
                    this.ElementDirection[3] = "o";
                }
                

            }
            else if (this.selectedElement.Type == TypeOfElement.Slab)
            {
                this.ElementDirection[0] = "o";

                if (this.FE1 != null && this.FE1.Type == TypeOfElement.Wall)
                {
                    this.ElementDirection[1] = "n";
                }
                if (this.FE2 != null)
                {
                    this.ElementDirection[2] = "o";
                }
                if (this.FE3 != null && this.FE3.Type == TypeOfElement.Wall)
                {
                    this.ElementDirection[3] = "n";
                }
                
            }
        }

        public void SetSEandFE2()
        {
            //For junctionbox 2 and 5 the selected element is the same in SE and in FE2 (due to definition of the boxes!)
            if (this.selectedElement != this.FE2)
            {
                if (this.FE2 == null)
                {
                    this.FE2 = this.selectedElement;
                    this.FE2List = this.selectedElementList;
                }
                else
                {
                    Console.WriteLine("ERROR: In JunctionBox " + this.Nr + " - SelectedElement and FE2 are not the same.");
                }
            }
        }


        public double CommonLength; 
        public void SetCommonLength(Box[] ListOfJunctionBoxes)
        {

            if (selectedElement.Type == TypeOfElement.Wall)
            {
                if (this.Nr == 1 || this.Nr == 2 || this.Nr == 3) //JB1,2,3 for walls
                {
                    CommonLength = Math.Round(selectedElement.Max.Z - selectedElement.Min.Z, 2);
                }
                else //JB4,5,6 for walls
                {
                    if (selectedElement.AreaVector.NormalVector.X == 1 || selectedElement.AreaVector.NormalVector.X == -1)
                    {
                        CommonLength = Math.Round(selectedElement.CoreMax.Y - selectedElement.CoreMin.Y);
                        //if (ListOfJunctionBoxes[0].Junction == JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[0].Junction == JunctionType.JunctionTypeError
                        //    || ListOfJunctionBoxes[1].Junction == JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[1].Junction == JunctionType.JunctionTypeError)
                        //{
                        //    //no calculation possible
                        //}
                        //else if (ListOfJunctionBoxes[0].FE1 != null && ListOfJunctionBoxes[1].FE1 != null) //[0] = JB1, [1] = JB2
                        //{
                        //    var length = Math.Round(Math.Abs(ListOfJunctionBoxes[0].FE1.CoreMax.Y - ListOfJunctionBoxes[1].FE1.CoreMax.Y), 2);
                        //    CommonLength = Math.Min(CommonLength, length);
                        //}
                        //else if (ListOfJunctionBoxes[1].FE1 != null && ListOfJunctionBoxes[2].FE1 != null) //[2] = JB3, [1] = JB2
                        //{
                        //    var length = Math.Round(Math.Abs(ListOfJunctionBoxes[1].FE1.CoreMin.Y - ListOfJunctionBoxes[2].FE1.CoreMax.Y), 2);
                        //    CommonLength = Math.Min(CommonLength, length);
                        //}
                        //if ((ListOfJunctionBoxes[0].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[0].Junction != JunctionType.JunctionTypeError)
                        //    & (ListOfJunctionBoxes[1].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[1].Junction != JunctionType.JunctionTypeError)) //[0] = JB1, [1] = JB2
                        //{
                        //    var length =Math.Round(Math.Abs(ListOfJunctionBoxes[0].FE1.CoreMax.Y - ListOfJunctionBoxes[1].FE1.CoreMax.Y) ,2);
                        //    CommonLength = Math.Min(CommonLength, length);
                        //}
                        //else if ((ListOfJunctionBoxes[2].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[2].Junction != JunctionType.JunctionTypeError)
                        //    || (ListOfJunctionBoxes[1].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[1].Junction != JunctionType.JunctionTypeError)) //[2] = JB3, [1] = JB2
                        //{
                        //    var length =Math.Round( Math.Abs(ListOfJunctionBoxes[1].FE1.CoreMin.Y - ListOfJunctionBoxes[1].FE1.CoreMax.Y),2);
                        //    CommonLength = Math.Min(CommonLength, length);
                        //}
                    }
                    if (selectedElement.AreaVector.NormalVector.Y == 1 || selectedElement.AreaVector.NormalVector.Y == -1)
                    {
                        CommonLength = Math.Round(Math.Abs(selectedElement.CoreMax.X - selectedElement.CoreMin.X),2);
                        //if ((ListOfJunctionBoxes[0].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[0].Junction != JunctionType.JunctionTypeError)
                        //   || (ListOfJunctionBoxes[1].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[1].Junction != JunctionType.JunctionTypeError)) //[0] = JB1, [1] = JB2
                        //{
                        //    var length = Math.Round(Math.Abs(ListOfJunctionBoxes[0].FE1.CoreMax.X - ListOfJunctionBoxes[1].FE1.CoreMax.X),2);
                        //    CommonLength = Math.Min(CommonLength, length);
                        //}
                        //else if ((ListOfJunctionBoxes[2].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[2].Junction != JunctionType.JunctionTypeError)
                        //    || (ListOfJunctionBoxes[1].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[1].Junction != JunctionType.JunctionTypeError)) //[2] = JB3, [1] = JB2
                        //{
                        //    var length = Math.Round(Math.Round(ListOfJunctionBoxes[2].FE1.CoreMin.X - ListOfJunctionBoxes[1].FE1.CoreMax.X),2);
                        //    CommonLength = Math.Min(CommonLength, length);
                        //}
                    }
                }

            }
            else // Type of element =slab
            {
                if (this.Nr == 1 || this.Nr == 2 || this.Nr == 3) //JB1,2,3 for slabs
                {
                    CommonLength = Math.Round(selectedElement.Max.X - selectedElement.Min.X, 2);
                    //if ((ListOfJunctionBoxes[3].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[3].Junction != JunctionType.JunctionTypeError)
                    //       || (ListOfJunctionBoxes[4].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[4].Junction != JunctionType.JunctionTypeError)) //[3] = JB4, [4] = JB5
                    //{
                    //    var length =Math.Round( Math.Abs(ListOfJunctionBoxes[3].FE1.CoreMax.X - ListOfJunctionBoxes[4].FE1.CoreMax.X),2);
                    //    CommonLength = Math.Min(CommonLength, length);
                    //}
                    //else if ((ListOfJunctionBoxes[5].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[5].Junction != JunctionType.JunctionTypeError)
                    //    || (ListOfJunctionBoxes[4].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[4].Junction != JunctionType.JunctionTypeError)) //[4] = JB5, [5] = JB6
                    //{
                    //    var length = Math.Round(Math.Abs(ListOfJunctionBoxes[5].FE1.CoreMin.X - ListOfJunctionBoxes[4].FE1.CoreMax.X));
                    //    CommonLength = Math.Min(CommonLength, length);
                    //}
                }
                else
                {
                    CommonLength = Math.Round(selectedElement.Max.Y - selectedElement.Min.Y, 2);
                    //if ((ListOfJunctionBoxes[0].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[0].Junction != JunctionType.JunctionTypeError)
                    //        || (ListOfJunctionBoxes[1].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[1].Junction != JunctionType.JunctionTypeError)) //[0] = JB1, [1] = JB2
                    //{
                    //    var length = Math.Round(Math.Abs(ListOfJunctionBoxes[0].FE1.CoreMax.Y - ListOfJunctionBoxes[1].FE1.CoreMax.Y),2);
                    //    CommonLength = Math.Min(CommonLength, length);
                    //}
                    //else if ((ListOfJunctionBoxes[2].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[2].Junction != JunctionType.JunctionTypeError)
                    //    || (ListOfJunctionBoxes[1].Junction != JunctionType.ErrorNoCorrectDirection || ListOfJunctionBoxes[1].Junction != JunctionType.JunctionTypeError)) //[2] = JB3, [1] = JB2
                    //{
                    //    var length = Math.Round(Math.Abs(ListOfJunctionBoxes[1].FE1.CoreMin.Y - ListOfJunctionBoxes[1].FE1.CoreMax.Y),2);
                    //    CommonLength = Math.Min(CommonLength, length);
                    //}
                }

            }

        }

    }



    public class JunctionBox      //Ienumerable einfügen? Dann koennte foreach Schleife gehen
    {
        public Box JBox1 { get; set; }

        public Box JBox2 { get; set; }

        public Box JBox3 { get; set; }

        public Box JBox4 { get; set; }

        public Box JBox5 { get; set; }

        public Box JBox6 { get; set; }

        
        //Calculate the Junction Boxes for the selected element
        public void GetJBoxes(Point3D MinPoint, Point3D MaxPoint, Vector3D NormalVectordirection)
        {
            //Box[] ListOfJunctionBoxes = new Box[7];

            JBox1 = new Box();
            JBox1.Nr = 1;
            JBox2 = new Box();
            JBox2.Nr = 2;
            JBox3 = new Box();
            JBox3.Nr = 3;
            JBox4 = new Box();
            JBox4.Nr = 4;
            JBox5 = new Box();
            JBox5.Nr = 5;
            JBox6 = new Box();
            JBox6.Nr = 6;

            if (NormalVectordirection == new Vector3D(1, 0, 0) || NormalVectordirection == new Vector3D(-1, 0, 0)) //wall, normalVector = X
            {
                JBox1.Min.X = MinPoint.X - 0.3;
                JBox1.Min.Y = MinPoint.Y - 0.5;
                JBox1.Min.Z = MinPoint.Z;
                JBox1.Max.X = MaxPoint.X + 0.3;
                JBox1.Max.Y = MinPoint.Y + 0.5;
                JBox1.Max.Z = MaxPoint.Z;

                JBox2.Min.X = MinPoint.X - 0.3;
                JBox2.Min.Y = MinPoint.Y + 0.5;
                JBox2.Min.Z = MinPoint.Z;
                JBox2.Max.X = MaxPoint.X + 0.3;
                JBox2.Max.Y = MaxPoint.Y - 0.5;
                JBox2.Max.Z = MaxPoint.Z;

                JBox3.Min.X = MinPoint.X - 0.3;
                JBox3.Min.Y = MaxPoint.Y - 0.5;
                JBox3.Min.Z = MinPoint.Z;
                JBox3.Max.X = MaxPoint.X + 0.3;
                JBox3.Max.Y = MaxPoint.Y - 0.5;
                JBox3.Max.Z = MaxPoint.Z;

                JBox4.Min.X = MinPoint.X - 0.3;
                JBox4.Min.Y = MinPoint.Y;
                JBox4.Min.Z = MinPoint.Z - 0.5;
                JBox4.Max.X = MaxPoint.X + 0.3;
                JBox4.Max.Y = MaxPoint.Y;
                JBox4.Max.Z = MaxPoint.Z + 0.5;

                JBox5.Min.X = MinPoint.X - 0.3;
                JBox5.Min.Y = MinPoint.Y;
                JBox5.Min.Z = MinPoint.Z + 0.5;
                JBox5.Max.X = MaxPoint.X + 0.3;
                JBox5.Max.Y = MaxPoint.Y;
                JBox5.Max.Z = MaxPoint.Z - 0.5;

                JBox6.Min.X = MinPoint.X - 0.3;
                JBox6.Min.Y = MinPoint.Y;
                JBox6.Min.Z = MaxPoint.Z - 0.5;
                JBox6.Max.X = MaxPoint.X + 0.3;
                JBox6.Max.Y = MaxPoint.Y;
                JBox6.Max.Z = MaxPoint.Z + 0.5;

            }
            else if (NormalVectordirection == new Vector3D(0, 1, 0) || NormalVectordirection == new Vector3D(0, -1, 0)) //wall, normalVector = Y
            {
                JBox1.Min.X = MinPoint.X - 0.5;
                JBox1.Min.Y = MinPoint.Y - 0.3;
                JBox1.Min.Z = MinPoint.Z;
                JBox1.Max.X = MinPoint.X + 0.5;
                JBox1.Max.Y = MaxPoint.Y + 0.3;
                JBox1.Max.Z = MaxPoint.Z;

                JBox2.Min.X = MaxPoint.X + 0.5;
                JBox2.Min.Y = MinPoint.Y - 0.3;
                JBox2.Min.Z = MinPoint.Z;
                JBox2.Max.X = MaxPoint.X - 0.5;
                JBox2.Max.Y = MaxPoint.Y + 0.3;
                JBox2.Max.Z = MaxPoint.Z;

                JBox3.Min.X = MaxPoint.X - 0.5;
                JBox3.Min.Y = MinPoint.Y - 0.3;
                JBox3.Min.Z = MinPoint.Z;
                JBox3.Max.X = MaxPoint.X + 0.5;
                JBox3.Max.Y = MaxPoint.Y + 0.3;
                JBox3.Max.Z = MaxPoint.Z;

                JBox4.Min.X = MinPoint.X;
                JBox4.Min.Y = MinPoint.Y - 0.3;
                JBox4.Min.Z = MaxPoint.Z - 0.5;
                JBox4.Max.X = MaxPoint.X;
                JBox4.Max.Y = MaxPoint.Y + 0.3;
                JBox4.Max.Z = MaxPoint.Z + 0.5;

                JBox5.Min.X = MaxPoint.X;
                JBox5.Min.Y = MaxPoint.Y + 0.3;
                JBox5.Min.Z = MaxPoint.Z + 0.5;
                JBox5.Max.X = MinPoint.X;
                JBox5.Max.Y = MinPoint.Y - 0.3;
                JBox5.Max.Z = MaxPoint.Z - 0.5;

                JBox6.Min.X = MinPoint.X;
                JBox6.Min.Y = MinPoint.Y - 0.3;
                JBox6.Min.Z = MaxPoint.Z - 0.5;
                JBox6.Max.X = MaxPoint.X;
                JBox6.Max.Y = MaxPoint.Y + 0.3;
                JBox6.Max.Z = MaxPoint.Z + 0.5;
            }
            else if (NormalVectordirection == new Vector3D(0, 0, 1) || NormalVectordirection == new Vector3D(0, 0, -1)) //slab, normalVector = Z
            {
                JBox1.Min.X = MinPoint.X;
                JBox1.Min.Y = MinPoint.Y - 0.5;
                JBox1.Min.Z = MinPoint.Z - 0.3;
                JBox1.Max.X = MaxPoint.X;
                JBox1.Max.Y = MinPoint.Y + 0.5;
                JBox1.Max.Z = MaxPoint.Z + 0.3;

                JBox2.Min.X = MinPoint.X;
                JBox2.Min.Y = MinPoint.Y + 0.5;
                JBox2.Min.Z = MinPoint.Z - 0.3;
                JBox2.Max.X = MaxPoint.X;
                JBox2.Max.Y = MaxPoint.Y - 0.5;
                JBox2.Max.Z = MaxPoint.Z + 0.3;

                JBox3.Min.X = MinPoint.X;
                JBox3.Min.Y = MaxPoint.Y - 0.5;
                JBox3.Min.Z = MinPoint.Z - 0.3;
                JBox3.Max.X = MaxPoint.X;
                JBox3.Max.Y = MinPoint.Y + 0.5;
                JBox3.Max.Z = MaxPoint.Z + 0.3;

                JBox4.Min.X = MinPoint.X - 0.5;
                JBox4.Min.Y = MinPoint.Y;
                JBox4.Min.Z = MinPoint.Z - 0.3;
                JBox4.Max.X = MinPoint.X + 0.5;
                JBox4.Max.Y = MaxPoint.Y;
                JBox4.Max.Z = MaxPoint.Z + 0.3;

                JBox5.Min.X = MinPoint.X + 0.5;
                JBox5.Min.Y = MinPoint.Y;
                JBox5.Min.Z = MinPoint.Z - 0.3;
                JBox5.Max.X = MaxPoint.X - 0.5;
                JBox5.Max.Y = MaxPoint.Y;
                JBox5.Max.Z = MaxPoint.Z + 0.3;

                JBox6.Min.X = MaxPoint.X - 0.5;
                JBox6.Min.Y = MinPoint.Y;
                JBox6.Min.Z = MinPoint.Z - 0.3;
                JBox6.Max.X = MaxPoint.X + 0.5;
                JBox6.Max.Y = MaxPoint.Y;
                JBox6.Max.Z = MaxPoint.Z + 0.3;
            }
            else
            {
                // Problem... Element nicht im 90-Grad-Winkel/Raster
                // Die Element-Achse muss bestimmt werden, daraus wird die Richtung für +/- Werte bestimmt
                // Frage: werden die Junction Boxes dann AxisAligned erstellt oder nicht?
            }


        }


        public void FillInBox(Box BoxToBeFilled, MyElement ElementToFillIn, int Angle)
        {
            
            //TODO: handling double walls for selectedElement and flanking elements -> Add to List first -> angle == 1
            if (Angle == 0)
            {
               
                if (BoxToBeFilled.FE2 == null)
                {
                    BoxToBeFilled.FE2 = ElementToFillIn;
                    BoxToBeFilled.ConnectionMatrix[0, 2] = ElementToFillIn.DistanceDirection;
                }
                else
                {
                    if(CheckSameSlot(BoxToBeFilled.selectedElement, ElementToFillIn, BoxToBeFilled.selectedElement.Nr)==true)
                    {
                        BoxToBeFilled.selectedElementList.Add(ElementToFillIn);
                        Console.WriteLine("# JunctionBox ueberfuellt: Position FE2 besetzt in JBox" + BoxToBeFilled.Nr + ". Element in selectedElementList hinzugefügt " + ElementToFillIn);
                    }
                    else
                    {
                        BoxToBeFilled.FE2List.Add(ElementToFillIn);
                        Console.WriteLine("# JunctionBox ueberfuellt: Position FE2 besetzt in JBox" + BoxToBeFilled.Nr + ". Element in FE2List hinzugefügt " + ElementToFillIn);
                    }
                   
                    
                }
            }

            else if (Angle == 90)
            {
                if (BoxToBeFilled.selectedElement.AreaVector.NormalVector.X == 1 || BoxToBeFilled.selectedElement.AreaVector.NormalVector.X == -1)
                {
                    if (ElementToFillIn.CoreMin.X < BoxToBeFilled.selectedElement.CoreMin.X)
                    {
                        if (BoxToBeFilled.FE1 == null)
                        {
                            BoxToBeFilled.FE1 = ElementToFillIn;
                            BoxToBeFilled.ConnectionMatrix[0, 1] = ElementToFillIn.DistanceDirection;
                        }
                        else
                        {
                            BoxToBeFilled.FE1List.Add(ElementToFillIn);
                        }
                    }
                    else
                    {
                        if (BoxToBeFilled.FE3 == null)
                        {
                            BoxToBeFilled.FE3 = ElementToFillIn;
                            BoxToBeFilled.ConnectionMatrix[0, 3] = ElementToFillIn.DistanceDirection;
                        }
                        else
                        {
                            BoxToBeFilled.FE3List.Add(ElementToFillIn);
                        }
                    }

                }
                else if (BoxToBeFilled.selectedElement.AreaVector.NormalVector.Y == 1 || BoxToBeFilled.selectedElement.AreaVector.NormalVector.Y == -1)
                {
                    if (ElementToFillIn.CoreMin.Y < BoxToBeFilled.selectedElement.CoreMin.Y)
                    {
                        if (BoxToBeFilled.FE1 == null)
                        {
                            BoxToBeFilled.FE1 = ElementToFillIn;
                            BoxToBeFilled.ConnectionMatrix[0, 1] = ElementToFillIn.DistanceDirection;
                        }
                        else
                        {
                            BoxToBeFilled.FE1List.Add(ElementToFillIn);
                        }
                    }
                    else
                    {
                        if (BoxToBeFilled.FE3 == null)
                        {
                            BoxToBeFilled.FE3 = ElementToFillIn;
                            BoxToBeFilled.ConnectionMatrix[0, 3] = ElementToFillIn.DistanceDirection;
                        }
                        else
                        {
                            BoxToBeFilled.FE3List.Add(ElementToFillIn);
                        }
                    }

                }
                else if (BoxToBeFilled.selectedElement.AreaVector.NormalVector.Z == 1 || BoxToBeFilled.selectedElement.AreaVector.NormalVector.Z == -1)
                {
                    if (ElementToFillIn.CoreMin.Z < BoxToBeFilled.selectedElement.CoreMin.Z)
                    {
                        if (BoxToBeFilled.FE1 == null)
                        {
                            BoxToBeFilled.FE1 = ElementToFillIn;
                            BoxToBeFilled.ConnectionMatrix[0, 1] = ElementToFillIn.DistanceDirection;
                        }
                        else
                        {
                            BoxToBeFilled.FE1List.Add(ElementToFillIn);
                        }
                    }
                    else
                    {
                        if (BoxToBeFilled.FE3 == null)
                        {
                            BoxToBeFilled.FE3 = ElementToFillIn;
                            BoxToBeFilled.ConnectionMatrix[0, 3] = ElementToFillIn.DistanceDirection;
                        }
                        else
                        {
                            BoxToBeFilled.FE3List.Add(ElementToFillIn);
                        }
                    }

                }

            }
            else if (Angle == 1)
            {
                BoxToBeFilled.selectedElementList.Add(ElementToFillIn);
                Console.WriteLine("# JunctionBox ueberfuellt: Position SE besetzt in JBox" + BoxToBeFilled.Nr + ". Element in selectedElementList hinzugefügt " + ElementToFillIn);

            }

        }

        


        public Boolean CheckSameSlot(MyElement element1, MyElement element2, int JunctionBoxNr)
        {
            if (element1.AreaVector.NormalVector.X == 1 || element1.AreaVector.NormalVector.X == -1)
	        {
                if ((element2.CoreMax.Y <= element1.CoreMax.Y && element2.CoreMax.Y > element1.CoreMin.Y) ||
                    (element2.CoreMin.Y >= element1.CoreMin.Y && element2.CoreMin.Y < element1.CoreMax.Y))
                {
                    return true;
                }
                else
                { 
                    return false;
                }
	        }
            else if (element1.AreaVector.NormalVector.Y == 1 || element1.AreaVector.NormalVector.Y == -1)
            {
                if ((element2.CoreMax.X <= element1.CoreMax.X && element2.CoreMax.X > element1.CoreMin.X) ||
                   (element2.CoreMin.X >= element1.CoreMin.X && element2.CoreMin.X < element1.CoreMax.X))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (element1.AreaVector.NormalVector.Z == 1 || element1.AreaVector.NormalVector.Z == -1)
            {
                if (JunctionBoxNr == 1 || JunctionBoxNr == 3)
                {
                    if ((element2.CoreMax.Y <= element1.CoreMax.Y && element2.CoreMax.Y > element1.CoreMin.Y) ||
                    (element2.CoreMin.Y >= element1.CoreMin.Y && element2.CoreMin.Y < element1.CoreMax.Y))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (JunctionBoxNr == 4 || JunctionBoxNr == 6)
                {
                    if ((element2.CoreMax.X <= element1.CoreMax.X && element2.CoreMax.X > element1.CoreMin.X) ||
                   (element2.CoreMin.X >= element1.CoreMin.X && element2.CoreMin.X < element1.CoreMax.X))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        

    }
    

}
