using Abp.Dependency;
using AbpNet8.Configuration;
using Admin.Helper.Queue;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Admin.Shared.Helper.Queue
{
    public class QueueHelper : ITransientDependency
    {
        //private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;
        private string HostName;
        private int Port;
        private string QueueSendMail;
        private string UserName;
        private string Password;
        private string QueueDongBo;
        public QueueHelper(IWebHostEnvironment env)
        {
            //_env = env;
            _appConfiguration = env.GetAppConfiguration();
            HostName = _appConfiguration["QueueConfiguration:HostName"];
            Port = int.Parse(_appConfiguration["QueueConfiguration:Port"]);
            QueueSendMail = _appConfiguration["QueueConfiguration:QueueSendMail"];
            UserName = _appConfiguration["QueueConfiguration:UserName"];
            Password = _appConfiguration["QueueConfiguration:Password"];
            QueueDongBo = string.IsNullOrEmpty(_appConfiguration["QueueConfiguration:QueueDongBo"]) ? "DongBoQLHoaDon" : _appConfiguration["QueueConfiguration:QueueDongBo"];
        }
        public void SendMail(string json)
        {
            //JsonConvert.SerializeObject(SendMailDto
            var factory = new ConnectionFactory()
            {
                HostName = HostName,
                Port = Port,
                UserName = UserName,
                Password = Password,
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QueueSendMail,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(json);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.BasicPublish(exchange: "",
                                     routingKey: QueueSendMail,
                                     basicProperties: properties,
                                     body: body);
            }
        }

        public void DongBoQueue(Service_DongBoDto input)
        {
            var factory = new ConnectionFactory()
            {
                HostName = HostName,
                Port = Port,
                UserName = UserName,
                Password = Password,
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QueueDongBo,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var json = JsonConvert.SerializeObject(input);
                var body = Encoding.UTF8.GetBytes(json);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.BasicPublish(exchange: "",
                                     routingKey: QueueDongBo,
                                     basicProperties: properties,
                                     body: body);
            }
        }

        public byte[] ZipByteArray(byte[] b)
        {
            using (var compressedFileStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                {
                    var zipEntry = zipArchive.CreateEntry("data.xml");

                    using (var originalFileStream = new MemoryStream(b))
                    using (var zipEntryStream = zipEntry.Open())
                        originalFileStream.CopyTo(zipEntryStream);
                }
                return compressedFileStream.ToArray();
            }
        }
    }
}
