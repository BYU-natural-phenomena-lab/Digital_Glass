using System.Xml.Linq;

namespace Walle.Eagle
{
    public interface IDesignRules
    {
        XElement RulesXml { get; }
    }
    public class DefaultDesignRules : IDesignRules
    {
        public XElement RulesXml
        {
            get { return new XElement("designrules", new XAttribute("name", "default"),
new XElement("param", new XAttribute("name","layerSetup"), new XAttribute("value","(1*16)")),
new XElement("param", new XAttribute("name","mtCopper"), new XAttribute("value","0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm")),
new XElement("param", new XAttribute("name","mtIsolate"), new XAttribute("value","1.5mm 0.15mm 0.2mm 0.15mm 0.2mm 0.15mm 0.2mm 0.15mm 0.2mm 0.15mm 0.2mm 0.15mm 0.2mm 0.15mm 0.2mm")),
new XElement("param", new XAttribute("name","mdWireWire"), new XAttribute("value","8mil")),
new XElement("param", new XAttribute("name","mdWirePad"), new XAttribute("value","8mil")),
new XElement("param", new XAttribute("name","mdWireVia"), new XAttribute("value","8mil")),
new XElement("param", new XAttribute("name","mdPadPad"), new XAttribute("value","8mil")),
new XElement("param", new XAttribute("name","mdPadVia"), new XAttribute("value","8mil")),
new XElement("param", new XAttribute("name","mdViaVia"), new XAttribute("value","8mil")),
new XElement("param", new XAttribute("name","mdSmdPad"), new XAttribute("value","8mil")),
new XElement("param", new XAttribute("name","mdSmdVia"), new XAttribute("value","8mil")),
new XElement("param", new XAttribute("name","mdSmdSmd"), new XAttribute("value","8mil")),
new XElement("param", new XAttribute("name","mdViaViaSameLayer"), new XAttribute("value","8mil")),
new XElement("param", new XAttribute("name","mnLayersViaInSmd"), new XAttribute("value","2")),
new XElement("param", new XAttribute("name","mdCopperDimension"), new XAttribute("value","40mil")),
new XElement("param", new XAttribute("name","mdDrill"), new XAttribute("value","8mil")),
new XElement("param", new XAttribute("name","mdSmdStop"), new XAttribute("value","0mil")),
new XElement("param", new XAttribute("name","msWidth"), new XAttribute("value","10mil")),
new XElement("param", new XAttribute("name","msDrill"), new XAttribute("value","24mil")),
new XElement("param", new XAttribute("name","msMicroVia"), new XAttribute("value","9.99mm")),
new XElement("param", new XAttribute("name","msBlindViaRatio"), new XAttribute("value","0.5")),
new XElement("param", new XAttribute("name","rvPadTop"), new XAttribute("value","0.25")),
new XElement("param", new XAttribute("name","rvPadInner"), new XAttribute("value","0.25")),
new XElement("param", new XAttribute("name","rvPadBottom"), new XAttribute("value","0.25")),
new XElement("param", new XAttribute("name","rvViaOuter"), new XAttribute("value","0.25")),
new XElement("param", new XAttribute("name","rvViaInner"), new XAttribute("value","0.25")),
new XElement("param", new XAttribute("name","rvMicroViaOuter"), new XAttribute("value","0.25")),
new XElement("param", new XAttribute("name","rvMicroViaInner"), new XAttribute("value","0.25")),
new XElement("param", new XAttribute("name","rlMinPadTop"), new XAttribute("value","10mil")),
new XElement("param", new XAttribute("name","rlMaxPadTop"), new XAttribute("value","20mil")),
new XElement("param", new XAttribute("name","rlMinPadInner"), new XAttribute("value","10mil")),
new XElement("param", new XAttribute("name","rlMaxPadInner"), new XAttribute("value","20mil")),
new XElement("param", new XAttribute("name","rlMinPadBottom"), new XAttribute("value","10mil")),
new XElement("param", new XAttribute("name","rlMaxPadBottom"), new XAttribute("value","20mil")),
new XElement("param", new XAttribute("name","rlMinViaOuter"), new XAttribute("value","8mil")),
new XElement("param", new XAttribute("name","rlMaxViaOuter"), new XAttribute("value","20mil")),
new XElement("param", new XAttribute("name","rlMinViaInner"), new XAttribute("value","8mil")),
new XElement("param", new XAttribute("name","rlMaxViaInner"), new XAttribute("value","20mil")),
new XElement("param", new XAttribute("name","rlMinMicroViaOuter"), new XAttribute("value","4mil")),
new XElement("param", new XAttribute("name","rlMaxMicroViaOuter"), new XAttribute("value","20mil")),
new XElement("param", new XAttribute("name","rlMinMicroViaInner"), new XAttribute("value","4mil")),
new XElement("param", new XAttribute("name","rlMaxMicroViaInner"), new XAttribute("value","20mil")),
new XElement("param", new XAttribute("name","psTop"), new XAttribute("value","-1")),
new XElement("param", new XAttribute("name","psBottom"), new XAttribute("value","-1")),
new XElement("param", new XAttribute("name","psFirst"), new XAttribute("value","-1")),
new XElement("param", new XAttribute("name","psElongationLong"), new XAttribute("value","100")),
new XElement("param", new XAttribute("name","psElongationOffset"), new XAttribute("value","100")),
new XElement("param", new XAttribute("name","mvStopFrame"), new XAttribute("value","1")),
new XElement("param", new XAttribute("name","mvCreamFrame"), new XAttribute("value","0")),
new XElement("param", new XAttribute("name","mlMinStopFrame"), new XAttribute("value","4mil")),
new XElement("param", new XAttribute("name","mlMaxStopFrame"), new XAttribute("value","4mil")),
new XElement("param", new XAttribute("name","mlMinCreamFrame"), new XAttribute("value","0mil")),
new XElement("param", new XAttribute("name","mlMaxCreamFrame"), new XAttribute("value","0mil")),
new XElement("param", new XAttribute("name","mlViaStopLimit"), new XAttribute("value","0mil")),
new XElement("param", new XAttribute("name","srRoundness"), new XAttribute("value","0")),
new XElement("param", new XAttribute("name","srMinRoundness"), new XAttribute("value","0mil")),
new XElement("param", new XAttribute("name","srMaxRoundness"), new XAttribute("value","0mil")),
new XElement("param", new XAttribute("name","slThermalIsolate"), new XAttribute("value","10mil")),
new XElement("param", new XAttribute("name","slThermalsForVias"), new XAttribute("value","0")),
new XElement("param", new XAttribute("name","dpMaxLengthDifference"), new XAttribute("value","10mm")),
new XElement("param", new XAttribute("name","dpGapFactor"), new XAttribute("value","2.5")),
new XElement("param", new XAttribute("name","checkGrid"), new XAttribute("value","0")),
new XElement("param", new XAttribute("name","checkAngle"), new XAttribute("value","0")),
new XElement("param", new XAttribute("name","checkFont"), new XAttribute("value","1")),
new XElement("param", new XAttribute("name","checkRestrict"), new XAttribute("value","1")),
new XElement("param", new XAttribute("name","useDiameter"), new XAttribute("value","13")),
new XElement("param", new XAttribute("name","maxErrors"), new XAttribute("value","50"))); }
        }
    }
}