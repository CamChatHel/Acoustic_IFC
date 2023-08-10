using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using static Xbim.WinformsSample.JsonJunctionElement;

namespace Xbim.WinformsSample
{
	public class GetJsonInformation
	{
        public string AusgabeCalculationResults(JsonJunctionElementCalculationResults convertedJsonJunction, DataGridView dgv)
        {
            string ausgabe = "";


            if (convertedJsonJunction.InSituResults != null)
            {
                ausgabe = "Trennelement: " + convertedJsonJunction.InSituResults.ToString() + "\r\n";

                ausgabe = "Berechnete Werte aus VBAcoustic: \r\n";
                ausgabe = ausgabe + "Rw= " + convertedJsonJunction.AcousticElementInSitu.SoundReductionIndexSingle.ToString() + " dB" + "\r\n";

                DataGridViewRow Test = new DataGridViewRow();
                Test.CreateCells(dgv);
                Test.Cells[0].Value = "Calculation Results";
                dgv.Rows.Add(Test);

                DataGridViewRow rRW = new DataGridViewRow();
                rRW.CreateCells(dgv);
                rRW.Cells[0].Value = "Rw";
                rRW.Cells[1].Value = convertedJsonJunction.AcousticElementInSitu.SoundReductionIndexSingle.ToString();
                for (int i = 0; i < 21; i++)
                {
                    try
                    {
                        rRW.Cells[i + 2].Value = convertedJsonJunction.AcousticElementInSitu.SoundReduction[i];
                    }
                    catch { }
                }
                dgv.Rows.Add(rRW);

                DataGridViewRow rLnW = new DataGridViewRow();
                rLnW.CreateCells(dgv);
                rLnW.Cells[0].Value = "Lnw";
                rLnW.Cells[1].Value = convertedJsonJunction.AcousticImpactInsitu.ImpactSoundSingle.ToString();
                for (int i = 0; i < 21; i++)
                {
                    try
                    {
                        rLnW.Cells[i + 2].Value = convertedJsonJunction.AcousticImpactInsitu.ImpactSound[i];
                    }
                    catch { }
                }
                dgv.Rows.Add(rLnW);

                if (convertedJsonJunction.AllJunctions[0] == null)
                {
                    ausgabe = "No Junctions in this file. \r\n";
                }
                else
                {
                    foreach (var item in convertedJsonJunction.AllJunctions)
                    {
                        ausgabe = ausgabe + "\r\n junctiontype: " + item.TypeOfJunction.ToString() + "\r\n";

                        
                       
                        foreach (var path in item.TransmissionPaths)
                        {
                            DataGridViewRow rKij = new DataGridViewRow();
                            rKij.CreateCells(dgv);
                            rKij.Cells[0].Value = "Kij";

                            ausgabe = ausgabe + "\r\n element i: " + path.Is_i + " , element j: " + path.Is_j + "\r\n";
                           // ausgabe = ausgabe + "\n Kij:\n";
                            //foreach (var value in path.AcousticPath.VibrationReductionIndex)
                            //{
                            //    ausgabe = ausgabe + value.ToString() + ";";
                                
                            //}

                            rKij.Cells[1].Value = item.TypeOfJunction.ToString() + ", path " + path.pathName.ToString();

                            for (int i = 0; i < 21; i++)
                            {
                                try
                                {
                                    rKij.Cells[i + 2].Value = path.AcousticPath.VibrationReductionIndex[i];
                                }
                                catch { }
                            }

                            dgv.Rows.Add(rKij);
                        }

                        
                    }
                }


            }


            return ausgabe;
        }

        public string AusgabeCalculationResults (JsonJunctionElementCalculationResults convertedJsonJunction)
        {
            string ausgabe = "";

            if (convertedJsonJunction.InSituResults != null)
            {
                ausgabe = "Trennelement: " + convertedJsonJunction.InSituResults.ToString() + "\n";

                ausgabe = "Berechnete Werte aus VBAcoustic: \n";
                ausgabe = ausgabe + "Rw= " + convertedJsonJunction.AcousticElementInSitu.SoundReductionIndexSingle.ToString() + " dB";
                
                if (convertedJsonJunction.AllJunctions[0] == null)
                {
                    ausgabe = "No Junctions in this file. \n";
                }
                else
                {
                    foreach (var item in convertedJsonJunction.AllJunctions)
                    {
                        ausgabe = ausgabe + "\n junctiontype: " + item.TypeOfJunction.ToString();
                        foreach (var path in item.TransmissionPaths)
                        {
                            ausgabe = ausgabe + "\nelement i: " + path.Is_i + " , element j: " + path.Is_j;
                            ausgabe = ausgabe + "\nKij:\n";
                            foreach (var value in path.AcousticPath.VibrationReductionIndex)
                            {
                                ausgabe = ausgabe + value.ToString() + ";";
                            }
                        }

                    }
                }
                

            }


            return ausgabe;
		}



        public string AusgabeInpuData(JsonJunctionElementInputData convertedJsonJunction)
        {
            string ausgabe = "";

            if (convertedJsonJunction.AllJunctions[0] == null)
            {
                ausgabe = "Keine Stoßstellen angegeben. \r\n";
            }
            else
            {
                ausgabe = "Trennelement: " + convertedJsonJunction.AllJunctions[0].SeparatingElementID.ToString() + "\r\n";


                if (convertedJsonJunction.AllJunctions[0] == null)
                {
                    ausgabe = "No Junctions in this file. \r\n";
                }
                else
                {
                    foreach (var item in convertedJsonJunction.AllJunctions)
                    {
                        ausgabe = ausgabe + "\r\n junctiontype: " + item.TypeOfJunction.ToString();

                        foreach (var el in item.BuildingElements)
                        {
                            ausgabe = ausgabe + "\r\n name: " + el.ElementName.ToString() + ", GUID: " + el.ElementID.ToString();

                        }

                        foreach (var path in item.TransmissionPaths)
                        {
                            ausgabe = ausgabe + "\r\n path name: " + path.pathName.ToString() + "element i: " + path.Is_i + " , element j: " + path.Is_j;

                        }

                    }
                }
            }




            return ausgabe;
        }
    }
}
