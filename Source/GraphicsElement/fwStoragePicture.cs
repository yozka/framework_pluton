﻿#region Using framework
using System.IO;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion





namespace Pluton.GraphicsElement
{
    ///--------------------------------------------------------------------------------------
    using Pluton.SystemProgram;
    using Pluton.Helper;
    ///--------------------------------------------------------------------------------------





     ///=====================================================================================
    ///
    /// <summary>
    /// Загруженная картинка из сервера
    /// </summary>
    /// 
    ///--------------------------------------------------------------------------------------
    public class AStoragePicture
         
             
    {
        ///--------------------------------------------------------------------------------------




        ///--------------------------------------------------------------------------------------
        private string      mFileName   = null;
        private Texture2D   mTexture    = null;
        private int         mGDHandle   = 0;
        ///--------------------------------------------------------------------------------------








         ///=====================================================================================
        ///
        /// <summary>
        /// constructor
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public AStoragePicture()
        {
    
        }
        ///--------------------------------------------------------------------------------------






         ///=====================================================================================
        ///
        /// <summary>
        /// возвратить  текущее значение загруженной текстуры
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public static implicit operator Texture2D(AStoragePicture p)
        {
            return p != null ? p.mTexture : null;
        }
        ///--------------------------------------------------------------------------------------






         ///=====================================================================================
        ///
        /// <summary>
        /// возвратим имя файла
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public string fileName
        {
            get
            {
                return mFileName == null ? string.Empty : mFileName;
            }

            set
            {
                mFileName = value;
            }
        }
        ///--------------------------------------------------------------------------------------







        ///=====================================================================================
        ///
        /// <summary>
        /// проверяет статус, команда запущенна илил нет
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------

        /*
        public bool isLoading()
        {
            return mStatus == EStatus.idle || mStatus == EStatus.loading;
        }*/
        ///--------------------------------------------------------------------------------------






        ///=====================================================================================
        ///
        /// <summary>
        /// проверяет статус, команда выполнела свой запрос или нет
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        /*
        public bool isCompleted()
        {
            return mStatus == EStatus.loadingCompleted;
        }*/
        ///--------------------------------------------------------------------------------------

















        ///=====================================================================================
        ///
        /// <summary>
        /// команда выполнилась
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        /*
        public void loadingCompleted(GraphicsDevice graphicsDevice, Stream stream)
        {
            mStatus = EStatus.loadingCompleted;
            loadStream(graphicsDevice, stream);
            mCountSend = 0;
            mCache = false;
            if (signal_completed != null)
            {
                signal_completed(this);
            }
        }*/
        ///--------------------------------------------------------------------------------------








         ///=====================================================================================
        ///
        /// <summary>
        /// загрузка картинки
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool loadStream(GraphicsDevice graphicsDevice, Stream stream)
        {
      
            try
            {
                stream.Position = 0;                    
                mTexture = Texture2D.FromStream(graphicsDevice, stream);
                mGDHandle = ASpriteBatch.GDHandle();

            }
            catch (Exception e)
            {
                mTexture = null;
                return false;
            }
            return true;
        }
        ///--------------------------------------------------------------------------------------




         ///=====================================================================================
        ///
        /// <summary>
        /// проверка, чвляется ли текстура номрально загруженная илил нет
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool isReboot()
        {
            if (mTexture == null)
            {
                return false;
            }
            return ASpriteBatch.isHandle(mGDHandle) ? false : true;
        }
        ///--------------------------------------------------------------------------------------




        ///=====================================================================================
        ///
        /// <summary>
        /// проверка, был резет графического девайса или нет
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        /*
        protected internal override void GraphicsDeviceResetting()
        {
            mResetting = true;

        }*/



    }
}