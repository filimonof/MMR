//OpenPOP.Net 
#region Examples
/*
 OpenPOP.POP3.POPClient popClient = new OpenPOP.POP3.POPClient(
    "pop.mail.ru", 110, "xxx@mail.ru", "xxx", 
    OpenPOP.POP3.AuthenticationMethod.TRYBOTH);
try
{
    Console.WriteLine(string.Format("Message count {0}", popClient.GetMessageCount()));
    for (int i = 1; i <= popClient.GetMessageCount(); i++)
    {                    
        OpenPOP.MIMEParser.Message msg = popClient.GetMessage(i, false);

        msg.GetMessageBody(msg.RawMessageBody);
        string text = msg.MessageBody.Count != 0 ? msg.MessageBody[0].ToString() : string.Empty;
        byte[] data = Encoding.Default.GetBytes(text);
        string body = Encoding.GetEncoding("koi8-r").GetString(data);


        for (int j = 1; j <= msg.AttachmentCount - 1; j++)
        {                        
            OpenPOP.MIMEParser.Attachment attach = msg.GetAttachment(j);                          
            byte[] fileAttach = attach.DecodedAsBytes();
            if (fileAttach != null)
                File.WriteAllBytes(string.Format(@"C:\{0}", attach.ContentFileName), fileAttach);
        }

        Console.WriteLine(string.Format("Message nummber {0}", i));
        Console.WriteLine(string.Format("ID {0}", msg.MessageID));
        Console.WriteLine(string.Format("Subject {0}", msg.Subject));
        Console.WriteLine(string.Format("Body {0}", body));
    }
}
finally
{
   if (popClient.Connected)
        popClient.Disconnect();
}
 */
#endregion

