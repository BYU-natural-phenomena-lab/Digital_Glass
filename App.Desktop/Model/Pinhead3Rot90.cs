using System.Collections.Generic;

namespace Walle.Model
{
    public class Pinhead3Rot90 : EagleBoard.IPackage
    {
        private readonly IList<string> _pads = new[]
        {
            "1",
            "2",
            "3",
        };

        public string PackageName
        {
            get { return "1X03/90"; }
        }

        public IList<string> Pads
        {
            get { return _pads; }
        }

        public string WiresXml()
        {
            return @"
<wire x1=""-3.81"" y1=""-1.905"" x2=""-1.27"" y2=""-1.905"" width=""0.1524"" layer=""21""/>
<wire x1=""-1.27"" y1=""-1.905"" x2=""-1.27"" y2=""0.635"" width=""0.1524"" layer=""21""/>
<wire x1=""-1.27"" y1=""0.635"" x2=""-3.81"" y2=""0.635"" width=""0.1524"" layer=""21""/>
<wire x1=""-3.81"" y1=""0.635"" x2=""-3.81"" y2=""-1.905"" width=""0.1524"" layer=""21""/>
<wire x1=""-2.54"" y1=""6.985"" x2=""-2.54"" y2=""1.27"" width=""0.762"" layer=""21""/>
<wire x1=""-1.27"" y1=""-1.905"" x2=""1.27"" y2=""-1.905"" width=""0.1524"" layer=""21""/>
<wire x1=""1.27"" y1=""-1.905"" x2=""1.27"" y2=""0.635"" width=""0.1524"" layer=""21""/>
<wire x1=""1.27"" y1=""0.635"" x2=""-1.27"" y2=""0.635"" width=""0.1524"" layer=""21""/>
<wire x1=""0"" y1=""6.985"" x2=""0"" y2=""1.27"" width=""0.762"" layer=""21""/>
<wire x1=""1.27"" y1=""-1.905"" x2=""3.81"" y2=""-1.905"" width=""0.1524"" layer=""21""/>
<wire x1=""3.81"" y1=""-1.905"" x2=""3.81"" y2=""0.635"" width=""0.1524"" layer=""21""/>
<wire x1=""3.81"" y1=""0.635"" x2=""1.27"" y2=""0.635"" width=""0.1524"" layer=""21""/>
<wire x1=""2.54"" y1=""6.985"" x2=""2.54"" y2=""1.27"" width=""0.762"" layer=""21""/>
<pad name=""1"" x=""-2.54"" y=""-3.81"" drill=""1.016"" shape=""long"" rot=""R90""/>
<pad name=""2"" x=""0"" y=""-3.81"" drill=""1.016"" shape=""long"" rot=""R90""/>
<pad name=""3"" x=""2.54"" y=""-3.81"" drill=""1.016"" shape=""long"" rot=""R90""/>
<text x=""-4.445"" y=""-3.81"" size=""1.27"" layer=""25"" ratio=""10"" rot=""R90"">&gt;NAME</text>
<text x=""5.715"" y=""-3.81"" size=""1.27"" layer=""27"" rot=""R90"">&gt;VALUE</text>
<rectangle x1=""-2.921"" y1=""0.635"" x2=""-2.159"" y2=""1.143"" layer=""21""/>
<rectangle x1=""-0.381"" y1=""0.635"" x2=""0.381"" y2=""1.143"" layer=""21""/>
<rectangle x1=""2.159"" y1=""0.635"" x2=""2.921"" y2=""1.143"" layer=""21""/>
<rectangle x1=""-2.921"" y1=""-2.921"" x2=""-2.159"" y2=""-1.905"" layer=""21""/>
<rectangle x1=""-0.381"" y1=""-2.921"" x2=""0.381"" y2=""-1.905"" layer=""21""/>
<rectangle x1=""2.159"" y1=""-2.921"" x2=""2.921"" y2=""-1.905"" layer=""21""/>
";
        }
    }
}