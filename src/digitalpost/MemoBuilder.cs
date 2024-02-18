using System;
using System.IO;
using System.Text;
using Dk.Digst.Digital.Post.Memolib.Builder;
using Dk.Digst.Digital.Post.Memolib.Container;
using Dk.Digst.Digital.Post.Memolib.Factory;
using Dk.Digst.Digital.Post.Memolib.Model;
using Dk.Digst.Digital.Post.Memolib.Writer;
using File = Dk.Digst.Digital.Post.Memolib.Model.File;
using Message = Dk.Digst.Digital.Post.Memolib.Model.Message;

namespace digitalpost
{
      

    public class MemoBuilder
    {


        public MemoBuilder()
        {
        }

        public Dk.Digst.Digital.Post.Memolib.Model.Message CreateMemo(Guid memoUuid)
        {
            Dk.Digst.Digital.Post.Memolib.Model.Message message = BuildMemo(memoUuid);

            return message;
        }

        public FileStream CreateMemoFile(Guid memoUuid)
        {
            Dk.Digst.Digital.Post.Memolib.Model.Message message = BuildMemo(memoUuid);

            return SaveMemo(message);
        }

        public FileStream SaveMemo(Dk.Digst.Digital.Post.Memolib.Model.Message message)
        {
            FileStream memoFile = CreateNewFile(GetFileName(message.MessageHeader.messageUUID));
            IMeMoStreamWriter writer = MeMoWriterFactory.CreateWriter(memoFile);
            writer.Write(message);
            memoFile.Position = 0;

            return memoFile;
        }

        private string GetFileName(string messageUuid)
        {
            return $"{messageUuid}.xml";
        }

        public FileStream CreateNewFile(string fileName)
        {
            FileStream file = System.IO.File.Create($"{fileName}");
            return file;
        }

        //public FileStream CreateMemoTar()
        //{
        //    string fileName = Guid.NewGuid() + ".tar.lzma";
        //    FileStream fileStream = memoPersister.CreateNewFile(fileName);

        //    MeMoContainerWriter writer = new MeMoContainerWriter(new MeMoContainerOutputStream(fileStream));
        //    for (int i = 0; i < memoConfiguration.NumberOfMemos; i++)
        //    {
        //        Message message = BuildMemo(Guid.NewGuid());
        //        memoPersister.WriteTarEntry(writer, message);
        //    }

        //    writer.Dispose();
        //    fileStream.Dispose();
        //    return memoPersister.OpenFile(fileName);
        //}

        private Dk.Digst.Digital.Post.Memolib.Model.Message BuildMemo(Guid memoUuid)
        {
            return MessageBuilder.NewBuilder()
                .MessageHeader(
                    MessageHeaderBuilder.NewBuilder()
                        .MessageType(memoMessageType.DIGITALPOST)
                        .MessageUUID(memoUuid)
                        .Sender(BuildSender())
                        .Recipient(BuildRecipient())
                        .Notification("advis tekst").Label("titel på meddelelse")
                        .Mandatory(false) //not allowed as private company
                        .LegalNotification(false).Build()) //not allowed as private company
                .MessageBody(BuildMessageBody())
                .Build();
        }

        private MessageBody BuildMessageBody()
        {
            return MessageBodyBuilder.NewBuilder()
                .CreatedDateTime(DateTime.UtcNow.ToLocalTime())
                .MainDocument(BuildMainDocumentation())
                .Build();
        }

        private Dk.Digst.Digital.Post.Memolib.Model.Sender BuildSender()
        {
            return SenderBuilder.NewBuilder()
                .SenderId("34485771")
                .IdType("cvr")
                .Label("afsendernavn")
                .Build();
        }

        private Dk.Digst.Digital.Post.Memolib.Model.Recipient BuildRecipient()
        {
            return RecipientBuilder.NewBuilder()
                .RecipientID("34485771")
                .IdType("cvr")
                //.RecipientID("mail@sjkp.dk")
                //.IdType("email")
                .Build();
        }

        private MainDocument BuildMainDocumentation()
        {
            return MainDocumentBuilder.NewBuilder()
                .DocumentID("123")
                .Label("Hoveddokument")
                .AddFile(BuildFile())
                .Build();
        }

        private Dk.Digst.Digital.Post.Memolib.Model.File BuildFile()
        {
            return FileBuilder.NewBuilder()
                .Language("da")
                .EncodingFormat("text/plain")
                .Filename("hovedfil.txt")
                .Content(FileContentBuilder.NewBuilder()
                    .Base64Content("RGVyIGVyIGluZ2VuIGdydW5kIHRpbCBhdCBsw6ZzZSBkZXQgaGVyLCBkZXIgc3TDpXIgaWtrZSBub2dldCBpbnRlcmVzc2FudA==").Build())
                .Build();
        }
    }
}

