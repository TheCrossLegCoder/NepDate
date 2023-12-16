using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NepDate.Core.Enums
{
    public enum Separators
    {

        [Description("2081/01/01")]
        ForwardSlash = 0,

        [Description("2081\\01\\01")]
        BackwardSlash = 1,

        [Description("2081.01.01")]
        Dot = 2,

        [Description("2081_01_01")]
        Underscore = 3,

        [Description("2081-01-01")]
        Dash = 4,

        [Description("2081 01 01")]
        Space = 5
    }
}
