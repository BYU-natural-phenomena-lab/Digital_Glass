﻿namespace Walle.Eagle
{
    public interface IDesignRules
    {
        string RulesXml { get; }
    }
    public class DefaultDesignRules : IDesignRules
    {
        public string RulesXml
        {
            get { return @"
<description language=""en"">
</description>
<param name=""layerSetup"" value=""(1*16)""/>
<param name=""mtCopper"" value=""0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm 0.035mm""/>
<param name=""mtIsolate"" value=""1.5mm 0.15mm 0.2mm 0.15mm 0.2mm 0.15mm 0.2mm 0.15mm 0.2mm 0.15mm 0.2mm 0.15mm 0.2mm 0.15mm 0.2mm""/>
<param name=""mdWireWire"" value=""8mil""/>
<param name=""mdWirePad"" value=""8mil""/>
<param name=""mdWireVia"" value=""8mil""/>
<param name=""mdPadPad"" value=""8mil""/>
<param name=""mdPadVia"" value=""8mil""/>
<param name=""mdViaVia"" value=""8mil""/>
<param name=""mdSmdPad"" value=""8mil""/>
<param name=""mdSmdVia"" value=""8mil""/>
<param name=""mdSmdSmd"" value=""8mil""/>
<param name=""mdViaViaSameLayer"" value=""8mil""/>
<param name=""mnLayersViaInSmd"" value=""2""/>
<param name=""mdCopperDimension"" value=""40mil""/>
<param name=""mdDrill"" value=""8mil""/>
<param name=""mdSmdStop"" value=""0mil""/>
<param name=""msWidth"" value=""10mil""/>
<param name=""msDrill"" value=""24mil""/>
<param name=""msMicroVia"" value=""9.99mm""/>
<param name=""msBlindViaRatio"" value=""0.5""/>
<param name=""rvPadTop"" value=""0.25""/>
<param name=""rvPadInner"" value=""0.25""/>
<param name=""rvPadBottom"" value=""0.25""/>
<param name=""rvViaOuter"" value=""0.25""/>
<param name=""rvViaInner"" value=""0.25""/>
<param name=""rvMicroViaOuter"" value=""0.25""/>
<param name=""rvMicroViaInner"" value=""0.25""/>
<param name=""rlMinPadTop"" value=""10mil""/>
<param name=""rlMaxPadTop"" value=""20mil""/>
<param name=""rlMinPadInner"" value=""10mil""/>
<param name=""rlMaxPadInner"" value=""20mil""/>
<param name=""rlMinPadBottom"" value=""10mil""/>
<param name=""rlMaxPadBottom"" value=""20mil""/>
<param name=""rlMinViaOuter"" value=""8mil""/>
<param name=""rlMaxViaOuter"" value=""20mil""/>
<param name=""rlMinViaInner"" value=""8mil""/>
<param name=""rlMaxViaInner"" value=""20mil""/>
<param name=""rlMinMicroViaOuter"" value=""4mil""/>
<param name=""rlMaxMicroViaOuter"" value=""20mil""/>
<param name=""rlMinMicroViaInner"" value=""4mil""/>
<param name=""rlMaxMicroViaInner"" value=""20mil""/>
<param name=""psTop"" value=""-1""/>
<param name=""psBottom"" value=""-1""/>
<param name=""psFirst"" value=""-1""/>
<param name=""psElongationLong"" value=""100""/>
<param name=""psElongationOffset"" value=""100""/>
<param name=""mvStopFrame"" value=""1""/>
<param name=""mvCreamFrame"" value=""0""/>
<param name=""mlMinStopFrame"" value=""4mil""/>
<param name=""mlMaxStopFrame"" value=""4mil""/>
<param name=""mlMinCreamFrame"" value=""0mil""/>
<param name=""mlMaxCreamFrame"" value=""0mil""/>
<param name=""mlViaStopLimit"" value=""0mil""/>
<param name=""srRoundness"" value=""0""/>
<param name=""srMinRoundness"" value=""0mil""/>
<param name=""srMaxRoundness"" value=""0mil""/>
<param name=""slThermalIsolate"" value=""10mil""/>
<param name=""slThermalsForVias"" value=""0""/>
<param name=""dpMaxLengthDifference"" value=""10mm""/>
<param name=""dpGapFactor"" value=""2.5""/>
<param name=""checkGrid"" value=""0""/>
<param name=""checkAngle"" value=""0""/>
<param name=""checkFont"" value=""1""/>
<param name=""checkRestrict"" value=""1""/>
<param name=""useDiameter"" value=""13""/>
<param name=""maxErrors"" value=""50""/>
"; }
        }
    }
}