namespace OpenPOP.MIMEParser
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Collections;
    using System.Globalization;

    /// <summary> 
    /// Summary description for Attachment. 
    /// </summary> 
    public class Attachment : IComparable
    {
        #region Member Variables

        private string _contentType = null;
        private string _contentCharset = null;
        private string _contentFormat = null;
        private string _contentTransferEncoding = null;
        private string _contentDescription = null;
        private string _contentDisposition = null;
        private string _contentFileName = "";
        private string _defaultFileName = "body.htm";
        private string _defaultFileName2 = "body*.htm";
        private string _defaultReportFileName = "report.htm";
        private string _defaultMIMEFileName = "body.eml";
        private string _defaultMSTNEFFileName = "winmail.dat";
        private string _contentID = null;
        private long _contentLength = 0;
        private string _rawAttachment = null;
        private bool _inBytes = false;
        private byte[] _rawBytes = null;

        #endregion

        #region Properties

        /// <summary> 
        /// raw attachment content bytes 
        /// </summary> 
        public byte[] RawBytes
        {
            get
            {
                return _rawBytes;
            }
            set
            {
                _rawBytes = value;
            }
        }

        /// <summary> 
        /// whether attachment is in bytes 
        /// </summary> 
        public bool InBytes
        {
            get
            {
                return _inBytes;
            }
            set
            {
                _inBytes = value;
            }
        }

        /// <summary> 
        /// Content length 
        /// </summary> 
        public long ContentLength
        {
            get
            {
                return _contentLength;
            }
        }

        /// <summary> 
        /// verify the attachment whether it is a real attachment or not 
        /// </summary> 
        /// <remarks>this is so far not comprehensive and needs more work to finish</remarks> 
        public bool NotAttachment
        {
            get
            {
                /*    if (_contentDisposition==null||_contentType==null) 
                     return true; 
                    else 
                     return (_contentDisposition.IndexOf("attachment")==-1 && _contentType.IndexOf("text/plain")!=-1); */
                /*    if (_contentType==null) 
                     return true; 
                    else 
                     return (_contentFileName!="");*/
                if ((_contentType == null || _contentFileName == "") && _contentID == null) //&&_contentType.ToLower().IndexOf("text/")!=-1) 
                    return true;
                else
                    return false;

            }
        }

        /// <summary> 
        /// Content format 
        /// </summary> 
        public string ContentFormat
        {
            get
            {
                return _contentFormat;
            }
        }

        /// <summary> 
        /// Content charset 
        /// </summary> 
        public string ContentCharset
        {
            get
            {
                return _contentCharset;
            }
        }

        /// <summary> 
        /// default file name 
        /// </summary> 
        public string DefaultFileName
        {
            get
            {
                return _defaultFileName;
            }
            set
            {
                _defaultFileName = value;
            }
        }

        /// <summary> 
        /// default file name 2 
        /// </summary> 
        public string DefaultFileName2
        {
            get
            {
                return _defaultFileName2;
            }
            set
            {
                _defaultFileName2 = value;
            }
        }

        /// <summary> 
        /// default report file name 
        /// </summary> 
        public string DefaultReportFileName
        {
            get
            {
                return _defaultReportFileName;
            }
            set
            {
                _defaultReportFileName = value;
            }
        }

        /// <summary> 
        /// default MIME File Name 
        /// </summary> 
        public string DefaultMIMEFileName
        {
            get
            {
                return _defaultMIMEFileName;
            }
            set
            {
                _defaultMIMEFileName = value;
            }
        }

        /// <summary> 
        /// Content Type 
        /// </summary> 
        public string ContentType
        {
            get
            {
                return _contentType;
            }
        }

        /// <summary> 
        /// Content Transfer Encoding 
        /// </summary> 
        public string ContentTransferEncoding
        {
            get
            {
                return _contentTransferEncoding;
            }
        }

        /// <summary> 
        /// Content Description 
        /// </summary> 
        public string ContentDescription
        {
            get
            {
                return _contentDescription;
            }
        }

        /// <summary> 
        /// Content File Name 
        /// </summary> 
        public string ContentFileName
        {
            get
            {
                return _contentFileName;
            }
            set
            {
                _contentFileName = value;
            }
        }

        /// <summary> 
        /// Content Disposition 
        /// </summary> 
        public string ContentDisposition
        {
            get
            {
                return _contentDisposition;
            }
        }

        /// <summary> 
        /// Content ID 
        /// </summary> 
        public string ContentID
        {
            get
            {
                return _contentID;
            }
        }

        /// <summary> 
        /// Raw Attachment 
        /// </summary> 
        public string RawAttachment
        {
            get
            {
                return _rawAttachment;
            }
        }

        /// <summary> 
        /// decoded attachment in bytes 
        /// </summary> 
        public byte[] DecodedAttachment
        {
            get
            {
                return DecodedAsBytes();
            }
        }

        #endregion

        /// <summary> 
        /// release all objects 
        /// </summary> 
        ~Attachment()
        {
            _rawBytes = null;
            _rawAttachment = null;
        }

        /// <summary> 
        /// New Attachment 
        /// </summary> 
        /// <param name="bytAttachment">attachment bytes content</param> 
        /// <param name="lngFileLength">file length</param> 
        /// <param name="strFileName">file name</param> 
        /// <param name="strContentType">content type</param> 
        public Attachment(byte[] bytAttachment, long lngFileLength, string strFileName, string strContentType)
        {
            _inBytes = true;
            _rawBytes = bytAttachment;
            _contentLength = lngFileLength;
            _contentFileName = strFileName;
            _contentType = strContentType;
        }

        /// <summary> 
        /// New Attachment 
        /// </summary> 
        /// <param name="bytAttachment">attachment bytes content</param> 
        /// <param name="strFileName">file name</param> 
        /// <param name="strContentType">content type</param> 
        public Attachment(byte[] bytAttachment, string strFileName, string strContentType)
        {
            _inBytes = true;
            _rawBytes = bytAttachment;
            _contentLength = bytAttachment.Length;
            _contentFileName = strFileName;
            _contentType = strContentType;
        }

        /// <summary> 
        /// New Attachment 
        /// </summary> 
        /// <param name="strAttachment">attachment content</param> 
        /// <param name="strContentType">content type</param> 
        /// <param name="blnParseHeader">whether only parse the header or not</param> 
        public Attachment(string strAttachment, string strContentType, bool blnParseHeader)
        {
            if (!blnParseHeader)
            {
                _contentFileName = _defaultMSTNEFFileName;
                _contentType = strContentType;
            }
            this.NewAttachment(strAttachment, blnParseHeader);
        }

        /// <summary> 
        /// New Attachment 
        /// </summary> 
        /// <param name="strAttachment">attachment content</param> 
        public Attachment(string strAttachment)
        {
            this.NewAttachment(strAttachment, true);
        }

        /// <summary> 
        /// create attachment 
        /// </summary> 
        /// <param name="strAttachment">raw attachment text</param> 
        /// <param name="blnParseHeader">parse header</param> 
        private void NewAttachment(string strAttachment, bool blnParseHeader)
        {
            _inBytes = false;

            if (strAttachment == null)
                throw new ArgumentNullException("strAttachment");

            StringReader srReader = new StringReader(strAttachment);

            if (blnParseHeader)
            {
                string strLine = srReader.ReadLine();
                while (Utility.IsNotNullTextEx(strLine))
                {
                    ParseHeader(srReader, ref strLine);
                    if (Utility.IsOrNullTextEx(strLine))
                        break;
                    else
                        strLine = srReader.ReadLine();
                }
            }

            this._rawAttachment = srReader.ReadToEnd();
            _contentLength = this._rawAttachment.Length;
        }

        /// <summary> 
        /// Parse header fields and set member variables 
        /// </summary> 
        /// <param name="srReader">string reader</param> 
        /// <param name="strLine">header line</param> 
        private void ParseHeader(StringReader srReader, ref string strLine)
        {
            string[] array = Utility.GetHeadersValue(strLine); //Regex.Split(strLine,":"); 
            string[] values = Regex.Split(array[1], ";"); //array[1].Split(';'); 
            string strRet = null;

            switch (array[0].ToUpper())
            {
                case "CONTENT-TYPE":
                    if (values.Length > 0)
                        _contentType = values[0].Trim();
                    if (values.Length > 1)
                    {
                        _contentCharset = Utility.GetQuotedValue(values[1], "=", "charset");
                    }
                    if (values.Length > 2)
                    {
                        _contentFormat = Utility.GetQuotedValue(values[2], "=", "format");
                    }
                    _contentFileName = Utility.ParseFileName(strLine);
                    if (_contentFileName == "")
                    {
                        strRet = srReader.ReadLine();
                        if (strRet == "")
                        {
                            strLine = "";
                            break;
                        }
                        _contentFileName = Utility.ParseFileName(strLine);
                        if (_contentFileName == "")
                            ParseHeader(srReader, ref strRet);
                    }
                    break;
                case "CONTENT-TRANSFER-ENCODING":
                    _contentTransferEncoding = Utility.SplitOnSemiColon(array[1])[0].Trim();
                    break;
                case "CONTENT-DESCRIPTION":
                    _contentDescription = Utility.DecodeText(Utility.SplitOnSemiColon(array[1])[0].Trim());
                    break;
                case "CONTENT-DISPOSITION":
                    if (values.Length > 0)
                        _contentDisposition = values[0].Trim();

                    ///<bug>reported by grandepuffo @ https://sourceforge.net/forum/message.php?msg_id=2589759 
                    //_contentFileName=values[1]; 
                    if (values.Length > 1)
                    {
                        _contentFileName = values[1];
                    }
                    else
                    {
                        _contentFileName = "";
                    }

                    if (_contentFileName == "")
                        _contentFileName = srReader.ReadLine();

                    _contentFileName = _contentFileName.Replace("\t", "");
                    _contentFileName = Utility.GetQuotedValue(_contentFileName, "=", "filename");
                    _contentFileName = Utility.DecodeText(_contentFileName);
                    break;
                case "CONTENT-ID":
                    _contentID = Utility.SplitOnSemiColon(array[1])[0].Trim('<').Trim('>');
                    break;
            }
        }

        /// <summary> 
        /// verify the encoding 
        /// </summary> 
        /// <param name="encoding">encoding to verify</param> 
        /// <returns>true if encoding</returns> 
        private bool IsEncoding(string encoding)
        {
            return _contentTransferEncoding.ToLower().IndexOf(encoding.ToLower()) != -1;
        }

        /// <summary> 
        /// Decode the attachment to text 
        /// </summary> 
        /// <returns>Decoded attachment text</returns> 
        public string DecodeAsText()
        {
            string decodedAttachment = null;

            try
            {
                if (_contentType.ToLower() == "message/rfc822".ToLower())
                    decodedAttachment = Utility.DecodeText(_rawAttachment);
                else if (_contentTransferEncoding != null)
                {
                    decodedAttachment = _rawAttachment;

                    if (!IsEncoding("7bit"))
                    {
                        if (IsEncoding("8bit") && _contentCharset != null & _contentCharset != "")
                            decodedAttachment = Utility.Change(decodedAttachment, _contentCharset);

                        if (Utility.IsQuotedPrintable(_contentTransferEncoding))
                            decodedAttachment = DecodeQP.ConvertHexContent(decodedAttachment);
                        else if (IsEncoding("8bit"))
                            decodedAttachment = decodedAttachment;
                        else
                            decodedAttachment = Utility.deCodeB64s(Utility.RemoveNonB64(decodedAttachment));
                    }
                }
                else if (_contentCharset != null)
                    decodedAttachment = Utility.Change(_rawAttachment, _contentCharset); //Encoding.Default.GetString(Encoding.GetEncoding(_contentCharset).GetBytes(_rawAttachment)); 
                else
                    decodedAttachment = _rawAttachment;
            }
            catch
            {
                decodedAttachment = _rawAttachment;
            }
            return decodedAttachment;
        }

        /// <summary> 
        /// decode attachment to be a message object 
        /// </summary> 
        /// <returns>message</returns> 
        public Message DecodeAsMessage()
        {
            bool blnRet = false;
            return new Message(ref blnRet, "", false, _rawAttachment, false);
        }

        /// <summary> 
        /// Decode the attachment to bytes 
        /// </summary> 
        /// <returns>Decoded attachment bytes</returns> 
        public byte[] DecodedAsBytes()
        {
            if (_rawAttachment == null)
                return null;
            if (_contentFileName != "")
            {
                byte[] decodedBytes = null;

                if (_contentType != null && _contentType.ToLower() == "message/rfc822".ToLower())
                    decodedBytes = Encoding.Default.GetBytes(Utility.DecodeText(_rawAttachment));
                else if (_contentTransferEncoding != null)
                {
                    string bytContent = _rawAttachment;

                    if (!IsEncoding("7bit"))
                    {
                        if (IsEncoding("8bit") && _contentCharset != null & _contentCharset != "")
                            bytContent = Utility.Change(bytContent, _contentCharset);

                        if (Utility.IsQuotedPrintable(_contentTransferEncoding))
                            decodedBytes = Encoding.Default.GetBytes(DecodeQP.ConvertHexContent(bytContent));
                        else if (IsEncoding("8bit"))
                            decodedBytes = Encoding.Default.GetBytes(bytContent);
                        else
                            decodedBytes = Convert.FromBase64String(Utility.RemoveNonB64(bytContent));
                    }
                    else
                        decodedBytes = Encoding.Default.GetBytes(bytContent);
                }
                else if (_contentCharset != null)
                    decodedBytes = Encoding.Default.GetBytes(Utility.Change(_rawAttachment, _contentCharset)); //Encoding.Default.GetString(Encoding.GetEncoding(_contentCharset).GetBytes(_rawAttachment)); 
                else
                    decodedBytes = Encoding.Default.GetBytes(_rawAttachment);

                return decodedBytes;
            }
            else
            {
                return null;
            }
        }

        public int CompareTo(object attachment)
        {
            return (this.RawAttachment.CompareTo(((Attachment)(attachment)).RawAttachment));
        }
    }

    public enum MessageImportanceType
    {
        HIGH = 5,
        NORMAL = 3,
        LOW = 1
    }

    /// <summary> 
    /// Decoding Quoted-Printable text 
    /// 
    /// </summary> 
    public class DecodeQP
    {
        public DecodeQP()
        {
        }

        /// <summary> 
        /// Decoding Quoted-Printable string 
        /// </summary> 
        /// <param name="Hexstring">Quoted-Printable encoded string</param> 
        /// <param name="encode">encoding method</param> 
        /// <returns>decoded string</returns> 
        public static string ConvertHexToString(string Hexstring, string Encoding)
        {
            try
            {
                return ConvertHexToString(Hexstring, System.Text.Encoding.GetEncoding(Encoding));
            }
            catch
            {
                return ConvertHexContent(Hexstring);
            }
        }

        /// <summary> 
        /// Decoding Quoted-Printable string 
        /// </summary> 
        /// <param name="Hexstring">Quoted-Printable encoded string</param> 
        /// <param name="encode">encoding method</param> 
        /// <returns>decoded string</returns> 
        public static string ConvertHexToString(string Hexstring, Encoding encode)
        {
            try
            {
                if (Hexstring == null || Hexstring.Equals("")) return "";

                if (Hexstring.StartsWith("=")) Hexstring = Hexstring.Substring(1);

                string[] aHex = Hexstring.Split(new char[1] { '=' });
                byte[] abyte = new Byte[aHex.Length];

                for (int i = 0; i < abyte.Length; i++)
                {
                    // Console.WriteLine(aHex[i]); 
                    abyte[i] = (byte)int.Parse(aHex[i], NumberStyles.HexNumber);
                }
                return encode.GetString(abyte);
            }
            catch
            {
                return Hexstring;
            }
        }

        /// <summary> 
        /// Decoding Quoted-Printable string at a position 
        /// </summary> 
        /// <param name="Hexstring">Quoted-Printable encoded string</param> 
        /// <param name="encode">encoding method, "Default" is suggested</param> 
        /// <param name="nStart">position to start, normally 0</param> 
        /// <returns>decoded string</returns> 
        public static string ConvertHexContent(string Hexstring, Encoding encode, long nStart)
        {
            if (nStart >= Hexstring.Length) return Hexstring;

            //to hold string to be decoded 
            StringBuilder sbHex = new StringBuilder();
            sbHex.Append("");
            //to hold decoded string 
            StringBuilder sbEncoded = new StringBuilder();
            sbEncoded.Append("");
            //wether we reach Quoted-Printable string 
            bool isBegin = false;
            string temp;
            int i = (int)nStart;

            while (i < Hexstring.Length)
            {
                //init next loop 
                sbHex.Remove(0, sbHex.Length);
                isBegin = false;
                int count = 0;

                while (i < Hexstring.Length)
                {
                    temp = Hexstring.Substring(i, 1); //before reaching Quoted-Printable string, one char at a time 
                    if (temp.StartsWith("="))
                    {
                        temp = Hexstring.Substring(i, 3); //get 3 chars 
                        if (temp.EndsWith("\r\n")) //return char 
                        {
                            if (isBegin && (count % 2 == 0))
                                break;
                            // sbEncoded.Append(""); 
                            i = i + 3;
                        }
                        else if (!temp.EndsWith("3D"))
                        {
                            sbHex.Append(temp);
                            isBegin = true; //we reach Quoted-Printable string, put it into buffer 
                            i = i + 3;
                            count++;
                        }
                        else //if it ends with 3D, it is "=" 
                        {
                            if (isBegin && (count % 2 == 0)) //wait until even items to handle all character sets 
                                break;

                            sbEncoded.Append("=");
                            i = i + 3;
                        }

                    }
                    else
                    {
                        if (isBegin) //we have got the how Quoted-Printable string, break it 
                            break;
                        sbEncoded.Append(temp); //not Quoted-Printable string, put it into buffer 
                        i++;
                    }

                }
                //decode Quoted-Printable string 
                sbEncoded.Append(ConvertHexToString(sbHex.ToString(), encode));
            }

            return sbEncoded.ToString();
        }


        /// <summary> 
        /// Decoding Quoted-Printable string using default encoding and begin at 0 
        /// </summary> 
        /// <param name="Hexstring">Quoted-Printable encoded string</param> 
        /// <returns>decoded string</returns> 
        public static string ConvertHexContent(string Hexstring)
        {
            if (Hexstring == null || Hexstring.Equals("")) return Hexstring;

            return ConvertHexContent(Hexstring, Encoding.Default, 0);

        }
    }

    /// <summary> 
    /// Message Parser. 
    /// </summary> 
    public class Message
    {
        #region Member Variables

        private ArrayList _attachments = new ArrayList();
        private string _rawHeader = null;
        private string _rawMessage = null;
        private string _rawMessageBody = null;
        private int _attachmentCount = 0;
        private string _replyTo = null;
        private string _replyToEmail = null;
        private string _from = null;
        private string _fromEmail = null;
        private string _date = null;
        private string _dateTimeInfo = null;
        private string _subject = null;
        private string[] _to = new string[0];
        private string[] _cc = new string[0];
        private string[] _bcc = new string[0];
        private ArrayList _keywords = new ArrayList();
        private string _contentType = null;
        private string _contentCharset = null;
        private string _reportType = null;
        private string _contentTransferEncoding = null;
        private bool _html = false;
        private long _contentLength = 0;
        private string _contentEncoding = null;
        private string _returnPath = null;
        private string _mimeVersion = null;
        private string _received = null;
        private string _importance = null;
        private string _messageID = null;
        private string _attachmentboundry = null;
        private string _attachmentboundry2 = null;
        private bool _hasAttachment = false;
        private string _dispositionNotificationTo = null;
        private ArrayList _messageBody = new ArrayList();
        private string _basePath = null;
        private bool _autoDecodeMSTNEF = false;
        private Hashtable _customHeaders = new Hashtable();

        #endregion

        #region Properties

        /// <summary> 
        /// custom headers 
        /// </summary> 
        public Hashtable CustomHeaders
        {
            get
            {
                return _customHeaders;
            }
            set
            {
                _customHeaders = value;
            }
        }

        /// <summary> 
        /// whether auto decoding MS-TNEF attachment files 
        /// </summary> 
        public bool AutoDecodeMSTNEF
        {
            get
            {
                return _autoDecodeMSTNEF;
            }
            set
            {
                _autoDecodeMSTNEF = value;
            }
        }

        /// <summary> 
        /// path to extract MS-TNEF attachment files 
        /// </summary> 
        public string BasePath
        {
            get
            {
                return _basePath;
            }
            set
            {
                try
                {
                    if (value.EndsWith("\\"))
                        _basePath = value;
                    else
                        _basePath = value + "\\";
                }
                catch
                {
                }
            }
        }

        /// <summary> 
        /// message keywords 
        /// </summary> 
        public ArrayList Keywords
        {
            get
            {
                return _keywords;
            }
        }

        /// <summary> 
        /// disposition notification 
        /// </summary> 
        public string DispositionNotificationTo
        {
            get
            {
                return _dispositionNotificationTo;
            }
        }

        /// <summary> 
        /// received server 
        /// </summary> 
        public string Received
        {
            get
            {
                return _received;
            }
        }

        /// <summary> 
        /// importance level 
        /// </summary> 
        public string Importance
        {
            get
            {
                return _importance;
            }
        }

        /// <summary> 
        /// importance level type 
        /// </summary> 
        public MessageImportanceType ImportanceType
        {
            get
            {
                switch (_importance.ToUpper())
                {
                    case "5":
                    case "HIGH":
                        return MessageImportanceType.HIGH;
                    case "3":
                    case "NORMAL":
                        return MessageImportanceType.NORMAL;
                    case "1":
                    case "LOW":
                        return MessageImportanceType.LOW;
                    default:
                        return MessageImportanceType.NORMAL;
                }
            }
        }

        /// <summary> 
        /// Content Charset 
        /// </summary> 
        public string ContentCharset
        {
            get
            {
                return _contentCharset;
            }
        }

        /// <summary> 
        /// Content Transfer Encoding 
        /// </summary> 
        public string ContentTransferEncoding
        {
            get
            {
                return _contentTransferEncoding;
            }
        }

        /// <summary> 
        /// Message Bodies 
        /// </summary> 
        public ArrayList MessageBody
        {
            get
            {
                return _messageBody;
            }
        }

        /// <summary> 
        /// Attachment Boundry 
        /// </summary> 
        public string AttachmentBoundry
        {
            get
            {
                return _attachmentboundry;
            }
        }

        /// <summary> 
        /// Alternate Attachment Boundry 
        /// </summary> 
        public string AttachmentBoundry2
        {
            get
            {
                return _attachmentboundry2;
            }
        }

        /// <summary> 
        /// Attachment Count 
        /// </summary> 
        public int AttachmentCount
        {
            get
            {
                return _attachmentCount;
            }
        }

        /// <summary> 
        /// Attachments 
        /// </summary> 
        public ArrayList Attachments
        {
            get
            {
                return _attachments;
            }
        }

        /// <summary> 
        /// CC 
        /// </summary> 
        public string[] CC
        {
            get
            {
                return _cc;
            }
        }

        /// <summary> 
        /// BCC 
        /// </summary> 
        public string[] BCC
        {
            get
            {
                return _bcc;
            }
        }

        /// <summary> 
        /// TO 
        /// </summary> 
        public string[] TO
        {
            get
            {
                return _to;
            }
        }

        /// <summary> 
        /// Content Encoding 
        /// </summary> 
        public string ContentEncoding
        {
            get
            {
                return _contentEncoding;
            }
        }

        /// <summary> 
        /// Content Length 
        /// </summary> 
        public long ContentLength
        {
            get
            {
                return _contentLength;
            }
        }

        /// <summary> 
        /// Content Type 
        /// </summary> 
        public string ContentType
        {
            get
            {
                return _contentType;
            }
        }

        /// <summary> 
        /// Report Type 
        /// </summary> 
        public string ReportType
        {
            get
            {
                return _reportType;
            }
        }

        /// <summary> 
        /// HTML 
        /// </summary> 
        public bool HTML
        {
            get
            {
                return _html;
            }
        }

        /// <summary> 
        /// Date 
        /// </summary> 
        public string Date
        {
            get
            {
                return _date;
            }
        }

        /// <summary> 
        /// DateTime Info 
        /// </summary> 
        public string DateTimeInfo
        {
            get
            {
                return _dateTimeInfo;
            }
        }

        /// <summary> 
        /// From name 
        /// </summary> 
        public string From
        {
            get
            {
                return _from;
            }
        }

        /// <summary> 
        /// From Email 
        /// </summary> 
        public string FromEmail
        {
            get
            {
                return _fromEmail;
            }
        }

        /// <summary> 
        /// Reply to name 
        /// </summary> 
        public string ReplyTo
        {
            get
            {
                return _replyTo;
            }
        }

        /// <summary> 
        /// Reply to email 
        /// </summary> 
        public string ReplyToEmail
        {
            get
            {
                return _replyToEmail;
            }
        }

        /// <summary> 
        /// whether has attachment 
        /// </summary> 
        public bool HasAttachment
        {
            get
            {
                return _hasAttachment;
            }
        }

        /// <summary> 
        /// raw message body 
        /// </summary> 
        public string RawMessageBody
        {
            get
            {
                return _rawMessageBody;
            }
        }

        /// <summary> 
        /// Message ID 
        /// </summary> 
        public string MessageID
        {
            get
            {
                return _messageID;
            }
        }

        /// <summary> 
        /// MIME version 
        /// </summary> 
        public string MimeVersion
        {
            get
            {
                return _mimeVersion;
            }
        }

        /// <summary> 
        /// raw header 
        /// </summary> 
        public string RawHeader
        {
            get
            {
                return _rawHeader;
            }
        }

        /// <summary> 
        /// raw message 
        /// </summary> 
        public string RawMessage
        {
            get
            {
                return _rawMessage;
            }
        }

        /// <summary> 
        /// return path 
        /// </summary> 
        public string ReturnPath
        {
            get
            {
                return _returnPath;
            }
        }

        /// <summary> 
        /// subject 
        /// </summary> 
        public string Subject
        {
            get
            {
                return _subject;
            }
        }

        #endregion

        /// <summary> 
        /// release all objects 
        /// </summary> 
        ~Message()
        {
            _attachments.Clear();
            _attachments = null;
            _keywords.Clear();
            _keywords = null;
            _messageBody.Clear();
            _messageBody = null;
            _customHeaders.Clear();
            _customHeaders = null;
        }

        /// <summary> 
        /// New Message 
        /// </summary> 
        /// <param name="blnFinish">reference for the finishing state</param> 
        /// <param name="strBasePath">path to extract MS-TNEF attachment files</param> 
        /// <param name="blnAutoDecodeMSTNEF">whether auto decoding MS-TNEF attachments</param> 
        /// <param name="blnOnlyHeader">whether only decode the header without body</param> 
        /// <param name="strEMLFile">file of email content to load from</param> 
        public Message(ref bool blnFinish, string strBasePath, bool blnAutoDecodeMSTNEF, bool blnOnlyHeader, string strEMLFile)
        {
            string strMessage = null;
            if (Utility.ReadPlainTextFromFile(strEMLFile, ref strMessage))
            {
                NewMessage(ref blnFinish, strBasePath, blnAutoDecodeMSTNEF, strMessage, blnOnlyHeader);
            }
            else
                blnFinish = true;
        }

        /// <summary> 
        /// New Message 
        /// </summary> 
        /// <param name="blnFinish">reference for the finishing state</param> 
        /// <param name="strBasePath">path to extract MS-TNEF attachment files</param> 
        /// <param name="blnAutoDecodeMSTNEF">whether auto decoding MS-TNEF attachments</param> 
        /// <param name="strMessage">raw message content</param> 
        /// <param name="blnOnlyHeader">whether only decode the header without body</param> 
        public Message(ref bool blnFinish, string strBasePath, bool blnAutoDecodeMSTNEF, string strMessage, bool blnOnlyHeader)
        {
            NewMessage(ref blnFinish, strBasePath, blnAutoDecodeMSTNEF, strMessage, blnOnlyHeader);
        }

        /// <summary> 
        /// New Message 
        /// </summary> 
        /// <param name="blnFinish">reference for the finishing state</param> 
        /// <param name="strMessage">raw message content</param> 
        /// <param name="blnOnlyHeader">whether only decode the header without body</param> 
        public Message(ref bool blnFinish, string strMessage, bool blnOnlyHeader)
        {
            NewMessage(ref blnFinish, "", false, strMessage, blnOnlyHeader);
        }

        /// <summary> 
        /// New Message 
        /// </summary> 
        /// <param name="blnFinish">reference for the finishing state</param> 
        /// <param name="strMessage">raw message content</param> 
        public Message(ref bool blnFinish, string strMessage)
        {
            NewMessage(ref blnFinish, "", false, strMessage, false);
        }

        /// <summary> 
        /// get valid attachment 
        /// </summary> 
        /// <param name="intAttachmentNumber">attachment index in the attachments collection</param> 
        /// <returns>attachment</returns> 
        public Attachment GetAttachment(int intAttachmentNumber)
        {
            if (intAttachmentNumber < 0 || intAttachmentNumber > _attachmentCount || intAttachmentNumber > _attachments.Count)
            {
                Utility.LogError("GetAttachment():attachment not exist");
                throw new ArgumentOutOfRangeException("intAttachmentNumber");
            }
            return (Attachment)_attachments[intAttachmentNumber];
        }

        /// <summary> 
        /// New Message 
        /// </summary> 
        /// <param name="blnFinish">reference for the finishing state</param> 
        /// <param name="strBasePath">path to extract MS-TNEF attachment files</param> 
        /// <param name="blnAutoDecodeMSTNEF">whether auto decoding MS-TNEF attachments</param> 
        /// <param name="strMessage">raw message content</param> 
        /// <param name="blnOnlyHeader">whether only decode the header without body</param> 
        /// <returns>construction result whether successfully new a message</returns> 
        private bool NewMessage(ref bool blnFinish, string strBasePath, bool blnAutoDecodeMSTNEF, string strMessage, bool blnOnlyHeader)
        {
            StringReader srdReader = new StringReader(strMessage);
            StringBuilder sbdBuilder = new StringBuilder();
            _basePath = strBasePath;
            _autoDecodeMSTNEF = blnAutoDecodeMSTNEF;

            _rawMessage = strMessage;

            string strLine = srdReader.ReadLine();
            while (Utility.IsNotNullTextEx(strLine))
            {
                sbdBuilder.Append(strLine + "\r\n");
                ParseHeader(sbdBuilder, srdReader, ref strLine);
                if (Utility.IsOrNullTextEx(strLine))
                    break;
                else
                    strLine = srdReader.ReadLine();
            }

            _rawHeader = sbdBuilder.ToString();

            SetAttachmentBoundry2(_rawHeader);

            if (_contentLength == 0)
                _contentLength = strMessage.Length; //_rawMessageBody.Length; 

            if (blnOnlyHeader == false)
            {
                _rawMessageBody = srdReader.ReadToEnd().Trim();

                //the auto reply mail by outlook uses ms-tnef format 
                if ((_hasAttachment == true && _attachmentboundry != null) || MIMETypes.IsMSTNEF(_contentType))
                {
                    set_attachments();

                    if (this.Attachments.Count > 0)
                    {
                        Attachment at = this.GetAttachment(0);
                        if (at != null && at.NotAttachment)
                            this.GetMessageBody(at.DecodeAsText());
                        else
                        {
                        }
                        //in case body parts as text[0] html[1] 
                        if (this.Attachments.Count > 1 && !this.IsReport())
                        {
                            at = this.GetAttachment(1);
                            if (at != null && at.NotAttachment)
                                this.GetMessageBody(at.DecodeAsText());
                            else
                            {
                            }
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                    GetMessageBody(_rawMessageBody);
                }
            }

            blnFinish = true;
            return true;
        }

        /// <summary> 
        /// parse message body 
        /// </summary> 
        /// <param name="strBuffer">raw message body</param> 
        /// <returns>message body</returns> 
        public string GetTextBody(string strBuffer)
        {
            if (strBuffer.EndsWith("\r\n."))
                return strBuffer.Substring(0, strBuffer.Length - "\r\n.".Length);
            else
                return strBuffer;
        }

        /// <summary> 
        /// parse message body 
        /// </summary> 
        /// <param name="strBuffer">raw message body</param> 
        public void GetMessageBody(string strBuffer)
        {
            int end, begin;
            string body;
            string encoding = "";

            begin = end = 0;
            _messageBody.Clear();

            try
            {
                if (Utility.IsOrNullTextEx(strBuffer))
                    return;
                else if (Utility.IsOrNullTextEx(_contentType) && _contentTransferEncoding == null)
                {
                    _messageBody.Add(GetTextBody(strBuffer));
                }
                else if (_contentType != null && _contentType.IndexOf("digest") >= 0)
                {
                    // this is a digest method 
                    //ParseDigestMessage(strBuffer); 
                    _messageBody.Add(GetTextBody(strBuffer));
                }
                else if (_attachmentboundry2 == null)
                {
                    body = GetTextBody(strBuffer);

                    if (Utility.IsQuotedPrintable(_contentTransferEncoding))
                    {
                        body = DecodeQP.ConvertHexContent(body);
                    }
                    else if (Utility.IsBase64(_contentTransferEncoding))
                    {
                        body = Utility.deCodeB64s(Utility.RemoveNonB64(body));
                    }
                    else if (Utility.IsNotNullText(_contentCharset))
                    {
                        body = Encoding.GetEncoding(_contentCharset).GetString(Encoding.Default.GetBytes(body));
                    }
                    _messageBody.Add(Utility.RemoveNonB64(body));
                }
                else
                {
                    begin = 0;

                    while (begin != -1)
                    {
                        // find "\r\n\r\n" denoting end of header 
                        begin = strBuffer.IndexOf("--" + _attachmentboundry2, begin);
                        if (begin != -1)
                        {
                            encoding = MIMETypes.GetContentTransferEncoding(strBuffer, begin);

                            begin = strBuffer.IndexOf("\r\n\r\n", begin + 1); //strBuffer.LastIndexOfAny(ALPHABET.ToCharArray());

                            // find end of text 
                            end = strBuffer.IndexOf("--" + _attachmentboundry2, begin + 1);

                            if (begin != -1)
                            {
                                if (end != -1)
                                {
                                    begin += 4;
                                    if (begin >= end)
                                        continue;
                                    else if (this._contentEncoding != null && this._contentEncoding.IndexOf("8bit") != -1)
                                        body = Utility.Change(strBuffer.Substring(begin, end - begin - 2), _contentCharset);
                                    else
                                        body = strBuffer.Substring(begin, end - begin - 2);
                                }
                                else
                                {
                                    body = strBuffer.Substring(begin);
                                }

                                if (Utility.IsQuotedPrintable(encoding))
                                {
                                    string ret = body;
                                    ret = DecodeQP.ConvertHexContent(ret);
                                    _messageBody.Add(ret);
                                }
                                else if (Utility.IsBase64(encoding))
                                {
                                    string ret = Utility.RemoveNonB64(body);
                                    ret = Utility.deCodeB64s(ret);
                                    if (ret != "\0")
                                        _messageBody.Add(ret);
                                    else
                                        _messageBody.Add(body);
                                }
                                else
                                    _messageBody.Add(body);

                                if (end == -1) break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (_messageBody.Count == 0)
                            {
                                _messageBody.Add(strBuffer);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Utility.LogError("GetMessageBody():" + e.Message);
                _messageBody.Add(Utility.deCodeB64s(strBuffer));
            }

            if (_messageBody.Count > 1)
                _html = true;
        }

        /// <summary> 
        /// verify if the message is a report 
        /// </summary> 
        /// <returns>if it is a report message, return true, else, false</returns> 
        public bool IsReport()
        {
            if (Utility.IsNotNullText(_contentType))
                return (_contentType.ToLower().IndexOf("report".ToLower()) != -1);
            else
                return false;
        }

        /// <summary> 
        /// verify if the attachment is MIME Email file 
        /// </summary> 
        /// <param name="attItem">attachment</param> 
        /// <returns>if MIME Email file, return true, else, false</returns> 
        public bool IsMIMEMailFile(Attachment attItem)
        {
            try
            {
                return (attItem.ContentFileName.ToLower().EndsWith(".eml".ToLower()) || attItem.ContentType.ToLower() == "message/rfc822".ToLower());
            }
            catch (Exception e)
            {
                Utility.LogError("IsMIMEMailFile():" + e.Message);
                return false;
            }
        }

        /// <summary> 
        /// translate pictures url within the body 
        /// </summary> 
        /// <param name="strBody">message body</param> 
        /// <param name="hsbFiles">pictures collection</param> 
        /// <returns>translated message body</returns> 
        public string TranslateHTMLPictureFiles(string strBody, Hashtable hsbFiles)
        {
            try
            {
                for (int i = 0; i < this.AttachmentCount; i++)
                {
                    Attachment att = this.GetAttachment(i);
                    if (Utility.IsPictureFile(att.ContentFileName) == true)
                    {
                        if (Utility.IsNotNullText(att.ContentID))
                            //support for embedded pictures 
                            strBody = strBody.Replace("cid:" + att.ContentID, hsbFiles[att.ContentFileName].ToString());

                        strBody = strBody.Replace(att.ContentFileName, hsbFiles[att.ContentFileName].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Utility.LogError("TranslateHTMLPictureFiles():" + e.Message);
            }
            return strBody;
        }

        /// <summary> 
        /// translate pictures url within the body 
        /// </summary> 
        /// <param name="strBody">message body</param> 
        /// <param name="strPath">path of the pictures</param> 
        /// <returns>translated message body</returns> 
        public string TranslateHTMLPictureFiles(string strBody, string strPath)
        {
            try
            {
                if (!strPath.EndsWith("\\"))
                {
                    strPath += "\\";
                }
                for (int i = 0; i < this.AttachmentCount; i++)
                {
                    Attachment att = this.GetAttachment(i);
                    if (Utility.IsPictureFile(att.ContentFileName) == true)
                    {
                        if (Utility.IsNotNullText(att.ContentID))
                            //support for embedded pictures 
                            strBody = strBody.Replace("cid:" + att.ContentID, strPath + att.ContentFileName);
                        strBody = strBody.Replace(att.ContentFileName, strPath + att.ContentFileName);
                    }
                }
            }
            catch (Exception e)
            {
                Utility.LogError("TranslateHTMLPictureFiles():" + e.Message);
            }
            return strBody;
        }

        /// <summary> 
        /// Get the proper attachment file name 
        /// </summary> 
        /// <param name="attItem">attachment</param> 
        /// <returns>propery attachment file name</returns> 
        public string GetAttachmentFileName(Attachment attItem)
        {
            int items = 0;

            //return unique body file names 
            for (int i = 0; i < _attachments.Count; i++)
            {
                if (attItem.ContentFileName == attItem.DefaultFileName)
                {
                    items++;
                    attItem.ContentFileName = attItem.DefaultFileName2.Replace("*", items.ToString());
                }
            }
            string name = attItem.ContentFileName;

            return (name == null || name == "" ? (IsReport() == true ? (this.IsMIMEMailFile(attItem) == true ? attItem.DefaultMIMEFileName : attItem.DefaultReportFileName) : (attItem.ContentID != null ? attItem.ContentID : attItem.DefaultFileName)) : name);
        }

        /// <summary> 
        /// save attachments to a defined path 
        /// </summary> 
        /// <param name="strPath">path to have attachments to be saved to</param> 
        /// <returns>true if save successfully, false if failed</returns> 
        public bool SaveAttachments(string strPath)
        {
            if (Utility.IsNotNullText(strPath))
            {
                try
                {
                    bool blnRet = true;

                    if (!strPath.EndsWith("\\"))
                    {
                        strPath += "\\";
                    }
                    for (int i = 0; i < this.Attachments.Count; i++)
                    {
                        Attachment att = GetAttachment(i);
                        blnRet = SaveAttachment(att, strPath + GetAttachmentFileName(att));
                        if (!blnRet)
                            break;
                    }
                    return blnRet;
                }
                catch (Exception e)
                {
                    Utility.LogError(e.Message);
                    return false;
                }
            }
            else
                return false;
        }

        /// <summary> 
        /// save attachment to file 
        /// </summary> 
        /// <param name="attItem">Attachment</param> 
        /// <param name="strFileName">File to be saved to</param> 
        /// <returns>true if save successfully, false if failed</returns> 
        public bool SaveAttachment(Attachment attItem, string strFileName)
        {
            byte[] da;
            try
            {
                //    FileStream fs=File.Create(strFileName); 
                //    byte[] da; 
                //    if(attItem.ContentFileName.Length>0) 
                //    { 
                //     da=attItem.DecodedAttachment; 
                //    } 
                //    else 
                //    { 
                //     this.GetMessageBody(attItem.DecodeAttachmentAsText()); 
                //     da=Encoding.Default.GetBytes((string)this.MessageBody[this.MessageBody.Count-1]); 
                //    } 
                //    fs.Write(da,0,da.Length); 
                //    fs.Close(); 
                //    return true; 
                if (attItem.InBytes)
                {
                    da = attItem.RawBytes;
                }
                else if (attItem.ContentFileName.Length > 0)
                {
                    da = attItem.DecodedAttachment;
                }
                else if (attItem.ContentType.ToLower() == "message/rfc822".ToLower())
                {
                    da = Encoding.Default.GetBytes(attItem.RawAttachment);
                }
                else
                {
                    this.GetMessageBody(attItem.DecodeAsText());
                    da = Encoding.Default.GetBytes((string)this.MessageBody[this.MessageBody.Count - 1]);
                }
                return Utility.SaveByteContentToFile(strFileName, da);
            }
            catch
            {
                /*Utility.LogError("SaveAttachment():"+e.Message); 
                return false;*/
                da = Encoding.Default.GetBytes(attItem.RawAttachment);
                return Utility.SaveByteContentToFile(strFileName, da);
            }
        }

        /// <summary> 
        /// set attachments 
        /// </summary> 
        private void set_attachments()
        {
            int indexOf_attachmentstart = 0;
            int indexOfAttachmentEnd = 0;
            bool processed = false;

            Attachment att = null;

            SetAttachmentBoundry2(_rawMessageBody);

            while (!processed)
            {
                if (Utility.IsNotNullText(_attachmentboundry))
                {
                    indexOf_attachmentstart = _rawMessageBody.IndexOf(_attachmentboundry, indexOf_attachmentstart) + _attachmentboundry.Length;
                    if (_rawMessageBody == "" || indexOf_attachmentstart < 0) return;

                    indexOfAttachmentEnd = _rawMessageBody.IndexOf(_attachmentboundry, indexOf_attachmentstart + 1);
                }
                else
                {
                    indexOfAttachmentEnd = -1;
                }

                //if(indexOfAttachmentEnd<0)return; 
                if (indexOfAttachmentEnd != -1)
                {
                }
                else if (indexOfAttachmentEnd == -1 && !processed && _attachmentCount == 0)
                {
                    processed = true;
                    indexOfAttachmentEnd = _rawMessageBody.Length;
                }
                else
                    return;

                if (indexOf_attachmentstart == indexOfAttachmentEnd - 9)
                {
                    indexOf_attachmentstart = 0;
                    processed = true;
                }

                string strLine = _rawMessageBody.Substring(indexOf_attachmentstart, (indexOfAttachmentEnd - indexOf_attachmentstart - 2));
                bool isMSTNEF;
                isMSTNEF = MIMETypes.IsMSTNEF(_contentType);
                att = new Attachment(strLine.Trim(), _contentType, !isMSTNEF);

                //ms-tnef format might contain multiple attachments 
                if (MIMETypes.IsMSTNEF(att.ContentType) && AutoDecodeMSTNEF && !isMSTNEF)
                {
                    Utility.LogError("set_attachments():found ms-tnef file");
                    TNEFParser tnef = new TNEFParser();
                    TNEFAttachment tatt = new TNEFAttachment();
                    Attachment attNew = null;

                    tnef.Verbose = false;
                    tnef.BasePath = this.BasePath;
                    //tnef.LogFilePath=this.BasePath + "OpenPOP.TNEF.log"; 
                    if (tnef.OpenTNEFStream(att.DecodedAsBytes()))
                    {
                        if (tnef.Parse())
                        {
                            for (IDictionaryEnumerator i = tnef.Attachments().GetEnumerator(); i.MoveNext(); )
                            {
                                tatt = (TNEFAttachment)i.Value;
                                attNew = new Attachment(tatt.FileContent, tatt.FileLength, tatt.FileName, MIMETypes.GetMimeType(tatt.FileName));
                                _attachmentCount++;
                                _attachments.Add(attNew);
                            }
                        }
                        else
                            Utility.LogError("set_attachments():ms-tnef file parse failed");
                    }
                    else
                        Utility.LogError("set_attachments():ms-tnef file open failed");
                }
                else
                {
                    _attachmentCount++;
                    _attachments.Add(att);
                }

                indexOf_attachmentstart++;
            }
        }

        /// <summary> 
        /// Set alternative attachment boundry 
        /// </summary> 
        /// <param name="strBuffer">raw message</param> 
        private void SetAttachmentBoundry2(string strBuffer)
        {
            int indexOfAttachmentBoundry2Begin = 0;
            int indexOfAttachmentBoundry2End = 0;
            indexOfAttachmentBoundry2Begin = strBuffer.ToLower().IndexOf("Multipart/Alternative".ToLower());
            if (indexOfAttachmentBoundry2Begin != -1)
            {
                /*    indexOfAttachmentBoundry2Begin=strBuffer.IndexOf("boundary=\""); 
                    indexOfAttachmentBoundry2End=strBuffer.IndexOf("\"",indexOfAttachmentBoundry2Begin+10); 
                    if(indexOfAttachmentBoundry2Begin!=-1&&indexOfAttachmentBoundry2End!=-1) 
                     _attachmentboundry2=strBuffer.Substring(indexOfAttachmentBoundry2Begin+10,indexOfAttachmentBoundry2End-indexOfAttachmentBoundry2Begin-10).Trim(); 
                */
                indexOfAttachmentBoundry2Begin = strBuffer.IndexOf("boundary=");
                if (indexOfAttachmentBoundry2Begin != -1)
                {
                    int p = strBuffer.IndexOf("\r\n", indexOfAttachmentBoundry2Begin);
                    string s = strBuffer.Substring(indexOfAttachmentBoundry2Begin + 29, 4);
                    indexOfAttachmentBoundry2End = strBuffer.IndexOf("\r\n", indexOfAttachmentBoundry2Begin + 9);
                    if (indexOfAttachmentBoundry2End == -1)
                        indexOfAttachmentBoundry2End = strBuffer.Length;
                    _attachmentboundry2 = Utility.RemoveQuote(strBuffer.Substring(indexOfAttachmentBoundry2Begin + 9, indexOfAttachmentBoundry2End - indexOfAttachmentBoundry2Begin - 9));
                }
            }
            else
            {
                _attachmentboundry2 = _attachmentboundry;
            }
        }

        /// <summary> 
        /// Save message content to eml file 
        /// </summary> 
        /// <param name="strFile"></param> 
        /// <returns></returns> 
        public bool SaveToMIMEEmailFile(string strFile, bool blnReplaceExists)
        {
            return Utility.SavePlainTextToFile(strFile, _rawMessage, blnReplaceExists);
        }

        /// <summary> 
        /// parse multi-line header 
        /// </summary> 
        /// <param name="sbdBuilder">string builder to hold header content</param> 
        /// <param name="srdReader">string reader to get each line of the header</param> 
        /// <param name="strValue">first line content</param> 
        /// <param name="strLine">reference header line</param> 
        /// <param name="alCollection">collection to hold every content line</param> 
        private void ParseStreamLines(StringBuilder sbdBuilder
                                      , StringReader srdReader
                                      , string strValue
                                      , ref string strLine
                                      , ArrayList alCollection)
        {
            string strFormmated;
            int intLines = 0;
            alCollection.Add(strValue);

            sbdBuilder.Append(strLine);

            strLine = srdReader.ReadLine();

            while (strLine.Trim() != "" && (strLine.StartsWith("\t") || strLine.StartsWith(" ")))
            {
                strFormmated = strLine.Substring(1);
                alCollection.Add(Utility.DecodeLine(strFormmated));
                sbdBuilder.Append(strLine);
                strLine = srdReader.ReadLine();
                intLines++;
            }

            if (strLine != "")
            {
                sbdBuilder.Append(strLine);
            }
            else if (intLines == 0)
            {
                strLine = srdReader.ReadLine();
                sbdBuilder.Append(strLine);
            }

            ParseHeader(sbdBuilder, srdReader, ref strLine);
        }

        /// <summary> 
        /// parse multi-line header 
        /// </summary> 
        /// <param name="sbdBuilder">string builder to hold header content</param> 
        /// <param name="srdReader">string reader to get each line of the header</param> 
        /// <param name="strName">collection key</param> 
        /// <param name="strValue">first line content</param> 
        /// <param name="strLine">reference header line</param> 
        /// <param name="hstCollection">collection to hold every content line</param> 
        private void ParseStreamLines(StringBuilder sbdBuilder
                                      , StringReader srdReader
                                      , string strName
                                      , string strValue
                                      , ref string strLine
                                      , Hashtable hstCollection)
        {
            string strFormmated;
            string strReturn = strValue;
            int intLines = 0;

            //sbdBuilder.Append(strLine);

            strLine = srdReader.ReadLine();
            while (strLine.Trim() != "" && (strLine.StartsWith("\t") || strLine.StartsWith(" ")))
            {
                strFormmated = strLine.Substring(1);
                strReturn += Utility.DecodeLine(strFormmated);
                sbdBuilder.Append(strLine + "\r\n");
                strLine = srdReader.ReadLine();
                intLines++;
            }
            if (!hstCollection.ContainsKey(strName))
                hstCollection.Add(strName, strReturn);

            if (strLine != "")
            {
                sbdBuilder.Append(strLine + "\r\n");
            }
            else if (intLines == 0)
            {
                //     strLine=srdReader.ReadLine(); 
                //     sbdBuilder.Append(strLine + "\r\n"); 
            }

            ParseHeader(sbdBuilder, srdReader, ref strLine);
        }

        /// <summary> 
        /// parse multi-line header 
        /// </summary> 
        /// <param name="sbdBuilder">string builder to hold header content</param> 
        /// <param name="srdReader">string reader to get each line of the header</param> 
        /// <param name="strValue">first line content</param> 
        /// <param name="strLine">reference header line</param> 
        /// <param name="strReturn">return value</param> 
        /// <param name="blnLineDecode">decode each line</param> 
        private void ParseStreamLines(StringBuilder sbdBuilder
                                      , StringReader srdReader
                                      , string strValue
                                      , ref string strLine
                                      , ref string strReturn
                                      , bool blnLineDecode)
        {
            string strFormmated;
            int intLines = 0;
            strReturn = strValue;

            sbdBuilder.Append(strLine + "\r\n");

            if (blnLineDecode == true)
                strReturn = Utility.DecodeLine(strReturn);

            strLine = srdReader.ReadLine();
            while (strLine.Trim() != "" && (strLine.StartsWith("\t") || strLine.StartsWith(" ")))
            {
                strFormmated = strLine.Substring(1);
                strReturn += (blnLineDecode == true ? Utility.DecodeLine(strFormmated) : "\r\n" + strFormmated);
                sbdBuilder.Append(strLine + "\r\n");
                strLine = srdReader.ReadLine();
                intLines++;
            }

            if (strLine != "")
            {
                sbdBuilder.Append(strLine + "\r\n");
            }
            else if (intLines == 0)
            {
                strLine = srdReader.ReadLine();
                sbdBuilder.Append(strLine + "\r\n");
            }

            if (!blnLineDecode)
            {
                strReturn = Utility.RemoveWhiteBlanks(Utility.DecodeText(strReturn));
            }

            ParseHeader(sbdBuilder, srdReader, ref strLine);
        }

        /// <summary> 
        /// Parse the headers populating respective member fields 
        /// </summary> 
        /// <param name="sbdBuilder">string builder to hold the header content</param> 
        /// <param name="srdReader">string reader to get each line of the header</param> 
        /// <param name="strLine">reference header line</param> 
        private void ParseHeader(StringBuilder sbdBuilder, StringReader srdReader, ref string strLine)
        {
            string[] array = Utility.GetHeadersValue(strLine); //Regex.Split(strLine,":"); 

            switch (array[0].ToUpper())
            {
                case "TO":
                    _to = array[1].Split(',');
                    for (int i = 0; i < _to.Length; i++)
                    {
                        _to[i] = Utility.DecodeLine(_to[i].Trim());
                    }
                    break;

                case "CC":
                    _cc = array[1].Split(',');
                    for (int i = 0; i < _cc.Length; i++)
                    {
                        _cc[i] = Utility.DecodeLine(_cc[i].Trim());
                    }
                    break;

                case "BCC":
                    _bcc = array[1].Split(',');
                    for (int i = 0; i < _bcc.Length; i++)
                    {
                        _bcc[i] = Utility.DecodeLine(_bcc[i].Trim());
                    }
                    break;

                case "FROM":
                    Utility.ParseEmailAddress(array[1], ref _from, ref _fromEmail);
                    break;

                case "REPLY-TO":
                    Utility.ParseEmailAddress(array[1], ref _replyTo, ref _replyToEmail);
                    break;

                case "KEYWORDS": //ms outlook keywords 
                    ParseStreamLines(sbdBuilder, srdReader, array[1].Trim(), ref strLine, _keywords);
                    break;

                case "RECEIVED":
                    ParseStreamLines(sbdBuilder, srdReader, array[1].Trim(), ref strLine, ref _received, true);
                    break;

                case "IMPORTANCE":
                    _importance = array[1].Trim();
                    break;

                case "DISPOSITION-NOTIFICATION-TO":
                    _dispositionNotificationTo = array[1].Trim();
                    break;

                case "MIME-VERSION":
                    _mimeVersion = array[1].Trim();
                    break;

                case "SUBJECT":
                case "THREAD-TOPIC":
                    string strRet = null;
                    for (int i = 1; i < array.Length; i++)
                    {
                        strRet += array[i];
                    }
                    ParseStreamLines(sbdBuilder, srdReader, strRet, ref strLine, ref _subject, false);
                    break;

                case "RETURN-PATH":
                    _returnPath = array[1].Trim().Trim('>').Trim('<');
                    break;

                case "MESSAGE-ID":
                    _messageID = array[1].Trim().Trim('>').Trim('<');
                    break;

                case "DATE":
                    for (int i = 1; i < array.Length; i++)
                    {
                        _dateTimeInfo += array[i];
                    }
                    _dateTimeInfo = _dateTimeInfo.Trim();
                    _date = Utility.ParseEmailDate(_dateTimeInfo);
                    break;

                case "CONTENT-LENGTH":
                    _contentLength = Convert.ToInt32(array[1]);
                    break;

                case "CONTENT-TRANSFER-ENCODING":
                    _contentTransferEncoding = array[1].Trim();
                    break;

                case "CONTENT-TYPE":
                    //if already content type has been assigned 
                    if (_contentType != null)
                        return;

                    strLine = array[1];

                    _contentType = strLine.Split(';')[0];
                    _contentType = _contentType.Trim();

                    int intCharset = strLine.IndexOf("charset=");
                    if (intCharset != -1)
                    {
                        int intBound2 = strLine.ToLower().IndexOf(";", intCharset + 8);
                        if (intBound2 == -1)
                            intBound2 = strLine.Length;
                        intBound2 -= (intCharset + 8);
                        _contentCharset = strLine.Substring(intCharset + 8, intBound2);
                        _contentCharset = Utility.RemoveQuote(_contentCharset);
                    }
                    else
                    {
                        intCharset = strLine.ToLower().IndexOf("report-type=".ToLower());
                        if (intCharset != -1)
                        {
                            int intPos = strLine.IndexOf(";", intCharset + 13);
                            _reportType = strLine.Substring(intCharset + 12, intPos - intCharset - 13);
                        }
                        else if (strLine.ToLower().IndexOf("boundary=".ToLower()) == -1)
                        {
                            strLine = srdReader.ReadLine();
                            if (strLine == "")
                                return;
                            intCharset = strLine.ToLower().IndexOf("charset=".ToLower());
                            if (intCharset != -1)
                                _contentCharset = strLine.Substring(intCharset + 9, strLine.Length - intCharset - 10);
                            else if (strLine.IndexOf(":") != -1)
                            {
                                sbdBuilder.Append(strLine + "\r\n");
                                ParseHeader(sbdBuilder, srdReader, ref strLine);
                                return;
                            }
                            else
                            {
                                sbdBuilder.Append(strLine + "\r\n");
                            }
                        }
                    }
                    if (_contentType == "text/plain")
                        return;
                    else if (_contentType.ToLower() == "text/html" || _contentType.ToLower().IndexOf("multipart/") != -1)
                        _html = true;

                    if (strLine.Trim().Length == _contentType.Length + 1 || strLine.ToLower().IndexOf("boundary=".ToLower()) == -1)
                    {
                        strLine = srdReader.ReadLine();
                        if (strLine == null || strLine == "" || strLine.IndexOf(":") != -1)
                        {
                            sbdBuilder.Append(strLine + "\r\n");
                            ParseHeader(sbdBuilder, srdReader, ref strLine);
                            return;
                        }
                        else
                        {
                            sbdBuilder.Append(strLine + "\r\n");
                        }

                        if (strLine.ToLower().IndexOf("boundary=".ToLower()) == -1)
                        {
                            _attachmentboundry = srdReader.ReadLine();
                            sbdBuilder.Append(_attachmentboundry + "\r\n");
                        }
                        _attachmentboundry = strLine;
                    }
                    else
                    {
                        /*if(strLine.IndexOf(";")!=-1) 
                         _attachmentboundry=strLine.Split(';')[1]; 
                        else*/
                        _attachmentboundry = strLine;
                    }

                    int intBound = _attachmentboundry.ToLower().IndexOf("boundary=");
                    if (intBound != -1)
                    {
                        int intBound2 = _attachmentboundry.ToLower().IndexOf(";", intBound + 10);
                        if (intBound2 == -1)
                            intBound2 = _attachmentboundry.Length;
                        intBound2 -= (intBound + 9);
                        _attachmentboundry = _attachmentboundry.Substring(intBound + 9, intBound2);
                    }
                    _attachmentboundry = Utility.RemoveQuote(_attachmentboundry);
                    _hasAttachment = true;

                    break;

                default:
                    if (array.Length > 1) //here we parse all custom headers 
                    {
                        string headerName = array[0].Trim();
                        if (headerName.ToUpper().StartsWith("X")) //every custom header starts with "X" 
                        {
                            ParseStreamLines(sbdBuilder, srdReader, headerName, array[1].Trim(), ref strLine, _customHeaders);
                        }
                    }
                    break;
            }
        }
    }

    /// <summary> 
    /// MIMETypes 
    /// </summary> 
    public class MIMETypes
    {
        public const string MIMEType_MSTNEF = "application/ms-tnef";
        private const string Content_Transfer_Encoding_Tag = "Content-Transfer-Encoding";
        private static Hashtable _MIMETypeList = null;


        public static string GetContentTransferEncoding(string strBuffer, int pos)
        {
            int begin = 0, end = 0;
            begin = strBuffer.ToLower().IndexOf(Content_Transfer_Encoding_Tag.ToLower(), pos);
            if (begin != -1)
            {
                end = strBuffer.ToLower().IndexOf("\r\n".ToLower(), begin + 1);
                return strBuffer.Substring(begin + Content_Transfer_Encoding_Tag.Length + 1, end - begin - Content_Transfer_Encoding_Tag.Length).Trim();
            }
            else
                return "";
        }

        public static bool IsMSTNEF(string strContentType)
        {
            if (strContentType != null & strContentType != "")
                if (strContentType.ToLower() == MIMEType_MSTNEF.ToLower())
                    return true;
                else
                    return false;
            else
                return false;
        }

        public static string ContentType(string strExtension)
        {
            if (_MIMETypeList.ContainsKey(strExtension))
                return _MIMETypeList[strExtension].ToString();
            else
                return null;
        }

        public static Hashtable MIMETypeList
        {
            get
            {
                return _MIMETypeList;
            }
            set
            {
                _MIMETypeList = value;
            }
        }

        ~MIMETypes()
        {
            _MIMETypeList.Clear();
            _MIMETypeList = null;
        }

        public MIMETypes()
        {
            _MIMETypeList.Add(".323", "text/h323");
            _MIMETypeList.Add(".3gp", "video/3gpp");
            _MIMETypeList.Add(".3gpp", "video/3gpp");
            _MIMETypeList.Add(".acp", "audio/x-mei-aac");
            _MIMETypeList.Add(".act", "text/xml");
            _MIMETypeList.Add(".actproj", "text/plain");
            _MIMETypeList.Add(".ade", "application/msaccess");
            _MIMETypeList.Add(".adp", "application/msaccess");
            _MIMETypeList.Add(".ai", "application/postscript");
            _MIMETypeList.Add(".aif", "audio/aiff");
            _MIMETypeList.Add(".aifc", "audio/aiff");
            _MIMETypeList.Add(".aiff", "audio/aiff");
            _MIMETypeList.Add(".asf", "video/x-ms-asf");
            _MIMETypeList.Add(".asm", "text/plain");
            _MIMETypeList.Add(".asx", "video/x-ms-asf");
            _MIMETypeList.Add(".au", "audio/basic");
            _MIMETypeList.Add(".avi", "video/avi");
            _MIMETypeList.Add(".bmp", "image/bmp");
            _MIMETypeList.Add(".bwp", "application/x-bwpreview");
            _MIMETypeList.Add(".c", "text/plain");
            _MIMETypeList.Add(".cat", "application/vnd.ms-pki.seccat");
            _MIMETypeList.Add(".cc", "text/plain");
            _MIMETypeList.Add(".cdf", "application/x-cdf");
            _MIMETypeList.Add(".cer", "application/x-x509-ca-cert");
            _MIMETypeList.Add(".cod", "text/plain");
            _MIMETypeList.Add(".cpp", "text/plain");
            _MIMETypeList.Add(".crl", "application/pkix-crl");
            _MIMETypeList.Add(".crt", "application/x-x509-ca-cert");
            _MIMETypeList.Add(".cs", "text/plain");
            _MIMETypeList.Add(".css", "text/css");
            _MIMETypeList.Add(".csv", "application/vnd.ms-excel");
            _MIMETypeList.Add(".cxx", "text/plain");
            _MIMETypeList.Add(".dbs", "text/plain");
            _MIMETypeList.Add(".def", "text/plain");
            _MIMETypeList.Add(".der", "application/x-x509-ca-cert");
            _MIMETypeList.Add(".dib", "image/bmp");
            _MIMETypeList.Add(".dif", "video/x-dv");
            _MIMETypeList.Add(".dll", "application/x-msdownload");
            _MIMETypeList.Add(".doc", "application/msword");
            _MIMETypeList.Add(".dot", "application/msword");
            _MIMETypeList.Add(".dsp", "text/plain");
            _MIMETypeList.Add(".dsw", "text/plain");
            _MIMETypeList.Add(".dv", "video/x-dv");
            _MIMETypeList.Add(".edn", "application/vnd.adobe.edn");
            _MIMETypeList.Add(".eml", "message/rfc822");
            _MIMETypeList.Add(".eps", "application/postscript");
            _MIMETypeList.Add(".etd", "application/x-ebx");
            _MIMETypeList.Add(".etp", "text/plain");
            _MIMETypeList.Add(".exe", "application/x-msdownload");
            _MIMETypeList.Add(".ext", "text/plain");
            _MIMETypeList.Add(".fdf", "application/vnd.fdf");
            _MIMETypeList.Add(".fif", "application/fractals");
            _MIMETypeList.Add(".fky", "text/plain");
            _MIMETypeList.Add(".gif", "image/gif");
            _MIMETypeList.Add(".gz", "application/x-gzip");
            _MIMETypeList.Add(".h", "text/plain");
            _MIMETypeList.Add(".hpp", "text/plain");
            _MIMETypeList.Add(".hqx", "application/mac-binhex40");
            _MIMETypeList.Add(".hta", "application/hta");
            _MIMETypeList.Add(".htc", "text/x-component");
            _MIMETypeList.Add(".htm", "text/html");
            _MIMETypeList.Add(".html", "text/html");
            _MIMETypeList.Add(".htt", "text/webviewhtml");
            _MIMETypeList.Add(".hxx", "text/plain");
            _MIMETypeList.Add(".i", "text/plain");
            _MIMETypeList.Add(".iad", "application/x-iad");
            _MIMETypeList.Add(".ico", "image/x-icon");
            _MIMETypeList.Add(".ics", "text/calendar");
            _MIMETypeList.Add(".idl", "text/plain");
            _MIMETypeList.Add(".iii", "application/x-iphone");
            _MIMETypeList.Add(".inc", "text/plain");
            _MIMETypeList.Add(".infopathxml", "application/ms-infopath.xml");
            _MIMETypeList.Add(".inl", "text/plain");
            _MIMETypeList.Add(".ins", "application/x-internet-signup");
            _MIMETypeList.Add(".iqy", "text/x-ms-iqy");
            _MIMETypeList.Add(".isp", "application/x-internet-signup");
            _MIMETypeList.Add(".java", "text/java");
            _MIMETypeList.Add(".jfif", "image/jpeg");
            _MIMETypeList.Add(".jnlp", "application/x-java-jnlp-file");
            _MIMETypeList.Add(".jpe", "image/jpeg");
            _MIMETypeList.Add(".jpeg", "image/jpeg");
            _MIMETypeList.Add(".jpg", "image/jpeg");
            _MIMETypeList.Add(".jsl", "text/plain");
            _MIMETypeList.Add(".kci", "text/plain");
            _MIMETypeList.Add(".la1", "audio/x-liquid-file");
            _MIMETypeList.Add(".lar", "application/x-laplayer-reg");
            _MIMETypeList.Add(".latex", "application/x-latex");
            _MIMETypeList.Add(".lavs", "audio/x-liquid-secure");
            _MIMETypeList.Add(".lgn", "text/plain");
            _MIMETypeList.Add(".lmsff", "audio/x-la-lms");
            _MIMETypeList.Add(".lqt", "audio/x-la-lqt");
            _MIMETypeList.Add(".lst", "text/plain");
            _MIMETypeList.Add(".m1v", "video/mpeg");
            _MIMETypeList.Add(".m3u", "audio/mpegurl");
            _MIMETypeList.Add(".m4e", "video/mpeg4");
            _MIMETypeList.Add(".MAC", "image/x-macpaint");
            _MIMETypeList.Add(".mak", "text/plain");
            _MIMETypeList.Add(".man", "application/x-troff-man");
            _MIMETypeList.Add(".map", "text/plain");
            _MIMETypeList.Add(".mda", "application/msaccess");
            _MIMETypeList.Add(".mdb", "application/msaccess");
            _MIMETypeList.Add(".mde", "application/msaccess");
            _MIMETypeList.Add(".mdi", "image/vnd.ms-modi");
            _MIMETypeList.Add(".mfp", "application/x-shockwave-flash");
            _MIMETypeList.Add(".mht", "message/rfc822");
            _MIMETypeList.Add(".mhtml", "message/rfc822");
            _MIMETypeList.Add(".mid", "audio/mid");
            _MIMETypeList.Add(".midi", "audio/mid");
            _MIMETypeList.Add(".mk", "text/plain");
            _MIMETypeList.Add(".mnd", "audio/x-musicnet-download");
            _MIMETypeList.Add(".mns", "audio/x-musicnet-stream");
            _MIMETypeList.Add(".MP1", "audio/mp1");
            _MIMETypeList.Add(".mp2", "video/mpeg");
            _MIMETypeList.Add(".mp2v", "video/mpeg");
            _MIMETypeList.Add(".mp3", "audio/mpeg");
            _MIMETypeList.Add(".mp4", "video/mp4");
            _MIMETypeList.Add(".mpa", "video/mpeg");
            _MIMETypeList.Add(".mpe", "video/mpeg");
            _MIMETypeList.Add(".mpeg", "video/mpeg");
            _MIMETypeList.Add(".mpf", "application/vnd.ms-mediapackage");
            _MIMETypeList.Add(".mpg", "video/mpeg");
            _MIMETypeList.Add(".mpg4", "video/mp4");
            _MIMETypeList.Add(".mpga", "audio/rn-mpeg");
            _MIMETypeList.Add(".mpv2", "video/mpeg");
            _MIMETypeList.Add(".NMW", "application/nmwb");
            _MIMETypeList.Add(".nws", "message/rfc822");
            _MIMETypeList.Add(".odc", "text/x-ms-odc");
            _MIMETypeList.Add(".odh", "text/plain");
            _MIMETypeList.Add(".odl", "text/plain");
            _MIMETypeList.Add(".p10", "application/pkcs10");
            _MIMETypeList.Add(".p12", "application/x-pkcs12");
            _MIMETypeList.Add(".p7b", "application/x-pkcs7-certificates");
            _MIMETypeList.Add(".p7c", "application/pkcs7-mime");
            _MIMETypeList.Add(".p7m", "application/pkcs7-mime");
            _MIMETypeList.Add(".p7r", "application/x-pkcs7-certreqresp");
            _MIMETypeList.Add(".p7s", "application/pkcs7-signature");
            _MIMETypeList.Add(".PCT", "image/pict");
            _MIMETypeList.Add(".pdf", "application/pdf");
            _MIMETypeList.Add(".pdx", "application/vnd.adobe.pdx");
            _MIMETypeList.Add(".pfx", "application/x-pkcs12");
            _MIMETypeList.Add(".pic", "image/pict");
            _MIMETypeList.Add(".PICT", "image/pict");
            _MIMETypeList.Add(".pko", "application/vnd.ms-pki.pko");
            _MIMETypeList.Add(".png", "image/png");
            _MIMETypeList.Add(".pnt", "image/x-macpaint");
            _MIMETypeList.Add(".pntg", "image/x-macpaint");
            _MIMETypeList.Add(".pot", "application/vnd.ms-powerpoint");
            _MIMETypeList.Add(".ppa", "application/vnd.ms-powerpoint");
            _MIMETypeList.Add(".pps", "application/vnd.ms-powerpoint");
            _MIMETypeList.Add(".ppt", "application/vnd.ms-powerpoint");
            _MIMETypeList.Add(".prc", "text/plain");
            _MIMETypeList.Add(".prf", "application/pics-rules");
            _MIMETypeList.Add(".ps", "application/postscript");
            _MIMETypeList.Add(".pub", "application/vnd.ms-publisher");
            _MIMETypeList.Add(".pwz", "application/vnd.ms-powerpoint");
            _MIMETypeList.Add(".qt", "video/quicktime");
            _MIMETypeList.Add(".qti", "image/x-quicktime");
            _MIMETypeList.Add(".qtif", "image/x-quicktime");
            _MIMETypeList.Add(".qtl", "application/x-quicktimeplayer");
            _MIMETypeList.Add(".qup", "application/x-quicktimeupdater");
            _MIMETypeList.Add(".r1m", "application/vnd.rn-recording");
            _MIMETypeList.Add(".r3t", "text/vnd.rn-realtext3d");
            _MIMETypeList.Add(".RA", "audio/vnd.rn-realaudio");
            _MIMETypeList.Add(".RAM", "audio/x-pn-realaudio");
            _MIMETypeList.Add(".rat", "application/rat-file");
            _MIMETypeList.Add(".rc", "text/plain");
            _MIMETypeList.Add(".rc2", "text/plain");
            _MIMETypeList.Add(".rct", "text/plain");
            _MIMETypeList.Add(".rec", "application/vnd.rn-recording");
            _MIMETypeList.Add(".rgs", "text/plain");
            _MIMETypeList.Add(".rjs", "application/vnd.rn-realsystem-rjs");
            _MIMETypeList.Add(".rjt", "application/vnd.rn-realsystem-rjt");
            _MIMETypeList.Add(".RM", "application/vnd.rn-realmedia");
            _MIMETypeList.Add(".rmf", "application/vnd.adobe.rmf");
            _MIMETypeList.Add(".rmi", "audio/mid");
            _MIMETypeList.Add(".RMJ", "application/vnd.rn-realsystem-rmj");
            _MIMETypeList.Add(".RMM", "audio/x-pn-realaudio");
            _MIMETypeList.Add(".rms", "application/vnd.rn-realmedia-secure");
            _MIMETypeList.Add(".rmvb", "application/vnd.rn-realmedia-vbr");
            _MIMETypeList.Add(".RMX", "application/vnd.rn-realsystem-rmx");
            _MIMETypeList.Add(".RNX", "application/vnd.rn-realplayer");
            _MIMETypeList.Add(".rp", "image/vnd.rn-realpix");
            _MIMETypeList.Add(".RPM", "audio/x-pn-realaudio-plugin");
            _MIMETypeList.Add(".rqy", "text/x-ms-rqy");
            _MIMETypeList.Add(".rsml", "application/vnd.rn-rsml");
            _MIMETypeList.Add(".rt", "text/vnd.rn-realtext");
            _MIMETypeList.Add(".rtf", "application/msword");
            _MIMETypeList.Add(".rul", "text/plain");
            _MIMETypeList.Add(".RV", "video/vnd.rn-realvideo");
            _MIMETypeList.Add(".s", "text/plain");
            _MIMETypeList.Add(".sc2", "application/schdpl32");
            _MIMETypeList.Add(".scd", "application/schdpl32");
            _MIMETypeList.Add(".sch", "application/schdpl32");
            _MIMETypeList.Add(".sct", "text/scriptlet");
            _MIMETypeList.Add(".sd2", "audio/x-sd2");
            _MIMETypeList.Add(".sdp", "application/sdp");
            _MIMETypeList.Add(".sit", "application/x-stuffit");
            _MIMETypeList.Add(".slk", "application/vnd.ms-excel");
            _MIMETypeList.Add(".sln", "application/octet-stream");
            _MIMETypeList.Add(".SMI", "application/smil");
            _MIMETypeList.Add(".smil", "application/smil");
            _MIMETypeList.Add(".snd", "audio/basic");
            _MIMETypeList.Add(".snp", "application/msaccess");
            _MIMETypeList.Add(".spc", "application/x-pkcs7-certificates");
            _MIMETypeList.Add(".spl", "application/futuresplash");
            _MIMETypeList.Add(".sql", "text/plain");
            _MIMETypeList.Add(".srf", "text/plain");
            _MIMETypeList.Add(".ssm", "application/streamingmedia");
            _MIMETypeList.Add(".sst", "application/vnd.ms-pki.certstore");
            _MIMETypeList.Add(".stl", "application/vnd.ms-pki.stl");
            _MIMETypeList.Add(".swf", "application/x-shockwave-flash");
            _MIMETypeList.Add(".tab", "text/plain");
            _MIMETypeList.Add(".tar", "application/x-tar");
            _MIMETypeList.Add(".tdl", "text/xml");
            _MIMETypeList.Add(".tgz", "application/x-compressed");
            _MIMETypeList.Add(".tif", "image/tiff");
            _MIMETypeList.Add(".tiff", "image/tiff");
            _MIMETypeList.Add(".tlh", "text/plain");
            _MIMETypeList.Add(".tli", "text/plain");
            _MIMETypeList.Add(".torrent", "application/x-bittorrent");
            _MIMETypeList.Add(".trg", "text/plain");
            _MIMETypeList.Add(".txt", "text/plain");
            _MIMETypeList.Add(".udf", "text/plain");
            _MIMETypeList.Add(".udt", "text/plain");
            _MIMETypeList.Add(".uls", "text/iuls");
            _MIMETypeList.Add(".user", "text/plain");
            _MIMETypeList.Add(".usr", "text/plain");
            _MIMETypeList.Add(".vb", "text/plain");
            _MIMETypeList.Add(".vcf", "text/x-vcard");
            _MIMETypeList.Add(".vcproj", "text/plain");
            _MIMETypeList.Add(".viw", "text/plain");
            _MIMETypeList.Add(".vpg", "application/x-vpeg005");
            _MIMETypeList.Add(".vspscc", "text/plain");
            _MIMETypeList.Add(".vsscc", "text/plain");
            _MIMETypeList.Add(".vssscc", "text/plain");
            _MIMETypeList.Add(".wav", "audio/wav");
            _MIMETypeList.Add(".wax", "audio/x-ms-wax");
            _MIMETypeList.Add(".wbk", "application/msword");
            _MIMETypeList.Add(".wiz", "application/msword");
            _MIMETypeList.Add(".wm", "video/x-ms-wm");
            _MIMETypeList.Add(".wma", "audio/x-ms-wma");
            _MIMETypeList.Add(".wmd", "application/x-ms-wmd");
            _MIMETypeList.Add(".wmv", "video/x-ms-wmv");
            _MIMETypeList.Add(".wmx", "video/x-ms-wmx");
            _MIMETypeList.Add(".wmz", "application/x-ms-wmz");
            _MIMETypeList.Add(".wpl", "application/vnd.ms-wpl");
            _MIMETypeList.Add(".wprj", "application/webzip");
            _MIMETypeList.Add(".wsc", "text/scriptlet");
            _MIMETypeList.Add(".wvx", "video/x-ms-wvx");
            _MIMETypeList.Add(".XBM", "image/x-xbitmap");
            _MIMETypeList.Add(".xdp", "application/vnd.adobe.xdp+xml");
            _MIMETypeList.Add(".xfd", "application/vnd.adobe.xfd+xml");
            _MIMETypeList.Add(".xfdf", "application/vnd.adobe.xfdf");
            _MIMETypeList.Add(".xla", "application/vnd.ms-excel");
            _MIMETypeList.Add(".xlb", "application/vnd.ms-excel");
            _MIMETypeList.Add(".xlc", "application/vnd.ms-excel");
            _MIMETypeList.Add(".xld", "application/vnd.ms-excel");
            _MIMETypeList.Add(".xlk", "application/vnd.ms-excel");
            _MIMETypeList.Add(".xll", "application/vnd.ms-excel");
            _MIMETypeList.Add(".xlm", "application/vnd.ms-excel");
            _MIMETypeList.Add(".xls", "application/vnd.ms-excel");
            _MIMETypeList.Add(".xlt", "application/vnd.ms-excel");
            _MIMETypeList.Add(".xlv", "application/vnd.ms-excel");
            _MIMETypeList.Add(".xlw", "application/vnd.ms-excel");
            _MIMETypeList.Add(".xml", "text/xml");
            _MIMETypeList.Add(".xpl", "audio/scpls");
            _MIMETypeList.Add(".xsl", "text/xml");
            _MIMETypeList.Add(".z", "application/x-compress");
            _MIMETypeList.Add(".zip", "application/x-zip-compressed");
        }

        /// <summary>Returns the MIME content-type for the supplied file extension</summary> 
        /// <returns>string MIME type (Example: \"text/plain\")</returns> 
        public static string GetMimeType(string strFileName)
        {
            try
            {
                string strFileExtension = new FileInfo(strFileName).Extension;
                string strContentType = null;
                bool MONO = false;

                if (MONO)
                {
                    strContentType = MIMETypes.ContentType(strFileExtension);
                }
                else
                {
                    Microsoft.Win32.RegistryKey extKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(strFileExtension);
                    strContentType = (string)extKey.GetValue("Content Type");
                }

                if (strContentType.ToString() != null)
                {
                    return strContentType.ToString();
                }
                else
                {
                    return "application/octet-stream";
                }
            }
            catch (System.Exception)
            {
                return "application/octet-stream";
            }
        }

    }

    /// <summary> 
    /// Summary description for Coding. 
    /// </summary> 
    public class QuotedCoding
    {
        /// <summary> 
        /// zwraca tablice bajtow 
        /// zamienia 3 znaki np '=A9' na odp wartosc. 
        /// zamienia '_' na znak 32 
        /// </summary> 
        /// <param name="s">Kupis_Pawe=B3</param> 
        /// <returns>Kupis Pawe?/returns> 
        public static byte[] GetByteArray(string s)
        {
            byte[] buffer = new byte[s.Length];

            int bufferPosition = 0;
            if (s.Length > 1)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '=')
                    {
                        if (s[i + 1] == '\r' && s[i + 2] == '\n')
                            bufferPosition--;
                        else
                            buffer[bufferPosition] = System.Convert.ToByte(s.Substring(i + 1, 2), 16);
                        i += 2;
                    }
                    else if (s[i] == '_')
                        buffer[bufferPosition] = 32;
                    else
                        buffer[bufferPosition] = (byte)s[i];
                    bufferPosition++;
                }
            }
            else
            {
                buffer[bufferPosition] = 32;
            }

            byte[] newArray = new byte[bufferPosition];
            Array.Copy(buffer, newArray, bufferPosition);
            return newArray;
        }

        /// <summary> 
        /// Decoduje string "=?iso-8859-2?Q?Kupis_Pawe=B3?=" 
        /// lub zakodowany base64 
        /// na poprawny 
        /// </summary> 
        /// <param name="s">"=?iso-8859-2?Q?Kupis_Pawe=B3?="</param> 
        /// <returns>Kupis Pawe?/returns> 
        public static string DecodeOne(string s)
        {
            char[] separator = { '?' };
            string[] sArray = s.Split(separator);
            if (sArray[0].Equals("=") == false)
                return s;

            byte[] bArray;
            //rozpoznaj rodzj kodowania 
            if (sArray[2].ToUpper() == "Q") //querystring 
                bArray = GetByteArray(sArray[3]);
            else if (sArray[2].ToUpper() == "B") //base64 
                bArray = Convert.FromBase64String(sArray[3]);
            else
                return s;
            //pobierz strone kodowa 
            Encoding encoding = Encoding.GetEncoding(sArray[1]);
            return encoding.GetString(bArray);
        }

        /// <summary> 
        /// decoduje string zamienia wpisy (=?...?=) na odp wartosci 
        /// </summary> 
        /// <param name="s">"ala i =?iso-8859-2?Q?Kupis_Pawe=B3?= ma kota"</param> 
        /// <returns>"ala i Pawe?Kupis ma kota"</returns> 
        public static string Decode(string s)
        {
            StringBuilder retstring = new StringBuilder();
            int old = 0, start = 0, stop;
            for (; ; )
            {
                start = s.IndexOf("=?", start);
                if (start == -1)
                {
                    retstring.Append(s, old, s.Length - old);
                    return retstring.ToString();
                }
                stop = s.IndexOf("?=", start + 2);
                if (stop == -1) //blad w stringu 
                    return s;
                retstring.Append(s, old, start - old);
                retstring.Append(DecodeOne(s.Substring(start, stop - start + 2)));
                start = stop + 2;
                old = stop + 2;
            }
        }

    }

    /// <summary> 
    /// TNEFAttachment 
    /// </summary> 
    public class TNEFAttachment
    {
        #region Member Variables

        private string _fileName = "";
        private long _fileLength = 0;
        private string _subject = "";
        private byte[] _fileContent = null;

        #endregion

        #region Properties

        /// <summary> 
        /// attachment subject 
        /// </summary> 
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }

        /// <summary> 
        /// attachment file length 
        /// </summary> 
        public long FileLength
        {
            get
            {
                return _fileLength;
            }
            set
            {
                _fileLength = value;
            }
        }

        /// <summary> 
        /// attachment file name 
        /// </summary> 
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }

        /// <summary> 
        /// attachment file content 
        /// </summary> 
        public byte[] FileContent
        {
            get
            {
                return _fileContent;
            }
            set
            {
                _fileContent = value;
            }
        }

        #endregion

        public TNEFAttachment()
        {
        }

        ~TNEFAttachment()
        {
            _fileContent = null;
        }
    }

    /// <summary> 
    /// OpenPOP.MIMEParser.TNEFParser 
    /// </summary> 
    public class TNEFParser
    {
        #region Member Variables

        private const int TNEF_SIGNATURE = 0x223e9f78;
        private const int LVL_MESSAGE = 0x01;
        private const int LVL_ATTACHMENT = 0x02;
        private const int _string = 0x00010000;
        private const int _BYTE = 0x00060000;
        private const int _WORD = 0x00070000;
        private const int _DWORD = 0x00080000;

        private const int AVERSION = (_DWORD | 0x9006);
        private const int AMCLASS = (_WORD | 0x8008);
        private const int ASUBJECT = (_DWORD | 0x8004);
        private const int AFILENAME = (_string | 0x8010);
        private const int ATTACHDATA = (_BYTE | 0x800f);

        private Stream fsTNEF;
        private Hashtable _attachments = new Hashtable();
        private TNEFAttachment _attachment = null;

        private bool _verbose = false;
        //private string _logFile="OpenPOP.TNEF.log"; 
        private string _basePath = null;
        private int _skipSignature = 0;
        private bool _searchSignature = false;
        private long _offset = 0;
        private long _fileLength = 0;
        private string _tnefFile = "";
        private string strSubject;

        #endregion

        #region Properties

        //  public string LogFilePath 
        //  { 
        //   get{return _logFile;} 
        //   set{_logFile=value;} 
        //  } 

        public string TNEFFile
        {
            get
            {
                return _tnefFile;
            }
            set
            {
                _tnefFile = value;
            }
        }

        public bool Verbose
        {
            get
            {
                return _verbose;
            }
            set
            {
                _verbose = value;
            }
        }

        public string BasePath
        {
            get
            {
                return _basePath;
            }
            set
            {
                try
                {
                    if (value.EndsWith("\\"))
                        _basePath = value;
                    else
                        _basePath = value + "\\";
                }
                catch
                {
                }
            }
        }

        public int SkipSignature
        {
            get
            {
                return _skipSignature;
            }
            set
            {
                _skipSignature = value;
            }
        }

        public bool SearchSignature
        {
            get
            {
                return _searchSignature;
            }
            set
            {
                _searchSignature = value;
            }
        }

        public long Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
            }
        }

        #endregion

        private int GETINT32(byte[] p)
        {
            return (p[0] + (p[1] << 8) + (p[2] << 16) + (p[3] << 24));
        }

        private short GETINT16(byte[] p)
        {
            return (short)(p[0] + (p[1] << 8));
        }

        private int geti32()
        {
            byte[] buf = new byte[4];

            if (StreamReadBytes(buf, 4) != 1)
            {
                Utility.LogError("geti32():unexpected end of input\n");
                return 1;
            }
            return GETINT32(buf);
        }

        private int geti16()
        {
            byte[] buf = new byte[2];

            if (StreamReadBytes(buf, 2) != 1)
            {
                Utility.LogError("geti16():unexpected end of input\n");
                return 1;
            }
            return GETINT16(buf);
        }

        private int geti8()
        {
            byte[] buf = new byte[1];

            if (StreamReadBytes(buf, 1) != 1)
            {
                Utility.LogError("geti8():unexpected end of input\n");
                return 1;
            }
            return (int)buf[0];
        }

        private int StreamReadBytes(byte[] buffer, int size)
        {
            try
            {
                if (fsTNEF.Position + size <= _fileLength)
                {
                    fsTNEF.Read(buffer, 0, size);
                    return 1;
                }
                else
                    return 0;
            }
            catch (Exception e)
            {
                Utility.LogError("StreamReadBytes():" + e.Message);
                return 0;
            }
        }

        private void CloseTNEFStream()
        {
            try
            {
                fsTNEF.Close();
            }
            catch (Exception e)
            {
                Utility.LogError("CloseTNEFStream():" + e.Message);
            }
        }

        /// <summary> 
        /// Open the MS-TNEF stream from file 
        /// </summary> 
        /// <param name="strFile">MS-TNEF file</param> 
        /// <returns></returns> 
        public bool OpenTNEFStream(string strFile)
        {
            //Utility.LogFilePath=LogFilePath; 

            TNEFFile = strFile;
            try
            {
                fsTNEF = new FileStream(strFile, FileMode.Open, FileAccess.Read);
                FileInfo fi = new FileInfo(strFile);
                _fileLength = fi.Length;
                fi = null;
                return true;
            }
            catch (Exception e)
            {
                Utility.LogError("OpenTNEFStream(File):" + e.Message);
                return false;
            }
        }

        /// <summary> 
        /// Open the MS-TNEF stream from bytes 
        /// </summary> 
        /// <param name="bytContents">MS-TNEF bytes</param> 
        /// <returns></returns> 
        public bool OpenTNEFStream(byte[] bytContents)
        {
            //Utility.LogFilePath=LogFilePath; 

            try
            {
                fsTNEF = new MemoryStream(bytContents);
                _fileLength = bytContents.Length;
                return true;
            }
            catch (Exception e)
            {
                Utility.LogError("OpenTNEFStream(Bytes):" + e.Message);
                return false;
            }
        }

        /// <summary> 
        /// Find the MS-TNEF signature 
        /// </summary> 
        /// <returns>true if found, vice versa</returns> 
        public bool FindSignature()
        {
            bool ret = false;
            long lpos = 0;

            int d;

            try
            {
                for (lpos = 0; ; lpos++)
                {
                    if (fsTNEF.Seek(lpos, SeekOrigin.Begin) == -1)
                    {
                        PrintResult("No signature found\n");
                        return false;
                    }

                    d = geti32();
                    if (d == TNEF_SIGNATURE)
                    {
                        PrintResult("Signature found at {0}\n", lpos);
                        break;
                    }
                }
                ret = true;
            }
            catch (Exception e)
            {
                Utility.LogError("FindSignature():" + e.Message);
                ret = false;
            }

            fsTNEF.Position = lpos;

            return ret;
        }

        private void decode_attribute(int d)
        {
            byte[] buf = new byte[4000];
            int len;
            int v;
            int i;

            len = geti32(); /* data length */

            switch (d & 0xffff0000)
            {
                case _BYTE:
                    PrintResult("Attribute {0} =", d & 0xffff);
                    for (i = 0; i < len; i += 1)
                    {
                        v = geti8();

                        if (i < 10) PrintResult(" {0}", v);
                        else if (i == 10) PrintResult("...");
                    }
                    PrintResult("\n");
                    break;
                case _WORD:
                    PrintResult("Attribute {0} =", d & 0xffff);
                    for (i = 0; i < len; i += 2)
                    {
                        v = geti16();

                        if (i < 6) PrintResult(" {0}", v);
                        else if (i == 6) PrintResult("...");
                    }
                    PrintResult("\n");
                    break;
                case _DWORD:
                    PrintResult("Attribute {0} =", d & 0xffff);
                    for (i = 0; i < len; i += 4)
                    {
                        v = geti32();

                        if (i < 4) PrintResult(" {0}", v);
                        else if (i == 4) PrintResult("...");
                    }
                    PrintResult("\n");
                    break;
                case _string:
                    StreamReadBytes(buf, len);

                    PrintResult("Attribute {0} = {1}\n", d & 0xffff, Encoding.Default.GetString(buf));
                    break;
                default:
                    StreamReadBytes(buf, len);
                    PrintResult("Attribute {0}\n", d);
                    break;
            }

            geti16(); /* checksum */
        }

        private void decode_message()
        {
            int d;

            d = geti32();

            decode_attribute(d);
        }

        private void decode_attachment()
        {
            byte[] buf = new byte[4096];
            int d;
            int len;
            int i, chunk;

            d = geti32();

            switch (d)
            {
                case ASUBJECT:
                    len = geti32();

                    StreamReadBytes(buf, len);

                    byte[] _subjectBuffer = new byte[len - 1];

                    Array.Copy(buf, _subjectBuffer, (long)len - 1);

                    strSubject = Encoding.Default.GetString(_subjectBuffer);

                    PrintResult("Found subject: {0}", strSubject);

                    geti16(); /* checksum */

                    break;

                case AFILENAME:
                    len = geti32();
                    StreamReadBytes(buf, len);
                    //PrintResult("File-Name: {0}\n", buf); 
                    byte[] _fileNameBuffer = new byte[len - 1];
                    Array.Copy(buf, _fileNameBuffer, (long)len - 1);

                    if (_fileNameBuffer == null) _fileNameBuffer = Encoding.Default.GetBytes("tnef.dat");
                    string strFileName = Encoding.Default.GetString(_fileNameBuffer);

                    PrintResult("{0}: WRITING {1}\n", BasePath, strFileName);

                    //new attachment found because attachment data goes before attachment name 
                    _attachment.FileName = strFileName;
                    _attachment.Subject = strSubject;
                    _attachments.Add(_attachment.FileName, _attachment);

                    geti16(); /* checksum */

                    break;

                case ATTACHDATA:
                    len = geti32();
                    PrintResult("ATTACH-DATA: {0} bytes\n", len);

                    _attachment = new TNEFAttachment();
                    _attachment.FileContent = new byte[len];
                    _attachment.FileLength = len;

                    for (i = 0; i < len; )
                    {
                        chunk = len - i;
                        if (chunk > buf.Length) chunk = buf.Length;

                        StreamReadBytes(buf, chunk);

                        Array.Copy(buf, 0, _attachment.FileContent, i, chunk);

                        i += chunk;
                    }

                    geti16(); /* checksum */

                    break;

                default:
                    decode_attribute(d);
                    break;
            }
        }

        /// <summary> 
        /// decoded attachments 
        /// </summary> 
        /// <returns>attachment array</returns> 
        public Hashtable Attachments()
        {
            return _attachments;
        }

        /// <summary> 
        /// save all decoded attachments to files 
        /// </summary> 
        /// <returns>true is succeded, vice versa</returns> 
        public bool SaveAttachments()
        {
            bool blnRet = false;
            IDictionaryEnumerator ideAttachments = _attachments.GetEnumerator();

            while (ideAttachments.MoveNext())
            {
                blnRet = SaveAttachment((TNEFAttachment)ideAttachments.Value);
            }

            return blnRet;
        }

        /// <summary> 
        /// save a decoded attachment to file 
        /// </summary> 
        /// <param name="attachment">decoded attachment</param> 
        /// <returns>true is succeded, vice versa</returns> 
        public bool SaveAttachment(TNEFAttachment attachment)
        {
            try
            {
                string strOutFile = BasePath + attachment.FileName;

                if (File.Exists(strOutFile))
                    File.Delete(strOutFile);
                FileStream fsData = new FileStream(strOutFile, FileMode.CreateNew, FileAccess.Write);

                fsData.Write(attachment.FileContent, 0, (int)attachment.FileLength);

                fsData.Close();

                return true;
            }
            catch (Exception e)
            {
                Utility.LogError("SaveAttachment():" + e.Message);
                return false;
            }
        }

        /// <summary> 
        /// parse MS-TNEF stream 
        /// </summary> 
        /// <returns>true is succeded, vice versa</returns> 
        public bool Parse()
        {
            byte[] buf = new byte[4];
            int d;

            if (FindSignature())
            {
                if (SkipSignature < 2)
                {
                    d = geti32();
                    if (SkipSignature < 1)
                    {
                        if (d != TNEF_SIGNATURE)
                        {
                            PrintResult("Seems not to be a TNEF file\n");
                            return false;
                        }
                    }
                }

                d = geti16();
                PrintResult("TNEF Key is: {0}\n", d);
                for (; ; )
                {
                    if (StreamReadBytes(buf, 1) == 0)
                        break;

                    d = (int)buf[0];

                    switch (d)
                    {
                        case LVL_MESSAGE:
                            PrintResult("{0}: Decoding Message Attributes\n", fsTNEF.Position);
                            decode_message();
                            break;
                        case LVL_ATTACHMENT:
                            PrintResult("Decoding Attachment\n");
                            decode_attachment();
                            break;
                        default:
                            PrintResult("Coding Error in TNEF file\n");
                            return false;
                    }
                }
                return true;
            }
            else
                return false;
        }

        private void PrintResult(string strResult, params object[] strContent)
        {
            string strRet = string.Format(strResult, strContent);
            if (Verbose)
                Utility.LogError(strRet);
        }

        ~TNEFParser()
        {
            _attachments = null;
            CloseTNEFStream();
        }

        public TNEFParser()
        {
        }

        /// <summary> 
        /// open MS-TNEF stream from a file 
        /// </summary> 
        /// <param name="strFile">MS-TNEF file</param> 
        public TNEFParser(string strFile)
        {
            OpenTNEFStream(strFile);
        }

        /// <summary> 
        /// open MS-TNEF stream from bytes 
        /// </summary> 
        /// <param name="bytContents">MS-TNEF bytes</param> 
        public TNEFParser(byte[] bytContents)
        {
            OpenTNEFStream(bytContents);
        }
    }

    /// <summary> 
    /// Summary description for Utility. 
    /// </summary> 
    public class Utility
    {
        private static bool m_blnLog = false;
        private static string m_strLogFile = "OpenPOP.log";

        public Utility()
        {
        }

        //  public static string[] SplitText(string strText, string strSplitter) 
        //  { 
        //   string []segments=new string[0]; 
        //   int indexOfstrSplitter=strText.IndexOf(strSplitter); 
        //   if(indexOfstrSplitter!=-1) 
        //   { 
        // 
        //   } 
        //   return segments; 
        //  } 
        // 

        /// <summary> 
        /// Verifies whether the file is of picture type or not 
        /// </summary> 
        /// <param name="strFile">File to be verified</param> 
        /// <returns>True if picture file, false if not</returns> 
        public static bool IsPictureFile(string strFile)
        {
            try
            {
                if (strFile != null && strFile != "")
                {
                    strFile = strFile.ToLower();
                    if (strFile.EndsWith(".jpg") || strFile.EndsWith(".bmp") || strFile.EndsWith(".ico") || strFile.EndsWith(".gif") || strFile.EndsWith(".png"))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary> 
        /// Parse date time info from MIME header 
        /// </summary> 
        /// <param name="strDate">Encoded MIME date time</param> 
        /// <returns>Decoded date time info</returns> 
        public static string ParseEmailDate(string strDate)
        {
            string strRet = strDate.Trim();
            int indexOfTag = strRet.IndexOf(",");
            if (indexOfTag != -1)
            {
                strRet = strRet.Substring(indexOfTag + 1);
            }

            strRet = QuoteText(strRet, "+");
            strRet = QuoteText(strRet, "-");
            strRet = QuoteText(strRet, "GMT");
            strRet = QuoteText(strRet, "CST");
            return strRet.Trim();
        }

        /// <summary> 
        /// Quote the text according to a tag 
        /// </summary> 
        /// <param name="strText">Text to be quoted</param> 
        /// <param name="strTag">Quote tag</param> 
        /// <returns>Quoted Text</returns> 
        public static string QuoteText(string strText, string strTag)
        {
            int indexOfTag = strText.IndexOf(strTag);
            if (indexOfTag != -1)
                return strText.Substring(0, indexOfTag - 1);
            else
                return strText;
        }

        /// <summary> 
        /// Parse file name from MIME header 
        /// </summary> 
        /// <param name="strHeader">MIME header</param> 
        /// <returns>Decoded file name</returns> 
        public static string ParseFileName(string strHeader)
        {
            string strTag;
            strTag = "filename=";
            int intPos = strHeader.ToLower().IndexOf(strTag);
            if (intPos == -1)
            {
                strTag = "name=";
                intPos = strHeader.ToLower().IndexOf(strTag);
            }
            string strRet;
            if (intPos != -1)
            {
                strRet = strHeader.Substring(intPos + strTag.Length);
                intPos = strRet.ToLower().IndexOf(";");
                if (intPos != -1)
                    strRet = strRet.Substring(1, intPos - 1);
                strRet = RemoveQuote(strRet);
            }
            else
                strRet = "";

            return strRet;
        }

        /// <summary> 
        /// Parse email address from MIME header 
        /// </summary> 
        /// <param name="strEmailAddress">MIME header</param> 
        /// <param name="strUser">Decoded user name</param> 
        /// <param name="strAddress">Decoded email address</param> 
        /// <returns>True if decoding succeeded, false if failed</returns> 
        public static bool ParseEmailAddress(string strEmailAddress, ref string strUser, ref string strAddress)
        {
            int indexOfAB = strEmailAddress.Trim().LastIndexOf("<");
            int indexOfEndAB = strEmailAddress.Trim().LastIndexOf(">");
            strUser = strEmailAddress;
            strAddress = strEmailAddress;
            if (indexOfAB >= 0 && indexOfEndAB >= 0)
            {
                if (indexOfAB > 0)
                {
                    strUser = strUser.Substring(0, indexOfAB - 1);
                    //     strUser=strUser.Substring(0,indexOfAB-1).Trim('\"'); 
                    //     if(strUser.IndexOf("\"")>=0) 
                    //     { 
                    //      strUser=strUser.Substring(1,strUser.Length-1); 
                    //     } 
                }
                strUser = strUser.Trim();
                strUser = strUser.Trim('\"');
                strAddress = strAddress.Substring(indexOfAB + 1, indexOfEndAB - (indexOfAB + 1));
            }
            strUser = strUser.Trim();
            strUser = DecodeText(strUser);
            strAddress = strAddress.Trim();

            return true;
        }

        /// <summary> 
        /// Save byte content to a file 
        /// </summary> 
        /// <param name="strFile">File to be saved to</param> 
        /// <param name="bytContent">Byte array content</param> 
        /// <returns>True if saving succeeded, false if failed</returns> 
        public static bool SaveByteContentToFile(string strFile, byte[] bytContent)
        {
            try
            {
                if (File.Exists(strFile))
                    File.Delete(strFile);
                FileStream fs = File.Create(strFile);
                fs.Write(bytContent, 0, bytContent.Length);
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                Utility.LogError("SaveByteContentToFile():" + e.Message);
                return false;
            }
        }

        /// <summary> 
        /// Save text content to a file 
        /// </summary> 
        /// <param name="strFile">File to be saved to</param> 
        /// <param name="strText">Text content</param> 
        /// <param name="blnReplaceExists">Replace file if exists</param> 
        /// <returns>True if saving succeeded, false if failed</returns> 
        public static bool SavePlainTextToFile(string strFile, string strText, bool blnReplaceExists)
        {
            try
            {
                bool blnRet = true;

                if (File.Exists(strFile))
                {
                    if (blnReplaceExists)
                        File.Delete(strFile);
                    else
                        blnRet = false;
                }

                if (blnRet == true)
                {
                    StreamWriter sw = File.CreateText(strFile);
                    sw.Write(strText);
                    sw.Close();
                }

                return blnRet;
            }
            catch (Exception e)
            {
                Utility.LogError("SavePlainTextToFile():" + e.Message);
                return false;
            }
        }

        /// <summary> 
        /// Read text content from a file 
        /// </summary> 
        /// <param name="strFile">File to be read from</param> 
        /// <param name="strText">Read text content</param> 
        /// <returns>True if reading succeeded, false if failed</returns> 
        public static bool ReadPlainTextFromFile(string strFile, ref string strText)
        {
            if (File.Exists(strFile))
            {
                StreamReader fs = new StreamReader(strFile);
                strText = fs.ReadToEnd();
                fs.Close();
                return true;
            }
            else
                return false;
        }

        /// <summary> 
        /// Sepearte header name and header value 
        /// </summary> 
        /// <param name="strRawHeader"></param> 
        /// <returns></returns> 
        public static string[] GetHeadersValue(string strRawHeader)
        {
            if (strRawHeader == null)
                throw new ArgumentNullException("strRawHeader", "Argument was null");

            string[] array = new string[2] { "", "" };
            int indexOfColon = strRawHeader.IndexOf(":");

            try
            {
                array[0] = strRawHeader.Substring(0, indexOfColon).Trim();
                array[1] = strRawHeader.Substring(indexOfColon + 1).Trim();
            }
            catch (Exception)
            {
            }

            return array;
        }

        /// <summary> 
        /// Get quoted text 
        /// </summary> 
        /// <param name="strText">Text with quotes</param> 
        /// <param name="strSplitter">Splitter</param> 
        /// <param name="strTag">Target tag</param> 
        /// <returns>Text without quote</returns> 
        public static string GetQuotedValue(string strText, string strSplitter, string strTag)
        {
            if (strText == null)
                throw new ArgumentNullException("strText", "Argument was null");

            string[] array = new string[2] { "", "" };
            int indexOfstrSplitter = strText.IndexOf(strSplitter);

            try
            {
                array[0] = strText.Substring(0, indexOfstrSplitter).Trim();
                array[1] = strText.Substring(indexOfstrSplitter + 1).Trim();
                int pos = array[1].IndexOf("\"");
                if (pos != -1)
                {
                    int pos2 = array[1].IndexOf("\"", pos + 1);
                    array[1] = array[1].Substring(pos + 1, pos2 - pos - 1);
                }
            }
            catch (Exception)
            {
            }

            //return array; 
            if (array[0].ToLower() == strTag.ToLower())
                return array[1].Trim();
            else
                return null;

            /*   string []array=null; 
               try 
               { 
                array=Regex.Split(strText,strSplitter); 
                //return array; 
                if(array[0].ToLower()==strTag.ToLower()) 
                 return RemoveQuote(array[1].Trim()); 
                else 
                 return null; 
               } 
               catch 
               {return null;}*/
        }

        /// <summary> 
        /// Change text encoding 
        /// </summary> 
        /// <param name="strText">Source encoded text</param> 
        /// <param name="strCharset">New charset</param> 
        /// <returns>Encoded text with new charset</returns> 
        public static string Change(string strText, string strCharset)
        {
            if (strCharset == null || strCharset == "")
                return strText;
            byte[] b = Encoding.Default.GetBytes(strText);
            return new string(Encoding.GetEncoding(strCharset).GetChars(b));
        }

        /// <summary> 
        /// Remove non-standard base 64 characters 
        /// </summary> 
        /// <param name="strText">Source text</param> 
        /// <returns>standard base 64 text</returns> 
        public static string RemoveNonB64(string strText)
        {
            return strText.Replace("\0", "");
        }

        /// <summary> 
        /// Remove white blank characters 
        /// </summary> 
        /// <param name="strText">Source text</param> 
        /// <returns>Text with white blanks</returns> 
        public static string RemoveWhiteBlanks(string strText)
        {
            return strText.Replace("\0", "").Replace("\r\n", "");
        }

        /// <summary> 
        /// Remove quotes 
        /// </summary> 
        /// <param name="strText">Text with quotes</param> 
        /// <returns>Text without quotes</returns> 
        public static string RemoveQuote(string strText)
        {
            string strRet = strText;
            if (strRet.StartsWith("\""))
                strRet = strRet.Substring(1);
            if (strRet.EndsWith("\""))
                strRet = strRet.Substring(0, strRet.Length - 1);
            return strRet;
        }

        /// <summary> 
        /// Decode one line of text 
        /// </summary> 
        /// <param name="strText">Encoded text</param> 
        /// <returns>Decoded text</returns> 
        public static string DecodeLine(string strText)
        {
            return DecodeText(RemoveWhiteBlanks(strText));
        }

        /// <summary> 
        /// Verifies wether the text is a valid MIME Text or not 
        /// </summary> 
        /// <param name="strText">Text to be verified</param> 
        /// <returns>True if MIME text, false if not</returns> 
        private static bool IsValidMIMEText(string strText)
        {
            int intPos = strText.IndexOf("=?");
            return (intPos != -1 && strText.IndexOf("?=", intPos + 6) != -1 && strText.Length > 7);
        }

        /// <summary> 
        /// Decode text 
        /// </summary> 
        /// <param name="strText">Source text</param> 
        /// <returns>Decoded text</returns> 
        public static string DecodeText(string strText)
        {
            /*try 
            { 
             string strRet=""; 
             string strBody=""; 
             MatchCollection mc=Regex.Matches(strText,@"\=\?(?<Charset>\S+)\?(?<Encoding>\w)\?(?<Content>\S+)\?\="); 

             for(int i=0;i<mc.Count;i++) 
             { 
              if(mc[i].Success) 
              { 
               strBody=mc[i].Groups["Content"].Value; 

               switch(mc[i].Groups["Encoding"].Value.ToUpper()) 
               { 
                case "B": 
                 strBody=deCodeB64s(strBody,mc[i].Groups["Charset"].Value); 
                 break; 
                case "Q": 
                 strBody=DecodeQP.ConvertHexContent(strBody);//, m.Groups["Charset"].Value); 
                 break; 
                default: 
                 break; 
               } 
               strRet+=strBody; 
              } 
              else 
              { 
               strRet+=mc[i].Value; 
              } 
             } 
             return strRet; 
            } 
            catch 
            {return strText;}*/

            try
            {
                string strRet = "";
                string[] strParts = Regex.Split(strText, "\r\n");
                string strBody = "";
                const string strRegEx = @"\=\?(?<Charset>\S+)\?(?<Encoding>\w)\?(?<Content>\S+)\?\=";
                Match m = null;

                for (int i = 0; i < strParts.Length; i++)
                {
                    m = Regex.Match(strParts[i], strRegEx);
                    if (m.Success)
                    {
                        strBody = m.Groups["Content"].Value;

                        switch (m.Groups["Encoding"].Value.ToUpper())
                        {
                            case "B":
                                strBody = deCodeB64s(strBody, m.Groups["Charset"].Value);
                                break;
                            case "Q":
                                strBody = DecodeQP.ConvertHexContent(strBody); //, m.Groups["Charset"].Value); 
                                break;
                            default:
                                break;
                        }
                        strRet += strBody;
                    }
                    else
                    {
                        if (!IsValidMIMEText(strParts[i]))
                            strRet += strParts[i];
                        else
                        {
                            //blank text 
                        }
                    }
                }
                return strRet;
            }
            catch
            {
                return strText;
            }

            /*   
              { 
               try 
               { 
                if(strText!=null&&strText!="") 
                { 
                 if(IsValidMIMEText(strText)) 
                 { 
                  //position at the end of charset 
                  int intPos=strText.IndexOf("=?"); 
                  int intPos2=strText.IndexOf("?",intPos+2); 
                  if(intPos2>3) 
                  { 
                   string strCharset=strText.Substring(2,intPos2-2); 
                   string strEncoding=strText.Substring(intPos2+1,1); 
                   int intPos3=strText.IndexOf("?=",intPos2+3); 
                   string strBody=strText.Substring(intPos2+3,intPos3-intPos2-3); 
                   string strHead=""; 
                   if(intPos>0) 
                   { 
                    strHead=strText.Substring(0,intPos-1); 
                   } 
                   string strEnd=""; 
                   if(intPos3<strText.Length-2) 
                   { 
                    strEnd=strText.Substring(intPos3+2); 
                   } 
                   switch(strEncoding.ToUpper()) 
                   { 
                    case "B": 
                     strBody=deCodeB64s(strBody); 
                     break; 
                    case "Q": 
                     strBody=DecodeQP.ConvertHexContent(strBody); 
                     break; 
                    default: 
                     break; 
                   } 
                   strText=strHead+strBody+strEnd; 
                   if(IsValidMIMEText(strText)) 
                    return DecodeText(strText); 
                   else 
                    return strText; 
                  } 
                  else 
                  {return strText;} 
                 } 
                 else 
                 {return strText;} 
                } 
                else 
                {return strText;} 
               } 
               catch 
               {return strText;}*/
        }

        /// <summary> 
        /// 
        /// </summary> 
        /// <param name="strText"></param> 
        /// <returns></returns> 
        public static string deCodeB64s(string strText)
        {
            return Encoding.Default.GetString(deCodeB64(strText));
        }

        public static string deCodeB64s(string strText, string strEncoding)
        {
            try
            {
                if (strEncoding.ToLower() == "ISO-8859-1".ToLower())
                    return deCodeB64s(strText);
                else
                    return Encoding.GetEncoding(strEncoding).GetString(deCodeB64(strText));
            }
            catch
            {
                return deCodeB64s(strText);
            }
        }

        private static byte[] deCodeB64(string strText)
        {
            byte[] by = null;
            try
            {
                if (strText != "")
                {
                    by = Convert.FromBase64String(strText);
                    //strText=Encoding.Default.GetString(by); 
                }
            }
            catch (Exception e)
            {
                by = Encoding.Default.GetBytes("\0");
                LogError("deCodeB64():" + e.Message);
            }
            return by;
        }

        /// <summary> 
        /// Turns file logging on and off. 
        /// </summary> 
        /// <remarks>Comming soon.</remarks> 
        public static bool Log
        {
            get
            {
                return m_blnLog;
            }
            set
            {
                m_blnLog = value;
            }
        }

        internal static void LogError(string strText)
        {
            //Log=true; 
            if (Log)
            {
                FileInfo file = null;
                FileStream fs = null;
                StreamWriter sw = null;
                try
                {
                    file = new FileInfo(m_strLogFile);
                    sw = file.AppendText();
                    //fs = new FileStream(m_strLogFile, FileMode.OpenOrCreate, FileAccess.Write); 
                    //sw = new StreamWriter(fs); 
                    sw.WriteLine(DateTime.Now);
                    sw.WriteLine(strText);
                    sw.WriteLine("\r\n");
                    sw.Flush();
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Close();
                        sw = null;
                    }
                    if (fs != null)
                    {
                        fs.Close();
                        fs = null;
                    }

                }
            }
        }

        public static bool IsQuotedPrintable(string strText)
        {
            if (strText != null)
                return (strText.ToLower() == "quoted-printable".ToLower());
            else
                return false;
        }

        public static bool IsBase64(string strText)
        {
            if (strText != null)
                return (strText.ToLower() == "base64".ToLower());
            else
                return false;
        }

        public static string[] SplitOnSemiColon(string strText)
        {
            if (strText == null)
                throw new ArgumentNullException("strText", "Argument was null");

            string[] array = null;
            int indexOfColon = strText.IndexOf(";");

            if (indexOfColon < 0)
            {
                array = new string[1];
                array[0] = strText;
                return array;
            }
            else
            {
                array = new string[2];
            }

            try
            {
                array[0] = strText.Substring(0, indexOfColon).Trim();
                array[1] = strText.Substring(indexOfColon + 1).Trim();
            }
            catch (Exception)
            {
            }

            return array;
        }

        public static bool IsNotNullText(string strText)
        {
            try
            {
                return (strText != null && strText != "");
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNotNullTextEx(string strText)
        {
            try
            {
                return (strText != null && strText.Trim() != "");
            }
            catch
            {
                return false;
            }
        }

        public static bool IsOrNullTextEx(string strText)
        {
            try
            {
                return (strText == null || strText.Trim() == "");
            }
            catch
            {
                return false;
            }
        }

    }

}

namespace OpenPOP.POP3
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Collections;
    using System.Net.Sockets;
    using System.Security.Cryptography;
    using System.Threading;

    /// <summary> 
    /// Possible responses received from the server when performing an Authentication 
    /// </summary> 
    public enum AuthenticationResponse
    {
        /// <summary> 
        /// Authentication succeeded 
        /// </summary> 
        SUCCESS = 0,
        /// <summary> 
        /// Login doesn't exist on the POP3 server 
        /// </summary>   
        INVALIDUSER = 1,
        /// <summary> 
        /// Password is invalid for the give login 
        /// </summary> 
        INVALIDPASSWORD = 2,
        /// <summary> 
        /// Invalid login and/or password 
        /// </summary> 
        INVALIDUSERORPASSWORD = 3
    }

    /// <summary> 
    /// Authentication method to use 
    /// </summary> 
    /// <remarks>TRYBOTH means code will first attempt by using APOP method as its more secure. 
    ///  In case of failure the code will fall back to USERPASS method. 
    /// </remarks> 
    public enum AuthenticationMethod
    {
        /// <summary> 
        /// Authenticate using the USER/PASS method. USER/PASS is secure but all POP3 servers may not support this method 
        /// </summary> 
        USERPASS = 0,
        /// <summary> 
        /// Authenticate using the APOP method 
        /// </summary> 
        APOP = 1,
        /// <summary> 
        /// Authenticate using USER/PASS. In case USER/PASS fails then revert to APOP 
        /// </summary> 
        TRYBOTH = 2
    }

    /// <summary> 
    /// Thrown when the POP3 Server sends an error (-ERR) during intial handshake (HELO) 
    /// </summary> 
    public class PopServerNotAvailableException : Exception
    {
    }

    /// <summary> 
    /// Thrown when the specified POP3 Server can not be found or connected with 
    /// </summary>  
    public class PopServerNotFoundException : Exception
    {
    }

    /// <summary> 
    /// Thrown when the attachment is not in a format supported by OpenPOP.NET 
    /// </summary> 
    /// <remarks>Supported attachment encodings are Base64,Quoted Printable,MS TNEF</remarks> 
    public class AttachmentEncodingNotSupportedException : Exception
    {
    }

    /// <summary> 
    /// Thrown when the supplied login doesn't exist on the server 
    /// </summary> 
    /// <remarks>Should be used only when using USER/PASS Authentication Method</remarks> 
    public class InvalidLoginException : Exception
    {
    }

    /// <summary> 
    /// Thrown when the password supplied for the login is invalid 
    /// </summary>  
    /// <remarks>Should be used only when using USER/PASS Authentication Method</remarks> 
    public class InvalidPasswordException : Exception
    {
    }

    /// <summary> 
    /// Thrown when either the login or the password is invalid on the POP3 Server 
    /// </summary> 
    /// /// <remarks>Should be used only when using APOP Authentication Method</remarks> 
    public class InvalidLoginOrPasswordException : Exception
    {
    }

    /// <summary> 
    /// Thrown when the user mailbox is in a locked state 
    /// </summary> 
    /// <remarks>The mail boxes are locked when an existing session is open on the mail server. Lock conditions are also met in case of aborted sessions</remarks> 
    public class PopServerLockException : Exception
    {
    }

    /// <summary> 
    /// Summary description for MyMD5. 
    /// </summary> 
    public class MyMD5
    {
        public static string GetMD5Hash(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //the GetBytes method returns byte array equavalent of a string 
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            char[] temp = new char[res.Length];
            //copy to a char array which can be passed to a String constructor 
            System.Array.Copy(res, temp, res.Length);
            //return the result as a string 
            return new String(temp);
        }

        public static string GetMD5HashHex(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            DES des = new DESCryptoServiceProvider();
            //the GetBytes method returns byte array equavalent of a string 
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);

            String returnThis = "";

            for (int i = 0; i < res.Length; i++)
            {
                returnThis += System.Uri.HexEscape((char)res[i]);
            }
            returnThis = returnThis.Replace("%", "");
            returnThis = returnThis.ToLower();

            return returnThis;


        }

    }

    /// <summary> 
    /// POPClient 
    /// </summary> 
    public class POPClient
    {
        /// <summary> 
        /// Event that fires when begin to connect with target POP3 server. 
        /// </summary> 
        public event EventHandler CommunicationBegan;

        /// <summary> 
        /// Event that fires when connected with target POP3 server. 
        /// </summary> 
        public event EventHandler CommunicationOccured;

        /// <summary> 
        /// Event that fires when disconnected with target POP3 server. 
        /// </summary> 
        public event EventHandler CommunicationLost;

        /// <summary> 
        /// Event that fires when authentication began with target POP3 server. 
        /// </summary> 
        public event EventHandler AuthenticationBegan;

        /// <summary> 
        /// Event that fires when authentication finished with target POP3 server. 
        /// </summary> 
        public event EventHandler AuthenticationFinished;

        /// <summary> 
        /// Event that fires when message transfer has begun. 
        /// </summary>   
        public event EventHandler MessageTransferBegan;

        /// <summary> 
        /// Event that fires when message transfer has finished. 
        /// </summary> 
        public event EventHandler MessageTransferFinished;

        internal void OnCommunicationBegan(EventArgs e)
        {
            if (CommunicationBegan != null)
                CommunicationBegan(this, e);
        }

        internal void OnCommunicationOccured(EventArgs e)
        {
            if (CommunicationOccured != null)
                CommunicationOccured(this, e);
        }

        internal void OnCommunicationLost(EventArgs e)
        {
            if (CommunicationLost != null)
                CommunicationLost(this, e);
        }

        internal void OnAuthenticationBegan(EventArgs e)
        {
            if (AuthenticationBegan != null)
                AuthenticationBegan(this, e);
        }

        internal void OnAuthenticationFinished(EventArgs e)
        {
            if (AuthenticationFinished != null)
                AuthenticationFinished(this, e);
        }

        internal void OnMessageTransferBegan(EventArgs e)
        {
            if (MessageTransferBegan != null)
                MessageTransferBegan(this, e);
        }

        internal void OnMessageTransferFinished(EventArgs e)
        {
            if (MessageTransferFinished != null)
                MessageTransferFinished(this, e);
        }

        private const string RESPONSE_OK = "+OK";
        //private const string RESPONSE_ERR="-ERR"; 
        private TcpClient clientSocket = null;
        private StreamReader reader;
        private StreamWriter writer;
        private string _Error = "";
        private int _receiveTimeOut = 60000;
        private int _sendTimeOut = 60000;
        private int _receiveBufferSize = 4090;
        private int _sendBufferSize = 4090;
        private string _basePath = null;
        private bool _receiveFinish = false;
        private bool _autoDecodeMSTNEF = true;
        private int _waitForResponseInterval = 200;
        private int _receiveContentSleepInterval = 100;
        private string _aPOPTimestamp;
        private string _lastCommandResponse;
        private bool _connected = true;


        public bool Connected
        {
            get
            {
                return _connected;
            }
        }

        public string APOPTimestamp
        {
            get
            {
                return _aPOPTimestamp;
            }
        }

        /// <summary> 
        /// receive content sleep interval 
        /// </summary> 
        public int ReceiveContentSleepInterval
        {
            get
            {
                return _receiveContentSleepInterval;
            }
            set
            {
                _receiveContentSleepInterval = value;
            }
        }

        /// <summary> 
        /// wait for response interval 
        /// </summary> 
        public int WaitForResponseInterval
        {
            get
            {
                return _waitForResponseInterval;
            }
            set
            {
                _waitForResponseInterval = value;
            }
        }

        /// <summary> 
        /// whether auto decoding MS-TNEF attachment files 
        /// </summary> 
        public bool AutoDecodeMSTNEF
        {
            get
            {
                return _autoDecodeMSTNEF;
            }
            set
            {
                _autoDecodeMSTNEF = value;
            }
        }

        /// <summary> 
        /// path to extract MS-TNEF attachment files 
        /// </summary> 
        public string BasePath
        {
            get
            {
                return _basePath;
            }
            set
            {
                try
                {
                    if (value.EndsWith("\\"))
                        _basePath = value;
                    else
                        _basePath = value + "\\";
                }
                catch
                {
                }
            }
        }

        /// <summary> 
        /// Receive timeout for the connection to the SMTP server in milliseconds. 
        /// The default value is 60000 milliseconds. 
        /// </summary> 
        public int ReceiveTimeOut
        {
            get
            {
                return _receiveTimeOut;
            }
            set
            {
                _receiveTimeOut = value;
            }
        }

        /// <summary> 
        /// Send timeout for the connection to the SMTP server in milliseconds. 
        /// The default value is 60000 milliseconds. 
        /// </summary> 
        public int SendTimeOut
        {
            get
            {
                return _sendTimeOut;
            }
            set
            {
                _sendTimeOut = value;
            }
        }

        /// <summary> 
        /// Receive buffer size 
        /// </summary> 
        public int ReceiveBufferSize
        {
            get
            {
                return _receiveBufferSize;
            }
            set
            {
                _receiveBufferSize = value;
            }
        }

        /// <summary> 
        /// Send buffer size 
        /// </summary> 
        public int SendBufferSize
        {
            get
            {
                return _sendBufferSize;
            }
            set
            {
                _sendBufferSize = value;
            }
        }

        private void WaitForResponse(bool blnCondiction, int intInterval)
        {
            if (intInterval == 0)
                intInterval = WaitForResponseInterval;
            while (!blnCondiction == true)
            {
                Thread.Sleep(intInterval);
            }
        }

        private void WaitForResponse(ref StreamReader rdReader, int intInterval)
        {
            if (intInterval == 0)
                intInterval = WaitForResponseInterval;
            //while(rdReader.Peek()==-1 || !rdReader.BaseStream.CanRead) 
            while (!rdReader.BaseStream.CanRead)
            {
                Thread.Sleep(intInterval);
            }
        }

        private void WaitForResponse(ref StreamReader rdReader)
        {
            DateTime dtStart = DateTime.Now;
            TimeSpan tsSpan;
            while (!rdReader.BaseStream.CanRead)
            {
                tsSpan = DateTime.Now.Subtract(dtStart);
                if (tsSpan.Milliseconds > _receiveTimeOut)
                    break;
                Thread.Sleep(_waitForResponseInterval);
            }
        }

        private void WaitForResponse(ref StreamWriter wrWriter, int intInterval)
        {
            if (intInterval == 0)
                intInterval = WaitForResponseInterval;
            while (!wrWriter.BaseStream.CanWrite)
            {
                Thread.Sleep(intInterval);
            }
        }

        /// <summary> 
        /// Examines string to see if it contains a timestamp to use with the APOP command 
        /// If it does, sets the ApopTimestamp property to this value 
        /// </summary> 
        /// <param name="strResponse">string to examine</param> 
        private void ExtractApopTimestamp(string strResponse)
        {
            Match match = Regex.Match(strResponse, "<.+>");
            if (match.Success)
            {
                _aPOPTimestamp = match.Value;
            }
        }

        /// <summary> 
        /// Tests a string to see if it's a "+OK" string 
        /// </summary> 
        /// <param name="strResponse">string to examine</param> 
        /// <returns>true if response is an "+OK" string</returns> 
        private bool IsOkResponse(string strResponse)
        {
            return (strResponse.Substring(0, 3) == RESPONSE_OK);
        }

        /// <summary> 
        /// get response content 
        /// </summary> 
        /// <param name="strResponse">string to examine</param> 
        /// <returns>response content</returns> 
        private string GetResponseContent()
        {
            return _lastCommandResponse.Substring(3);
        }

        /// <summary> 
        /// Sends a command to the POP server. 
        /// </summary> 
        /// <param name="strCommand">command to send to server</param> 
        /// <param name="blnSilent">Do not give error</param> 
        /// <returns>true if server responded "+OK"</returns> 
        private bool SendCommand(string strCommand, bool blnSilent)
        {
            _lastCommandResponse = "";
            try
            {
                if (writer.BaseStream.CanWrite)
                {
                    writer.WriteLine(strCommand);
                    writer.Flush();
                    //WaitForResponse(ref reader,WaitForResponseInterval); 
                    WaitForResponse(ref reader);
                    _lastCommandResponse = reader.ReadLine();
                    return IsOkResponse(_lastCommandResponse);
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                if (!blnSilent)
                {
                    _Error = strCommand + ":" + e.Message;
                    Utility.LogError(_Error);
                }
                return false;
            }
        }

        /// <summary> 
        /// Sends a command to the POP server. 
        /// </summary> 
        /// <param name="strCommand">command to send to server</param> 
        /// <returns>true if server responded "+OK"</returns> 
        private bool SendCommand(string strCommand)
        {
            return SendCommand(strCommand, false);
        }

        /// <summary> 
        /// Sends a command to the POP server, expects an integer reply in the response 
        /// </summary> 
        /// <param name="strCommand">command to send to server</param> 
        /// <returns>integer value in the reply</returns> 
        private int SendCommandIntResponse(string strCommand)
        {
            int retVal = 0;
            if (SendCommand(strCommand))
            {
                try
                {
                    retVal = int.Parse(_lastCommandResponse.Split(' ')[1]);
                }
                catch (Exception e)
                {
                    Utility.LogError(strCommand + ":" + e.Message);
                }
            }
            return retVal;
        }

        /// <summary> 
        /// Construct new POPClient 
        /// </summary> 
        public POPClient()
        {
            Utility.Log = false;
        }

        /// <summary> 
        /// Construct new POPClient 
        /// </summary> 
        public POPClient(string strHost, int intPort, string strlogin, string strPassword, AuthenticationMethod authenticationMethod)
        {
            Connect(strHost, intPort);
            Authenticate(strlogin, strPassword, authenticationMethod);
        }

        /// <summary> 
        /// connect to remote server 
        /// </summary> 
        /// <param name="strHost">POP3 host</param> 
        /// <param name="intPort">POP3 port</param> 
        public void Connect(string strHost, int intPort)
        {
            OnCommunicationBegan(EventArgs.Empty);

            clientSocket = new TcpClient();
            clientSocket.ReceiveTimeout = _receiveTimeOut;
            clientSocket.SendTimeout = _sendTimeOut;
            clientSocket.ReceiveBufferSize = _receiveBufferSize;
            clientSocket.SendBufferSize = _sendBufferSize;

            try
            {
                clientSocket.Connect(strHost, intPort);
            }
            catch (SocketException e)
            {
                Disconnect();
                Utility.LogError("Connect():" + e.Message);
                throw new PopServerNotFoundException();
            }

            reader = new StreamReader(clientSocket.GetStream(), Encoding.Default, true);
            writer = new StreamWriter(clientSocket.GetStream());
            writer.AutoFlush = true;

            WaitForResponse(ref reader, WaitForResponseInterval);

            string strResponse = reader.ReadLine();

            if (IsOkResponse(strResponse))
            {
                ExtractApopTimestamp(strResponse);
                _connected = true;
                OnCommunicationOccured(EventArgs.Empty);
            }
            else
            {
                Disconnect();
                Utility.LogError("Connect():" + "Error when login, maybe POP3 server not exist");
                throw new PopServerNotAvailableException();
            }
        }

        /// <summary> 
        /// Disconnect from POP3 server 
        /// </summary> 
        public void Disconnect()
        {
            try
            {
                clientSocket.ReceiveTimeout = 500;
                clientSocket.SendTimeout = 500;
                SendCommand("QUIT", true);
                clientSocket.ReceiveTimeout = _receiveTimeOut;
                clientSocket.SendTimeout = _sendTimeOut;
                reader.Close();
                writer.Close();
                clientSocket.GetStream().Close();
                clientSocket.Close();
            }
            catch
            {
                //Utility.LogError("Disconnect():"+e.Message); 
            }
            finally
            {
                reader = null;
                writer = null;
                clientSocket = null;
            }
            OnCommunicationLost(EventArgs.Empty);
        }

        /// <summary> 
        /// release me 
        /// </summary> 
        ~POPClient()
        {
            Disconnect();
        }

        /// <summary> 
        /// verify user and password 
        /// </summary> 
        /// <param name="strlogin">user name</param> 
        /// <param name="strPassword">password</param> 
        public void Authenticate(string strlogin, string strPassword)
        {
            Authenticate(strlogin, strPassword, AuthenticationMethod.USERPASS);
        }

        /// <summary> 
        /// verify user and password 
        /// </summary> 
        /// <param name="strlogin">user name</param> 
        /// <param name="strPassword">strPassword</param> 
        /// <param name="authenticationMethod">verification mode</param> 
        public void Authenticate(string strlogin, string strPassword, AuthenticationMethod authenticationMethod)
        {
            if (authenticationMethod == AuthenticationMethod.USERPASS)
            {
                AuthenticateUsingUSER(strlogin, strPassword);
            }
            else if (authenticationMethod == AuthenticationMethod.APOP)
            {
                AuthenticateUsingAPOP(strlogin, strPassword);
            }
            else if (authenticationMethod == AuthenticationMethod.TRYBOTH)
            {
                try
                {
                    AuthenticateUsingUSER(strlogin, strPassword);
                }
                catch (InvalidLoginException e)
                {
                    Utility.LogError("Authenticate():" + e.Message);
                }
                catch (InvalidPasswordException e)
                {
                    Utility.LogError("Authenticate():" + e.Message);
                }
                catch (Exception e)
                {
                    Utility.LogError("Authenticate():" + e.Message);
                    AuthenticateUsingAPOP(strlogin, strPassword);
                }
            }
        }

        /// <summary> 
        /// verify user and password 
        /// </summary> 
        /// <param name="strlogin">user name</param> 
        /// <param name="strPassword">password</param> 
        private void AuthenticateUsingUSER(string strlogin, string strPassword)
        {
            OnAuthenticationBegan(EventArgs.Empty);

            if (!SendCommand("USER " + strlogin))
            {
                Utility.LogError("AuthenticateUsingUSER():wrong user");
                throw new InvalidLoginException();
            }

            WaitForResponse(ref writer, WaitForResponseInterval);

            if (!SendCommand("PASS " + strPassword))
            {
                if (_lastCommandResponse.ToLower().IndexOf("lock") != -1)
                {
                    Utility.LogError("AuthenticateUsingUSER():maildrop is locked");
                    throw new PopServerLockException();
                }
                else
                {
                    Utility.LogError("AuthenticateUsingUSER():wrong password or " + GetResponseContent());
                    throw new InvalidPasswordException();
                }
            }

            OnAuthenticationFinished(EventArgs.Empty);
        }

        /// <summary> 
        /// verify user and password using APOP 
        /// </summary> 
        /// <param name="strlogin">user name</param> 
        /// <param name="strPassword">password</param> 
        private void AuthenticateUsingAPOP(string strlogin, string strPassword)
        {
            OnAuthenticationBegan(EventArgs.Empty);

            if (!SendCommand("APOP " + strlogin + " " + MyMD5.GetMD5HashHex(strPassword)))
            {
                Utility.LogError("AuthenticateUsingAPOP():wrong user or password");
                throw new InvalidLoginOrPasswordException();
            }

            OnAuthenticationFinished(EventArgs.Empty);
        }

        /*  private string GetCommand(string input) 
          {    
           try 
           { 
            return input.Split(' ')[0]; 
           } 
           catch(Exception e) 
           { 
            Utility.LogError("GetCommand():"+e.Message); 
            return ""; 
           } 
          }*/

        private string[] GetParameters(string input)
        {
            string[] temp = input.Split(' ');
            string[] retStringArray = new string[temp.Length - 1];
            Array.Copy(temp, 1, retStringArray, 0, temp.Length - 1);

            return retStringArray;
        }

        /// <summary> 
        /// get message count 
        /// </summary> 
        /// <returns>message count</returns> 
        public int GetMessageCount()
        {
            return SendCommandIntResponse("STAT");
        }

        /// <summary> 
        /// Deletes message with given index when Close() is called 
        /// </summary> 
        /// <param name="intMessageIndex"> </param> 
        public bool DeleteMessage(int intMessageIndex)
        {
            return SendCommand("DELE " + intMessageIndex.ToString());
        }

        /// <summary> 
        /// Deletes messages 
        /// </summary> 
        public bool DeleteAllMessages()
        {
            int messageCount = GetMessageCount();
            for (int messageItem = messageCount; messageItem > 0; messageItem--)
            {
                if (!DeleteMessage(messageItem))
                    return false;
            }
            return true;
        }

        /// <summary> 
        /// quit POP3 server 
        /// </summary> 
        public bool QUIT()
        {
            return SendCommand("QUIT");
        }

        /// <summary> 
        /// keep server active 
        /// </summary> 
        public bool NOOP()
        {
            return SendCommand("NOOP");
        }

        /// <summary> 
        /// keep server active 
        /// </summary> 
        public bool RSET()
        {
            return SendCommand("RSET");
        }

        /// <summary> 
        /// identify user 
        /// </summary> 
        public bool USER()
        {
            return SendCommand("USER");

        }

        /// <summary> 
        /// get messages info 
        /// </summary> 
        /// <param name="intMessageNumber">message number</param> 
        /// <returns>Message object</returns> 
        public MIMEParser.Message GetMessageHeader(int intMessageNumber)
        {
            OnMessageTransferBegan(EventArgs.Empty);

            //MIMEParser.Message msg = FetchMessage("TOP " + intMessageNumber.ToString() + " 0", true);

            //modify by playyuer $at$ Microshaoft.com 
            MIMEParser.Message msg = FetchMessage(intMessageNumber, "TOP {0} 0", true);

            OnMessageTransferFinished(EventArgs.Empty);

            return msg;
        }

        /// <summary> 
        /// get message uid 
        /// </summary> 
        /// <param name="intMessageNumber">message number</param> 
        public string GetMessageUID(int intMessageNumber)
        {
            string[] strValues = null;
            if (SendCommand("UIDL " + intMessageNumber.ToString()))
            {
                strValues = GetParameters(_lastCommandResponse);
            }
            return strValues[1];
        }

        /// <summary> 
        /// get message uids 
        /// </summary> 
        public ArrayList GetMessageUIDs()
        {
            ArrayList uids = new ArrayList();
            if (SendCommand("UIDL"))
            {
                string strResponse = reader.ReadLine();
                while (strResponse != ".")
                {
                    uids.Add(strResponse.Split(' ')[1]);
                    strResponse = reader.ReadLine();
                }
                return uids;
            }
            else
            {
                return null;
            }
        }

        /// <summary> 
        /// Get the sizes of all the messages 
        /// CAUTION:  Assumes no messages have been deleted 
        /// </summary> 
        /// <returns>Size of each message</returns> 
        public ArrayList LIST()
        {
            ArrayList sizes = new ArrayList();
            if (SendCommand("LIST"))
            {
                string strResponse = reader.ReadLine();
                while (strResponse != ".")
                {
                    sizes.Add(int.Parse(strResponse.Split(' ')[1]));
                    strResponse = reader.ReadLine();
                }
                return sizes;
            }
            else
            {
                return null;
            }
        }

        /// <summary> 
        /// get the size of a message 
        /// </summary> 
        /// <param name="intMessageNumber">message number</param> 
        /// <returns>Size of message</returns> 
        public int LIST(int intMessageNumber)
        {
            return SendCommandIntResponse("LIST " + intMessageNumber.ToString());
        }

        //add by playyuer $at$ Microshaoft.com 
        public delegate void DataEventHandler(int MessageID, int Data);

        public event DataEventHandler DataArrival;

        public void OnMessageReceiving(int MessageID, int Data)
        {
            if (DataArrival != null)
            {
                DataArrival(MessageID, Data);
            }
        }

        /// <summary> 
        /// read stream content 
        /// </summary> 
        /// <param name="intContentLength">length of content to read</param> 
        /// <returns>content</returns> 
        /// 
        private string ReceiveContent(int MessageID, int intContentLength)
        {
            string strResponse = null;
            StringBuilder builder = new StringBuilder();

            WaitForResponse(ref reader, WaitForResponseInterval);

            strResponse = reader.ReadLine();
            int intLines = 0;
            int intLen = 0;

            //add by playyuer $at$ microshaoft.com 
            OnMessageReceiving(MessageID, intLen);

            while (strResponse != ".") // || (intLen<intContentLength)) //(strResponse.IndexOf(".")==0 && intLen<intContentLength) 
            {
                builder.Append(strResponse + "\r\n");
                intLines += 1;
                intLen += strResponse.Length + "\r\n".Length;

                //add by playyuer $at$ microshaoft.com 
                OnMessageReceiving(MessageID, intLen);

                WaitForResponse(ref reader, 1);

                strResponse = reader.ReadLine();
                if ((intLines % _receiveContentSleepInterval) == 0) //make an interval pause to ensure response from server 
                    Thread.Sleep(1);
            }

            OnMessageReceiving(MessageID, -1); //接收一封邮件完毕 

            builder.Append(strResponse + "\r\n");

            return builder.ToString();

        }

        /// <summary> 
        /// get message info 
        /// </summary> 
        /// <param name="number">message number on server</param> 
        /// <returns>Message object</returns> 
        public MIMEParser.Message GetMessage(int intNumber, bool blnOnlyHeader)
        {
            OnMessageTransferBegan(EventArgs.Empty);

            //modify by playyuer $at$ Microshaoft.com 
            MIMEParser.Message msg = FetchMessage(intNumber, "RETR {0}", blnOnlyHeader);

            OnMessageTransferFinished(EventArgs.Empty);

            return msg;
        }

        /// <summary> 
        /// fetches a message or a message header 
        /// </summary> 
        /// <param name="strCommand">Command to send to Pop server</param> 
        /// <param name="blnOnlyHeader">Only return message header?</param> 
        /// <returns>Message object</returns> 
        public MIMEParser.Message FetchMessage(string strCommand, bool blnOnlyHeader)
        {
            _receiveFinish = false;
            if (!SendCommand(strCommand))
                return null;

            try
            {
                string receivedContent = ReceiveContent(0, -1);

                MIMEParser.Message msg = new MIMEParser.Message(ref _receiveFinish, _basePath, _autoDecodeMSTNEF, receivedContent, blnOnlyHeader);

                WaitForResponse(_receiveFinish, WaitForResponseInterval);

                return msg;
            }
            catch (Exception e)
            {
                Utility.LogError("FetchMessage():" + e.Message);
                return null;
            }
        }

        //overload by playyuer©㊣®microshaoft.com 
        public MIMEParser.Message FetchMessage(int MessageID, string strCommand, bool blnOnlyHeader)
        {
            _receiveFinish = false;

            string s = string.Format(strCommand, MessageID).ToUpper();
            if (!SendCommand(s))
                return null;

            try
            {
                string receivedContent = ReceiveContent(MessageID, -1);

                MIMEParser.Message msg = new MIMEParser.Message(ref _receiveFinish, _basePath, _autoDecodeMSTNEF, receivedContent, blnOnlyHeader);

                WaitForResponse(_receiveFinish, WaitForResponseInterval);

                return msg;
            }
            catch (Exception e)
            {
                Utility.LogError("FetchMessage():" + e.Message);
                return null;
            }
        }

    }

    /// <summary> 
    /// Utility functions 
    /// </summary> 
    public class Utility
    {
        /// <summary> 
        /// Weather auto loggin is on or off 
        /// </summary> 
        private static bool m_blnLog = false;

        /// <summary> 
        /// The file name in which the logging will be done 
        /// </summary> 
        private static string m_strLogFile = "OpenPOP.log";

        /// <summary> 
        /// Turns file logging on and off.<font color="red"><h1>Change Property Name</h1></font> 
        /// </summary> 
        /// <remarks>Comming soon.</remarks> 
        public static bool Log
        {
            get
            {
                return m_blnLog;
            }
            set
            {
                m_blnLog = value;
            }
        }

        /// <summary> 
        /// Log an error to the log file 
        /// </summary> 
        /// <param name="strText">The error text to log</param> 
        internal static void LogError(string strText)
        {
            //Log=true; 
            if (Log)
            {
                FileInfo file = null;
                FileStream fs = null;
                StreamWriter sw = null;
                try
                {
                    file = new FileInfo(m_strLogFile);
                    sw = file.AppendText();
                    //fs = new FileStream(m_strLogFile, FileMode.OpenOrCreate, FileAccess.Write); 
                    //sw = new StreamWriter(fs); 
                    sw.WriteLine(DateTime.Now);
                    sw.WriteLine(strText);
                    sw.WriteLine("\r\n");
                    sw.Flush();
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Close();
                        sw = null;
                    }
                    if (fs != null)
                    {
                        fs.Close();
                        fs = null;
                    }

                }
            }
        }

    }

}