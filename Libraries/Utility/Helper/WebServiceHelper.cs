using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace Utility.Helper
{
    public class WebServiceHelper
    {
       /// <summary>
       ///  Code done for all Service Data Process in XML Format Post
       /// </summary>
       /// <param name="destinationUrl"></param>
       /// <param name="model"></param>
       /// <param name="methodType"></param>
       /// <returns></returns>
        public string XMLDataProcess(string destinationUrl, dynamic model, string methodType)
        {
            string requestXMLData = string.Empty;
            using (var stringwriter = new StringWriter())
            {
                var serializer = new XmlSerializer(model.GetType());
                serializer.Serialize(stringwriter, model);
                requestXMLData = stringwriter.ToString();
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);
            byte[] bytes;
            bytes = Encoding.ASCII.GetBytes(requestXMLData);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = methodType;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            else
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
        }
    }
}
