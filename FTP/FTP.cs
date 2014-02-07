namespace Tools.FTP
{

public class FTP
	{
		public string Host { get; set; }
		public string Login { get; set; }
		public string Pwd { get; set; }
		public Mode Mode { get; set; }
		public Action<string> LogDelegate { get; set; }

		string _distantPath;
		public string DistantPath
		{
			get
			{
				return _distantPath;
			}
		}

		private static void SetMethodRequiresCWD()
		{
			Type requestType = typeof(FtpWebRequest);
			FieldInfo methodInfoField = requestType.GetField("m_MethodInfo", BindingFlags.NonPublic | BindingFlags.Instance);
			Type methodInfoType = methodInfoField.FieldType;


			FieldInfo knownMethodsField = methodInfoType.GetField("KnownMethodInfo", BindingFlags.Static | BindingFlags.NonPublic);
			Array knownMethodsArray = (Array)knownMethodsField.GetValue(null);

			FieldInfo flagsField = methodInfoType.GetField("Flags", BindingFlags.NonPublic | BindingFlags.Instance);

			int MustChangeWorkingDirectoryToPath = 0x100;
			foreach (object knownMethod in knownMethodsArray)
			{
				int flags = (int)flagsField.GetValue(knownMethod);
				flags |= MustChangeWorkingDirectoryToPath;
				flagsField.SetValue(knownMethod, flags);
			}
		}

		public OperationResult<NoType> PushFile(string localFilePath, string distantDirectory)
		{
			return Mode == Mode.Sftp ? PushFileSFTP(localFilePath, distantDirectory) : PushFileFTP(localFilePath, distantDirectory);
		}

		private OperationResult<NoType> PushFileSFTP(string localFilePath, string distantDirectory)
		{
			if (!distantDirectory.StartsWith("/"))
				distantDirectory = string.Concat("/", distantDirectory);
			string distantPath = string.Format("ftp://{0}{1}", Host, distantDirectory);
			LogDelegate(string.Format("[FTP] Distant path: {0}", distantPath));
			try
			{
				SftpClient sftpClient = new SftpClient(new PasswordConnectionInfo(Host, 22, Login, Pwd));
				//new SftpClient(Host, 22, Login, Pwd)

				using (var sftp = sftpClient)
				{
					sftp.Connect();
					var sftpFiles = sftp.ListDirectory(distantDirectory);

					using (FileStream fs = File.Create(localFilePath))
					{
						sftp.UploadFile(fs, distantDirectory, null);
					}
				}
				return OperationResult<NoType>.OkResult;

			}
			catch (Exception e)
			{
				WebException we = (WebException)e;
				String status = ((FtpWebResponse)we.Response).StatusDescription;
				if (LogDelegate != null)
				{
					LogDelegate("[FTP] Exception envoi : " + we.Message + " /// " + status);
					Mailer mailer = new Mailer();
					mailer.LogDelegate = LogDelegate;

					string emailConf = Common.NotificationEmailsToFromConfig();
					mailer.SendMail(emailConf, "[Moulinette Canal Collecte] Erreur !", e.Message + " " + e.StackTrace, null);
				}
				return OperationResult<NoType>.BadResultFormat("[FTP] Exception envoi: {0} /// {1}", we.Message, we.Status);
			}
		}


		private OperationResult<NoType> PushFileFTP(string localFilePath, string distantDirectory)
		{
			if (!distantDirectory.StartsWith("/"))
				distantDirectory = string.Concat("/", distantDirectory);
			string distantPath = string.Format("ftp://{0}{1}", Host, distantDirectory);
			LogDelegate(string.Format("[FTP] Distant path: {0}", distantPath));
			try
			{
				SetMethodRequiresCWD();
				FtpWebRequest request = (FtpWebRequest)WebRequest.Create(distantPath);
				request.Method = WebRequestMethods.Ftp.UploadFile;
				request.KeepAlive = false;
				request.UsePassive = true;
				//request.

				// This example assumes the FTP site uses anonymous logon.
				request.Credentials = new NetworkCredential(Login, Pwd);

				// Copy the contents of the file to the request stream.
				StreamReader sourceStream = new StreamReader(localFilePath, true);
				byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
				fileContents = Encoding.UTF8.GetPreamble().Concat(fileContents).ToArray();
				//byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
				sourceStream.Close();
				request.ContentLength = fileContents.Length;

				Stream requestStream = request.GetRequestStream();
				//requestStream.
				requestStream.Write(fileContents, 0, fileContents.Length);
				requestStream.Close();

				FtpWebResponse response = (FtpWebResponse)request.GetResponse();
				LogDelegate("response : " + response.StatusCode + " " + response.StatusDescription);
				//Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

				response.Close();
				return OperationResult<NoType>.OkResult;

			}
			catch (Exception e)
			{
				WebException we = (WebException)e;
				String status = ((FtpWebResponse)we.Response).StatusDescription;
				if (LogDelegate != null)
				{
					LogDelegate("[FTP] Exception envoi : " + we.Message + " /// " + status);
					Mailer mailer = new Mailer();
					mailer.LogDelegate = LogDelegate;

					string emailConf = Common.NotificationEmailsToFromConfig();
					mailer.SendMail(emailConf, "[Moulinette Canal Collecte] Erreur !", e.Message + " " + e.StackTrace, null);
				}
				return OperationResult<NoType>.BadResultFormat("[FTP] Exception envoi: {0} /// {1}", we.Message, we.Status);
			}
		}
	}

	public enum Mode
	{
		Ftp,
		Sftp
	}

}