using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DomObjectImport.WorkClas
{
    class excelvertibas
    {
        static public List<string> getexternalid { get; set; }
        static public DataTable DataTableExcel { get; set; }
        static public string Pathtoexcel { get; set; }

        static public string username { get; set; }
        static public string password { get; set; }

        static public string sheetname { get; set; }

        static public string SourcePath { get; set; }
        
        static public List<string> errorlist { get; set; }

        static public bool verify_only { get; set; }
        static public int Maximum_Progr { get; set; }

        static public DataTable exceltabula { get; set; }

        static public bool rowcoloring { get; set; }
    }

    class serverurl
    {
        static public string serviceurl { get; set; }

    }

    class sendfiles
    {
        public static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

    


        public static string ChunkRequest(int chunkNumber, byte[] buffer, int? DomID, string username, string password, string baseAddres, int? fileId)
        {

            // string errorString = HelperClass.ChunkRequest(i, bytes, DomID, username, password, baseAddress, domResponseA.FileId);

            // AddFileChunkResponse ChunkResponse = new AddFileChunkResponse();
            SimpleResponse ChunkResponse = new SimpleResponse();

            // baseAddres = "https://svc.proc.test.lndb.lv/";

            bool temp = false;

            // Uri uri = new Uri(baseAddres + "AddFileChunk?objectId=" + DomID + "&chunkNumber=" + chunkNumber);
            Uri uri = new Uri(baseAddres + "AddFileChunk?fileId=" + fileId);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            request.UseDefaultCredentials = true;
            string credentials1 = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
            request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials1;

            request.Method = "POST";
            request.Timeout = 10000;

            request.ContentLength = buffer.Length;
            using (var reqStream = request.GetRequestStream())
            {
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();
            }

            string errorMesage = "";
            string responseString = "";
            int statusCode = 0;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                statusCode = (int)response.StatusCode;

                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                responseString = reader.ReadToEnd();

                ChunkResponse = JsonConvert.DeserializeObject<SimpleResponse>(responseString);

                temp = ChunkResponse.OK;

                if (ChunkResponse.Errors != null)
                {
                    foreach (var m in ChunkResponse.Errors)
                    {
                        errorMesage += m.ToString();
                    }
                }

            }

            if (temp == true)
            {
                return errorMesage;
            }
            else
            {
                return errorMesage;
            }

        }


    }
}
