using System.ComponentModel;

namespace NepDate.Core.Enums
{
    public enum Separators
    {
        /// <summary>
        /// Probable output example : 2081/01/01
        /// </summary>
        [Description("2081/01/01")]
        ForwardSlash = 0,


        /// <summary>
        /// Probable output example : 2081\01\01
        /// </summary>
        [Description("2081\\01\\01")]
        BackwardSlash = 1,


        /// <summary>
        /// Probable output example : 2081.01.01
        /// </summary>
        [Description("2081.01.01")]
        Dot = 2,


        /// <summary>
        /// Probable output example : 2081_01_01
        /// </summary>
        [Description("2081_01_01")]
        Underscore = 3,


        /// <summary>
        /// Probable output example : 2081-01-01
        /// </summary>
        [Description("2081-01-01")]
        Dash = 4,

        /// <summary>
        /// Probable output example : 2081 01 01
        /// </summary>
        [Description("2081 01 01")]
        Space = 5
    }
}
