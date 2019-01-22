using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
//using System.Drawing.Imaging;
using TOCRDemo;

namespace Org.TOCR
{
	public class OCR : TOCRdeclares, IDisposable
	{
		// Demonstrates how to OCR a file
		public string GetText(string path, string imgFmt)
		{

			int Status;
			int JobNo = 0;
			string Msg = "";
			string result = String.Empty;

			TOCRRESULTS Results = new TOCRRESULTS();
			TOCRJOBINFO2 JobInfo2 = new TOCRJOBINFO2();

			JobInfo2.ProcessOptions.DisableCharacter = new short[256];

			TOCRSetConfig(TOCRCONFIG_DEFAULTJOB, TOCRCONFIG_DLL_ERRORMODE, TOCRERRORMODE_MSGBOX);

			JobInfo2.InputFile = path;
			// set file type

			string ext = System.IO.Path.GetExtension(path);

			if (imgFmt.ToLower() == "tif")
				JobInfo2.JobType = TOCRJOBTYPE_TIFFFILE;
			else
				JobInfo2.JobType = TOCRJOBTYPE_DIBFILE;


			Status = TOCRInitialise(ref JobNo);
			if (Status == TOCR_OK)
			{
				// or
				//if (OCRPoll(JobNo, JobInfo2))
				if (OCRWait(JobNo, JobInfo2))
				{
					if (GetResults(JobNo, ref Results))
					{
						if (FormatResults(Results, ref Msg))
						{
							result = Msg;
							//MessageBox.Show(Msg, "Example 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					}
				}
				TOCRShutdown(JobNo);
			}

			return result;
		} 


		// Wait for the engine to complete
		private static bool OCRWait(int JobNo, TOCRJOBINFO2 JobInfo2)
		{
			int Status;
			int JobStatus = 0;
			int ErrorMode = 0;

			Status = TOCRDoJob2(JobNo, ref JobInfo2);
			if (Status == TOCR_OK)
			{
				Status = TOCRWaitForJob(JobNo, ref JobStatus);
			}

			if (Status == TOCR_OK && JobStatus == TOCRJOBSTATUS_DONE)
			{
				return true;
			}
			else
			{
				// If something has gone wrong display a message
				// (Check that the OCR engine hasn't already displayed a message)
				TOCRGetConfig(JobNo, TOCRCONFIG_DLL_ERRORMODE, ref ErrorMode);
				if (ErrorMode == TOCRERRORMODE_NONE)
				{
					StringBuilder Msg = new StringBuilder(TOCRJOBMSGLENGTH);
					TOCRGetJobStatusMsg(JobNo, Msg);
					//MessageBox.Show(Msg.ToString(), "OCRWait", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				return false;
			}
		}


		// OVERLOADED function to retrieve the results from the service process and load into 'Results'
		// Remember the character numbers returned refer to the Windows character set.
		private static bool GetResults(int JobNo, ref TOCRRESULTS Results)
		{
			int ResultsInf = 0; // number of bytes needed for results
			byte[] Bytes;
			int Offset;
			bool RetStatus = false;
			GCHandle BytesGC;
			System.IntPtr AddrOfItemBytes;

			Results.Hdr.NumItems = 0;
			if (TOCRGetJobResults(JobNo, ref ResultsInf, IntPtr.Zero) == TOCR_OK)
			{
				if (ResultsInf > 0)
				{
					Bytes = new Byte[ResultsInf - 1];
					// pin the Bytes array so that TOCRGetJobResults can write to it
					BytesGC = GCHandle.Alloc(Bytes, GCHandleType.Pinned);
					if (TOCRGetJobResults(JobNo, ref ResultsInf, BytesGC.AddrOfPinnedObject()) == TOCR_OK)
					{
						Results.Hdr = ((TOCRRESULTSHEADER)(Marshal.PtrToStructure(BytesGC.AddrOfPinnedObject(), typeof(TOCRRESULTSHEADER))));
						if (Results.Hdr.NumItems > 0)
						{
							Results.Item = new TOCRRESULTSITEM[Results.Hdr.NumItems];
							Offset = Marshal.SizeOf(typeof(TOCRRESULTSHEADER));
							for (int ItemNo = 0; ItemNo <= Results.Hdr.NumItems - 1; ItemNo++)
							{
								AddrOfItemBytes = Marshal.UnsafeAddrOfPinnedArrayElement(Bytes, Offset);
								Results.Item[ItemNo] = ((TOCRRESULTSITEM)(Marshal.PtrToStructure(AddrOfItemBytes, typeof(TOCRRESULTSITEM))));
								Offset = Offset + Marshal.SizeOf(typeof(TOCRRESULTSITEM));
							}
						}

						RetStatus = true;

						// Double check results
						if (Results.Hdr.StructId == 0)
						{
							if (Results.Hdr.NumItems > 0)
							{
								if (Results.Item[0].StructId != 0)
								{
									//MessageBox.Show("Wrong results item structure Id " + Results.Item[0].StructId.ToString(), "GetResults", MessageBoxButtons.OK, MessageBoxIcon.Stop);
									Results.Hdr.NumItems = 0;
									RetStatus = false;
								}
							}
						}
						else
						{
							//MessageBox.Show("Wrong results header structure Id " + Results.Item[0].StructId.ToString(), "GetResults", MessageBoxButtons.OK, MessageBoxIcon.Stop);
							Results.Hdr.NumItems = 0;
							RetStatus = false;
						}
					}
					BytesGC.Free();
				}
			}
			return RetStatus;
		}

		// OVERLOADED function to convert results to a string
		private static bool FormatResults(TOCRRESULTS Results, ref string Msg)
		{
			if (Results.Hdr.NumItems > 0)
			{
				for (int ItemNo = 0; ItemNo < Results.Hdr.NumItems; ItemNo++)
				{
					if (Results.Item[ItemNo].OCRCha == 13)
						Msg = Msg + Environment.NewLine;
					else
						Msg = Msg + Convert.ToChar(Results.Item[ItemNo].OCRCha);
				}

				/*Encoding UNICODE = Encoding.Unicode;
				Encoding ANSI = Encoding.GetEncoding(1252);
				byte[] ansibytes = new Byte[Results.Hdr.NumItems];
				byte[] unicodebytes = new Byte[Results.Hdr.NumItems];

				for (int ItemNo = 0; ItemNo < Results.Hdr.NumItems; ItemNo++)
{
						if (Results.Item[ItemNo].OCRCha == 13)
								ansibytes[ItemNo] = (byte)'\n';
						else
						{
								ansibytes[ItemNo] = (byte)Results.Item[ItemNo].OCRCha;
						}
}
				unicodebytes = Encoding.Convert(ANSI, UNICODE, ansibytes);
				Msg = UNICODE.GetString(unicodebytes);
				*/
				return true;
			}
			else
			{
				//MessageBox.Show("No results returned", "FormatResults", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return false;
			}
		}

		// OVERLOADED function to convert results to a string
		private static bool FormatResults(TOCRRESULTSEX ResultsEx, ref string Msg)
		{
			Msg = "";

			if (ResultsEx.Hdr.NumItems > 0)
			{
				for (int ItemNo = 0; ItemNo < ResultsEx.Hdr.NumItems; ItemNo++)
				{
					if (ResultsEx.Item[ItemNo].OCRCha == 13)
						Msg = Msg + Environment.NewLine;
					else
						Msg = Msg + Convert.ToChar(ResultsEx.Item[ItemNo].OCRCha);
				}
				return true;
			}
			else
			{
				//MessageBox.Show("No results returned", "FormatResults", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return false;
			}
		}

    public void Dispose()
    {

    }
	}
}
