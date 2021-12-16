using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace InternalCargoWiseReport.Data.Internal
{
    public class SendFileToServer
    {

        public static string destination = @"/upload/E_1/";
        public static string host = "sftp2.em2.cloud.oracle.com";
        public static string username = "ekTOBP64";
        public static string password = "Apollo_217";
        public static int port = 22;

        public static int Send(string fileName)
        {
            try
            {
                using (SftpClient client = new SftpClient(host, port, username, password))
                {
                    client.Connect();
                    client.ChangeDirectory(destination);
                    using (FileStream fs = new FileStream(fileName, FileMode.Open))
                    {
                        client.BufferSize = 4 * 1024;
                        client.UploadFile(fs, Path.GetFileName(fileName));
                    }
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

    }
}
