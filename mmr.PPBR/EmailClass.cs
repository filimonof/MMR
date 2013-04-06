using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;
using vit.progress;
using mmr.share;

namespace mmr.share.PPBR
{
    /// <summary>
    /// Ошибки при отправке или получении почты  ППБР
    /// </summary>
    public class EmailException : Exception
    {
        public EmailException() : base() { }
        public EmailException(string message) : base(message) { }
        public EmailException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Отправка почты с ППБР
    /// </summary>
    public class EmailClass_Send
    {
        #region SendMail - отправка почты
        /// <summary>
        /// отправка почты
        /// </summary>
        /// <param name="connectionString">строка подключения к базе</param>
        /// <param name="files">вложенные файлы</param>
        public static void SendMail(string connectionString, List<string> files)
        {
            // отправить почту
            string emailReply;
            string emailSender;
            string emailSubject;
            string smtpServer;
            int smtpPort;
            int timeOut;
            using (ParameterClass<ParameterShareEnum> param = new ParameterClass<ParameterShareEnum>(connectionString))
            {
                param.Open();
                emailSender = param.Get(ParameterShareEnum.PPBR_Email_Sender, string.Empty);
                emailReply = param.Get(ParameterShareEnum.PPBR_Email_Replay, string.Empty);
                emailSubject = param.Get(ParameterShareEnum.PPBR_Email_Subject, string.Empty);
                smtpServer = param.Get(ParameterShareEnum.PPBR_SMTP_Server, string.Empty);
                smtpPort = param.Get(ParameterShareEnum.PPBR_SMTP_Port, 0);
                timeOut = param.Get(ParameterShareEnum.PPBR_Email_Timeout, 60);
                if (emailReply == string.Empty) throw new EmailException("В настройках не указаны адреса на которые пересылать макет ППБР");
                if (smtpServer == string.Empty) throw new EmailException("В настройках не указан SMTP сервер");
                if (smtpPort == 0) throw new EmailException("В настройках не указан SMTP порт");
            }

            MailMessage msg = new MailMessage(emailSender, emailReply, emailSubject, emailSubject);
            try
            {
                foreach (string fl in files)
                    msg.Attachments.Add(new Attachment(fl));
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                smtpClient.Timeout = timeOut * 1000;
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                throw new EmailException(string.Format("Ошибка при отправке почты с макетом ППБР:\n {0}", ex.Message));
            }
            finally
            {
                msg.Dispose();
            }

        }

        /* //CreateMessageWithAttachment
        public static void CreateMessageWithAttachment(string server)
        {
            // Specify the file to be attached and sent.
            // This example assumes that a file named Data.xls exists in the
            // current working directory.
            string file = "data.xls";
            // Create a message and set up the recipients.
            MailMessage message = new MailMessage(
               "jane@contoso.com",
               "ben@contoso.com",
               "Quarterly data report.",
               "See the attached spreadsheet.");

            // Create  the file attachment for this e-mail message.
            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            // Add the file attachment to this e-mail message.
            message.Attachments.Add(data);
            //Send the message.
            SmtpClient client = new SmtpClient(server);
            // Add credentials if the SMTP server requires them.
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            client.Send(message);
            // Display the values in the ContentDisposition for the attachment.
            ContentDisposition cd = data.ContentDisposition;
            Console.WriteLine("Content disposition");
            Console.WriteLine(cd.ToString());
            Console.WriteLine("File {0}", cd.FileName);
            Console.WriteLine("Size {0}", cd.Size);
            Console.WriteLine("Creation {0}", cd.CreationDate);
            Console.WriteLine("Modification {0}", cd.ModificationDate);
            Console.WriteLine("Read {0}", cd.ReadDate);
            Console.WriteLine("Inline {0}", cd.Inline);
            Console.WriteLine("Parameters: {0}", cd.Parameters.Count);
            foreach (DictionaryEntry d in cd.Parameters)
            {
                Console.WriteLine("{0} = {1}", d.Key, d.Value);
            }
            data.Dispose();
        }
        */

        #endregion
    }

    /// <summary>
    /// Получение почты
    /// </summary>
    public class EmailClass_Get: ProgressBaseClass
    {
        /// <summary>список загруженых писем</summary>
        private List<int> _logsID;

