﻿using System.Collections.Generic;

namespace Walle.Model
{
    public class WS2812B : EagleBoard.IPackage
    {
        private readonly IList<string> _pads = new[]
        {
            "1-VDD",
            "2-DOUT",
            "3-GND",
            "4-DIN"
        };

        public string PackageName
        {
            get { return "WS2812B"; }
        }

        public IList<string> Pads
        {
            get { return _pads; }
        }

        public string WiresXml()
        {
            return @"
<wire x1=""2.5"" y1=""-2.5"" x2=""-2.5"" y2=""-2.5"" width=""0.127"" layer=""21""/>
<wire x1=""-2.5"" y1=""-2.5"" x2=""-2.5"" y2=""1.6"" width=""0.127"" layer=""21""/>
<wire x1=""-2.5"" y1=""1.6"" x2=""-2.5"" y2=""2.5"" width=""0.127"" layer=""21""/>
<wire x1=""-2.5"" y1=""2.5"" x2=""-1.6"" y2=""2.5"" width=""0.127"" layer=""21""/>
<wire x1=""-1.6"" y1=""2.5"" x2=""2.5"" y2=""2.5"" width=""0.127"" layer=""21""/>
<wire x1=""2.5"" y1=""2.5"" x2=""2.5"" y2=""-2.5"" width=""0.127"" layer=""21""/>
<wire x1=""-2.5"" y1=""1.6"" x2=""-1.6"" y2=""2.5"" width=""0.127"" layer=""21""/>
<smd name=""1-VDD"" x=""2.45"" y=""-1.65"" dx=""1.5"" dy=""0.9"" layer=""1"" rot=""R180""/>
<smd name=""2-DOUT"" x=""2.45"" y=""1.65"" dx=""1.5"" dy=""0.9"" layer=""1"" rot=""R180""/>
<smd name=""3-GND"" x=""-2.45"" y=""1.65"" dx=""1.5"" dy=""0.9"" layer=""1"" rot=""R180""/>
<smd name=""4-DIN"" x=""-2.45"" y=""-1.65"" dx=""1.5"" dy=""0.9"" layer=""1"" rot=""R180""/>
<circle x=""0"" y=""0"" radius=""1.7204625"" width=""0.127"" layer=""21""/>
<text x=""3.4925"" y=""1.5875"" size=""0.8128"" layer=""25"" ratio=""10"" rot=""R270"">&gt;NAME</text>
<wire x1=""-1.6"" y1=""2.5"" x2=""-1.3"" y2=""2.8"" width=""0.127"" layer=""21""/>
<wire x1=""-1.3"" y1=""2.8"" x2=""-1.7"" y2=""3.2"" width=""0.127"" layer=""21""/>
<wire x1=""-1.7"" y1=""3.2"" x2=""-2.5"" y2=""2.5"" width=""0.127"" layer=""21""/>
";
        }
    }
}