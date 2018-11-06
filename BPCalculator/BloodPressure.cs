using System;
using System.ComponentModel.DataAnnotations;

namespace BPCalculator
{
    // BP categories
    public enum BPCategory
    {
        [Display(Name="Low Blood Pressure")] Low,
        [Display(Name="Normal Blood Pressure")]  Normal,
        [Display(Name="Pre-High Blood Pressure")] PreHigh,
        [Display(Name ="High Blood Pressure")]  High,
        [Display(Name = "Unable to calculate Blood Pressure")] Invalid
    };

    public class BloodPressure
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        public const int LowBPDiastolicMax = 60;
        public const int LowBPSystolicMax = 90;
        public const int IdealBPDiastolicMax = 80;
        public const int IdealBPSystolicMax = 120;
        public const int PreHighBPDiastolicMax = 90;
        public const int PreHighBPSystolicMax = 140;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       // mmHG

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      // mmHG

        // calculate BP category
        public BPCategory Category
        {
            get
            {
                if (Systolic < LowBPSystolicMax && Diastolic < LowBPDiastolicMax)
                {
                    return BPCategory.Low;
                }
                else if (Systolic < IdealBPSystolicMax && Diastolic < IdealBPDiastolicMax)
                {
                    return BPCategory.Normal;
                }
                else if (Systolic < PreHighBPSystolicMax && Diastolic < PreHighBPDiastolicMax)
                {
                    return BPCategory.PreHigh;
                }
                else if (Systolic >= PreHighBPSystolicMax && Diastolic >= PreHighBPDiastolicMax)
                {
                    return BPCategory.High;
                }
                else
                {
                    return BPCategory.Invalid;
                }
            }
        }
    }
}