        #region Конструкторы
        public EmailClass_Get() : this(null) { }

        public EmailClass_Get(ProgressDlg progress)
            : base(progress)
        {
            this._logsID = new List<int>();
        }
        #endregion

        #region GetMail - Получение почты
        /// <summary>
        /// Получение почты с макетами
        /// </summary>
        /// <param name="connectionString">строка подключения к БД</param>
        public void GetMail(string connectionString)
        {
            this.WriteComment("Чтение настроек почты");
            string email;
            string emailPassword;
            string emailSubject;
            string popServer;
            int popPort;
            int timeOut;
            bool isEmailDeleted;
            string templateFilenameMaket;
            using (ParameterClass<ParameterShareEnum> param = new ParameterClass<ParameterShareEnum>(connectionString))
            {
                param.Open();
                email = param.Get(ParameterShareEnum.PPBR_Email, string.Empty);
                emailPassword = param.Get(ParameterShareEnum.PPBR_Email_Password, string.Empty);
                emailSubject = param.Get(ParameterShareEnum.PPBR_Email_Subject, string.Empty);
                popServer = param.Get(ParameterShareEnum.PPBR_POP3_Server, string.Empty);
                popPort = param.Get(ParameterShareEnum.PPBR_POP3_Port, 0);
                timeOut = param.Get(ParameterShareEnum.PPBR_Email_Timeout, 0);
                isEmailDeleted = param.Get(ParameterShareEnum.PPBR_Email_DeletedAfterLoad, false);
                templateFilenameMaket = param.Get(ParameterShareEnum.PPBR_FileName_Template, string.Empty);
                if (email == string.Empty) throw new EmailException("В настройках не почтовый ящик на который приходят макеты ППБР");
                if (popServer == string.Empty) throw new EmailException("В настройках не указан POP3 сервер");
                if (popPort == 0) throw new EmailException("В настройках не указан POP3 порт");
                if (templateFilenameMaket == string.Empty) throw new EmailException("Неверный шаблон файлов вложения");
            }

            this.WriteComment("Подключение к почтовому ящику");
            OpenPOP.POP3.POPClient popClient = new OpenPOP.POP3.POPClient();
            try
            {
                popClient.SendTimeOut = timeOut * 1000;
                popClient.ReceiveTimeOut = timeOut * 1000;
                popClient.Connect(popServer, popPort);
                popClient.Authenticate(email, emailPassword, OpenPOP.POP3.AuthenticationMethod.USERPASS);

                this.WriteComment(string.Format("Сообщений почтовом ящике {0}", popClient.GetMessageCount()));
                int messageCount = popClient.GetMessageCount();
                if (this._progress != null)
                    this._progress.CountNext = messageCount;
                for (int numMail = 1; numMail <= messageCount; numMail++)
                {
                    OpenPOP.MIMEParser.Message msg = popClient.GetMessage(numMail, false);
                    this.WriteComment(string.Format("Письмо №{0} от {1}<{2}>", numMail, msg.From, msg.FromEmail));

                    //является ли письмо нашим макетом
                    bool isTrustMail = false;

                    //проверка письма идёт по теме (subject), допускается маска {...}                  
                    if (ParseUtils.MaskCompare(msg.Subject, emailSubject))
                    {
                        if (MailParsed.IsParsed(connectionString, msg.MessageID))
                        {
                            this.WriteComment("  письмо уже обрабатывалось ранее");
                            this.Next();
                            continue;
                        }

                        if (!msg.HasAttachment)
                        {
                            this.WriteComment("  нет вложенный макетов");
                            this.Next();
                            continue;
                        }

                        //смотрим все вложения
                        for (int numAttach = 1; numAttach < msg.AttachmentCount; numAttach++)
                        {
                            OpenPOP.MIMEParser.Attachment attach = msg.GetAttachment(numAttach);
                            //если вложенный файл удовлетворяет шаблону имени файла макета
                            if (ParseUtils.MaskCompare(attach.ContentFileName, templateFilenameMaket))
                            {
                                isTrustMail = true;
                                this.WriteComment("Загрузка макета " + attach.ContentFileName);
                                //сохранение вложения на диск
                                byte[] fileAttach = attach.DecodedAsBytes();
                                string filename = Path.Combine(Path.GetTempPath(), attach.ContentFileName);
                                if (File.Exists(filename)) File.Delete(filename);
                                if (fileAttach != null)
                                    File.WriteAllBytes(filename, fileAttach);

                                this.WriteComment("Запись в лог события");
                                int logID = 0;
                                try
                                {
                                    //извлекаем дату из файла                                    
                                    DateTime dateInFile = MaketClass.ParseDateInMaket(connectionString, filename);
                                    //записываем событие в лог
                                    logID = LogClass.Add(connectionString, dateInFile);
                                    this.WriteComment(string.Format("Событие №{0} от {1}", logID, dateInFile.ToShortDateString()));
                                }
                                catch (Exception Ex)
                                {
                                    this.WriteComment(string.Format("Произошла ошибка при записи события: \n {0}", Ex.Message));
                                    this.WriteComment("Загрузка отменена");
                                    continue;
                                }

                                MaketClass parse = new MaketClass(connectionString, filename, logID, this._progress);
                                try
                                {
                                    parse.Parse();
                                }
                                catch (MaketException Ex)
                                {
                                    this.WriteComment(Ex.Message);
                                    this.WriteComment("Загрузка отменена");
                                    parse.ParseRollback(Ex.Message);
                                    continue;
                                }
                                catch (Exception Ex)
                                {
                                    string error = string.Format("Произошла ошибка при загрузке файла макета ППБР \"{0}\" в базу: \n {1}", filename, Ex.Message);
                                    this.WriteComment(error);
                                    this.WriteComment("Загрузка отменена");
                                    parse.ParseRollback(error);
                                    continue;
                                }
                                finally
                                {
                                    parse.Dispose();
                                    try
                                    {
                                        if (File.Exists(filename)) File.Delete(filename);
                                    }
                                    catch { }
                                }

                                //если все нормаотьно запоминаем logID загруженого
                                this._logsID.Add(logID);

                            } // MaskCompare attach filename
                            else
                            {
                                this.WriteComment(string.Format("  вложение {0} не вляется макетом", attach.ContentFileName));
                            }
                        } //цикл по всем вложениям 

                        if (isTrustMail && isEmailDeleted) //если были письма с вложенным макетом и в настройках стоит удалять после загрузки
                        {
                            this.WriteComment("Удаление сообщения из почтового ящика");
                            popClient.DeleteMessage(numMail);
                        }

                        this.WriteComment("Добавляем письмо в список обработанных");
                        MailParsed.Add(connectionString, msg.MessageID);

                    }//MaskCompare subject
                    else
                    {
                        this.WriteComment("  тема письма неправильная");
                    }
                    this.Next();
                }
                this.WriteComment("Закрытие почтового ящика");
            }
            catch (OpenPOP.POP3.PopServerNotFoundException)
            {
                throw new EmailException(string.Format("Сервер почты ({0}:{1}) не отвечает", popServer, popPort));
            }
            catch (OpenPOP.POP3.InvalidLoginOrPasswordException)
            {
                throw new EmailException(string.Format("Неверный логин или пароль пользователя {0}", email));
            }
            catch (OpenPOP.POP3.InvalidLoginException)
            {
                throw new EmailException(string.Format("Неверный логин пользователя {0}", email));
            }
            catch (OpenPOP.POP3.InvalidPasswordException)
            {
                throw new EmailException(string.Format("Неверный пароль пользователя {0}", email));
            }
            catch (Exception ex)
            {
                throw new EmailException(string.Format("Ошибка при получении почты с макетом ППБР:\n {0}", ex.Message));
            }
            finally
            {
                if (popClient.Connected) popClient.Disconnect();
            }
        }
        #endregion

        #region GetTryLoadedID - список загруженных писем
        /// <summary>
        /// список загруженных писем
        /// </summary>
        /// <returns>список с ID</returns>
        public List<int> GetTryLoadedID()
        {
            return this._logsID;
        }
        #endregion

        #region Dispose
        private bool _disposed = false;
        protected override void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {                  
                    // освобождаем управляемые ресурсы
                }
                // освобождаем неуправляемые ресурсы
                base.Dispose(disposing);
                this._disposed = true;
            }
        }
        #endregion
    }
}
