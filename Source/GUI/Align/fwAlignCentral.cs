﻿#region Using framework
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion




namespace Pluton.GUI
{
    ///------------------------------------------------------------------------------------------

    ///------------------------------------------------------------------------------------------






     ///=========================================================================================
    ///
    /// <summary>
    /// центральное позицирование элементов
    /// </summary>
    /// 
    ///------------------------------------------------------------------------------------------
    public class AAlignCentral
            :
                AAlignFrame
    {

        ///--------------------------------------------------------------------------------------





        ///=====================================================================================
        ///
        /// <summary>
        /// Конструктор 1
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public AAlignCentral(AFrame frame)
            :
                base(frame)
        {

        }
        ///--------------------------------------------------------------------------------------









        ///=====================================================================================
        ///
        /// <summary>
        /// пересборка содержимого фрейма
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        protected override void onResize()
        {
            //пробежимся по всем системным кнопкам, и выставим им позицию по умолчанию
            Rectangle rect = boundingRect();

           
            int iWidth = frame.contentWidth;
            int iHeight = frame.contentHeight;

            int x = (iWidth - rect.Width) / 2;
            int y = (iHeight - rect.Height) / 2;

            x = x - rect.Left;
            y = y - rect.Top;

            foreach (AWidget obj in frame.childs)
            {
                if (obj is ADockwidgetButton)
                {
                    continue;
                }

                obj.left += x;
                obj.top += y;
            }
        }
        ///--------------------------------------------------------------------------------------














    }
}
