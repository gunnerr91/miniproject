﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectEssential.EnumLib
{
    public enum UnitTypes
    {
        [Description("Inches & Pound")]
        InchesAndPound,

        [Description("Centimeter & KG")]
        CentimeterAndKilogram
    }

}
