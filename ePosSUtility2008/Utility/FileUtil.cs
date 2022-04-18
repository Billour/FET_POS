namespace Advtek.Utility
{
    using log4net;
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Web;

    public sealed class FileUtil
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(FileUtil));

        public static string ReadFile(string FullFilePath)
        {
            StreamReader objReader = null;
            string strLine = "";
            StringBuilder strResult = new StringBuilder();
            try
            {
                objReader = new StreamReader(FullFilePath, Encoding.GetEncoding(950));
                while (strLine != null)
                {
                    strLine = objReader.ReadLine();
                    if (strLine != null)
                    {
                        strResult.Append(strLine);
                        strResult.Append("\r\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                objReader.Close();
                objReader = null;
            }
            return strResult.ToString();
        }

        public static ArrayList ReadFileToArrayList(string FullFilePath)
        {
            StreamReader objReader = null;
            string strLine = "";
            ArrayList aryResult = new ArrayList();
            try
            {
                objReader = new StreamReader(FullFilePath, Encoding.GetEncoding(950));
                while (strLine != null)
                {
                    strLine = objReader.ReadLine();
                    if (strLine != null)
                    {
                        aryResult.Add(strLine);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                objReader.Close();
                objReader = null;
            }
            return aryResult;
        }

        private static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
        {
            try
            {
                FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    _Response.AddHeader("Accept-Ranges", "bytes");
                    _Response.Buffer = false;
                    long fileLength = myFile.Length;
                    long startBytes = 0L;
                    int pack = 0x2800;
                    int sleep = ((int) Math.Floor((1000.0 * pack) / ((double) _speed))) + 1;
                    if (_Request.Headers["Range"] != null)
                    {
                        _Response.StatusCode = 0xce;
                        startBytes = Convert.ToInt64(_Request.Headers["Range"].Split(new char[] { '=', '-' })[1]);
                    }
                    _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes != 0L)
                    {
                        _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1L, fileLength));
                    }
                    _Response.AddHeader("Connection", "Keep-Alive");
                    _Response.ContentType = "application/octet-stream";
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, Encoding.UTF8));
                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = ((int) Math.Floor(((double) (fileLength - startBytes)) / ((double) pack))) + 1;
                    for (int i = 0; i < maxCount; i++)
                    {
                        if (_Response.IsClientConnected)
                        {
                            _Response.BinaryWrite(br.ReadBytes(pack));
                            Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static void WriteFile(string FullFilePath, string Content)
        {
            FileStream fsOutput = null;
            StreamWriter srOutput = null;
            try
            {
                fsOutput = new FileStream(FullFilePath, FileMode.Create, FileAccess.Write);
                new StreamWriter(fsOutput, Encoding.GetEncoding(950)).WriteLine(Content);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                srOutput.Close();
                fsOutput.Close();
                srOutput = null;
                fsOutput = null;
            }
        }
    }
}

