using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mmr.share
{
    /// <summary>
    /// Общие параметры программы
    /// </summary>
    public enum ParameterShareEnum
    {
        #region СК - настройки подключения к серверу СК-2007
        /// <summary>Искользуется ли СК</summary>
        CK_Enabled,

        /// <summary>Название</summary>
        CK_Name,

        /// <summary>Имя SQL сервера 1</summary>
        CK_Server1_Name,

        /// <summary>Аутентификация SQL сервера 1</summary>
        CK_Server1_WinNT,

        /// <summary>Логин  SQL сервера 1</summary>
        CK_Server1_Login,

        /// <summary>Пароль  SQL сервера 1</summary>
        CK_Server1_Pas,

        /// <summary>Имя SQL сервера 2</summary>
        CK_Server2_Name,

        /// <summary>Аутентификация SQL сервера 2</summary>
        CK_Server2_WinNT,

        /// <summary>Логин  SQL сервера 2</summary>
        CK_Server2_Login,

        /// <summary>Пароль  SQL сервера 2</summary>
        CK_Server2_Pas,

        /// <summary>Имя SQL сервера 3</summary>
        CK_Server3_Name,

        /// <summary>Аутентификация SQL сервера 3</summary>
        CK_Server3_WinNT,

        /// <summary>Логин  SQL сервера 3</summary>
        CK_Server3_Login,

        /// <summary>Пароль  SQL сервера 3</summary>
        CK_Server3_Pas,

        #endregion

        #region ППБР - настройки макета
        /// <summary>кодировка макета ППБР</summary>
        PPBR_Encode,

        /// <summary>Шаблон имени файла макета ППБР</summary>
        PPBR_FileName_Template,

        /// <summary>заголовок макета ППБР</summary>
        PPBR_Header,

        /// <summary>строка с которой начинаются данные в макете</summary>
        PPBR_BeginData,

        /// <summary>разделитель макета ППБР</summary>
        PPBR_Separator,

        /// <summary>десятичная точка макета ППБР</summary>
        PPBR_DecimalPoint,

        #endregion

        #region ППБР - настройки почты
        /// <summary>SMTP сервер макета ППБР</summary>
        PPBR_SMTP_Server,

        /// <summary>SMTP порт макета ППБР</summary>
        PPBR_SMTP_Port,

        /// <summary>POP3 сервер макета ППБР</summary>
        PPBR_POP3_Server,

        /// <summary>POP3 порт макета ППБР</summary>
        PPBR_POP3_Port,

        /// <summary>Email макета ППБР</summary>
        PPBR_Email,

        /// <summary>Пароль на почту макета ППБР</summary>
        PPBR_Email_Password,

        /// <summary>Время ожидания события на сервере при приеме макетов ППБР</summary>
        PPBR_Email_Timeout,

        /// <summary>Тема письма в макете ППБР</summary>
        PPBR_Email_Subject,

        /// <summary>Пересылка макета ППБР на адресаты</summary>
        PPBR_Email_Replay,

        /// <summary>От чьего имени пересылаем макет ППБР</summary>
        PPBR_Email_Sender,

        /// <summary>Удалять письмо после загрузки макета</summary>
        PPBR_Email_DeletedAfterLoad,

        #endregion

        #region ППБР - настройки макета для СК-2003
        /// <summary>кодировка макета для СК-2003</summary>
        PPBR_CK_Encode,

        /// <summary>заголовок макета для СК-2003</summary>               
        PPBR_CK_Header,

        /// <summary>строка которой заканчиваются данные в макете для СК-2003</summary>
        PPBR_CK_EndData,

        /// <summary>разделитель макета СК-2003</summary>
        PPBR_CK_Separator,

        /// <summary>Шаблон имени файла макета СК-2003</summary>
        PPBR_CK_FileName_Template,

        /// <summary>Директория для выгрузки макета СК-2003</summary>
        PPBR_CK_DirectoryOutput,

        /// <summary>десятичная точка в макете CK-2003</summary>
        PPBR_CK_DecimalPoint,
        #endregion

        #region PlanPotr - Планирование потребления
        /// <summary>ТИ фактического потребления</summary>
        PlanPotr_FactPotr_TI,

        /// <summary>Категория фактического потребления</summary>
        PlanPotr_FactPotr_Cat,

        /// <summary>ТИ фактической температуры</summary>
        PlanPotr_FactTemperatura_TI,

        /// <summary>Категория фактической температуры</summary>
        PlanPotr_FactTemperatura_Cat,

        /// <summary>Код КПО макета прогноза потребления</summary>
        PlanPotr_LocationKPO
        #endregion
    }

}
