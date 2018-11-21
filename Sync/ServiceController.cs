using iSync.EAI;
using Microsoft.Xml.XMLGen;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace MsgApp.Controllers
{
    public class ServiceController
    {
        // Managers etc.
        MessageServiceClient Client = new MessageServiceClient();
        XmlDocument xmlDocument = new XmlDocument();

        // Move to Web.config ASAP
        readonly string logPath = @"C:\Users\Dev-2018-Oct-PC\Desktop\Colmart\RepoVPS\Colmart\Colmart\Sync\Log.txt";
        private readonly string root = @"C:\Users\Dev-2018-Oct-PC\Desktop\Colmart\RepoVPS\Colmart\Colmart\Sync\";

        public void clsMessageServiceClient(int operation)
        {
            // TODO: Move this to secrets ASAP
            Client.Login("web", "Password123b");

            // Variables
            var ops = Client.GetAvailableOperations().ToList();
            var Type = ops[operation];
            var act = string.Empty;

            switch (operation)
            {
                case 15: // "PersistWebSalesOrder"
                    act = "INSERT";
                    break; 
                case 16: // "GetWebSalesOrder"
                    act = "OTHER";
                    break;
                default:
                    Debug.WriteLine("XMLPUSHER ERROR");
                    break;
            }

            // GENERATE THE DOCUMENT


            // Populate the doc


            xmlDocument = GenerateXmlFromXsd(Type);

            // SETUP AND CREATE THE REQUEST
            var request = new Request
            {
                Action = act,                      // Compulsory ./
                Identifier = "0",                       // Optional consider using for BRANCH
                Instance = "0",                         // Optional consider using for ?
                MessageType = Type,                     // Compulsory add layer of abstraction 
                MessageXml = xmlDocument.OuterXml,      // Compulsory ./
                PublishDate = DateTime.Now,             // Compulsory ./
                SessionKey = "key",                     // TODO: Grab the current web-session key
                Source = "0"                            // Optional consider using for Location?
            };


            // This executes the current request
            var response = Client.Execute(request);

            Debug.WriteLine(PushLogger(logPath, response, request, Type, operation));
        }

        private XmlDocument GenerateXmlFromXsd(string opperation)
        {
            var xsdPath = $@"{root}Schemas\{opperation}.xsd";
            var xmlPath = $@"{root}XML\{opperation}.xml";

            var textWriter = new XmlTextWriter(xmlPath, null);
            textWriter.Formatting = Formatting.Indented;
            var qname = new XmlQualifiedName("PurchaseOrder", "http://tempuri.org");
            var generator = new XmlSampleGenerator(xsdPath, qname);
            generator.WriteXml(textWriter);

            return xmlDocument;
        }

        public string PushLogger(string path, Response response, Request request, string type, int opp)
        {
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path).Dispose();

                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine("Colmart Polling log:");
                    tw.Close();
                    PushLogger(path, response, request, type, opp);
                }
            }
            else if (System.IO.File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine($"{DateTime.Now} : =============================== {type} =============================== New poll {opp}:");
                    tw.WriteLine($"Common: ");
                    tw.WriteLine($"      Action - {request.Action}");
                    tw.WriteLine($" MessageType - {request.MessageType}");
                    tw.WriteLine($"      Source - {request.Source}");
                    tw.WriteLine($"  Identifier - {request.Identifier}");
                    tw.WriteLine($"    Instance - {request.Instance}");
                    tw.WriteLine($" PublishDate - {request.PublishDate.ToString()}");
                    tw.WriteLine($"Sent =================================================== {opp}:");
                    tw.WriteLine($"  MessageXml - {request.MessageXml}");
                    tw.WriteLine($"  SessionKey - {request.SessionKey}");
                    tw.WriteLine($"Received =============================================== {opp}:");
                    tw.WriteLine($"ResponseText - {response.ResponseText}");
                    tw.WriteLine($"   ResultXml - {response.ResultXml}");
                    tw.WriteLine($" Successflag - {response.Successflag.ToString()}");
                    tw.WriteLine($"{DateTime.Now} : =============================== {type} =============================== End poll {opp}:\r\n\r\n");
                }
            }
            return "Logged";
        }
    }
}