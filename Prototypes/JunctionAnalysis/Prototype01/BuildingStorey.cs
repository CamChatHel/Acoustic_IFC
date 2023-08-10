using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype01
{
    public class BuildingStorey : IComparable<BuildingStorey>
    {
        // Own Class of Sotrey to be able to sort the storey by Elevation
        public int EntityLabel { get; set; }
        public double Elevation { get; set; }
                              
            //damit binarySearch funktioniert
            public int CompareTo(BuildingStorey other)
            {
                return this.Elevation.CompareTo(other.Elevation);
            }


            public override bool Equals(object obj)
            {
                //definiert wann Objekte gleich sind
                //vergleicht die Stockwerkhoehe

                if (obj is BuildingStorey)
                {
                    BuildingStorey other = (BuildingStorey)obj;
                    return this.EntityLabel == other.EntityLabel;
                }
                return false;
            }
    }

}
