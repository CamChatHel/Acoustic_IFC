using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace Prototype01
{
    class CheckWithCorrectFlankingElements
    {
        // Compare the flanking elements found by the programm with the correct Flanking element List found per hand
        //ListofFlankingelements

        public static List<string> CorrectFlankingElementsGUID()
        {
            List<string> GUIDbyHand = new List<string>();

            //List fuer Datei 14
            GUIDbyHand.Add("2tXOpXkTD4sA9X_M67B4Ws");
            GUIDbyHand.Add("3Gu$FppSz81v_$kmG_xEu0");
            GUIDbyHand.Add("1Ig6TXwTX2sh6E$2gwYZLe");

            GUIDbyHand.Add("0l9A8DkyD01Q5byZ$rT7Mr");

            GUIDbyHand.Add("0swLdnuPnEPRBPhAKBX9Bk");
            GUIDbyHand.Add("2$LaxO9$9AqfdG5iHP6VFs");

            GUIDbyHand.Add("0swLdnuPnEPRBPhAKBX9MS");
            GUIDbyHand.Add("3zj1mBwqfAdeKX2u8dRXtn");
            

            return GUIDbyHand;

        }

        public static List<string> ProgramFlankingElementsGUID(List<IIfcElement> ListOfElements)
        {
            List<string> GUIDbyProgram = new List<string>();

            foreach (IIfcElement element in ListOfElements)
            {
                GUIDbyProgram.Add(element.GlobalId.ToString());
 
            }

            return GUIDbyProgram;
        }

        public void CompareFlankingElements(List<IIfcElement> ListOfElements)
        {
            List<string> GUIDProgramm = new List<string>();
            GUIDProgramm = ProgramFlankingElementsGUID(ListOfElements);
            List<string> GUIDbyHand = new List<string>();
            GUIDbyHand = CorrectFlankingElementsGUID();

            List<string> CorrectElementinProgram = new List<string>();
            List<string> MissingElementinProgramm = new List<string>();
            List<string> WrongElementinProgramm = new List<string>();

            foreach (string programmElement in GUIDProgramm)
            {

                if (GUIDbyHand.Contains(programmElement))
                {
                    CorrectElementinProgram.Add(programmElement);
                }
                else WrongElementinProgramm.Add(programmElement);

            }

            foreach (string byHandElement in GUIDbyHand)
            {

                if (GUIDProgramm.Contains(byHandElement) == false)
                {
                    MissingElementinProgramm.Add(byHandElement);
                }
                
            }

            //Ausgabe
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" --------------------------------------- ");
            Console.WriteLine("Correct Elements in Program Flanking Elements:");
            CorrectElementinProgram.ForEach(p => Console.WriteLine(p));
            Console.WriteLine();
            Console.WriteLine("Wrong Elements in Program Flanking Elements:");
            WrongElementinProgramm.ForEach(p => Console.WriteLine(p));
            Console.WriteLine();
            Console.WriteLine("Missing Elements in Program Flanking Elements:");
            MissingElementinProgramm.ForEach(p => Console.WriteLine(p));
            Console.WriteLine();
            Console.WriteLine(" --------------------------------------- ");
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadKey();
        }

    }
}
