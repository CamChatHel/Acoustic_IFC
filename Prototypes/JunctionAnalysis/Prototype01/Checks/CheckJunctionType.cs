using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype01
{
    class CheckJunctionType
    {
        public JunctionType GetJunctionType(ConnectionZones[,] CZmatrix, string[] elementDirection)
        {
            string alleZeilen = SerializeMatrix(CZmatrix);
            string elDi = SerializeMatrix(elementDirection);
            if (alleZeilen.Equals(SerializeMatrix(junctionLh1d2_a)) || alleZeilen.Equals(SerializeMatrix(junctionLh1d2_b)))
            {
                if (elDi.Equals("nmNoneNone"))
                {
                    return JunctionType.Lh1d2;
                }
                else if (elDi.Equals("noNoneNone") || elDi.Equals("onNoneNone"))
                {
                    return JunctionType.Lv1d2;
                }
                else 
                {
                    Console.WriteLine("");
                    Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: lh1-2 or Lv1-2");
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }
            else if (alleZeilen.Equals(SerializeMatrix(junctionLh1d2_c)) || alleZeilen.Equals(SerializeMatrix(junctionLh1d2_d)))
            {
                if (elDi.Equals("nNoneNonem"))
                {
                    return JunctionType.Lh1d2;
                }
                else if (elDi.Equals("nNoneNoneo") || elDi.Equals("oNoneNonen"))
                {
                    return JunctionType.Lv1d2;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: lh1-2 or Lv1-2");
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }
            //else if (alleZeilen.Equals(SerializeMatrix(junctionTh1d24_a)))
            //{ //Aenderung bei Element Direction Matrix 07.06.2023 -> Bei X-Stoss nicht mehr eindeutig!!
            //    if (elDi.Equals("nmNonem"))
            //    {
            //        return JunctionType.Th1d24;
            //    }
            //    else if (elDi.Equals("onNonen")) 
            //    {
            //        return JunctionType.Tv1d24;
            //    }
            //    else if (elDi.Equals("noNoneo"))
            //    {
            //        return JunctionType.Tv2d13;
            //    }
            //    else
            //    {
            //        Console.WriteLine("");
            //        Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: Th1-24 or Tv2-13 or Tv1-24");
            //        return JunctionType.ErrorNoCorrectDirection;
            //    }

            //}
            //else if (alleZeilen.Equals(SerializeMatrix(junctionTh1d24_c)))
            //{ //Aenderung bei Element Direction Matrix 07.06.2023 -> Bei X-Stoss nicht mehr eindeutig!!
            //    if (elDi.Equals("nNonenm")) 
            //    {
            //        return JunctionType.Th1d24;
            //    }
            //    else if (elDi.Equals("nNoneno"))
            //    {
            //        return JunctionType.Tv1d24;

            //    }
            //    else if (elDi.Equals("oNoneon"))
            //    {
            //        return JunctionType.Tv2d13;
            //    }
            //    else
            //    {
            //        Console.WriteLine("");
            //        Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: Th1-24 or Tv2-13 or Tv1-24");
            //        return JunctionType.ErrorNoCorrectDirection;
            //    }
            //}
            else if (alleZeilen.Equals(SerializeMatrix(junctionTh1d24_a)) || alleZeilen.Equals(SerializeMatrix(junctionTh1d24_c)))
            {
                if (elDi.Equals("nNoneNonem")) 
                {
                    return JunctionType.Th1d24;
                }
                else if (elDi.Equals("nNoneNoneo")) 
                {
                    return JunctionType.Tv2d13;
                }
                else if (elDi.Equals("oNoneNonen")) 
                {
                    return JunctionType.Tv1d24;
                }
                else if (elDi.Equals("nmNoneNone")) 
                {
                    return JunctionType.Th1d24;
                }
                else if (elDi.Equals("onNoneNone"))
                {
                    return JunctionType.Tv1d24;

                }
                else if (elDi.Equals("noNoneNone"))
                {
                    return JunctionType.Tv2d13;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: Th1-24 or Tv2-13 or Tv1-24");
                    return JunctionType.ErrorNoCorrectDirection;
                }

            }
            else if (alleZeilen.Equals(SerializeMatrix(junctionTh1d24_c)))
            {
                if (elDi.Equals("nmNoneNone"))
                {
                    return JunctionType.Th1d24;
                }
                else if (elDi.Equals("onNoneNone"))
                {
                    return JunctionType.Tv2d13;
                }
                else if (elDi.Equals("noNoneNone"))
                {
                    return JunctionType.Tv1d24;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: Th1-24 or Tv2-13 or Tv1-24");
                    return JunctionType.ErrorNoCorrectDirection;
                }

            }
            else if (alleZeilen.Equals(SerializeMatrix(junctionTh1d24_b)))
            {
                if (elDi.Equals("nmNoneNone"))
                {
                    return JunctionType.Th1d24;
                }
                else if (elDi.Equals("onNoneNone"))
                {
                    return JunctionType.Tv2d13;
                }
                else if (elDi.Equals("noNoneNone"))
                {
                    return JunctionType.Tv1d24;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: Th1-24 or Tv2-13 or Tv1-24");
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }
            else if (alleZeilen.Equals(SerializeMatrix(junctionTh1d2c4_a)))
            {
                if (elDi.Equals("nNonenm"))
                {
                    return JunctionType.Th1d2c4;
                }
                else if (elDi.Equals("nNoneno"))
                {
                    return JunctionType.Tv1d2c4;
                }
                else if (elDi.Equals("oNoneon"))
                {
                    return JunctionType.Tv2d1c3;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: Th1-2:4 or Tv2-1:3 or Tv1-2:4");
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }
            else if (alleZeilen.Equals(SerializeMatrix(junctionTh1d2c4_b)))
            {
                if (elDi.Equals("nmnNone"))
                {
                    return JunctionType.Th1d2c4;
                }
                else if (elDi.Equals("nonNone"))
                {
                    return JunctionType.Tv1d2c4;
                }
                else if (elDi.Equals("onoNone"))
                {
                    return JunctionType.Tv2d1c3;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: Th1-2:4 or Tv2-1:3 or Tv1-2:4");
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }
            else if (alleZeilen.Equals(SerializeMatrix(junctionTh1d2c4_c)))
            {
                if (elDi.Equals("nmNonem"))
                {
                    return JunctionType.Th1d2c4;
                }
                else if (elDi.Equals("noNoneo"))
                {
                    return JunctionType.Tv2d1c3;
                }
                else if (elDi.Equals("onNonen"))
                {
                    return JunctionType.Tv1d2c4;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: Th1-2:4 or Tv2-1:3 or Tv1-2:4");
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }
            else if (alleZeilen.Equals(SerializeMatrix(junctionTh2d1d4_a)))
            {
                if (elDi.Equals("nNonenm"))
                {
                    return JunctionType.Th2d1d4;
                }
                else if (elDi.Equals("nNoneno"))
                {
                    return JunctionType.Tv2d1d4;
                }
                else if (elDi.Equals("oNoneon"))
                {
                    return JunctionType.Tv1d2d3;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: Th1-2-4 or Tv2-1-3 or Tv1-2-4");
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }
            else if (alleZeilen.Equals(SerializeMatrix(junctionTh2d1d4_b)))
            {
                if (elDi.Equals("nmnNone"))
                {
                    return JunctionType.Th2d1d4;
                }
                else if (elDi.Equals("nonNone"))
                {
                    return JunctionType.Tv2d1d4;
                }
                else if (elDi.Equals("onoNone"))
                {
                    return JunctionType.Tv1d2d3;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: Th1-2:4 or Tv2-1:3 or Tv1-2:4");
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }
            else if (alleZeilen.Equals(SerializeMatrix(junctionTh2d1d4_c)))
            {
                if (elDi.Equals("nmNonem"))
                {
                    return JunctionType.Th2d1d4;
                }
                else if (elDi.Equals("noNoneo"))
                {
                    return JunctionType.Tv1d2d3;
                }
                else if (elDi.Equals("onNonen"))
                {
                    return JunctionType.Tv2d1d4;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: Th1-2:4 or Tv2-1:3 or Tv1-2:4");
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }
            else if (alleZeilen.Equals(SerializeMatrix(junctionXh1d24d3_a)))
            {
                if (elDi.Equals("nmnNone"))
                {
                    return JunctionType.Xh1d24d3;
                }
                else if (elDi.Equals("nonNone"))
                {
                    return JunctionType.Xv2d13d4;
                }
                else if (elDi.Equals("onoNone"))
                {
                    return JunctionType.Xv1d24d3;
                }
                else
                        {
                    Console.WriteLine("");
                    Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: Xh1-24-3 or Xv2-13-4 or Xv1-24-3");
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }
            else if (alleZeilen.Equals(SerializeMatrix(junctionXh1d24d3_c)))
            {
                if (elDi.Equals("nmNonem"))
                {
                    return JunctionType.Xh1d24d3;
                }
                else if (elDi.Equals("noNoneo"))
                {
                    return JunctionType.Xv1d24d3;
                }
                else if (elDi.Equals("onNonen"))
                {
                    return JunctionType.Xv2d13d4;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: Xh1-24-3 or Xv2-13-4 or Xv1-24-3");
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }
            else if (alleZeilen.Equals(SerializeMatrix(junctionXh2d1c3d4_a)) || alleZeilen.Equals(SerializeMatrix(junctionXh2d1c3d4_b)))
            {
                if (elDi.Equals("nmnm"))
                {
                    return JunctionType.Xh2d1c3d4;
                }
                else if (elDi.Equals("nono") || elDi.Equals("onon"))
                {
                    return JunctionType.Xv2d1c3d4;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("No exact junction type due to false element direction. Possible junctiontype: Xh2d1c3d4 or Xv2d1c3d4");
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }




            return JunctionType.ErrorNoCorrectDirection;

        }
        public JunctionType GetJunctionType(string zeile1, string zeile2, string zeile3, string zeile4, string[] elementDirection)
        {
            //Baum 1
            if (zeile1 == "NoneShortNoneNone") 
            {
                if (zeile2 == "BorderNoneNoneNone" &&
                    zeile3 == "NoneNoneNoneNone" && zeile4 == "NoneNoneNoneNone")
                {
                    //Lh1-2 OR Lv1-2
                    if (elementDirection[0] == "n" && elementDirection[1] == "m")
                    {
                        return JunctionType.Lh1d2;
                    }
                    else if (elementDirection[0] == "n" && elementDirection[1] == "o")
                    {
                        return JunctionType.Lv1d2;
                    }
                    else
                    {
                        return JunctionType.ErrorNoCorrectDirection;
                    }
                }
                else if (zeile2 == "MiddleNoneNoneNone" &&
                        zeile3 == "NoneNoneNoneNone" && zeile4 == "NoneNoneNoneNone")
                     {       
                        //Th1-24 OR Tv1-24 OR Tv2-13
                        if (elementDirection[0] == "n" && elementDirection[1] == "m")
                        {
                            return JunctionType.Th1d24;
                        }
                        else if (elementDirection[0] == "n" && elementDirection[1] == "o")
                        {
                            return JunctionType.Tv2d13;
                        }
                        else if (elementDirection[0] == "o" && elementDirection[1] == "n")
                        {
                            return JunctionType.Tv1d24;
                        }
                    else
                    {
                        return JunctionType.ErrorNoCorrectDirection;
                    }
                }
            }

            //Baum 2
            if (zeile1 == "NoneBorderNoneNone" && zeile2 == "ShortNoneNoneNone"
                        && zeile3 == "NoneNoneNoneNone" && zeile4 == "NoneNoneNoneNone")
            {
                if (elementDirection[0] == "n" && elementDirection[1] == "m")
                {
                    return JunctionType.Lh1d2;
                }
                else if (elementDirection[0] == "o" && elementDirection[1] == "n")
                {
                    return JunctionType.Lv1d2;
                }
                else
                {
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }

            //Baum 3
            if (zeile1 == "NoneMiddleNoneNone" && zeile2 == "ShortNoneNoneNone"
                        && zeile3 == "NoneNoneNoneNone" && zeile4 == "NoneNoneNoneNone")
            {
                if (elementDirection[0] == "n" && elementDirection[1] == "m" )
                {
                    return JunctionType.Th1d24;
                }
                else if (elementDirection[0] == "n" && elementDirection[1] == "o" )
                {
                    return JunctionType.Tv1d24;
                }
                else if (elementDirection[0] == "o" && elementDirection[1] == "n" )
                {
                    return JunctionType.Tv2d13;
                }
                else
                {
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }

            //Baum 4
            if (zeile1 == "NoneShortNoneShort" && zeile2 == "BorderNoneNoneShort"
                        && zeile3 == "NoneNoneNoneNone" && zeile4 == "BorderShortNoneNone")
            {
                if (elementDirection[0] == "n" && elementDirection[1] == "m" && elementDirection[3] == "m")
                {
                    return JunctionType.Th1d2c4;
                }
                else if (elementDirection[0] == "n" && elementDirection[1] == "o" && elementDirection[3] == "o")
                {
                    return JunctionType.Tv2d1c3;
                }
                else if (elementDirection[0] == "o" && elementDirection[1] == "n" && elementDirection[3] == "n")
                {
                    return JunctionType.Tv1d2c4;
                }
                else
                {
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }

            //Baum 5
            if (zeile1 == "NoneBorderShortNone" && zeile2 == "ShortNoneShortNone"
                        && zeile3 == "ShortBorderNoneNone" && zeile4 == "NoneNoneNoneNone")
            {
                if (elementDirection[0] == "n" && elementDirection[1] == "o" && elementDirection[2] == "n")
                {
                    return JunctionType.Th1d2c4;
                }
                else if (elementDirection[0] == "o" && elementDirection[1] == "n" && elementDirection[2] == "o")
                {
                    return JunctionType.Tv2d1c3;
                }
                else if (elementDirection[0] == "n" && elementDirection[1] == "m" && elementDirection[2] == "n")
                {
                    return JunctionType.Tv1d2c4;
                }
                else
                {
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }

            //Baum 6
            if (zeile1 == "NoneBorderNoneBorder" && zeile2 == "ShortNoneNoneZero"
                        && zeile3 == "NoneNoneNoneNone"  && zeile4 == "ShortZeroNoneNone")
            {
                if (elementDirection[0] == "n" && elementDirection[1] == "m" && elementDirection[3] == "m")
                {
                    return JunctionType.Tv2d1d4;
                }
                else if (elementDirection[0] == "o" && elementDirection[1] == "n" && elementDirection[3] == "n")
                {
                    return JunctionType.Th2d1d4;
                }
                else
                {
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }

            //Baum 7
            if (zeile1 == "NoneShortZeroNone" && zeile2 == "BorderNoneBorderNone"
                        && zeile3 == "ZeroShortNoneNone" && zeile4 == "NoneNoneNoneNone")
            {
                if (elementDirection[0] == "n" && elementDirection[1] == "m" && elementDirection[2] == "m")
                {
                    return JunctionType.Tv2d1d4;
                }
                else if (elementDirection[0] == "o" && elementDirection[1] == "n" && elementDirection[2] == "n")
                {
                    return JunctionType.Th2d1d4;
                }
                else
                {
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }

            //Baum 8
            if (zeile1 == "NoneShortZeroNone" && zeile2 == "MiddleNoneMiddleNone"
                        && zeile3 == "ZeroShortNoneNone" && zeile4 == "NoneNoneNoneNone")
            {
                if (elementDirection[0] == "o" && elementDirection[1] == "n" && elementDirection[2] == "o")
                {
                    return JunctionType.Xv1d24d3;
                }
                else if (elementDirection[0] == "n" && elementDirection[1] == "m" && elementDirection[2] == "n")
                {
                    return JunctionType.Xh1d24d3;
                }
                else if (elementDirection[0] == "n" && elementDirection[1] == "o" && elementDirection[2] == "n")
                {
                    return JunctionType.Xv2d13d4;
                }
                else
                {
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }

            //Baum 9
            if (zeile1 == "NoneMiddleNoneMiddle" && zeile2 == "ShortNoneNoneZero"
                        && zeile3 == "NoneNoneNoneNone" && zeile4 == "ShortZeroNoneNone")
            {
                if (elementDirection[0] == "n" && elementDirection[1] == "o" && elementDirection[3] == "o")
                {
                    return JunctionType.Xv1d24d3;
                }
                else if (elementDirection[0] == "n" && elementDirection[1] == "m" && elementDirection[3] == "m")
                {
                    return JunctionType.Xh1d24d3;
                }
                else if (elementDirection[0] == "o" && elementDirection[1] == "n" && elementDirection[3] == "n")
                {
                    return JunctionType.Xv2d13d4;
                }
                else
                {
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }

            //Baum 10
            if (zeile1 == "NoneBorderShortBorder" && zeile2 == "ShortNoneShortZero"
                        && zeile3 == "ShortBorderNoneBorder" && zeile4 == "ShortZeroShortNone")
            {
                if (elementDirection[0] == "o" && elementDirection[1] == "n" && elementDirection[2] == "o" && elementDirection[3] == "n")
                {
                    return JunctionType.Xv2d1c3d4;
                }
                else if (elementDirection[0] == "n" && elementDirection[1] == "m" && elementDirection[2] == "n" && elementDirection[3] == "m")
                {
                    return JunctionType.Xh2d1c3d4;
                }
                else
                {
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }

            //Baum 11
            if (zeile1 == "NoneShortZeroShort" && zeile2 == "BorderNoneBorderShort"
                        && zeile3 == "ZeroShortNoneShort" && zeile4 == "BorderShortBorderNone")
            {
                if (elementDirection[0] == "n" && elementDirection[1] == "o" && elementDirection[2] == "n" && elementDirection[3] == "o")
                {
                    return JunctionType.Xv2d1c3d4;
                }
                else if (elementDirection[0] == "n" && elementDirection[1] == "m" && elementDirection[2] == "n" && elementDirection[3] == "m")
                {
                    return JunctionType.Xh2d1c3d4;
                }
                else
                {
                    return JunctionType.ErrorNoCorrectDirection;
                }
            }

            //Falls nichts zutrifft
            return JunctionType.JunctionTypeError;

        }

        // Write 2d-Matrix in string to use Equals
        public string SerializeMatrix(ConnectionZones[,] matrix)
        {
            string arraylines = "";
            foreach (var item in matrix)
            {
                arraylines = arraylines + item;
            }
            return arraylines;
        }
        // Write 1d-Matrix in string to use Equals
        public string SerializeMatrix(string[] matrix)
        {
            string arraylines = "";
            foreach (var item in matrix)
            {
                arraylines = arraylines + item;
            }
            return arraylines;
        }

        //All Connection Zone Matrix to define junction types
        //lh1-2 or Lv1-2
        public static readonly ConnectionZones[,] junctionLh1d2_a = { { ConnectionZones.None, ConnectionZones.Border, ConnectionZones.None, ConnectionZones.None }, 
                                                                { ConnectionZones.Short, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None }};
        public static readonly ConnectionZones[,] junctionLh1d2_b = { { ConnectionZones.None, ConnectionZones.Short, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.Border, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None }};
        public static readonly ConnectionZones[,] junctionLh1d2_c = { { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.Border },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.Short, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None }};
        public static readonly ConnectionZones[,] junctionLh1d2_d = { { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.Short },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.Border, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None }};

        //Th1-24 or Tv2-13 or Tv1-24
        public static readonly ConnectionZones[,] junctionTh1d24_a = { { ConnectionZones.None, ConnectionZones.Short, ConnectionZones.None, ConnectionZones.Short },
                                                                { ConnectionZones.Middle, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.Middle, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None }};
        public static readonly ConnectionZones[,] junctionTh1d24_b = { { ConnectionZones.None, ConnectionZones.Middle, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.Short, ConnectionZones.None, ConnectionZones.Short, ConnectionZones.None },
                                                                { ConnectionZones.None, ConnectionZones.Middle, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None }};
        public static readonly ConnectionZones[,] junctionTh1d24_c = { { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.Middle },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.Middle },
                                                                { ConnectionZones.Short, ConnectionZones.None, ConnectionZones.Short, ConnectionZones.None }};

        //Tv1-2:4 or Tv2-1:3 or Th1-2:4
        public static readonly ConnectionZones[,] junctionTh1d2c4_a = { { ConnectionZones.None, ConnectionZones.None, ConnectionZones.Short, ConnectionZones.Border },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.Short, ConnectionZones.None, ConnectionZones.None, ConnectionZones.Border },
                                                                { ConnectionZones.Short, ConnectionZones.None, ConnectionZones.Short, ConnectionZones.None }};
        public static readonly ConnectionZones[,] junctionTh1d2c4_b = { { ConnectionZones.None, ConnectionZones.Border, ConnectionZones.Short, ConnectionZones.None },
                                                                { ConnectionZones.Short, ConnectionZones.None, ConnectionZones.Short, ConnectionZones.None },
                                                                { ConnectionZones.Short, ConnectionZones.Border, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None }};
        public static readonly ConnectionZones[,] junctionTh1d2c4_c = { { ConnectionZones.None, ConnectionZones.Short, ConnectionZones.None, ConnectionZones.Short },
                                                                { ConnectionZones.Border, ConnectionZones.None, ConnectionZones.None, ConnectionZones.Short },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.Border, ConnectionZones.Short, ConnectionZones.None, ConnectionZones.None }};

        //Tv2-1-1 or Th2-1-4 or Tv1-2-3
        public static readonly ConnectionZones[,] junctionTh2d1d4_a = { { ConnectionZones.None, ConnectionZones.None, ConnectionZones.Zero, ConnectionZones.Short },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.Zero, ConnectionZones.None, ConnectionZones.None, ConnectionZones.Short },
                                                                { ConnectionZones.Border, ConnectionZones.None, ConnectionZones.Border, ConnectionZones.None }};
        public static readonly ConnectionZones[,] junctionTh2d1d4_b = { { ConnectionZones.None, ConnectionZones.Short, ConnectionZones.Zero, ConnectionZones.None },
                                                                { ConnectionZones.Border, ConnectionZones.None, ConnectionZones.Border, ConnectionZones.None },
                                                                { ConnectionZones.Zero, ConnectionZones.Short, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None }};
        public static readonly ConnectionZones[,] junctionTh2d1d4_c = { { ConnectionZones.None, ConnectionZones.Border, ConnectionZones.None, ConnectionZones.Border },
                                                                { ConnectionZones.Short, ConnectionZones.None, ConnectionZones.None, ConnectionZones.Zero },
                                                                { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
                                                                { ConnectionZones.Short, ConnectionZones.Zero, ConnectionZones.None, ConnectionZones.None }};

        //Xh1-24-3 or Xv2-13-4 or Xv1-24-3
        public static readonly ConnectionZones[,] junctionXh1d24d3_a = { { ConnectionZones.None, ConnectionZones.Short, ConnectionZones.Zero, ConnectionZones.Short },
                                                                { ConnectionZones.Middle, ConnectionZones.None, ConnectionZones.Middle, ConnectionZones.None },
                                                                { ConnectionZones.Zero, ConnectionZones.Short, ConnectionZones.None, ConnectionZones.Short },
                                                                { ConnectionZones.Middle, ConnectionZones.None, ConnectionZones.Middle, ConnectionZones.None }};
        //public static readonly ConnectionZones[,] junctionXh1d24d3_b = { { ConnectionZones.None, ConnectionZones.Short, ConnectionZones.Zero, ConnectionZones.Short },
        //                                                        { ConnectionZones.Middle, ConnectionZones.None, ConnectionZones.Middle, ConnectionZones.None },
        //                                                        { ConnectionZones.Zero, ConnectionZones.Short, ConnectionZones.None, ConnectionZones.Short },
        //                                                        { ConnectionZones.Middle, ConnectionZones.None, ConnectionZones.Middle, ConnectionZones.None }};
        public static readonly ConnectionZones[,] junctionXh1d24d3_c = { { ConnectionZones.None, ConnectionZones.Middle, ConnectionZones.None, ConnectionZones.Middle },
                                                                { ConnectionZones.Short, ConnectionZones.None, ConnectionZones.Short, ConnectionZones.Zero },
                                                                { ConnectionZones.None, ConnectionZones.Middle, ConnectionZones.None, ConnectionZones.Middle },
                                                                { ConnectionZones.Short, ConnectionZones.Zero, ConnectionZones.Short, ConnectionZones.None }};
        //Xh2d1c3d4 or Xv2d1c3d4
        public static readonly ConnectionZones[,] junctionXh2d1c3d4_a = { { ConnectionZones.None, ConnectionZones.Short, ConnectionZones.Zero, ConnectionZones.Short },
                                                                { ConnectionZones.Border, ConnectionZones.None, ConnectionZones.Border, ConnectionZones.Short },
                                                                { ConnectionZones.Zero, ConnectionZones.Short, ConnectionZones.None, ConnectionZones.Short },
                                                                { ConnectionZones.Border, ConnectionZones.Short, ConnectionZones.Border, ConnectionZones.None }};
        public static readonly ConnectionZones[,] junctionXh2d1c3d4_b = { { ConnectionZones.None, ConnectionZones.Border, ConnectionZones.Short, ConnectionZones.Border },
                                                                { ConnectionZones.Short, ConnectionZones.None, ConnectionZones.Short, ConnectionZones.Zero },
                                                                { ConnectionZones.Short, ConnectionZones.Border, ConnectionZones.None, ConnectionZones.Border },
                                                                { ConnectionZones.Short, ConnectionZones.Zero, ConnectionZones.Short, ConnectionZones.None }};

        //Vorlage
        //public static readonly ConnectionZones[,] empty = { { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
        //                                                        { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
        //                                                        { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None },
        //                                                        { ConnectionZones.None, ConnectionZones.None, ConnectionZones.None, ConnectionZones.None }};
    }
}
