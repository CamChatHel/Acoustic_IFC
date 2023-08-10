using PropertyTools.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Xbim.WinformsSample
{
    public class JsonJunctionElement
    {

        public class JsonJunctionElementInputData //JSON-File with Input data BEFORE calculation in VBAcoustic!
        {

            public List<JsonJunction> AllJunctions { get; set; } = new List<JsonJunction>();



        }

        public class JsonJunctionElementCalculationResults //JSON-File with REsults AFTER calculation in VBAcoustic!
        {

            public string InSituResults { get; set; } //ID Trennelement bei InsituResults
            public JsonAcousticElementInSitu AcousticElementInSitu { get; set; }
            
            public JsonAcousticImpactInSitu AcousticImpactInsitu { get; set; } 
            
            public List<JsonJunction> AllJunctions { get; set; } = new List<JsonJunction>();



        }

        public class JsonAcousticElementInSitu
        {
            public double SoundReductionIndexSingle { get; set; }

            public double[] SoundReduction = new double[21];

            public string Origin { get; set; }
        }

        public class JsonAcousticImpactInSitu
        {
            public double ImpactSoundSingle { get; set; }

            public double[] ImpactSound = new double[21];

            public string Origin { get; set; }
        }

        public class JsonJunction
        {

            public string JunctionID { get; set; }
            public string SeparatingElementID { get; set; }

            public double? CommonLength { get; set; }
            public string TypeOfJunction { get; set; }
            public string TypeOfFastener { get; set; }

            public List<JsonFlankingElement> BuildingElements { get; set; } = new List<JsonFlankingElement>();

            public List<JsonTransmissionsPath> TransmissionPaths { get; set; } = new List<JsonTransmissionsPath>();


        }

        public class JsonFlankingElement
        {
            public string ElementID { get; set; }
            public string ElementName { get; set; }
            public string ElementMaterial { get; set; }

            public JsonCovering Covering1 { get; set; }
            public JsonCovering Covering2 { get; set; }

            public JsonAcousticElement AcousticElement { get; set; }
            public JsonAcousticImpactElement AcousticImpact  { get; set; }

        }

        public class JsonCovering
        {
            public string CoveringID { get; set; }
            public string CoveringMaterial { get; set; }

            public double SoundReductionImprovementSingle { get; set; }
            public double[] SoundReductionImprovement = new double[21];
            public double ImpactSoundImprovementSingle { get; set; }
            public double[] ImpactSoundImprovement = new double[21];
            public string Origin { get; set; }
        }

        public class JsonAcousticElement
        {
            public double SoundReductionSingle { get; set; }
            public double[] SoundReduction = new double[21];
            public double SurfaceRelatedMass { get; set; }
            public double Eigenfrequency { get; set; }
            public string Origin { get; set; }
        }

        public class JsonAcousticImpactElement
        {
            public double ImpactSoundSingle { get; set; }
            public double[] ImpactSound = new double[21];
            public string Origin { get; set; }
        }

        public class JsonTransmissionsPath
        {

            public string GlobalId { get; set; }

            public string pathName;

            public string Is_i { get; set; }
            public string Is_j { get; set; }

            public JsonAcousticPath AcousticPath { get; set; }


        }

        public class JsonAcousticPath
        {
            public double[] VibrationReductionIndex = new double[21];

            public double[] DirectionAvrVelocityLevel = new double[21];

            public double[] FlankingSoundReduction = new double[21];

            public double[] FlankingImpactSound = new double[21];

            public string Origin { get; set; }
        }

    }
}
