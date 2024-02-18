// See https://aka.ms/new-console-template for more information

using digitalpost;
using Dk.Digst.Digital.Post.Memolib.Builder;
using Dk.Digst.Digital.Post.Memolib.Model;
using Dk.Digst.Digital.Post.Memolib.Parser;
using Dk.Digst.Digital.Post.Memolib.Writer;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

var certificatePath = @"c:\users\blasi\Desktop\SJKPConsulting.p12";

// Create a new instance of the X509Certificate2 class using the PEM certificate file
var certificate = new X509Certificate2(certificatePath, "PASS FOR PFX/p12 cert");

var handler = new HttpClientHandler();

// Add the certificate to the ClientCertificates collection of the WebRequestHandler
handler.ClientCertificates.Add(certificate);


var client = new HttpClient(handler);

// Replace Auth value with your Auth value
client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "OGVjNWMyNjEtMWZlYi00ZmIyLWJmMWItYTdmNjIxZmFmN2M2OjUxMTYwOGE1LTE4ZjQtNDFmYS1iNmM1LWNjOTM4NmUyN2MzMA==");



var dgClient = new DigitalPostClient(client);
dgClient.BaseUrl = "https://api.digitalpost.dk/apis/v1";

//var res = await dgClient.QueryContactsAsync(null, null, null, null, null, null, null, null, null, null, null, null);
//Console.WriteLine(JsonSerializer.Serialize(res));


var memos = await dgClient.ListAvailableMemosAsync(null, 1000, null);
Console.WriteLine(JsonSerializer.Serialize(memos));

// We cannot send from a private CVR as that is not allowed
//Guid memoUuid = Guid.NewGuid();
//var memoBuilder = new MemoBuilder();
//FileStream stream = memoBuilder.CreateMemoFile(memoUuid);

//var res1 = await dgClient.SendMemoAsync(memoUuid, stream.Length, new FileParameter(stream, $"{memoUuid}.xml", "application/xml"));
//Console.WriteLine(JsonSerializer.Serialize(res1));

//var res = await dgClient.ListAvailableReceiptsAsync(new ReceiptSearchCommand()
//{
//    Size = 100,
//    Page = 1,
//});
//Console.WriteLine(JsonSerializer.Serialize(res));
//foreach (var receipt in res.Content)
//{

//    Console.WriteLine("------------------------------------------");
//    var res2 = await dgClient.FetchReceiptAsync(receipt, false);

//    Console.WriteLine(JsonSerializer.Serialize(res2));
//}

Console.ReadLine();


foreach (var memo in memos.Content)
{
    var response = await dgClient.FetchMemoAsync(memo);
    Console.WriteLine(memos.TotalElements);
    Console.WriteLine(JsonSerializer.Serialize(memos));
    
    using (IMeMoParser parser = MeMoParserFactory.CreateParser(response.Stream, true))
    {
        var message = parser.Parse();
        var file = message.MessageBody.MainDocument.File.FirstOrDefault();
        if (file != null)
        {

            System.IO.File.WriteAllBytes(file.filename, file.content);

            Decode.DecodeFromFile(file.filename, "decoded_" + file.filename);
        }        
    }

}
Console.ReadLine();


