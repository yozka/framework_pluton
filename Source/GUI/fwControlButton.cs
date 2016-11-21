﻿#region Using framework
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion


using Pluton;
using Pluton.SystemProgram;
using Pluton.SystemProgram.Devices;

namespace Pluton.GUI
{







    ///=========================================================================================
    ///
    /// <summary>
    /// Базовый контрол для GUI
    /// кнопка обрабатывающая нажатия
    /// </summary>
    /// 
    ///------------------------------------------------------------------------------------------
    public class AControlButton
                :
                    AWidget
    {

        ///--------------------------------------------------------------------------------------
        protected bool m_enabled = true;     // включена выключена кнопка
        protected bool m_pushDown = false;    // нажата или не нажата
        protected bool m_checkbox = false;    // переключена или нет


        protected string mSoundClick = string.Empty; //звук по умолчанию
        protected int[] mVibroTone = new int[] { 30 };
        //тригер срабатывания, кнопка начент действовать после 10n показов
        //
        //protected bool m_triger      = false;
        //protected int  m_autoTriger  = 0;


        private object mUserData = null; //пользовательские данные
        ///--------------------------------------------------------------------------------------





        ///=====================================================================================
        ///
        /// <summary>
        /// Конструктор 1
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public AControlButton()
        {
        }
        ///--------------------------------------------------------------------------------------





        ///=====================================================================================
        ///
        /// <summary>
        /// Конструктор 1
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public AControlButton(AWidget parent)
            :
                base(parent)
        {
        }
        ///--------------------------------------------------------------------------------------






        ///=====================================================================================
        ///
        /// <summary>
        /// Конструктор 2
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public AControlButton(AWidget parent, int left, int top, int width, int height)
            :
                base(parent, left, top, width, height)
        {
        }
        ///--------------------------------------------------------------------------------------







         ///=====================================================================================
        ///
        /// <summary>
        /// пользовательские данные
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public object userData
        {
            set
            {
                mUserData = value;
            }
            get
            {
                return mUserData;
            }
        }
        ///--------------------------------------------------------------------------------------




         ///=====================================================================================
        ///
        /// <summary>
        /// Отрисовка контрола
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool checkbox
        {
            set { m_checkbox = value; }
            get { return m_checkbox; }
        }
        ///--------------------------------------------------------------------------------------







        ///=====================================================================================
        ///
        /// <summary>
        /// активный не активная кнопка
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool enabled
        {
            set { m_enabled = value; }
            get { return m_enabled; }
        }
        ///--------------------------------------------------------------------------------------










        ///=====================================================================================
        ///
        /// <summary>
        /// Отрисовка контрола с учетом располжения родителя
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        protected override void onRender(ASpriteBatch spriteBatch, Rectangle rect)
        {
            if (!m_visible)
            {
                return;
            }
            float alpha = 0.7f;

            if (m_checkbox)
            {
                alpha = 0.9f;

            }


            if (m_pushDown)
            {
                alpha = 1.0f;

            }


            //spriteBatch.Draw(ASpritePrimitives.textureWhite, rect, Color.WhiteSmoke * alpha);
        }
        ///--------------------------------------------------------------------------------------








        ///=====================================================================================
        ///
        /// <summary>
        /// обработка нажатий, если обработка удачная то возвращаем true
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public override bool onHandleInput(AInputDevice input)
        {
            if (!m_enabled)
            {
                return false;
            }

            //1 обработка нажатие pushdown
            int index = input.containsRectangle(screenRect);
            if (index >= 0)
            {
                if (!m_pushDown)
                {
                    m_pushDown = true;
                    pushDown();
                    return true;
                }
                //уничтожим обработанный индекс
                input.release(index);
            }
            else
            {
                if (m_pushDown)
                {
                    m_pushDown = false;
                    
                    //проверка действительно ли нажали на кнопку в пределах указанной позиции
                    if (screenRect.Contains((int)input.lastTouch.X, (int)input.lastTouch.Y))
                    {
                        click();
                    }
                    return true;
                }
            }

            return false;
        }
        ///--------------------------------------------------------------------------------------











        ///=====================================================================================
        ///
        /// <summary>
        /// Обновление логики у контрола
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public override void onUpdate(TimeSpan gameTime)
        {
        }
        ///--------------------------------------------------------------------------------------






         ///=====================================================================================
        ///
        /// <summary>
        /// Обработка нажатия на кнопку
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public delegate void eventClick();
        public event eventClick signal_click;
        ///--------------------------------------------------------------------------------------





         ///=====================================================================================
        ///
        /// <summary>
        /// Обработка нажатия на кнопку
        /// с передачей с самой себя в качестве параметров
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public delegate void eventClickButton(AControlButton button);
        public event eventClickButton signal_click_button;
        ///--------------------------------------------------------------------------------------






        ///=====================================================================================
        ///
        /// <summary>
        /// кнопка отпустилась
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public delegate void eventPushDown();
        public event eventPushDown signal_pushDown;
        ///--------------------------------------------------------------------------------------




         ///=====================================================================================
        ///
        /// <summary>
        /// первый раз отжали на кнопку
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        private void pushDown()
        {
            if (mVibroTone != null)
            {
                gVibration.playOne(mVibroTone);
            }
            
            if (signal_pushDown != null)
            {
                signal_pushDown();
            }
        }
        ///--------------------------------------------------------------------------------------








        ///=====================================================================================
        ///
        /// <summary>
        /// нажатие на кнопку
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool click()
        {
            if (mSoundClick != string.Empty)
            {
                gSound.play(mSoundClick);
            }



            if (signal_click != null)
            {
                signal_click();
                onClick();
                return true;
            }


            if (signal_click_button != null)
            {
                signal_click_button(this);
                onClick();
                return true;
            }

            onClick();
            return false;
        }
        ///--------------------------------------------------------------------------------------









        ///=====================================================================================
        ///
        /// <summary>
        /// нажатие на кнопку
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        protected virtual void onClick()
        {
        }
        ///--------------------------------------------------------------------------------------












    }
}
