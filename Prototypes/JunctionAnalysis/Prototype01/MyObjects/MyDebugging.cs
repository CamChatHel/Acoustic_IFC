using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Common.Geometry;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Prototype01
{
    public class MyDebugging
    {
        public void IntermediateResults(bool YesNo, string titel, string txt)
        {
            if (YesNo==true)
            {
                Console.WriteLine("-------------- " + titel + " --------------");
                txt = txt + "\n" + "-------------- " + titel + " --------------" ;
            }
        }

        public void IntermediateResults(bool YesNo, string titel)
        {
            if (YesNo == true)
            {
                Console.WriteLine("-------------- " + titel + " --------------");
                
            }
        }

        public void IntermediateResults(bool YesNo, string titel, List<IIfcElement> ListOfElements)
        {
            if (YesNo==true)
            {
                Console.WriteLine("------------------------------------------------------------------------------ ");
                Console.WriteLine(titel);
                foreach (var E in ListOfElements)
                {
                    Console.WriteLine(E);
                }
            }
            
        }

        public void IntermediateResults(bool YesNo, string titel, List<IIfcElement> ListOfElements, string txt)
        {
            if (YesNo == true)
            {
                Console.WriteLine("------------------------------------------------------------------------------ ");
                Console.WriteLine(titel);
                txt = txt + "\n" + titel;
                foreach (var E in ListOfElements)
                {
                    Console.WriteLine(E);
                    txt = txt + "\n" + E;
                }
            }

        }

        


        public void IntermediateResults(bool YesNo, JunctionBox junctionBoxes, string txt)
        {
            if (YesNo == true)
            {
                Console.WriteLine("----------- JunctionBoxes for separating Element ----------");
                Console.WriteLine($"Box1: Min( {junctionBoxes.JBox1.Min} ) , Max: ( {junctionBoxes.JBox1.Max} )");
                Console.WriteLine($"Box2: Min( {junctionBoxes.JBox2.Min} ) , Max: ( {junctionBoxes.JBox2.Max} )");
                Console.WriteLine($"Box3: Min( {junctionBoxes.JBox3.Min} ) , Max: ( {junctionBoxes.JBox3.Max} )");
                Console.WriteLine($"Box4: Min( {junctionBoxes.JBox4.Min} ) , Max: ( {junctionBoxes.JBox4.Max} )");
                Console.WriteLine($"Box5: Min( {junctionBoxes.JBox5.Min} ) , Max: ( {junctionBoxes.JBox5.Max} )");
                Console.WriteLine($"Box6: Min( {junctionBoxes.JBox6.Min} ) , Max: ( {junctionBoxes.JBox6.Max} )");

                txt = txt + "\n" + "----------- JunctionBoxes for separating Element ----------";
                txt = txt + "\n" +  $"Box1: Min( {junctionBoxes.JBox1.Min} ) , Max: ( {junctionBoxes.JBox1.Max} )";
                txt = txt + "\n" + $"Box2: Min( {junctionBoxes.JBox2.Min} ) , Max: ( {junctionBoxes.JBox2.Max} )";
                txt = txt + "\n" + $"Box3: Min( {junctionBoxes.JBox3.Min} ) , Max: ( {junctionBoxes.JBox3.Max} )";
                txt = txt + "\n" + $"Box4: Min( {junctionBoxes.JBox4.Min} ) , Max: ( {junctionBoxes.JBox4.Max} )";
                txt = txt + "\n" + $"Box5: Min( {junctionBoxes.JBox5.Min} ) , Max: ( {junctionBoxes.JBox5.Max} )";
                txt = txt + "\n" + $"Box6: Min( {junctionBoxes.JBox6.Min} ) , Max: ( {junctionBoxes.JBox6.Max} )";

            }

        }


        public void IntermediateResultsConnectionMatrix(bool YesNo, Box[] ListOfJB, string txt)
        {
            if (YesNo == true)
            {
                Console.WriteLine("----------- Connection Matrix for Junction Boxes -----------");
                txt = txt + "\n" + "----------- Connection Matrix for Junction Boxes -----------";
                foreach (Box boxelement in ListOfJB)
                {
                    Console.WriteLine("BoxNr: " + boxelement.Nr);
                    txt = txt + "\n" + "BoxNr: " + boxelement.Nr;
                    if (boxelement.FE1 != null && boxelement.FE2 != null && boxelement.FE3 != null)
                    { 
                        Console.WriteLine("FE1: " + boxelement.FE1.Nr + ", FE2: " + boxelement.FE2.Nr + ", FE3: " + boxelement.FE3.Nr);
                        txt = txt + "\n" + "FE1: " + boxelement.FE1.Nr + ", FE2: " + boxelement.FE2.Nr + ", FE3: " + boxelement.FE3.Nr;
                            }
                    else if (boxelement.FE1 != null && boxelement.FE2 != null)
                    { 
                        Console.WriteLine("FE1: " + boxelement.FE1.Nr + ", FE2: " + boxelement.FE2.Nr);
                        txt = txt + "\n" + "FE1: " + boxelement.FE1.Nr + ", FE2: " + boxelement.FE2.Nr;
                            }
                    else if (boxelement.FE1 != null && boxelement.FE3 != null)
                    { 
                        Console.WriteLine("FE1: " + boxelement.FE1.Nr + ", FE3: " + boxelement.FE3.Nr);
                        txt = txt + "\n" + "FE1: " + boxelement.FE1.Nr + ", FE3: " + boxelement.FE3.Nr;
                    }
                    else if (boxelement.FE1 != null)
                    { Console.WriteLine("FE1: " + boxelement.FE1.Nr);
                        txt = txt + "\n" + "FE1: " + boxelement.FE1.Nr;
                            }

                    

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (boxelement.ConnectionMatrix[i, j] != null)
                            {
                                Console.WriteLine("[" + i + "," + j + "] " + boxelement.ConnectionMatrix[i, j].Direction + ", " + boxelement.ConnectionMatrix[i, j].Distance);
                                txt = txt + "\n" + "[" + i + "," + j + "] " + boxelement.ConnectionMatrix[i, j].Direction + ", " + boxelement.ConnectionMatrix[i, j].Distance;
                            }

                        }
                    }

                }
            }
        }

        public void IntermediateResultsConnectionMatrix(bool YesNo, Box[] ListOfJB)
        {
            if (YesNo == true)
            {
                Console.WriteLine("----------- Connection Matrix for Junction Boxes -----------");
                foreach (Box boxelement in ListOfJB)
                {
                    Console.WriteLine("BoxNr: " + boxelement.Nr);

                    if (boxelement.FE1 != null && boxelement.FE2 != null && boxelement.FE3 != null)
                    { Console.WriteLine("FE1: " + boxelement.FE1.Nr + ", FE2: " + boxelement.FE2.Nr + ", FE3: " + boxelement.FE3.Nr); }
                    else if (boxelement.FE1 != null && boxelement.FE2 != null)
                    { Console.WriteLine("FE1: " + boxelement.FE1.Nr + ", FE2: " + boxelement.FE2.Nr); }
                    else if (boxelement.FE1 != null && boxelement.FE3 != null)
                    { Console.WriteLine("FE1: " + boxelement.FE1.Nr + ", FE3: " + boxelement.FE3.Nr); }
                    else if (boxelement.FE1 != null)
                    { Console.WriteLine("FE1: " + boxelement.FE1.Nr); }

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (boxelement.ConnectionMatrix[i, j] != null)
                            {
                                Console.WriteLine("[" + i + "," + j + "] " + boxelement.ConnectionMatrix[i, j].Direction + ", " + boxelement.ConnectionMatrix[i, j].Distance);

                            }

                        }
                    }

                }
            }
        }

        public void JBconsoleOutput(bool YesNo,JunctionBox JB, string txt)
        {

            if (YesNo == true)
            {
                Console.WriteLine("----------- Flanking elements in junction Boxes ----------");
                txt = txt + "\n" + "----------- Flanking elements in junction Boxes ----------";


                if (JB.JBox1.FE1 == null & JB.JBox1.FE2 == null & JB.JBox1.FE3 == null)
                {
                    Console.WriteLine("JunctionBox1: - ");
                    txt = txt + "\n" + "JunctionBox1: - ";
                }
                else if (JB.JBox1.FE1 != null & JB.JBox1.FE2 == null & JB.JBox1.FE3 == null)
                {
                    Console.WriteLine("JunctionBox1: " + JB.JBox1.FE1.Nr);
                    txt = txt + "\n" + "JunctionBox1: " + JB.JBox1.FE1.Nr;
                }
                else if (JB.JBox1.FE1 != null & JB.JBox1.FE2 != null & JB.JBox1.FE3 == null)
                {
                    Console.WriteLine("JunctionBox1: " + JB.JBox1.FE1.Nr + ", " + JB.JBox1.FE2.Nr);
                    txt = txt + "\n" + "JunctionBox1: " + JB.JBox1.FE1.Nr + ", " + JB.JBox1.FE2.Nr;
                } 
                else if (JB.JBox1.FE1 != null & JB.JBox1.FE2 != null & JB.JBox1.FE3 != null)
                {
                    Console.WriteLine("JunctionBox1: " + JB.JBox1.FE1.Nr + ", " + JB.JBox1.FE2.Nr + ", " + JB.JBox1.FE3.Nr);
                    txt = txt + "\n" + "JunctionBox1: " + JB.JBox1.FE1.Nr + ", " + JB.JBox1.FE2.Nr + ", " + JB.JBox1.FE3.Nr;
                }

                if (JB.JBox2.FE1 == null & JB.JBox2.FE2 == null & JB.JBox2.FE3 == null)
                {
                    Console.WriteLine("JunctionBox2: - ");
                    txt = txt + "\n" + "JunctionBox2: - ";
                }
                else if (JB.JBox2.FE1 != null & JB.JBox2.FE2 == null & JB.JBox2.FE3 == null)
                {
                    Console.WriteLine("JunctionBox2: " + JB.JBox2.FE1.Nr);
                    txt = txt + "\n" + "JunctionBox2: " + JB.JBox2.FE1.Nr;
                }
                else if (JB.JBox2.FE1 != null & JB.JBox2.FE2 != null & JB.JBox2.FE3 == null)
                {
                    Console.WriteLine("JunctionBox2: " + JB.JBox2.FE1.Nr + ", " + JB.JBox2.FE2.Nr);
                    txt = txt + "\n" + "JunctionBox2: " + JB.JBox2.FE1.Nr + ", " + JB.JBox2.FE2.Nr;
                }
                else if (JB.JBox2.FE1 != null & JB.JBox2.FE2 != null & JB.JBox2.FE3 != null)
                {
                    Console.WriteLine("JunctionBox2: " + JB.JBox2.FE1.Nr + ", " + JB.JBox2.FE2.Nr + ", " + JB.JBox2.FE3.Nr);
                    txt = txt + "\n" + "JunctionBox2: " + JB.JBox2.FE1.Nr + ", " + JB.JBox2.FE2.Nr + ", " + JB.JBox2.FE3.Nr;
                }

                if (JB.JBox3.FE1 == null & JB.JBox3.FE2 == null & JB.JBox3.FE3 == null)
                {
                    Console.WriteLine("JunctionBox3: - ");
                    txt = txt + "\n" + "JunctionBox3: - ";
                }
                else if (JB.JBox3.FE1 != null & JB.JBox3.FE2 == null & JB.JBox3.FE3 == null)
                {
                    Console.WriteLine("JunctionBox3: " + JB.JBox3.FE1.Nr);
                    txt = txt + "\n" + "JunctionBox3: " + JB.JBox3.FE1.Nr;
                }
                else if (JB.JBox3.FE1 != null & JB.JBox3.FE2 != null & JB.JBox3.FE3 == null)
                {
                    Console.WriteLine("JunctionBox3: " + JB.JBox3.FE1.Nr + ", " + JB.JBox3.FE2.Nr);
                    txt = txt + "\n" + "JunctionBox3: " + JB.JBox3.FE1.Nr + ", " + JB.JBox3.FE2.Nr;
                }
                else if (JB.JBox3.FE1 != null & JB.JBox3.FE2 != null & JB.JBox3.FE3 != null)
                {
                    Console.WriteLine("JunctionBox3: " + JB.JBox3.FE1.Nr + ", " + JB.JBox3.FE2.Nr + ", " + JB.JBox3.FE3.Nr);
                    txt = txt + "\n" + "JunctionBox3: " + JB.JBox3.FE1.Nr + ", " + JB.JBox3.FE2.Nr + ", " + JB.JBox3.FE3.Nr;
                }

                if (JB.JBox4.FE1 == null & JB.JBox4.FE2 == null & JB.JBox4.FE3 == null)
                {
                    Console.WriteLine("JunctionBox4: - ");
                    txt = txt + "\n" + "JunctionBox4: - ";
                }
                else if (JB.JBox4.FE1 != null & JB.JBox4.FE2 == null & JB.JBox4.FE3 == null)
                {
                    Console.WriteLine("JunctionBox4: " + JB.JBox4.FE1.Nr);
                    txt = txt + "\n" + "JunctionBox4: " + JB.JBox4.FE1.Nr;
                }
                else if (JB.JBox4.FE1 != null & JB.JBox4.FE2 != null & JB.JBox4.FE3 == null)
                {
                    Console.WriteLine("JunctionBox4: " + JB.JBox4.FE1.Nr + ", " + JB.JBox4.FE2.Nr);
                    txt = txt + "\n" + "JunctionBox4: " + JB.JBox4.FE1.Nr + ", " + JB.JBox4.FE2.Nr;
                }
                else if (JB.JBox4.FE1 != null & JB.JBox4.FE2 != null & JB.JBox4.FE3 != null)
                {
                    Console.WriteLine("JunctionBox4: " + JB.JBox4.FE1.Nr + ", " + JB.JBox4.FE2.Nr + ", " + JB.JBox4.FE3.Nr);
                    txt = txt + "\n" + "JunctionBox4: " + JB.JBox4.FE1.Nr + ", " + JB.JBox4.FE2.Nr + ", " + JB.JBox4.FE3.Nr;
                } 

                if (JB.JBox5.FE1 == null & JB.JBox5.FE2 == null & JB.JBox5.FE3 == null)
                {
                    Console.WriteLine("JunctionBox5: - ");
                    txt = txt + "\n" + "JunctionBox5: - ";
                }
                else if (JB.JBox5.FE1 != null & JB.JBox5.FE2 == null & JB.JBox5.FE3 == null)
                {
                    Console.WriteLine("JunctionBox5: " + JB.JBox5.FE1.Nr);
                    txt = txt + "\n" + "JunctionBox5: " + JB.JBox5.FE1.Nr;
                }
                else if (JB.JBox5.FE1 != null & JB.JBox5.FE2 != null & JB.JBox5.FE3 == null)
                {
                    Console.WriteLine("JunctionBox5: " + JB.JBox5.FE1.Nr + ", " + JB.JBox5.FE2.Nr);
                    txt = txt + "\n" + "JunctionBox5: " + JB.JBox5.FE1.Nr + ", " + JB.JBox5.FE2.Nr;
                }
                else if (JB.JBox5.FE1 != null & JB.JBox5.FE2 != null & JB.JBox5.FE3 != null)
                {
                    Console.WriteLine("JunctionBox5: " + JB.JBox5.FE1.Nr + ", " + JB.JBox5.FE2.Nr + ", " + JB.JBox5.FE3.Nr);
                    txt = txt + "\n" + "JunctionBox5: " + JB.JBox5.FE1.Nr + ", " + JB.JBox5.FE2.Nr + ", " + JB.JBox5.FE3.Nr;
                }

                if (JB.JBox6.FE1 == null & JB.JBox6.FE2 == null & JB.JBox6.FE3 == null)
                {
                    Console.WriteLine("JunctionBox6: - ");
                    txt = txt + "\n" + "JunctionBox6: - ";
                }
                else if (JB.JBox6.FE1 != null & JB.JBox6.FE2 == null & JB.JBox6.FE3 == null)
                {
                    Console.WriteLine("JunctionBox6: " + JB.JBox6.FE1.Nr);
                    txt = txt + "\n" + "JunctionBox6: " + JB.JBox6.FE1.Nr;
                }
                else if (JB.JBox6.FE1 != null & JB.JBox6.FE2 != null & JB.JBox6.FE3 == null)
                {
                    Console.WriteLine("JunctionBox6: " + JB.JBox6.FE1.Nr + ", " + JB.JBox6.FE2.Nr);
                    txt = txt + "\n" + "JunctionBox6: " + JB.JBox6.FE1.Nr + ", " + JB.JBox6.FE2.Nr;
                }
                else if (JB.JBox6.FE1 != null & JB.JBox6.FE2 != null & JB.JBox6.FE3 != null)
                {
                    Console.WriteLine("JunctionBox6: " + JB.JBox6.FE1.Nr + ", " + JB.JBox6.FE2.Nr + ", " + JB.JBox6.FE3.Nr);
                    txt = txt + "\n" + "JunctionBox6: " + JB.JBox6.FE1.Nr + ", " + JB.JBox6.FE2.Nr + ", " + JB.JBox6.FE3.Nr;
                }

            }
 

        }

        public void JBconsoleOutput(bool YesNo, JunctionBox JB)
        {

            if (YesNo == true)
            {
                Console.WriteLine("----------- Flanking elements in junction Boxes ----------");


                if (JB.JBox1.FE1 == null & JB.JBox1.FE2 == null & JB.JBox1.FE3 == null)
                {
                    Console.WriteLine("JunctionBox1: - ");
                }
                else if (JB.JBox1.FE1 != null & JB.JBox1.FE2 == null & JB.JBox1.FE3 == null)
                {
                    Console.WriteLine("JunctionBox1: " + JB.JBox1.FE1.Nr);
                }
                else if (JB.JBox1.FE1 != null & JB.JBox1.FE2 != null & JB.JBox1.FE3 == null)
                {
                    Console.WriteLine("JunctionBox1: " + JB.JBox1.FE1.Nr + ", " + JB.JBox1.FE2.Nr);
                }
                else if (JB.JBox1.FE1 != null & JB.JBox1.FE2 != null & JB.JBox1.FE3 != null)
                {
                    Console.WriteLine("JunctionBox1: " + JB.JBox1.FE1.Nr + ", " + JB.JBox1.FE2.Nr + ", " + JB.JBox1.FE3.Nr);
                }

                if (JB.JBox2.FE1 == null & JB.JBox2.FE2 == null & JB.JBox2.FE3 == null)
                {
                    Console.WriteLine("JunctionBox2: - ");
                }
                else if (JB.JBox2.FE1 != null & JB.JBox2.FE2 == null & JB.JBox2.FE3 == null)
                {
                    Console.WriteLine("JunctionBox2: " + JB.JBox2.FE1.Nr);
                }
                else if (JB.JBox2.FE1 != null & JB.JBox2.FE2 != null & JB.JBox2.FE3 == null)
                {
                    Console.WriteLine("JunctionBox2: " + JB.JBox2.FE1.Nr + ", " + JB.JBox2.FE2.Nr);
                }
                else if (JB.JBox2.FE1 != null & JB.JBox2.FE2 != null & JB.JBox2.FE3 != null)
                {
                    Console.WriteLine("JunctionBox2: " + JB.JBox2.FE1.Nr + ", " + JB.JBox2.FE2.Nr + ", " + JB.JBox2.FE3.Nr);
                }

                if (JB.JBox3.FE1 == null & JB.JBox3.FE2 == null & JB.JBox3.FE3 == null)
                {
                    Console.WriteLine("JunctionBox3: - ");
                }
                else if (JB.JBox3.FE1 != null & JB.JBox3.FE2 == null & JB.JBox3.FE3 == null)
                {
                    Console.WriteLine("JunctionBox3: " + JB.JBox3.FE1.Nr);
                }
                else if (JB.JBox3.FE1 != null & JB.JBox3.FE2 != null & JB.JBox3.FE3 == null)
                {
                    Console.WriteLine("JunctionBox3: " + JB.JBox3.FE1.Nr + ", " + JB.JBox3.FE2.Nr);
                }
                else if (JB.JBox3.FE1 != null & JB.JBox3.FE2 != null & JB.JBox3.FE3 != null)
                {
                    Console.WriteLine("JunctionBox3: " + JB.JBox3.FE1.Nr + ", " + JB.JBox3.FE2.Nr + ", " + JB.JBox3.FE3.Nr);
                }

                if (JB.JBox4.FE1 == null & JB.JBox4.FE2 == null & JB.JBox4.FE3 == null)
                {
                    Console.WriteLine("JunctionBox4: - ");
                }
                else if (JB.JBox4.FE1 != null & JB.JBox4.FE2 == null & JB.JBox4.FE3 == null)
                {
                    Console.WriteLine("JunctionBox4: " + JB.JBox4.FE1.Nr);
                }
                else if (JB.JBox4.FE1 != null & JB.JBox4.FE2 != null & JB.JBox4.FE3 == null)
                {
                    Console.WriteLine("JunctionBox4: " + JB.JBox4.FE1.Nr + ", " + JB.JBox4.FE2.Nr);
                }
                else if (JB.JBox4.FE1 != null & JB.JBox4.FE2 != null & JB.JBox4.FE3 != null)
                {
                    Console.WriteLine("JunctionBox4: " + JB.JBox4.FE1.Nr + ", " + JB.JBox4.FE2.Nr + ", " + JB.JBox4.FE3.Nr);
                }

                if (JB.JBox5.FE1 == null & JB.JBox5.FE2 == null & JB.JBox5.FE3 == null)
                {
                    Console.WriteLine("JunctionBox5: - ");
                }
                else if (JB.JBox5.FE1 != null & JB.JBox5.FE2 == null & JB.JBox5.FE3 == null)
                {
                    Console.WriteLine("JunctionBox5: " + JB.JBox5.FE1.Nr);
                }
                else if (JB.JBox5.FE1 != null & JB.JBox5.FE2 != null & JB.JBox5.FE3 == null)
                {
                    Console.WriteLine("JunctionBox5: " + JB.JBox5.FE1.Nr + ", " + JB.JBox5.FE2.Nr);
                }
                else if (JB.JBox5.FE1 != null & JB.JBox5.FE2 != null & JB.JBox5.FE3 != null)
                {
                    Console.WriteLine("JunctionBox5: " + JB.JBox5.FE1.Nr + ", " + JB.JBox5.FE2.Nr + ", " + JB.JBox5.FE3.Nr);
                }

                if (JB.JBox6.FE1 == null & JB.JBox6.FE2 == null & JB.JBox6.FE3 == null)
                {
                    Console.WriteLine("JunctionBox6: - ");
                }
                else if (JB.JBox6.FE1 != null & JB.JBox6.FE2 == null & JB.JBox6.FE3 == null)
                {
                    Console.WriteLine("JunctionBox6: " + JB.JBox6.FE1.Nr);
                }
                else if (JB.JBox6.FE1 != null & JB.JBox6.FE2 != null & JB.JBox6.FE3 == null)
                {
                    Console.WriteLine("JunctionBox6: " + JB.JBox6.FE1.Nr + ", " + JB.JBox6.FE2.Nr);
                }
                else if (JB.JBox6.FE1 != null & JB.JBox6.FE2 != null & JB.JBox6.FE3 != null)
                {
                    Console.WriteLine("JunctionBox6: " + JB.JBox6.FE1.Nr + ", " + JB.JBox6.FE2.Nr + ", " + JB.JBox6.FE3.Nr);
                }

            }


        }

        public void IntermediateResultsConnectionZoneMatrix(bool YesNo, Box[] ListOfJB, string txt)
        {
            if (YesNo == true)
            {
                Console.WriteLine("----------- Connection Zone Matrix for Junction Boxes -----------");
                txt = txt + "\n" + "----------- Connection Zone Matrix for Junction Boxes -----------";
                foreach (Box boxelement in ListOfJB)
                {
                    Console.WriteLine("BoxNr: " + boxelement.Nr);
                    txt = txt + "\n" + "BoxNr: " + boxelement.Nr;
                    if (boxelement.FE1 != null && boxelement.FE2 != null && boxelement.FE3 != null)
                    { 
                        Console.WriteLine("FE1: " + boxelement.FE1.Nr + ", FE2: " + boxelement.FE2.Nr + ", FE3: " + boxelement.FE3.Nr);
                        txt = txt + "\n" + "FE1: " + boxelement.FE1.Nr + ", FE2: " + boxelement.FE2.Nr + ", FE3: " + boxelement.FE3.Nr;
                            }
                    else if (boxelement.FE1 != null && boxelement.FE2 != null)
                    {
                        Console.WriteLine("FE1: " + boxelement.FE1.Nr + ", FE2: " + boxelement.FE2.Nr);
                        txt = txt + "\n" + "FE1: " + boxelement.FE1.Nr + ", FE2: " + boxelement.FE2.Nr;
                            }
                    else if (boxelement.FE1 != null && boxelement.FE3 != null)
                    { 
                        Console.WriteLine("FE1: " + boxelement.FE1.Nr + ", FE3: " + boxelement.FE3.Nr);
                        txt = txt + "\n" + "FE1: " + boxelement.FE1.Nr + ", FE3: " + boxelement.FE3.Nr;
                            }
                    else if (boxelement.FE1 != null)
                    { 
                        Console.WriteLine("FE1: " + boxelement.FE1.Nr);
                        txt = txt + "\n" + "FE1: " + boxelement.FE1.Nr;
                            }

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (boxelement.ConnectionZoneMatrix[i, j] != ConnectionZones.None)
                            {
                                Console.WriteLine("[" + i + "," + j + "] " + boxelement.ConnectionZoneMatrix[i, j]);
                                txt = txt + "\n" + "[" + i + "," + j + "] " + boxelement.ConnectionZoneMatrix[i, j];
                            }

                        }
                    }

                }
            }
        }

        public void IntermediateResultsConnectionZoneMatrix(bool YesNo, Box[] ListOfJB)
        {
            if (YesNo == true)
            {
                Console.WriteLine("----------- Connection Zone Matrix for Junction Boxes -----------");
                foreach (Box boxelement in ListOfJB)
                {
                    Console.WriteLine("BoxNr: " + boxelement.Nr);

                    if (boxelement.FE1 != null && boxelement.FE2 != null && boxelement.FE3 != null)
                    { Console.WriteLine("FE1: " + boxelement.FE1.Nr + ", FE2: " + boxelement.FE2.Nr + ", FE3: " + boxelement.FE3.Nr); }
                    else if (boxelement.FE1 != null && boxelement.FE2 != null)
                    { Console.WriteLine("FE1: " + boxelement.FE1.Nr + ", FE2: " + boxelement.FE2.Nr); }
                    else if (boxelement.FE1 != null && boxelement.FE3 != null)
                    { Console.WriteLine("FE1: " + boxelement.FE1.Nr + ", FE3: " + boxelement.FE3.Nr); }
                    else if (boxelement.FE1 != null)
                    { Console.WriteLine("FE1: " + boxelement.FE1.Nr); }

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (boxelement.ConnectionZoneMatrix[i, j] != ConnectionZones.None)
                            {
                                Console.WriteLine("[" + i + "," + j + "] " + boxelement.ConnectionZoneMatrix[i, j]);

                            }

                        }
                    }

                }
            }
        }



    }
}
