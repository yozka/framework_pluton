﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


using Pluton.SystemProgram;


namespace Pluton.GraphicsElement
{
    ///--------------------------------------------------------------------------------------







     ///=====================================================================================
    ///
    /// <summary>
    /// Энимационный элемент
    /// проигрывает анимацию от 0 до 1 и 1 до 0 ввиде часть синусоиды
    /// 
    /// </summary>
    /// 
    ///--------------------------------------------------------------------------------------
    public class AAnimationLoopSin
    {
        ///--------------------------------------------------------------------------------------
        private double          mSpeed = 0;          //скорость перемещения
        private double          mDiff = 0;           //текущее изменение от 0 до 1;
        private bool            mActive = false;     //активность анимации
        private bool            mStoping = false;    //признак остоновки
        ///--------------------------------------------------------------------------------------








         ///=====================================================================================
        ///
        /// <summary>
        /// Constructor
        /// speed - скорость продолжительности анимации в милисикундах
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public AAnimationLoopSin(double speed)
        {
            mSpeed = Math.PI / speed;
        }
        ///--------------------------------------------------------------------------------------







         ///=====================================================================================
        ///
        /// <summary>
        /// возвратить  текущее значение анимации от 0..1
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public static implicit operator float(AAnimationLoopSin p)
        {
            return (float)Math.Sin(p.mDiff);
        }
        ///--------------------------------------------------------------------------------------






        



         ///=====================================================================================
        ///
        /// <summary>
        /// обработка анимации
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void update(TimeSpan gameTime)
        {
            if (mActive)
            {
                mDiff += mSpeed * gameTime.TotalMilliseconds;
                if (mDiff > Math.PI)
                {
                    mDiff = 0.0f;
                    mActive = !mStoping;
                }
            }
        }
        ///--------------------------------------------------------------------------------------






         ///=====================================================================================
        ///
        /// <summary>
        /// начало старт анимации
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void startOnce()
        {
            mActive = true;
            mStoping = true;
        }
        ///--------------------------------------------------------------------------------------





         ///=====================================================================================
        ///
        /// <summary>
        /// начало старт анимации
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void start()
        {
            mActive = true;
            mStoping = false;
        }
        ///--------------------------------------------------------------------------------------





         ///=====================================================================================
        ///
        /// <summary>
        /// начало старт анимации
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void start(double speed)
        {
            mSpeed = Math.PI / speed;
            start();
        }
        ///--------------------------------------------------------------------------------------





         ///=====================================================================================
        ///
        /// <summary>
        /// проверка, анимация остановилась илил нет
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool isStop()
        {
            return !mActive;
        }
        ///--------------------------------------------------------------------------------------






         ///=====================================================================================
        ///
        /// <summary>
        /// остановка анимации
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void stop()
        {
            mStoping = true;
        }


    }//AAnimationLoopSin
}