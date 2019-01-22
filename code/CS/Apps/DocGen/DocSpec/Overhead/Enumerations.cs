using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
    public enum DeType
    {
        NotSet = 0,
        Body,
        Bold,
        Border,
        Color,
        ContextualSpacing,
        Doc,
        DocMap,
        DocResource,
        FontSize,
        FontSizeComplexScript,
        Indentation,
        Italic,
        Justification,
        Languages,
        MapEntry,
        MapEntrySet,
        Margin,
        PageMargin,
        PageSize,
        Paragraph,
        ParagraphProperties,
        ParagraphStyleId,
        Run,
        RunFonts,
        RunProperties,
        Section,
        SectionProperties,
        Spacing,
        SpacingBetweenLines, 
        Style,
        StyleSet,
        Table,
        TableBorders,
        TableCell,
        TableCellBorders,
        TableCellMargin,
        TableCellMarginDefault,
        TableCellProperties,
        TableCellWidth,
        TableIndentation,
        TableLook,
        TableProperties,
        TableRow,
        TableRowHeight,
        TableStyle,
        TableWidth,
        Tabs,
        TabStop,
        Text,
        Underline
    }
    
    public enum StyleValues
    {
        Character,
        Paragraph,
        Table,
        Numbering,
        DocDefaults
    }

    public enum TableStyleValues
    {
        NotSet,
        TableGrid
    }

    public enum PropertyType
    {
        NotSet,
        SectionProperties,
        TableProperties,
        CellProperties
    }

    public enum UnitValues
    {
        NotSet,
        Auto
    }

    public enum TableWidthUnitValues
    {
        Nil = 0,
        Pct = 1,
        Dxa = 2,
        Auto = 3
    }

    public enum HeightRuleValues
    {
        Auto = 0,
        Exact,
        AtLeast
    }

    public enum PageOrientationValues
    {
        Portrait = 0,
        Landscape
    }

    public enum JustificationValues
    {
        Left,
        Start,
        Center,
        Right,
        End,
        Both,
        MediumKashida,
        Distribute,
        NumTab,
        HighKashida,
        LowKashida,
        ThaiDistribute
    }

    public enum ThemeColorValues
    {
        Dark1,
        Light1,
        Dark2,
        Light2,
        Accent1,
        Accent2,
        Accent3,
        Accent4,
        Accent5,
        Accent6,
        Hyperlink,
        FollowedHyperlink,
        None,
        Background1,
        Text1,
        Background2,
        Text2
    }

    public enum ThemeFontValues
    {
        Null = 0,
        MajorEastAsia,
        MajorBidi,
        MajorAscii,
        MajorHighAnsi,
        MinorEastAsia,
        MinorBidi,
        MinorAscii,
        MinorHighAnsi
    }

    public enum UnderlineValues
    {
        Single = 0,
        Words = 1,
        Double = 2, 
        Thick = 3,
        Dotted = 4,
        DottedHeavy = 5,
        Dash = 6,
        DashedHeavy = 7,
        DashLong = 8,
        DashLongHeavy = 9,
        DotDash = 10,
        DashDotHeavy = 11,
        DotDotDash = 12,
        DashDotDotHeavy = 13,
        Wave = 14,
        WavyHeavy = 15,
        WavyDouble = 16,
        None = 17
    }

    public enum TabStopValues
    {
        Clear = 0,
        Left = 1,
        Start = 2,
        Center = 3,
        Right = 4,
        End = 5,
        Decimal = 6,
        Bar = 7,
        Number = 8
    }

    public enum TabStopLeaderCharValues
    {
        None = 0,
        Dot = 1,
        Hyphen = 2,
        Underscore = 3,
        Heavy = 4,
        MiddleDot = 5
    }

    public enum FontTypeHintValues
    {
        Null = 0,
        Default,
        EastAsia,
        ComplexScript
    }

    public enum LineSpacingRuleValues
    {
        Auto,
        Exact,
        AtLeast
    }

    public enum TypeConversionDirection
    {
        AdsdiToOpenXml,
        OpenXmlToAdsdi        
    }

    public enum CommonUnits
    {
        Pct,
        Emu,
        Dxa,
        Pixels,
        Inches
    }

    public enum SizeControl
    {
        Auto,
        Nil,
        AtLeast,
        Exact
    }

    public enum SpecUnitDimension
    {
        Width = 0,
        Height,
        NotSet
    }

    public enum DrawingMode
    {
        FullDocument = 0,
        DocumentPortion
    }

}
