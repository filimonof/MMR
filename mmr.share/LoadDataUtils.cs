using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace mmr.share
{
    /// <summary>
    /// Утилита загрузки данных
    /// </summary>
    public static class LoadDataUtils
    {
        /// <summary>загрузка файла из интернета</summary>
        /// <param name="url">URL фала в сети</param>
        /// <param name="localname">путь и имя при сохранении локально</param>
        public static void DownloadFile(string url, string localname)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Proxy = LoadDataUtils.GetProxy();
                webClient.DownloadFile(url, localname);
            }
            catch (Exception e)
            {
                throw new Exception("LoadDataUtils.DownloadFile -> Ошибка при загрузке файла \n " + e.Message);
            }
        }

        /// <summary>загрузка страницы из интернеты</summary>
        /// <param name="url">адрес страницы</param>
        /// <returns>строка со страницей</returns>
        public static string DownloadPage(string url, Encoding encoding)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Proxy = LoadDataUtils.GetProxy();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("LoadDataUtils.DownloadPage -> Ошибка при загрузке страницы \n " + e.Message);
            }
        }

        /// <summary>получить прокси для выхода в интернет</summary>
        /// <returns>объект WebProxy</returns>
        private static WebProxy GetProxy()
        {
            //string proxy_ip = WebConfigurationManager.AppSettings["Proxy_IP"];
            //string port = WebConfigurationManager.AppSettings["Proxy_Port"];
            //string isAuth = WebConfigurationManager.AppSettings["Proxy_Auth"];
            //string isDefault = WebConfigurationManager.AppSettings["Proxy_IsDefaultUser"];
            //string login = WebConfigurationManager.AppSettings["Proxy_Login"];
            //string password = WebConfigurationManager.AppSettings["Proxy_Password"];
            //string domain = WebConfigurationManager.AppSettings["Proxy_Domain"];

            //todo: перенести в настройки
            string proxy_ip = "172.23.84.73";
            string port = "3128";
            string isAuth = "true";
            string isDefault = "true";
            string login = "HlopunovSA";
            string password = "hsa943";
            string domain = "ODUSV";

            //&& Uri.CheckSchemeName(proxy_ip)
            if (!string.IsNullOrEmpty(proxy_ip))
            {
                if (!string.IsNullOrEmpty(port))
                    proxy_ip += ":" + port;

                WebProxy proxy = new WebProxy(proxy_ip);

                bool byPass;
                if (!string.IsNullOrEmpty(isAuth) && bool.TryParse(isAuth, out byPass))
                    proxy.BypassProxyOnLocal = byPass;
                else
                    proxy.BypassProxyOnLocal = false;

                bool isDef;
                if (string.IsNullOrEmpty(isDefault) || !bool.TryParse(isDefault, out isDef))
                    isDef = true;
                if (isDef)
                    proxy.Credentials = CredentialCache.DefaultCredentials;
                else
                    proxy.Credentials = new NetworkCredential(login, password, domain);

                return proxy;
            }
            else return null;
        }
    }
}