using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
    public enum BorderValues
    {
        // Summary:
        //     No Border.
        //     When the item is serialized out as xml, its value is "nil".
        [EnumString("nil")]
        Nil = 0,
        //
        // Summary:
        //     No Border.
        //     When the item is serialized out as xml, its value is "none".
        [EnumString("none")]
        None = 1,
        //
        // Summary:
        //     Single Line Border.
        //     When the item is serialized out as xml, its value is "single".
        [EnumString("single")]
        Single = 2,
        //
        // Summary:
        //     Single Line Border.
        //     When the item is serialized out as xml, its value is "thick".
        [EnumString("thick")]
        Thick = 3,
        //
        // Summary:
        //     Double Line Border.
        //     When the item is serialized out as xml, its value is "double".
        [EnumString("double")]
        Double = 4,
        //
        // Summary:
        //     Dotted Line Border.
        //     When the item is serialized out as xml, its value is "dotted".
        [EnumString("dotted")]
        Dotted = 5,
        //
        // Summary:
        //     Dashed Line Border.
        //     When the item is serialized out as xml, its value is "dashed".
        [EnumString("dashed")]
        Dashed = 6,
        //
        // Summary:
        //     Dot Dash Line Border.
        //     When the item is serialized out as xml, its value is "dotDash".
        [EnumString("dotDash")]
        DotDash = 7,
        //
        // Summary:
        //     Dot Dot Dash Line Border.
        //     When the item is serialized out as xml, its value is "dotDotDash".
        [EnumString("dotDotDash")]
        DotDotDash = 8,
        //
        // Summary:
        //     Triple Line Border.
        //     When the item is serialized out as xml, its value is "triple".
        [EnumString("triple")]
        Triple = 9,
        //
        // Summary:
        //     Thin, Thick Line Border.
        //     When the item is serialized out as xml, its value is "thinThickSmallGap".
        [EnumString("thinThickSmallGap")]
        ThinThickSmallGap = 10,
        //
        // Summary:
        //     Thick, Thin Line Border.
        //     When the item is serialized out as xml, its value is "thickThinSmallGap".
        [EnumString("thickThinSmallGap")]
        ThickThinSmallGap = 11,
        //
        // Summary:
        //     Thin, Thick, Thin Line Border.
        //     When the item is serialized out as xml, its value is "thinThickThinSmallGap".
        [EnumString("thinThickThinSmallGap")]
        ThinThickThinSmallGap = 12,
        //
        // Summary:
        //     Thin, Thick Line Border.
        //     When the item is serialized out as xml, its value is "thinThickMediumGap".
        [EnumString("thinThickMediumGap")]
        ThinThickMediumGap = 13,
        //
        // Summary:
        //     Thick, Thin Line Border.
        //     When the item is serialized out as xml, its value is "thickThinMediumGap".
        [EnumString("thickThinMediumGap")]
        ThickThinMediumGap = 14,
        //
        // Summary:
        //     Thin, Thick, Thin Line Border.
        //     When the item is serialized out as xml, its value is "thinThickThinMediumGap".
        [EnumString("thinThickThinMediumGap")]
        ThinThickThinMediumGap = 15,
        //
        // Summary:
        //     Thin, Thick Line Border.
        //     When the item is serialized out as xml, its value is "thinThickLargeGap".
        [EnumString("thinThickLargeGap")]
        ThinThickLargeGap = 16,
        //
        // Summary:
        //     Thick, Thin Line Border.
        //     When the item is serialized out as xml, its value is "thickThinLargeGap".
        [EnumString("thickThinLargeGap")]
        ThickThinLargeGap = 17,
        //
        // Summary:
        //     Thin, Thick, Thin Line Border.
        //     When the item is serialized out as xml, its value is "thinThickThinLargeGap".
        [EnumString("thinThickThinLargeGap")]
        ThinThickThinLargeGap = 18,
        //
        // Summary:
        //     Wavy Line Border.
        //     When the item is serialized out as xml, its value is "wave".
        [EnumString("wave")]
        Wave = 19,
        //
        // Summary:
        //     Double Wave Line Border.
        //     When the item is serialized out as xml, its value is "doubleWave".
        [EnumString("doubleWave")]
        DoubleWave = 20,
        //
        // Summary:
        //     Dashed Line Border.
        //     When the item is serialized out as xml, its value is "dashSmallGap".
        [EnumString("dashSmallGap")]
        DashSmallGap = 21,
        //
        // Summary:
        //     Dash Dot Strokes Line Border.
        //     When the item is serialized out as xml, its value is "dashDotStroked".
        [EnumString("dashDotStroked")]
        DashDotStroked = 22,
        //
        // Summary:
        //     3D Embossed Line Border.
        //     When the item is serialized out as xml, its value is "threeDEmboss".
        [EnumString("threeDEmboss")]
        ThreeDEmboss = 23,
        //
        // Summary:
        //     3D Engraved Line Border.
        //     When the item is serialized out as xml, its value is "threeDEngrave".
        [EnumString("threeDEngrave")]
        ThreeDEngrave = 24,
        //
        // Summary:
        //     Outset Line Border.
        //     When the item is serialized out as xml, its value is "outset".
        [EnumString("outset")]
        Outset = 25,
        //
        // Summary:
        //     Inset Line Border.
        //     When the item is serialized out as xml, its value is "inset".
        [EnumString("inset")]
        Inset = 26,
        //
        // Summary:
        //     Apples Art Border.
        //     When the item is serialized out as xml, its value is "apples".
        [EnumString("apples")]
        Apples = 27,
        //
        // Summary:
        //     Arched Scallops Art Border.
        //     When the item is serialized out as xml, its value is "archedScallops".
        [EnumString("archedScallops")]
        ArchedScallops = 28,
        //
        // Summary:
        //     Baby Pacifier Art Border.
        //     When the item is serialized out as xml, its value is "babyPacifier".
        [EnumString("babyPacifier")]
        BabyPacifier = 29,
        //
        // Summary:
        //     Baby Rattle Art Border.
        //     When the item is serialized out as xml, its value is "babyRattle".
        [EnumString("babyRattle")]
        BabyRattle = 30,
        //
        // Summary:
        //     Three Color Balloons Art Border.
        //     When the item is serialized out as xml, its value is "balloons3Colors".
        [EnumString("balloons3Colors")]
        Balloons3Colors = 31,
        //
        // Summary:
        //     Hot Air Balloons Art Border.
        //     When the item is serialized out as xml, its value is "balloonsHotAir".
        [EnumString("balloonsHotAir")]
        BalloonsHotAir = 32,
        //
        // Summary:
        //     Black Dash Art Border.
        //     When the item is serialized out as xml, its value is "basicBlackDashes".
        [EnumString("basicBlackDashes")]
        BasicBlackDashes = 33,
        //
        // Summary:
        //     Black Dot Art Border.
        //     When the item is serialized out as xml, its value is "basicBlackDots".
        [EnumString("basicBlackDots")]
        BasicBlackDots = 34,
        //
        // Summary:
        //     Black Square Art Border.
        //     When the item is serialized out as xml, its value is "basicBlackSquares".
        [EnumString("basicBlackSquares")]
        BasicBlackSquares = 35,
        //
        // Summary:
        //     Thin Line Art Border.
        //     When the item is serialized out as xml, its value is "basicThinLines".
        [EnumString("basicThinLines")]
        BasicThinLines = 36,
        //
        // Summary:
        //     White Dash Art Border.
        //     When the item is serialized out as xml, its value is "basicWhiteDashes".
        [EnumString("basicWhiteDashes")]
        BasicWhiteDashes = 37,
        //
        // Summary:
        //     White Dot Art Border.
        //     When the item is serialized out as xml, its value is "basicWhiteDots".
        [EnumString("basicWhiteDots")]
        BasicWhiteDots = 38,
        //
        // Summary:
        //     White Square Art Border.
        //     When the item is serialized out as xml, its value is "basicWhiteSquares".
        [EnumString("basicWhiteSquares")]
        BasicWhiteSquares = 39,
        //
        // Summary:
        //     Wide Inline Art Border.
        //     When the item is serialized out as xml, its value is "basicWideInline".
        [EnumString("basicWideInline")]
        BasicWideInline = 40,
        //
        // Summary:
        //     Wide Midline Art Border.
        //     When the item is serialized out as xml, its value is "basicWideMidline".
        [EnumString("basicWideMidline")]
        BasicWideMidline = 41,
        //
        // Summary:
        //     Wide Outline Art Border.
        //     When the item is serialized out as xml, its value is "basicWideOutline".
        [EnumString("basicWideOutline")]
        BasicWideOutline = 42,
        //
        // Summary:
        //     Bats Art Border.
        //     When the item is serialized out as xml, its value is "bats".
        [EnumString("bats")]
        Bats = 43,
        //
        // Summary:
        //     Birds Art Border.
        //     When the item is serialized out as xml, its value is "birds".
        [EnumString("birds")]
        Birds = 44,
        //
        // Summary:
        //     Birds Flying Art Border.
        //     When the item is serialized out as xml, its value is "birdsFlight".
        [EnumString("birdsFlight")]
        BirdsFlight = 45,
        //
        // Summary:
        //     Cabin Art Border.
        //     When the item is serialized out as xml, its value is "cabins".
        [EnumString("cabins")]
        Cabins = 46,
        //
        // Summary:
        //     Cake Art Border.
        //     When the item is serialized out as xml, its value is "cakeSlice".
        [EnumString("cakeSlice")]
        CakeSlice = 47,
        //
        // Summary:
        //     Candy Corn Art Border.
        //     When the item is serialized out as xml, its value is "candyCorn".
        [EnumString("candyCorn")]
        CandyCorn = 48,
        //
        // Summary:
        //     Knot Work Art Border.
        //     When the item is serialized out as xml, its value is "celticKnotwork".
        [EnumString("celticKnotwork")]
        CelticKnotwork = 49,
        //
        // Summary:
        //     Certificate Banner Art Border.
        //     When the item is serialized out as xml, its value is "certificateBanner".
        [EnumString("certificateBanner")]
        CertificateBanner = 50,
        //
        // Summary:
        //     Chain Link Art Border.
        //     When the item is serialized out as xml, its value is "chainLink".
        [EnumString("chainLink")]
        ChainLink = 51,
        //
        // Summary:
        //     Champagne Bottle Art Border.
        //     When the item is serialized out as xml, its value is "champagneBottle".
        [EnumString("champagneBottle")]
        ChampagneBottle = 52,
        //
        // Summary:
        //     Black and White Bar Art Border.
        //     When the item is serialized out as xml, its value is "checkedBarBlack".
        [EnumString("checkedBarBlack")]
        CheckedBarBlack = 53,
        //
        // Summary:
        //     Color Checked Bar Art Border.
        //     When the item is serialized out as xml, its value is "checkedBarColor".
        [EnumString("checkedBarColor")]
        CheckedBarColor = 54,
        //
        // Summary:
        //     Checkerboard Art Border.
        //     When the item is serialized out as xml, its value is "checkered".
        [EnumString("checkered")]
        Checkered = 55,
        //
        // Summary:
        //     Christmas Tree Art Border.
        //     When the item is serialized out as xml, its value is "christmasTree".
        [EnumString("christmasTree")]
        ChristmasTree = 56,
        //
        // Summary:
        //     Circles And Lines Art Border.
        //     When the item is serialized out as xml, its value is "circlesLines".
        [EnumString("circlesLines")]
        CirclesLines = 57,
        //
        // Summary:
        //     Circles and Rectangles Art Border.
        //     When the item is serialized out as xml, its value is "circlesRectangles".
        [EnumString("circlesRectangles")]
        CirclesRectangles = 58,
        //
        // Summary:
        //     Wave Art Border.
        //     When the item is serialized out as xml, its value is "classicalWave".
        [EnumString("classicalWave")]
        ClassicalWave = 59,
        //
        // Summary:
        //     Clocks Art Border.
        //     When the item is serialized out as xml, its value is "clocks".
        [EnumString("clocks")]
        Clocks = 60,
        //
        // Summary:
        //     Compass Art Border.
        //     When the item is serialized out as xml, its value is "compass".
        [EnumString("compass")]
        Compass = 61,
        //
        // Summary:
        //     Confetti Art Border.
        //     When the item is serialized out as xml, its value is "confetti".
        [EnumString("confetti")]
        Confetti = 62,
        //
        // Summary:
        //     Confetti Art Border.
        //     When the item is serialized out as xml, its value is "confettiGrays".
        [EnumString("confettiGrays")]
        ConfettiGrays = 63,
        //
        // Summary:
        //     Confetti Art Border.
        //     When the item is serialized out as xml, its value is "confettiOutline".
        [EnumString("confettiOutline")]
        ConfettiOutline = 64,
        //
        // Summary:
        //     Confetti Streamers Art Border.
        //     When the item is serialized out as xml, its value is "confettiStreamers".
        [EnumString("confettiStreamers")]
        ConfettiStreamers = 65,
        //
        // Summary:
        //     Confetti Art Border.
        //     When the item is serialized out as xml, its value is "confettiWhite".
        [EnumString("confettiWhite")]
        ConfettiWhite = 66,
        //
        // Summary:
        //     Corner Triangle Art Border.
        //     When the item is serialized out as xml, its value is "cornerTriangles".
        [EnumString("cornerTriangles")]
        CornerTriangles = 67,
        //
        // Summary:
        //     Dashed Line Art Border.
        //     When the item is serialized out as xml, its value is "couponCutoutDashes".
        [EnumString("couponCutoutDashes")]
        CouponCutoutDashes = 68,
        //
        // Summary:
        //     Dotted Line Art Border.
        //     When the item is serialized out as xml, its value is "couponCutoutDots".
        [EnumString("couponCutoutDots")]
        CouponCutoutDots = 69,
        //
        // Summary:
        //     Maze Art Border.
        //     When the item is serialized out as xml, its value is "crazyMaze".
        [EnumString("crazyMaze")]
        CrazyMaze = 70,
        //
        // Summary:
        //     Butterfly Art Border.
        //     When the item is serialized out as xml, its value is "creaturesButterfly".
        [EnumString("creaturesButterfly")]
        CreaturesButterfly = 71,
        //
        // Summary:
        //     Fish Art Border.
        //     When the item is serialized out as xml, its value is "creaturesFish".
        [EnumString("creaturesFish")]
        CreaturesFish = 72,
        //
        // Summary:
        //     Insects Art Border.
        //     When the item is serialized out as xml, its value is "creaturesInsects".
        [EnumString("creaturesInsects")]
        CreaturesInsects = 73,
        //
        // Summary:
        //     Ladybug Art Border.
        //     When the item is serialized out as xml, its value is "creaturesLadyBug".
        [EnumString("creaturesLadyBug")]
        CreaturesLadyBug = 74,
        //
        // Summary:
        //     Cross-stitch Art Border.
        //     When the item is serialized out as xml, its value is "crossStitch".
        [EnumString("crossStitch")]
        CrossStitch = 75,
        //
        // Summary:
        //     Cupid Art Border.
        //     When the item is serialized out as xml, its value is "cup".
        [EnumString("cup")]
        Cup = 76,
        //
        // Summary:
        //     Archway Art Border.
        //     When the item is serialized out as xml, its value is "decoArch".
        [EnumString("decoArch")]
        DecoArch = 77,
        //
        // Summary:
        //     Color Archway Art Border.
        //     When the item is serialized out as xml, its value is "decoArchColor".
        [EnumString("decoArchColor")]
        DecoArchColor = 78,
        //
        // Summary:
        //     Blocks Art Border.
        //     When the item is serialized out as xml, its value is "decoBlocks".
        [EnumString("decoBlocks")]
        DecoBlocks = 79,
        //
        // Summary:
        //     Gray Diamond Art Border.
        //     When the item is serialized out as xml, its value is "diamondsGray".
        [EnumString("diamondsGray")]
        DiamondsGray = 80,
        //
        // Summary:
        //     Double D Art Border.
        //     When the item is serialized out as xml, its value is "doubleD".
        [EnumString("doubleD")]
        DoubleD = 81,
        //
        // Summary:
        //     Diamond Art Border.
        //     When the item is serialized out as xml, its value is "doubleDiamonds".
        [EnumString("doubleDiamonds")]
        DoubleDiamonds = 82,
        //
        // Summary:
        //     Earth Art Border.
        //     When the item is serialized out as xml, its value is "earth1".
        [EnumString("earth1")]
        Earth1 = 83,
        //
        // Summary:
        //     Earth Art Border.
        //     When the item is serialized out as xml, its value is "earth2".
        [EnumString("earth2")]
        Earth2 = 84,
        //
        // Summary:
        //     Shadowed Square Art Border.
        //     When the item is serialized out as xml, its value is "eclipsingSquares1".
        [EnumString("eclipsingSquares1")]
        EclipsingSquares1 = 85,
        //
        // Summary:
        //     Shadowed Square Art Border.
        //     When the item is serialized out as xml, its value is "eclipsingSquares2".
        [EnumString("eclipsingSquares2")]
        EclipsingSquares2 = 86,
        //
        // Summary:
        //     Painted Egg Art Border.
        //     When the item is serialized out as xml, its value is "eggsBlack".
        [EnumString("eggsBlack")]
        EggsBlack = 87,
        //
        // Summary:
        //     Fans Art Border.
        //     When the item is serialized out as xml, its value is "fans".
        [EnumString("fans")]
        Fans = 88,
        //
        // Summary:
        //     Film Reel Art Border.
        //     When the item is serialized out as xml, its value is "film".
        [EnumString("film")]
        Film = 89,
        //
        // Summary:
        //     Firecracker Art Border.
        //     When the item is serialized out as xml, its value is "firecrackers".
        [EnumString("firecrackers")]
        Firecrackers = 90,
        //
        // Summary:
        //     Flowers Art Border.
        //     When the item is serialized out as xml, its value is "flowersBlockPrint".
        [EnumString("flowersBlockPrint")]
        FlowersBlockPrint = 91,
        //
        // Summary:
        //     Daisy Art Border.
        //     When the item is serialized out as xml, its value is "flowersDaisies".
        [EnumString("flowersDaisies")]
        FlowersDaisies = 92,
        //
        // Summary:
        //     Flowers Art Border.
        //     When the item is serialized out as xml, its value is "flowersModern1".
        [EnumString("flowersModern1")]
        FlowersModern1 = 93,
        //
        // Summary:
        //     Flowers Art Border.
        //     When the item is serialized out as xml, its value is "flowersModern2".
        [EnumString("flowersModern2")]
        FlowersModern2 = 94,
        //
        // Summary:
        //     Pansy Art Border.
        //     When the item is serialized out as xml, its value is "flowersPansy".
        [EnumString("flowersPansy")]
        FlowersPansy = 95,
        //
        // Summary:
        //     Red Rose Art Border.
        //     When the item is serialized out as xml, its value is "flowersRedRose".
        [EnumString("flowersRedRose")]
        FlowersRedRose = 96,
        //
        // Summary:
        //     Roses Art Border.
        //     When the item is serialized out as xml, its value is "flowersRoses".
        [EnumString("flowersRoses")]
        FlowersRoses = 97,
        //
        // Summary:
        //     Flowers in a Teacup Art Border.
        //     When the item is serialized out as xml, its value is "flowersTeacup".
        [EnumString("flowersTeacup")]
        FlowersTeacup = 98,
        //
        // Summary:
        //     Small Flower Art Border.
        //     When the item is serialized out as xml, its value is "flowersTiny".
        [EnumString("flowersTiny")]
        FlowersTiny = 99,
        //
        // Summary:
        //     Gems Art Border.
        //     When the item is serialized out as xml, its value is "gems".
        [EnumString("gems")]
        Gems = 100,
        //
        // Summary:
        //     Gingerbread Man Art Border.
        //     When the item is serialized out as xml, its value is "gingerbreadMan".
        [EnumString("gingerbreadMan")]
        GingerbreadMan = 101,
        //
        // Summary:
        //     Triangle Gradient Art Border.
        //     When the item is serialized out as xml, its value is "gradient".
        [EnumString("gradient")]
        Gradient = 102,
        //
        // Summary:
        //     Handmade Art Border.
        //     When the item is serialized out as xml, its value is "handmade1".
        [EnumString("handmade1")]
        Handmade1 = 103,
        //
        // Summary:
        //     Handmade Art Border.
        //     When the item is serialized out as xml, its value is "handmade2".
        [EnumString("handmade2")]
        Handmade2 = 104,
        //
        // Summary:
        //     Heart-Shaped Balloon Art Border.
        //     When the item is serialized out as xml, its value is "heartBalloon".
        [EnumString("heartBalloon")]
        HeartBalloon = 105,
        //
        // Summary:
        //     Gray Heart Art Border.
        //     When the item is serialized out as xml, its value is "heartGray".
        [EnumString("heartGray")]
        HeartGray = 106,
        //
        // Summary:
        //     Hearts Art Border.
        //     When the item is serialized out as xml, its value is "hearts".
        [EnumString("hearts")]
        Hearts = 107,
        //
        // Summary:
        //     Pattern Art Border.
        //     When the item is serialized out as xml, its value is "heebieJeebies".
        [EnumString("heebieJeebies")]
        HeebieJeebies = 108,
        //
        // Summary:
        //     Holly Art Border.
        //     When the item is serialized out as xml, its value is "holly".
        [EnumString("holly")]
        Holly = 109,
        //
        // Summary:
        //     House Art Border.
        //     When the item is serialized out as xml, its value is "houseFunky".
        [EnumString("houseFunky")]
        HouseFunky = 110,
        //
        // Summary:
        //     Circular Art Border.
        //     When the item is serialized out as xml, its value is "hypnotic".
        [EnumString("hypnotic")]
        Hypnotic = 111,
        //
        // Summary:
        //     Ice Cream Cone Art Border.
        //     When the item is serialized out as xml, its value is "iceCreamCones".
        [EnumString("iceCreamCones")]
        IceCreamCones = 112,
        //
        // Summary:
        //     Light Bulb Art Border.
        //     When the item is serialized out as xml, its value is "lightBulb".
        [EnumString("lightBulb")]
        LightBulb = 113,
        //
        // Summary:
        //     Lightning Art Border.
        //     When the item is serialized out as xml, its value is "lightning1".
        [EnumString("lightning1")]
        Lightning1 = 114,
        //
        // Summary:
        //     Lightning Art Border.
        //     When the item is serialized out as xml, its value is "lightning2".
        [EnumString("lightning2")]
        Lightning2 = 115,
        //
        // Summary:
        //     Map Pins Art Border.
        //     When the item is serialized out as xml, its value is "mapPins".
        [EnumString("mapPins")]
        MapPins = 116,
        //
        // Summary:
        //     Maple Leaf Art Border.
        //     When the item is serialized out as xml, its value is "mapleLeaf".
        [EnumString("mapleLeaf")]
        MapleLeaf = 117,
        //
        // Summary:
        //     Muffin Art Border.
        //     When the item is serialized out as xml, its value is "mapleMuffins".
        [EnumString("mapleMuffins")]
        MapleMuffins = 118,
        //
        // Summary:
        //     Marquee Art Border.
        //     When the item is serialized out as xml, its value is "marquee".
        [EnumString("marquee")]
        Marquee = 119,
        //
        // Summary:
        //     Marquee Art Border.
        //     When the item is serialized out as xml, its value is "marqueeToothed".
        [EnumString("marqueeToothed")]
        MarqueeToothed = 120,
        //
        // Summary:
        //     Moon Art Border.
        //     When the item is serialized out as xml, its value is "moons".
        [EnumString("moons")]
        Moons = 121,
        //
        // Summary:
        //     Mosaic Art Border.
        //     When the item is serialized out as xml, its value is "mosaic".
        [EnumString("mosaic")]
        Mosaic = 122,
        //
        // Summary:
        //     Musical Note Art Border.
        //     When the item is serialized out as xml, its value is "musicNotes".
        [EnumString("musicNotes")]
        MusicNotes = 123,
        //
        // Summary:
        //     Patterned Art Border.
        //     When the item is serialized out as xml, its value is "northwest".
        [EnumString("northwest")]
        Northwest = 124,
        //
        // Summary:
        //     Oval Art Border.
        //     When the item is serialized out as xml, its value is "ovals".
        [EnumString("ovals")]
        Ovals = 125,
        //
        // Summary:
        //     Package Art Border.
        //     When the item is serialized out as xml, its value is "packages".
        [EnumString("packages")]
        Packages = 126,
        //
        // Summary:
        //     Black Palm Tree Art Border.
        //     When the item is serialized out as xml, its value is "palmsBlack".
        [EnumString("palmsBlack")]
        PalmsBlack = 127,
        //
        // Summary:
        //     Color Palm Tree Art Border.
        //     When the item is serialized out as xml, its value is "palmsColor".
        [EnumString("palmsColor")]
        PalmsColor = 128,
        //
        // Summary:
        //     Paper Clip Art Border.
        //     When the item is serialized out as xml, its value is "paperClips".
        [EnumString("paperClips")]
        PaperClips = 129,
        //
        // Summary:
        //     Papyrus Art Border.
        //     When the item is serialized out as xml, its value is "papyrus".
        [EnumString("papyrus")]
        Papyrus = 130,
        //
        // Summary:
        //     Party Favor Art Border.
        //     When the item is serialized out as xml, its value is "partyFavor".
        [EnumString("partyFavor")]
        PartyFavor = 131,
        //
        // Summary:
        //     Party Glass Art Border.
        //     When the item is serialized out as xml, its value is "partyGlass".
        [EnumString("partyGlass")]
        PartyGlass = 132,
        //
        // Summary:
        //     Pencils Art Border.
        //     When the item is serialized out as xml, its value is "pencils".
        [EnumString("pencils")]
        Pencils = 133,
        //
        // Summary:
        //     Character Art Border.
        //     When the item is serialized out as xml, its value is "people".
        [EnumString("people")]
        People = 134,
        //
        // Summary:
        //     Waving Character Border.
        //     When the item is serialized out as xml, its value is "peopleWaving".
        [EnumString("peopleWaving")]
        PeopleWaving = 135,
        //
        // Summary:
        //     Character With Hat Art Border.
        //     When the item is serialized out as xml, its value is "peopleHats".
        [EnumString("peopleHats")]
        PeopleHats = 136,
        //
        // Summary:
        //     Poinsettia Art Border.
        //     When the item is serialized out as xml, its value is "poinsettias".
        [EnumString("poinsettias")]
        Poinsettias = 137,
        //
        // Summary:
        //     Postage Stamp Art Border.
        //     When the item is serialized out as xml, its value is "postageStamp".
        [EnumString("postageStamp")]
        PostageStamp = 138,
        //
        // Summary:
        //     Pumpkin Art Border.
        //     When the item is serialized out as xml, its value is "pumpkin1".
        [EnumString("pumpkin1")]
        Pumpkin1 = 139,
        //
        // Summary:
        //     Push Pin Art Border.
        //     When the item is serialized out as xml, its value is "pushPinNote2".
        [EnumString("pushPinNote2")]
        PushPinNote2 = 140,
        //
        // Summary:
        //     Push Pin Art Border.
        //     When the item is serialized out as xml, its value is "pushPinNote1".
        [EnumString("pushPinNote1")]
        PushPinNote1 = 141,
        //
        // Summary:
        //     Pyramid Art Border.
        //     When the item is serialized out as xml, its value is "pyramids".
        [EnumString("pyramids")]
        Pyramids = 142,
        //
        // Summary:
        //     Pyramid Art Border.
        //     When the item is serialized out as xml, its value is "pyramidsAbove".
        [EnumString("pyramidsAbove")]
        PyramidsAbove = 143,
        //
        // Summary:
        //     Quadrants Art Border.
        //     When the item is serialized out as xml, its value is "quadrants".
        [EnumString("quadrants")]
        Quadrants = 144,
        //
        // Summary:
        //     Rings Art Border.
        //     When the item is serialized out as xml, its value is "rings".
        [EnumString("rings")]
        Rings = 145,
        //
        // Summary:
        //     Safari Art Border.
        //     When the item is serialized out as xml, its value is "safari".
        [EnumString("safari")]
        Safari = 146,
        //
        // Summary:
        //     Saw tooth Art Border.
        //     When the item is serialized out as xml, its value is "sawtooth".
        [EnumString("sawtooth")]
        Sawtooth = 147,
        //
        // Summary:
        //     Gray Saw tooth Art Border.
        //     When the item is serialized out as xml, its value is "sawtoothGray".
        [EnumString("sawtoothGray")]
        SawtoothGray = 148,
        //
        // Summary:
        //     Scared Cat Art Border.
        //     When the item is serialized out as xml, its value is "scaredCat".
        [EnumString("scaredCat")]
        ScaredCat = 149,
        //
        // Summary:
        //     Umbrella Art Border.
        //     When the item is serialized out as xml, its value is "seattle".
        [EnumString("seattle")]
        Seattle = 150,
        //
        // Summary:
        //     Shadowed Squares Art Border.
        //     When the item is serialized out as xml, its value is "shadowedSquares".
        [EnumString("shadowedSquares")]
        ShadowedSquares = 151,
        //
        // Summary:
        //     Shark Tooth Art Border.
        //     When the item is serialized out as xml, its value is "sharksTeeth".
        [EnumString("sharksTeeth")]
        SharksTeeth = 152,
        //
        // Summary:
        //     Bird Tracks Art Border.
        //     When the item is serialized out as xml, its value is "shorebirdTracks".
        [EnumString("shorebirdTracks")]
        ShorebirdTracks = 153,
        //
        // Summary:
        //     Rocket Art Border.
        //     When the item is serialized out as xml, its value is "skyrocket".
        [EnumString("skyrocket")]
        Skyrocket = 154,
        //
        // Summary:
        //     Snowflake Art Border.
        //     When the item is serialized out as xml, its value is "snowflakeFancy".
        [EnumString("snowflakeFancy")]
        SnowflakeFancy = 155,
        //
        // Summary:
        //     Snowflake Art Border.
        //     When the item is serialized out as xml, its value is "snowflakes".
        [EnumString("snowflakes")]
        Snowflakes = 156,
        //
        // Summary:
        //     Sombrero Art Border.
        //     When the item is serialized out as xml, its value is "sombrero".
        [EnumString("sombrero")]
        Sombrero = 157,
        //
        // Summary:
        //     Southwest-themed Art Border.
        //     When the item is serialized out as xml, its value is "southwest".
        [EnumString("southwest")]
        Southwest = 158,
        //
        // Summary:
        //     Stars Art Border.
        //     When the item is serialized out as xml, its value is "stars".
        [EnumString("stars")]
        Stars = 159,
        //
        // Summary:
        //     Stars On Top Art Border.
        //     When the item is serialized out as xml, its value is "starsTop".
        [EnumString("starsTop")]
        StarsTop = 160,
        //
        // Summary:
        //     3-D Stars Art Border.
        //     When the item is serialized out as xml, its value is "stars3d".
        [EnumString("stars3d")]
        Stars3d = 161,
        //
        // Summary:
        //     Stars Art Border.
        //     When the item is serialized out as xml, its value is "starsBlack".
        [EnumString("starsBlack")]
        StarsBlack = 162,
        //
        // Summary:
        //     Stars With Shadows Art Border.
        //     When the item is serialized out as xml, its value is "starsShadowed".
        [EnumString("starsShadowed")]
        StarsShadowed = 163,
        //
        // Summary:
        //     Sun Art Border.
        //     When the item is serialized out as xml, its value is "sun".
        [EnumString("sun")]
        Sun = 164,
        //
        // Summary:
        //     Whirligig Art Border.
        //     When the item is serialized out as xml, its value is "swirligig".
        [EnumString("swirligig")]
        Swirligig = 165,
        //
        // Summary:
        //     Torn Paper Art Border.
        //     When the item is serialized out as xml, its value is "tornPaper".
        [EnumString("tornPaper")]
        TornPaper = 166,
        //
        // Summary:
        //     Black Torn Paper Art Border.
        //     When the item is serialized out as xml, its value is "tornPaperBlack".
        [EnumString("tornPaperBlack")]
        TornPaperBlack = 167,
        //
        // Summary:
        //     Tree Art Border.
        //     When the item is serialized out as xml, its value is "trees".
        [EnumString("trees")]
        Trees = 168,
        //
        // Summary:
        //     Triangle Art Border.
        //     When the item is serialized out as xml, its value is "triangleParty".
        [EnumString("triangleParty")]
        TriangleParty = 169,
        //
        // Summary:
        //     Triangles Art Border.
        //     When the item is serialized out as xml, its value is "triangles".
        [EnumString("triangles")]
        Triangles = 170,
        //
        // Summary:
        //     Tribal Art Border One.
        //     When the item is serialized out as xml, its value is "tribal1".
        [EnumString("tribal1")]
        Tribal1 = 171,
        //
        // Summary:
        //     Tribal Art Border Two.
        //     When the item is serialized out as xml, its value is "tribal2".
        [EnumString("tribal2")]
        Tribal2 = 172,
        //
        // Summary:
        //     Tribal Art Border Three.
        //     When the item is serialized out as xml, its value is "tribal3".
        [EnumString("tribal3")]
        Tribal3 = 173,
        //
        // Summary:
        //     Tribal Art Border Four.
        //     When the item is serialized out as xml, its value is "tribal4".
        [EnumString("tribal4")]
        Tribal4 = 174,
        //
        // Summary:
        //     Tribal Art Border Five.
        //     When the item is serialized out as xml, its value is "tribal5".
        [EnumString("tribal5")]
        Tribal5 = 175,
        //
        // Summary:
        //     Tribal Art Border Six.
        //     When the item is serialized out as xml, its value is "tribal6".
        [EnumString("tribal6")]
        Tribal6 = 176,
        //
        // Summary:
        //     triangle1.
        //     When the item is serialized out as xml, its value is "triangle1".
        [EnumString("triangle1")]
        Triangle1 = 177,
        //
        // Summary:
        //     triangle2.
        //     When the item is serialized out as xml, its value is "triangle2".
        [EnumString("triangle2")]
        Triangle2 = 178,
        //
        // Summary:
        //     triangleCircle1.
        //     When the item is serialized out as xml, its value is "triangleCircle1".
        [EnumString("triangleCircle1")]
        TriangleCircle1 = 179,
        //
        // Summary:
        //     triangleCircle2.
        //     When the item is serialized out as xml, its value is "triangleCircle2".
        [EnumString("triangleCircle2")]
        TriangleCircle2 = 180,
        //
        // Summary:
        //     shapes1.
        //     When the item is serialized out as xml, its value is "shapes1".
        [EnumString("shapes1")]
        Shapes1 = 181,
        //
        // Summary:
        //     shapes2.
        //     When the item is serialized out as xml, its value is "shapes2".
        [EnumString("shapes2")]
        Shapes2 = 182,
        //
        // Summary:
        //     Twisted Lines Art Border.
        //     When the item is serialized out as xml, its value is "twistedLines1".
        [EnumString("twistedLines1")]
        TwistedLines1 = 183,
        //
        // Summary:
        //     Twisted Lines Art Border.
        //     When the item is serialized out as xml, its value is "twistedLines2".
        [EnumString("twistedLines2")]
        TwistedLines2 = 184,
        //
        // Summary:
        //     Vine Art Border.
        //     When the item is serialized out as xml, its value is "vine".
        [EnumString("vine")]
        Vine = 185,
        //
        // Summary:
        //     Wavy Line Art Border.
        //     When the item is serialized out as xml, its value is "waveline".
        [EnumString("waveline")]
        Waveline = 186,
        //
        // Summary:
        //     Weaving Angles Art Border.
        //     When the item is serialized out as xml, its value is "weavingAngles".
        [EnumString("weavingAngles")]
        WeavingAngles = 187,
        //
        // Summary:
        //     Weaving Braid Art Border.
        //     When the item is serialized out as xml, its value is "weavingBraid".
        [EnumString("weavingBraid")]
        WeavingBraid = 188,
        //
        // Summary:
        //     Weaving Ribbon Art Border.
        //     When the item is serialized out as xml, its value is "weavingRibbon".
        [EnumString("weavingRibbon")]
        WeavingRibbon = 189,
        //
        // Summary:
        //     Weaving Strips Art Border.
        //     When the item is serialized out as xml, its value is "weavingStrips".
        [EnumString("weavingStrips")]
        WeavingStrips = 190,
        //
        // Summary:
        //     White Flowers Art Border.
        //     When the item is serialized out as xml, its value is "whiteFlowers".
        [EnumString("whiteFlowers")]
        WhiteFlowers = 191,
        //
        // Summary:
        //     Woodwork Art Border.
        //     When the item is serialized out as xml, its value is "woodwork".
        [EnumString("woodwork")]
        Woodwork = 192,
        //
        // Summary:
        //     Crisscross Art Border.
        //     When the item is serialized out as xml, its value is "xIllusions".
        [EnumString("xIllusions")]
        XIllusions = 193,
        //
        // Summary:
        //     Triangle Art Border.
        //     When the item is serialized out as xml, its value is "zanyTriangles".
        [EnumString("zanyTriangles")]
        ZanyTriangles = 194,
        //
        // Summary:
        //     Zigzag Art Border.
        //     When the item is serialized out as xml, its value is "zigZag".
        [EnumString("zigZag")]
        ZigZag = 195,
        //
        // Summary:
        //     Zigzag stitch.
        //     When the item is serialized out as xml, its value is "zigZagStitch".
        [EnumString("zigZagStitch")]
        ZigZagStitch = 196,
    }
}
