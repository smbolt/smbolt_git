using System;
using System.Runtime.InteropServices;
using System.Text;

// TOCR declares Version 4.0.0.0

namespace TOCRDemo
{
	public class TOCRdeclares
	{
		#region Structures
		[StructLayout(LayoutKind.Sequential)]
		public struct TOCRPROCESSOPTIONS
		{
			public int StructId;
			public short InvertWholePage;
			public short DeskewOff;
			public byte Orientation;
			public short NoiseRemoveOff;
			public short LineRemoveOff;
			public short DeshadeOff;
			public short InvertOff;
			public short SectioningOn;
			public short MergeBreakOff;
			public short LineRejectOff;
			public short CharacterRejectOff;
			public short LexOff;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
			public short[] DisableCharacter;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		public struct TOCRJOBINFO2
		{
			public int StructId;
			public int JobType;
			public string InputFile;
			public System.IntPtr hMMF;
			public int PageNo;
			public TOCRPROCESSOPTIONS ProcessOptions;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		public struct TOCRJOBINFO
		{
			public int StructId;
			public int JobType;
			public string InputFile;
			public int PageNo;
			public TOCRPROCESSOPTIONS ProcessOptions;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct TOCRRESULTSHEADER
		{
			public int StructId;
			public int XPixelsPerInch;
			public int YPixelsPerInch;
			public int NumItems;
			public float MeanConfidence;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct TOCRRESULTSITEM
		{
			public short StructId;
			public short OCRCha;
			public float Confidence;
			public short XPos;
			public short YPos;
			public short XDim;
			public short YDim;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct TOCRRESULTS
		{
			public TOCRRESULTSHEADER Hdr;
			public TOCRRESULTSITEM[] Item;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct TOCRRESULTSITEMEXALT
		{
			public short Valid;
			public short OCRCha;
			public float Factor;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct TOCRRESULTSITEMEX
		{
			public short StructId;
			public short OCRCha;
			public float Confidence;
			public short XPos;
			public short YPos;
			public short XDim;
			public short YDim;
			public TOCRRESULTSITEMEXALT[] Alt;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct TOCRRESULTSEX
		{
			public TOCRRESULTSHEADER Hdr;
			public TOCRRESULTSITEMEX[] Item;
		}
		#endregion

		#region Declares
		[DllImport("TOCRDll")]
		public static extern int TOCRInitialise(ref int JobNo);

		[DllImport("TOCRDll")]
		public static extern int TOCRShutdown(int JobNo);

		[DllImport("TOCRDll")]
		public static extern int TOCRDoJob2(int JobNo, ref TOCRJOBINFO2 JobInfo2);

		[DllImport("TOCRDll")]
		public static extern int TOCRWaitForJob(int JobNo, ref int JobStatus);

		[DllImport("TOCRDll")]
		public static extern int TOCRWaitForAnyJob(ref int WaitAnyStatus, ref int JobNo);

		[DllImport("TOCRDll")]
		public static extern int TOCRGetJobDBInfo(System.IntPtr JobSlotInf);

		[DllImport("TOCRDll")]
		public static extern int TOCRGetJobStatus(int JobNo, ref int JobStatus);

		[DllImport("TOCRDll")]
		public static extern int TOCRGetJobStatusEx(int JobNo, ref int JobStatus, ref float Progress, ref int AutoOrientation);

		[DllImport("TOCRDll", CharSet = CharSet.Ansi)]
		public static extern int TOCRGetJobStatusMsg(int JobNo, StringBuilder Msg);

		[DllImport("TOCRDll", CharSet = CharSet.Ansi)]
		public static extern int TOCRGetNumPages(int JobNo, string Filename, int JobType, ref int NumPages);

		[DllImport("TOCRDll")]
		public static extern int TOCRGetJobResults(int JobNo, ref int ResultsInf, System.IntPtr Bytes);

		[DllImport("TOCRDll")]
		public static extern int TOCRGetJobResultsEx(int JobNo, int Mode, ref int ResultsInf, System.IntPtr Bytes);

		[DllImport("TOCRDll", CharSet = CharSet.Ansi)]
		public static extern int TOCRGetLicenceInfoEx(int JobNo, StringBuilder Licence, ref int Volume, ref int Time, ref int Remaining, ref int Features);

		// Convert a TIF file to a bitmap file
		[DllImport("TOCRDll", CharSet = CharSet.Ansi)]
		public static extern int TOCRConvertFormat(int JobNo, string InputAddr, int InputFormat, string OutputAddr, int OutputFormat, int PageNo);
		// Convert a TIF file to a memory mapped file handle
		[DllImport("TOCRDll", CharSet = CharSet.Ansi)]
		public static extern int TOCRConvertFormat(int JobNo, string InputAddr, int InputFormat, ref System.IntPtr OutputAddr, int OutputFormat, int PageNo);

		// These functions cannot be used to get/set the log file name in x64
		[DllImport("TOCRDll")]
		public static extern int TOCRSetConfig(int JobNo, int Parameter, int Value);
		[DllImport("TOCRDll")]
		public static extern int TOCRGetConfig(int JobNo, int Parameter, ref int Value);

		// These functions can be used to get/set the log file name in x64
		[DllImport("TOCRDll", CharSet = CharSet.Ansi)]
		public static extern int TOCRSetConfigStr(int JobNo, int Parameter, string Value);
		[DllImport("TOCRDll", CharSet = CharSet.Ansi)]
		public static extern int TOCRGetConfigStr(int JobNo, int Parameter, StringBuilder Value);

		[DllImport("TOCRDll")]
		public static extern int TOCRTWAINAcquire(ref int NumberOfImages);

		[DllImport("TOCRDll")]
		public static extern int TOCRTWAINGetImages(System.IntPtr GlobalMemoryDIBs);

		[DllImport("TOCRDll")]
		public static extern int TOCRTWAINSelectDS();

		[DllImport("TOCRDll")]
		public static extern int TOCRTWAINShowUI(short Show);

		// Superseded by TOCRGetConfig
		[DllImport("TOCRDll")]
		public static extern int TOCRGetErrorMode(int JobNo, ref int ErrorMode);

		// Superseded by TOCRSetConfig
		[DllImport("TOCRDll")]
		public static extern int TOCRSetErrorMode(int JobNo, int ErrorMode);

		// Superseded by TOCRDoJob2
		[DllImport("TOCRDll")]
		public static extern int TOCRDoJob(int JobNo, ref TOCRJOBINFO JobInfo);

		// REDUNDANT - use the Bitmap class in .NET
		[DllImport("TOCRDll")]
		public static extern int TOCRRotateMonoBitmap(ref IntPtr hBmp, int Width, int Height, int Orientation);

		// UNTESTED - obsolete, use TOCRConvertFormat
		//[DllImport("TOCRDll", CharSet=CharSet.Ansi)]
		//  public static extern int TOCRConvertTIFFtoDIB(int JobNo, string InputFilename, string OutputFilename, int PageNo);

		// UNTESTED - Superseded by TOCRGetLicenceInfoEx
		//[DllImport("TOCRDll", CharSet=CharSet.Ansi)]
		//  public static extern int TOCRGetLicenceInfo(ref NumberOfJobSlots, ref int Volume, ref int Time, ref int Remaining);

		#endregion

		#region User Constants
		public const short TOCRJOBMSGLENGTH = 512;			// max length of a job status message

		public const int TOCRMAXPPM = 78741;				// max pixels per metre
		public const int TOCRMINPPM = 984;					// min pixels per metre

		public const int TOCRDEFERRORMODE = -1;				// set/get the API error mode for all jobs

		// Settings for ErrorMode for TOCRSetErrorMode and TOCRGetErrorMode
		public const int TOCRERRORMODE_NONE = 0;			// API errors unseen (use return status of API calls)
		public const int TOCRERRORMODE_MSGBOX = 1;			// API errors will bring up a message box
		public const int TOCRERRORMODE_LOG = 2;				// errors are sent to a log file

		// Setting for TOCRShutdown
		public const int TOCRSHUTDOWNALL = -1;				//  stop and shutdown processing for all jobs

		// Values returned by TOCRGetJobStatus JobStatus
		public const int TOCRJOBSTATUS_ERROR = -1;			// an error ocurred
		public const int TOCRJOBSTATUS_BUSY = 0;			// the job is still processing
		public const int TOCRJOBSTATUS_DONE = 1;			// the job completed successfully
		public const int TOCRJOBSTATUS_IDLE = 2;			// no job has been specified yet

		// Settings for TOCRJOBINFO.JobType
		public const int TOCRJOBTYPE_TIFFFILE = 0;			// TOCRJOBINFO.InputFile specifies a tiff file
		public const int TOCRJOBTYPE_DIBFILE = 1;			// TOCRJOBINFO.InputFile specifies a dib (bmp) file
		public const int TOCRJOBTYPE_DIBCLIPBOARD = 2;		// clipboard contains a dib (clipboard format CF_DIB)
		public const int TOCRJOBTYPE_MMFILEHANDLE = 3;		// TOCRJOBINFO.PageNo specifies a handle to a memory mapped DIB file

		// Settings for TOCRJOBINFO.Orientation
		public const byte TOCRJOBORIENT_AUTO = 0;			// detect orientation and rotate automatically
		public const byte TOCRJOBORIENT_OFF = 255;			// don't rotate
		public const byte TOCRJOBORIENT_90 = 1;				// 90 degrees clockwise rotation
		public const byte TOCRJOBORIENT_180 = 2;			// 180 degrees clockwise rotation
		public const byte TOCRJOBORIENT_270 = 3;			// 270 degrees clockwise rotation

		// Values returned by TOCRGetJobDBInfo
		public const int TOCRJOBSLOT_FREE = 0;				// job slot is free for use
		public const int TOCRJOBSLOT_OWNEDBYYOU = 1;		// job slot is in use by your process
		public const int TOCRJOBSLOT_BLOCKEDBYYOU = 2;		// blocked by own process (re-initialise)
		public const int TOCRJOBSLOT_OWNEDBYOTHER = -1;		// job slot is in use by another process (can't use)
		public const int TOCRJOBSLOT_BLOCKEDBYOTHER = -2;	// blocked by another process (can't use)

		// Values returned in WaitAnyStatus by TOCRWaitForAnyJob
		public const int TOCRWAIT_OK = 0;					// JobNo is the job that finished (get and check it's JobStatus)
		public const int TOCRWAIT_SERVICEABORT = 1;			// JobNo is the job that failed (re-initialise)
		public const int TOCRWAIT_CONNECTIONBROKEN = 2;		// JobNo is the job that failed (re-initialise)
		public const int TOCRWAIT_FAILED = -1;				// JobNo not set - check manually
		public const int TOCRWAIT_NOJOBSFOUND = -2;			// JobNo not set - no running jobs found

		// Settings for Mode for TOCRGetJobResultsEx
		public const int TOCRGETRESULTS_NORMAL = 0;			// return results for TOCRRESULTS
		public const int TOCRGETRESULTS_EXTENDED = 1;		// return results for TOCRRESULTSEX

		// Values returned in ResultsInf by TOCRGetJobResults and TOCRGetJobResultsEx
		public const int TOCRGETRESULTS_NORESULTS = -1;		// no results are available

		// Values for TOCRConvertFormat InputFormat
		public const int TOCRCONVERTFORMAT_TIFFFILE = TOCRJOBTYPE_TIFFFILE;

		// Values for TOCRConvertFormat OutputFormat
		public const int TOCRCONVERTFORMAT_DIBFILE = TOCRJOBTYPE_DIBFILE;
		public const int TOCRCONVERTFORMAT_MMFILEHANDLE = TOCRJOBTYPE_MMFILEHANDLE;

		// Values for licence features (returned by TOCRGetLicenceInfoEx)
		public const int TOCRLICENCE_STANDARD = 1;			// standard licence (no higher characters)
		public const int TOCRLICENCE_EURO = 2;				// higher characters
		public const int TOCRLICENCE_EUROUPGRADE = 3;		// standard licence upgraded to euro
		public const int TOCRLICENCE_V3SE = 4;				// V3SE version 3 standard edition licence (no API)
		public const int TOCRLICENCE_V3SEUPGRADE = 5;		// versions 1/2 upgraded to V3 standard edition (no API)
		// Note V4 licences are the same as V3 Pro licences
		public const int TOCRLICENCE_V3PRO = 6;				// V3PRO version 3 pro licence
		public const int TOCRLICENCE_V3PROUPGRADE = 7;		// versions 1/2 upgraded to version 3 pro
		public const int TOCRLICENCE_V3SEPROUPGRADE = 8;	// version 3 standard edition upgraded to version 3 pro		#endregion

		// Values for TOCRSetConfig and TOCRGetConfig
		public const int TOCRCONFIG_DEFAULTJOB = -1;			// default job number (all new jobs)
		public const int TOCRCONFIG_DLL_ERRORMODE = 0;		// set the dll ErrorMode
		public const int TOCRCONFIG_SRV_ERRORMODE = 1;		// set the service ErrorMode
		public const int TOCRCONFIG_SRV_THREADPRIORITY = 2; // set the service thread priority
		public const int TOCRCONFIG_DLL_MUTEXWAIT = 3;		// set the dll mutex wait timeout (ms)
		public const int TOCRCONFIG_DLL_EVENTWAIT = 4;		// set the dll event wait timeout (ms)
		public const int TOCRCONFIG_SRV_MUTEXWAIT = 5;		// set the service mutex wait timeout (ms)
		public const int TOCRCONFIG_LOGFILE = 6;			// set the log file name
		#endregion

		#region Error Codes
		public const int TOCR_OK = 0;
		/*
		// Error codes returned by an API function
		public const int TOCRERR_ILLEGALJOBNO = 1;
		public const int TOCRERR_FAILLOCKDB = 2;
		public const int TOCRERR_NOFREEJOBSLOTS = 3;
		public const int TOCRERR_FAILSTARTSERVICE = 4;
		public const int TOCRERR_FAILINITSERVICE = 5;
		public const int TOCRERR_JOBSLOTNOTINIT = 6;
		public const int TOCRERR_JOBSLOTINUSE = 7;
		public const int TOCRERR_SERVICEABORT = 8;
		public const int TOCRERR_CONNECTIONBROKEN = 9;
		public const int TOCRERR_INVALIDSTRUCTID = 10;
		public const int TOCRERR_FAILGETVERSION = 11;
		public const int TOCRERR_FAILLICENCEINF = 12;
		public const int TOCRERR_LICENCEEXCEEDED = 13;
        public const int TOCRERR_MISMATCH 15;
		public const int TOCRERR_JOBSLOTNOTYOURS = 16;

		public const int TOCRERR_FAILGETJOBSTATUS1 = 20;
		public const int TOCRERR_FAILGETJOBSTATUS2 = 21;
		public const int TOCRERR_FAILGETJOBSTATUS3 = 22;
		public const int TOCRERR_FAILCONVERT = 23;
		public const int TOCRERR_FAILSETCONFIG = 24;
		public const int TOCRERR_FAILGETCONFIG = 25;
		
		public const int TOCRERR_FAILDOJOB1 = 30;
		public const int TOCRERR_FAILDOJOB2 = 31;
		public const int TOCRERR_FAILDOJOB3 = 32;
		public const int TOCRERR_FAILDOJOB4 = 33;
		public const int TOCRERR_FAILDOJOB5 = 34;
		public const int TOCRERR_FAILDOJOB6 = 35;
		public const int TOCRERR_FAILDOJOB7 = 36;
		public const int TOCRERR_FAILDOJOB8 = 37;
		public const int TOCRERR_FAILDOJOB9 = 38;
		public const int TOCRERR_FAILDOJOB10 = 39;
		public const int TOCRERR_UNKNOWNJOBTYPE1 = 40;
		public const int TOCRERR_JOBNOTSTARTED1 = 41;
		public const int TOCRERR_FAILDUPHANDLE = 42;
		
		public const int TOCRERR_FAILGETJOBSTATUSMSG1 = 45;
		public const int TOCRERR_FAILGETJOBSTATUSMSG2 = 46;
		
		public const int TOCRERR_FAILGETNUMPAGES1 = 50;
		public const int TOCRERR_FAILGETNUMPAGES2 = 51;
		public const int TOCRERR_FAILGETNUMPAGES3 = 52;
		public const int TOCRERR_FAILGETNUMPAGES4 = 53;
		public const int TOCRERR_FAILGETNUMPAGES5 = 54;
		
		public const int TOCRERR_FAILGETRESULTS1 = 60;
		public const int TOCRERR_FAILGETRESULTS2 = 61;
		public const int TOCRERR_FAILGETRESULTS3 = 62;
		public const int TOCRERR_FAILGETRESULTS4 = 63;
		public const int TOCRERR_FAILALLOCMEM100 = 64;
		public const int TOCRERR_FAILALLOCMEM101 = 65;
		public const int TOCRERR_FILENOTSPECIFIED = 66;
		public const int TOCRERR_INPUTNOTSPECIFIED = 67;
		public const int TOCRERR_OUTPUTNOTSPECIFIED = 68;
		
		public const int TOCRERR_FAILROTATEBITMAP = 70;
		
		public const int TOCERR_TWAINPARTIALACQUIRE = 80;
		public const int TOCERR_TWAINFAILEDACQUIRE = 81;
		public const int TOCERR_TWAINNOIMAGES = 82;
		public const int TOCERR_TWAINSELECTDSFAILED = 83;
		
		public const int TOCRERR_INVALIDSERVICESTART = 1000;
		public const int TOCRERR_FAILSERVICEINIT = 1001;
		public const int TOCRERR_FAILLICENCE1 = 1002;
		public const int TOCRERR_FAILSERVICESTART = 1003;
		public const int TOCRERR_UNKNOWNCMD = 1004;
		public const int TOCRERR_FAILREADCOMMAND = 1005;
		public const int TOCRERR_FAILREADOPTIONS = 1006;
		public const int TOCRERR_FAILWRITEJOBSTATUS1 = 1007;
		public const int TOCRERR_FAILWRITEJOBSTATUS2 = 1008;
		public const int TOCRERR_FAILWRITETHREADH = 1009;
		public const int TOCRERR_FAILREADJOBINFO1 = 1010;
		public const int TOCRERR_FAILREADJOBINFO2 = 1011;
		public const int TOCRERR_FAILREADJOBINFO3 = 1012;
		public const int TOCRERR_FAILWRITEPROGRESS = 1013;
		public const int TOCRERR_FAILWRITEJOBSTATUSMSG = 1014;
		public const int TOCRERR_FAILWRITERESULTSSIZE = 1015;
		public const int TOCRERR_FAILWRITERESULTS = 1016;
		public const int TOCRERR_FAILWRITEAUTOORIENT = 1017;
		public const int TOCRERR_FAILLICENCE2 = 1018;
		public const int TOCRERR_FAILLICENCE3 = 1019;
		
		public const int TOCRERR_TOOMANYCOLUMNS = 1020;
		public const int TOCRERR_TOOMANYROWS = 1021;
		public const int TOCRERR_EXCEEDEDMAXZONE = 1022;
		public const int TOCRERR_NSTACKTOOSMALL = 1023;
		public const int TOCRERR_ALGOERR1 = 1024;
		public const int TOCRERR_ALGOERR2 = 1025;
		public const int TOCRERR_EXCEEDEDMAXCP = 1026;
		public const int TOCRERR_CANTFINDPAGE = 1027;
		public const int TOCRERR_UNSUPPORTEDIMAGETYPE = 1028;
		public const int TOCRERR_IMAGETOOWIDE = 1029;
		public const int TOCRERR_IMAGETOOLONG = 1030;
		public const int TOCRERR_UNKNOWNJOBTYPE2 = 1031;
		public const int TOCRERR_TOOWIDETOROT = 1032;
		public const int TOCRERR_TOOLONGTOROT = 1033;
		public const int TOCRERR_INVALIDPAGENO = 1034;
		public const int TOCRERR_FAILREADJOBTYPENUMBYTES = 1035;
		public const int TOCRERR_FAILREADFILENAME = 1036;
		public const int TOCRERR_FAILSENDNUMPAGES = 1037;
		public const int TOCRERR_FAILOPENCLIP = 1038;
		public const int TOCRERR_NODIBONCLIP = 1039;
		public const int TOCRERR_FAILREADDIBCLIP = 1040;
		public const int TOCRERR_FAILLOCKDIBCLIP = 1041;
		public const int TOCRERR_UNKOWNDIBFORMAT = 1042;
		public const int TOCRERR_FAILREADDIB = 1043;
		public const int TOCRERR_NOXYPPM = 1044;
		public const int TOCRERR_FAILCREATEDIB = 1045;
		public const int TOCRERR_FAILWRITEDIBCLIP = 1046;
		public const int TOCRERR_FAILALLOCMEMDIB = 1047;
		public const int TOCRERR_FAILLOCKMEMDIB = 1048;
		public const int TOCRERR_FAILCREATEFILE = 1049;
		public const int TOCRERR_FAILOPENFILE1 = 1050;
		public const int TOCRERR_FAILOPENFILE2 = 1051;
		public const int TOCRERR_FAILOPENFILE3 = 1052;
		public const int TOCRERR_FAILOPENFILE4 = 1053;
		public const int TOCRERR_FAILREADFILE1 = 1054;
		public const int TOCRERR_FAILREADFILE2 = 1055;
		public const int TOCRERR_FAILFINDDATA1 = 1056;
		public const int TOCRERR_TIFFERROR1 = 1057;
		public const int TOCRERR_TIFFERROR2 = 1058;
		public const int TOCRERR_TIFFERROR3 = 1059;
		public const int TOCRERR_TIFFERROR4 = 1060;
		public const int TOCRERR_FAILREADDIBHANDLE = 1061;
		public const int TOCRERR_PAGETOOBIG = 1062;
		public const int TOCRERR_FAILSETTHREADPRIORITY = 1063;
		public const int TOCRERR_FAILSETSRVERRORMODE = 1064;

		public const int TOCRERR_FAILREADFILENAME1 = 1070;
		public const int TOCRERR_FAILREADFILENAME2 = 1071;
		public const int TOCRERR_FAILREADFILENAME3 = 1072;
		public const int TOCRERR_FAILREADFILENAME4 = 1073;
		public const int TOCRERR_FAILREADFILENAME5 = 1074;
		
		public const int TOCRERR_FAILREADFORMAT1 = 1080;
		public const int TOCRERR_FAILREADFORMAT2 = 1081;

		public const int TOCRERR_FAILALLOCMEM1 = 1101;
		public const int TOCRERR_FAILALLOCMEM2 = 1102;
		public const int TOCRERR_FAILALLOCMEM3 = 1103;
		public const int TOCRERR_FAILALLOCMEM4 = 1104;
		public const int TOCRERR_FAILALLOCMEM5 = 1105;
		public const int TOCRERR_FAILALLOCMEM6 = 1106;
		public const int TOCRERR_FAILALLOCMEM7 = 1107;
		public const int TOCRERR_FAILALLOCMEM8 = 1108;
		public const int TOCRERR_FAILALLOCMEM9 = 1109;
		public const int TOCRERR_FAILALLOCMEM10 = 1110;
		
		public const int TOCRERR_FAILWRITEMMFH = 1150;
		public const int TOCRERR_FAILREADACK = 1151;
		public const int TOCRERR_FAILFILEMAP = 1152;
		public const int TOCRERR_FAILFILEVIEW = 1153;
		
		public const int TOCRERR_BUFFEROVERFLOW1 = 2001;
		
		public const int TOCRERR_MAPOVERFLOW = 2002;
		public const int TOCRERR_REBREAKNEXTCALL = 2003;
		public const int TOCRERR_REBREAKNEXTDATA = 2004;
		public const int TOCRERR_REBREAKEXACTCALL = 2005;
		public const int TOCRERR_MAXZCANOVERFLOW1 = 2006;
		public const int TOCRERR_MAXZCANOVERFLOW2 = 2007;
		public const int TOCRERR_BUFFEROVERFLOW2 = 2008;
		public const int TOCRERR_NUMKCOVERFLOW = 2009;
		public const int TOCRERR_BUFFEROVERFLOW3 = 2010;
		public const int TOCRERR_BUFFEROVERFLOW4 = 2011;
		public const int TOCRERR_SEEDERROR = 2012;
		
		public const int TOCRERR_FCZYREF = 2020;
		public const int TOCRERR_MAXTEXTLINES1 = 2021;
		public const int TOCRERR_LINEINDEX = 2022;
		public const int TOCRERR_MAXFCZSONLINE = 2023;
		public const int TOCRERR_MEMALLOC1 = 2024;
		public const int TOCRERR_MERGEBREAK = 2025;
		
		public const int TOCRERR_DKERNPRANGE1 = 2030;
		public const int TOCRERR_DKERNPRANGE2 = 2031;
		public const int TOCRERR_BUFFEROVERFLOW5 = 2032;
		public const int TOCRERR_BUFFEROVERFLOW6 = 2033;
		
		public const int TOCRERR_FILEOPEN1 = 2040;
		public const int TOCRERR_FILEOPEN2 = 2041;
		public const int TOCRERR_FILEOPEN3 = 2042;
		public const int TOCRERR_FILEREAD1 = 2043;
		public const int TOCRERR_FILEREAD2 = 2044;
		public const int TOCRERR_SPWIDZERO = 2045;
		public const int TOCRERR_FAILALLOCMEMLEX1 = 2046;
		public const int TOCRERR_FAILALLOCMEMLEX2 = 2047;
		
		public const int TOCRERR_BADOBWIDTH = 2050;
		public const int TOCRERR_BADROTATION = 2051;
		
		public const int TOCRERR_REJHIDMEMALLOC = 2055;
		
		public const int TOCRERR_UIDA = 2070;
		public const int TOCRERR_UIDB = 2071;
		public const int TOCRERR_ZEROUID = 2072;
		public const int TOCRERR_CERTAINTYDBNOTINIT = 2073;
		public const int TOCRERR_MEMALLOCINDEX = 2074;
		public const int TOCRERR_CERTAINTYDB_INIT = 2075;
		public const int TOCRERR_CERTAINTYDB_DELETE = 2076;
		public const int TOCRERR_CERTAINTYDB_INSERT1 = 2077;
		public const int TOCRERR_CERTAINTYDB_INSERT2 = 2078;
		public const int TOCRERR_OPENXORNEAREST = 2079;
		public const int TOCRERR_XORNEAREST = 2079;
		
		public const int TOCRERR_OPENSETTINGS = 2080;
		public const int TOCRERR_READSETTINGS1 = 2081;
		public const int TOCRERR_READSETTINGS2 = 2082;
		public const int TOCRERR_BADSETTINGS = 2083;
		public const int TOCRERR_WRITESETTINGS = 2084;
		public const int TOCRERR_MAXSCOREDIFF = 2085;
		
		public const int TOCRERR_YDIMREFZERO1 = 2090;
		public const int TOCRERR_YDIMREFZERO2 = 2091;
		public const int TOCRERR_YDIMREFZERO3 = 2092;
		public const int TOCRERR_ASMFILEOPEN = 2093;
		public const int TOCRERR_ASMFILEREAD = 2094;
		public const int TOCRERR_MEMALLOCASM = 2095;
		public const int TOCRERR_MEMREALLOCASM = 2096;
		public const int TOCRERR_SDBFILEOPEN = 2097;
		public const int TOCRERR_SDBFILEREAD = 2098;
		public const int TOCRERR_SDBFILEBAD1 = 2099;
		public const int TOCRERR_SDBFILEBAD2 = 2100;
		public const int TOCRERR_MEMALLOCSDB = 2101;
		public const int TOCRERR_DEVEL1 = 2102;
		public const int TOCRERR_DEVEL2 = 2103;
		public const int TOCRERR_DEVEL3 = 2104;
		public const int TOCRERR_DEVEL4 = 2105;
		public const int TOCRERR_DEVEL5 = 2106;
		public const int TOCRERR_DEVEL6 = 2107;
		public const int TOCRERR_DEVEL7 = 2108;
		public const int TOCRERR_DEVEL8 = 2109;
		public const int TOCRERR_DEVEL9 = 2110;
		public const int TOCRERR_DEVEL10 = 2111;
		public const int TOCRERR_DEVEL11 = 2112;
		public const int TOCRERR_DEVEL12 = 2113;
		public const int TOCRERR_DEVEL13 = 2114;
		public const int TOCRERR_FILEOPEN4 = 2115;
		public const int TOCRERR_FILEOPEN5 = 2116;
		public const int TOCRERR_FILEOPEN6 = 2117;
		public const int TOCRERR_FILEREAD3 = 2118;
		public const int TOCRERR_FILEREAD4 = 2119;
		public const int TOCRERR_ZOOMGTOOBIG = 2120;
		public const int TOCRERR_ZOOMGOUTOFRANGE = 2121;
		
		public const int TOCRERR_MEMALLOCRESULTS = 2130;
		public const int TOCRERR_MEMALLOCHEAP = 2140;
		public const int TOCRERR_HEAPNOTINITIALISED = 2141;
		public const int TOCRERR_MEMLIMITHEAP = 2142;
		public const int TOCRERR_MEMREALLOCHEAP = 2143;
		public const int TOCRERR_MEMALLOCFCZBM = 2144;
		public const int TOCRERR_FCZBMOVERLAP = 2145;
		public const int TOCRERR_FCZBMLOCATION = 2146;
		public const int TOCRERR_MEMREALLOCFCZBM = 2147;
		public const int TOCRERR_MEMALLOCFCHBM = 2148;
		public const int TOCRERR_MEMREALLOCFCHBM = 2149;
		*/
		#endregion
	}
}
