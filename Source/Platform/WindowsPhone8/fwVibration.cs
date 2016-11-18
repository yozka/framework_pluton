﻿using System;
using Microsoft.Devices;
using Microsoft.Xna.Framework;



namespace Pluton.SystemProgram.Devices
{
    ///--------------------------------------------------------------------------------------







    ///=====================================================================================
    ///
    /// <summary>
    /// Система вибрации
    /// </summary>
    /// 
    ///--------------------------------------------------------------------------------------
    public class AVibrationDevice
    {
        ///--------------------------------------------------------------------------------------
        private readonly VibrateController mRig = null;


        private bool mEnabled = true;
        private int[] mCurrent = null; //текущий массив палитры вибраций
        private int mIndex = 0;    //текущий индекс прослушивания в палитре
        private TimeSpan mTimeNext = TimeSpan.Zero;
        ///--------------------------------------------------------------------------------------







        ///=====================================================================================
        ///
        /// <summary>
        /// Constructor
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public AVibrationDevice()
        {
            mRig = VibrateController.Default;
        }
        ///--------------------------------------------------------------------------------------






        ///=====================================================================================
        ///
        /// <summary>
        /// проверка, вибрация существует или нет
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool isVibration()
        {
            return true;
        }
        ///--------------------------------------------------------------------------------------







        ///=====================================================================================
        ///
        /// <summary>
        /// Включенна выключена вибрация
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool enabled
        {
            get
            {
                return mEnabled;
            }
            set
            {
                mEnabled = value;
                if (!mEnabled)
                {
                    mIndex = 0;
                    mTimeNext = TimeSpan.Zero;
                    mCurrent = null;
                }
            }
        }
        ///--------------------------------------------------------------------------------------









        ///=====================================================================================
        ///
        /// <summary>
        /// начало вибрации
        /// 
        /// описание партитуры вибрации в милесекундах
        /// четные числа - вбирация
        /// нечетные числа - ожидание
        /// private readonly int[] m_vibSelectMenu = new int[] { 50, 100, 100 };
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void playOne(int[] tone)
        {
            if (mEnabled)
            {
                mIndex = 0;
                mTimeNext = TimeSpan.Zero;
                mCurrent = tone;
            }
        }
        ///--------------------------------------------------------------------------------------







        ///=====================================================================================
        ///
        /// <summary>
        /// Вибрация, всякая
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void update(TimeSpan gameTime)
        {
            if (mEnabled && mCurrent != null)
            {
                mTimeNext -= gameTime;
                if (mTimeNext.TotalMilliseconds < 0)
                {
                    //следующий сонг
                    int iLength = mCurrent.Length;
                    if (mIndex < iLength)
                    {
                        //длительность звучания
                        int time = mCurrent[mIndex];
                        mRig.Start(TimeSpan.FromMilliseconds(time));

                        //длительность паузы после звучания
                        mIndex++;
                        if (mIndex < iLength)
                        {
                            time += mCurrent[mIndex];//пауза после звука
                            mIndex++;//следующий сонг
                        }
                        mTimeNext = TimeSpan.FromMilliseconds(time);
                    }

                    //првоерка, если сонг проигран, то обнулим все
                    if (mIndex >= iLength)
                    {
                        mIndex = 0;
                        mTimeNext = TimeSpan.Zero;
                        mCurrent = null;
                    }
                }
            }
        }
        ///--------------------------------------------------------------------------------------










        ///=====================================================================================
        ///
        /// <summary>
        /// Загрузка настроек
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void loadSettings(AStorage settings)
        {
            mEnabled = settings.readBoolean("vibration", mEnabled);
        }
        ///--------------------------------------------------------------------------------------







        ///=====================================================================================
        ///
        /// <summary>
        /// Сохранение настроек
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void saveSettings(AStorage settings)
        {
            settings.writeBoolean("vibration", mEnabled);
        }
        ///--------------------------------------------------------------------------------------














    }
}
