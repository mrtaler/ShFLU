using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            String MyURI = "http://localhost:8888/crypt.php";
            var xxxre = File.ReadAllText("XMLFile1.xml", Encoding.UTF8);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(MyURI);

            StringBuilder postData = new StringBuilder();
            string sid = "3c76a7bb-3734-4fdb-ad74-b41623cb2393";
            string oper = "sign";
            string doc = "zag";
            string eid = "0";
            string srv = "https://ep.isc.by/ep-test/auth.do";

            postData.Append($"sid={sid}&");
            postData.Append($"oper={oper}&");
            postData.Append($"doc={doc}&");
            postData.Append($"eid={eid}&");
            postData.Append($"srv={srv}&");
            postData.Append($"xmldoc={xxxre}");

            byte[] byteArray = Encoding.UTF8.GetBytes(postData.ToString());

            request.Method = "POST";
            request.ContentLength = byteArray.Length;
            //  request.ClientCertificates.Add(xc);
            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            request.ContentType = "application/x-www-form-urlencoded";

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);


            dataStream.Close();
             WebResponse response = request.GetResponse();

              Console.WriteLine(((HttpWebResponse)response).StatusDescription);
              using (StreamReader stream = new StreamReader(
       response.GetResponseStream(), Encoding.UTF8))
              {
                  Console.WriteLine(stream.ReadToEnd());
              }

            Console.ReadKey();

        }
    }
}
