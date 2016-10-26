﻿#region Using framework
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion




#if RENDER_DEBUG
using System.Diagnostics;
#endif


namespace Pluton.GUI
{
    ///------------------------------------------------------------------------------------------
    using Pluton;
    using Pluton.SystemProgram;
    using Pluton.SystemProgram.Devices;
    using Pluton.GraphicsElement;
    ///------------------------------------------------------------------------------------------








     ///=========================================================================================
    ///
    /// <summary>
    /// полоска прокрутки
    /// 
    /// </summary>
    /// 
    ///------------------------------------------------------------------------------------------
    public class AScrollBar
    {

        ///--------------------------------------------------------------------------------------
        ///--------------------------------------------------------------------------------------






         ///=====================================================================================
        ///
        /// <summary>
        /// Конструктор
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public AScrollBar()
        {

        }
        ///--------------------------------------------------------------------------------------




        


         ///=====================================================================================
        ///
        /// <summary>
        /// установим отступ и главного виджета
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public virtual void onMargin(AScrollArea area)
        {

        }
        ///--------------------------------------------------------------------------------------





         ///=====================================================================================
        ///
        /// <summary>
        /// изменение позиции виджета скроллинга
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public virtual void onPositionWidget(AScrollArea area)
        {

        }
        ///--------------------------------------------------------------------------------------







        ///=====================================================================================
        ///
        /// <summary>
        /// Отрисовка контрола с учетом располжения родителя
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public virtual void onRender(AScrollArea area, ASpriteBatch spriteBatch)
        {


            //spriteBatch.Draw(spriteBatch.getSprite(sprite.gui_scroll_horizontal_margin), new Rectangle(iRightIcon, rect.Top, 16, rect.Height), Color.White);
            //


            //spriteBatch.primitives.drawBorder(rect, 2, Color.Red);

        }
        ///--------------------------------------------------------------------------------------






        

         ///=====================================================================================
        ///
        /// <summary>
        /// вохвратим координаты для скроллинга
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public virtual Vector2 onScrollTouch(Vector2 ptWidget, Vector2 ptTouch)
        {
            return ptWidget;   
        }
        ///--------------------------------------------------------------------------------------


        


   

         ///=====================================================================================
        ///
        /// <summary>
        /// вохвратим координаты для скроллинга
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public virtual Vector2 onCorrectPosition(AScrollArea area)
        {
            return area.contentWidget.leftTop.toVector2();   
        }
        ///--------------------------------------------------------------------------------------




         ///=====================================================================================
        ///
        /// <summary>
        /// обработка нажатий, если обработка удачная то возвращаем true
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public virtual bool onHandleInput(AScrollArea area, AInputDevice input)
        {
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
        public virtual void onUpdate(AScrollArea area, TimeSpan gameTime)
        {
            
        }
        ///--------------------------------------------------------------------------------------






    }
}