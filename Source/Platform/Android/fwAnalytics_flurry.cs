﻿using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

using Android;
using Android.Content;
using Android.OS;
using Android.App;

namespace Pluton.SystemProgram.Devices
{
    ///--------------------------------------------------------------------------------------
    ///--------------------------------------------------------------------------------------






    ///=====================================================================================
    ///
    /// <summary>
    /// Система аналитики приложения
    /// </summary>
    /// 
    ///--------------------------------------------------------------------------------------
    public class AAnalytics_flurry
        :
            IAnalytics
    {
        ///--------------------------------------------------------------------------------------
        private readonly string mSessionID = null;
        ///--------------------------------------------------------------------------------------










         ///=====================================================================================
        ///
        /// <summary>
        /// Constructor
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public AAnalytics_flurry(string sessionID)
        {
            mSessionID = sessionID;
            Flurry.Analytics.FlurryAgent.Init(Application.Context, mSessionID);
        }
        ///--------------------------------------------------------------------------------------






         ///=====================================================================================
        ///
        /// <summary>
        /// Начало запуска программы
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void startSession()
        {
            Flurry.Analytics.FlurryAgent.OnStartSession(Application.Context);
        }
        ///--------------------------------------------------------------------------------------






         ///=====================================================================================
        ///
        /// <summary>
        /// выключение сесси
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void endSession()
        {
            Flurry.Analytics.FlurryAgent.OnEndSession(Application.Context);
        }
        ///--------------------------------------------------------------------------------------







        ///=====================================================================================
        ///
        /// <summary>
        /// событие c параметрами
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void trackEvent(string eventName, IDictionary<string, string> properties)
        {
            if (properties != null)
            {
                Flurry.Analytics.FlurryAgent.LogEvent(eventName, properties);
            }
            else
            {
                Flurry.Analytics.FlurryAgent.LogEvent(eventName);
            }
        }
        ///--------------------------------------------------------------------------------------





         ///=====================================================================================
        ///
        /// <summary>
        /// ошибка
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void trackException(Exception ex)
        {

        }
        ///--------------------------------------------------------------------------------------









    }
}
