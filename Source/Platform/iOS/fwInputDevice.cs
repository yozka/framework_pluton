﻿#region Using framework
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Devices.Sensors;
//using Microsoft.Xna.Framework.GamerServices;

using System.Threading.Tasks;

#endregion


namespace Pluton.SystemProgram.Devices
{



    ///=====================================================================================
    ///
    /// <summary>
    /// Контроллер ввода пользовательской информации.
    /// Опрос тачпанелей и кнопок телефона
    /// </summary>
    /// 
    ///--------------------------------------------------------------------------------------
    public class AInputDevice
    {

        //нажатие на тачпанель
        private const int c_touchMaxCount = 3;    //количесвто одновременных нажатий на экран
        private Vector2[] m_touch = null;         //координаты нажатых прикосновений к экрану
        private int m_touchCount = 0;       //Количесвто одновременных нажатий на экран
        private Vector2 mLastTouch = Vector2.Zero;

        //хардварные кнопки
        private GamePadState m_buttons;              //текущие нажатые кнопки
        private GamePadState m_buttonsLast;          //предыдущие нажатые кнопки


        //жесты
        /*
        private Vector2         m_gesturesTap = Vector2.Zero;       //одиночное нажатие
        private Vector2         m_gesturesTapLast = Vector2.Zero;   //последняя команда одиночного нажания
        */


        /*
        //акселерометр
        private Vector2         m_accel = Vector2.Zero;//подсчитанные данные
        private Vector2         m_zeroZone = Vector2.Zero;//текущая нулевая зона для калибровки
        private Vector3         m_avector = Vector3.Zero;//считанные данные с акселерометра
        private Accelerometer   m_accelerometer = null;
        private Vector2         m_angle = new Vector2(1, 1);//корректировка значения для расположение девайса

        //фильтр средних значений на акселерометр
        private const int       c_filterCount = 6;//количество данных в фильтре
        private Vector2[]       m_filter = null;//новые подсчитанные данные
        private int             m_filterID = 0;
        */
        ///--------------------------------------------------------------------------------------






        ///=====================================================================================
        ///
        /// <summary>
        /// Constructor
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public AInputDevice(Game deviceGame)
        {
            /* нажатие на кнопки
             */
            m_touch = new Vector2[c_touchMaxCount];
            m_touchCount = 0;

            /* хардварные кнопки
             */
            m_buttons = new GamePadState();
            m_buttonsLast = new GamePadState();


            /* акселерометр
             */
            /*
            m_filter = new Vector2[c_filterCount];
            m_accelerometer = new Accelerometer();
            m_accelerometer.CurrentValueChanged += accelerometerChanged;
            m_accelerometer.Start();

            // подцепим обработчк слежения за расположением экрана
            deviceGame.Window.OrientationChanged += new EventHandler<EventArgs>(window_OrientationChanged);
            orientationChanged(deviceGame.Window.CurrentOrientation);
            */


            /* систкма жестов
             */
            //TouchPanel.EnabledGestures = GestureType.Tap;
        }
        ///--------------------------------------------------------------------------------------






        ///=====================================================================================
        ///
        /// <summary>
        /// Чтение последнего состояния клавиатуры и геймпада.
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void update()
        {
            //кнопки
            m_buttonsLast = m_buttons;
            m_buttons = GamePad.GetState(PlayerIndex.One);



            //жесты
            /*
            m_gesturesTapLast = m_gesturesTap;
            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gs = TouchPanel.ReadGesture();
                switch (gs.GestureType)
                {
                    case GestureType.Tap: m_gesturesTap = gs.Position; break;
                }
            }
            else
            {
                m_gesturesTap = Vector2.Zero;
            }
            */


            //коррдинаты прикосновений
            //для телефона
            m_touchCount = 0;
            foreach (var item in TouchPanel.GetState())
            {
                if (item.State == TouchLocationState.Pressed
                    || item.State == TouchLocationState.Moved)
                {
                    // Get item.Position
                    mLastTouch = ASpriteBatch.fromViewPort(item.Position);
                    m_touch[m_touchCount] = mLastTouch;
                    m_touchCount++;
                    if (m_touchCount >= c_touchMaxCount)
                    {
                        //вышли за пределы количество нажатий
                        break;
                    }
                }
            }




        }
        ///--------------------------------------------------------------------------------------





        
         ///=====================================================================================
        ///
        /// <summary>
        /// последняя нажатая координата
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public Vector2 lastTouch
        {
            get
            {
                return mLastTouch;
            }
        }
        ///--------------------------------------------------------------------------------------






         ///=====================================================================================
        ///
        /// <summary>
        /// конечная процедура ввода текста
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public delegate void eventInputBox(string value);
        ///--------------------------------------------------------------------------------------




         ///=====================================================================================
        ///
        /// <summary>
        /// ввод текста
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void showInputBox(string title, string description, string value, eventInputBox signal)
        {
            /*
            IAsyncResult kbResult = Guide.BeginShowKeyboardInput(PlayerIndex.One, title, description, value, null, null);
            if (kbResult != null)
            {
                string text = Guide.EndShowKeyboardInput(kbResult);
                if (text != null)
                {
                    value = text;
                    return true;
                }
            }
            return false;
             * */

            onShowInputBox(title, description, value, signal);
        }
        ///--------------------------------------------------------------------------------------





         ///=====================================================================================
        ///
        /// <summary>
        /// Асинхронная версия ввода имени
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        private async void onShowInputBox(string title, string description, string value, eventInputBox signal)
        {
            string name = await KeyboardInput.Show(title, description, value);

            if (name != null && value != name)
            {
                if (signal != null)
                {
                    signal(name);
                }
            }
        }
        ///--------------------------------------------------------------------------------------







         ///=====================================================================================
        ///
        /// <summary>
        /// Отслеживание нажатие новой кнопки.
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool isNewButtonPress(Buttons button)
        {
            return (m_buttons.IsButtonDown(button) &&
                        m_buttonsLast.IsButtonUp(button));
        }
        ///--------------------------------------------------------------------------------------






         ///=====================================================================================
        ///
        /// <summary>
        /// Проверка, польователь нажал хардвардную кнопку выхода из меню или нет.
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool isMenuCancel()
        {
            return isNewButtonPress(Buttons.B) ||
                   isNewButtonPress(Buttons.Back);
        }
        ///--------------------------------------------------------------------------------------





        ///=====================================================================================
        ///
        /// <summary>
        /// Проверка, польователь нажал хардвардную кнопку нажатие назад или нет.
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool isCancel()
        {
            return isNewButtonPress(Buttons.B) ||
                   isNewButtonPress(Buttons.Back);
        }
        ///--------------------------------------------------------------------------------------








        ///=====================================================================================
        ///
        /// <summary>
        /// Проверка, пользователь нажал паузу или нет во время игры.
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool isPauseGame()
        {
            return isNewButtonPress(Buttons.Back) ||
                   isNewButtonPress(Buttons.Start);
        }
        ///--------------------------------------------------------------------------------------








        ///=====================================================================================
        ///
        /// <summary>
        /// Проверяем, находится ли нажатые точки в прямоугольнике, возвращаем первую попавшуюся 
        /// точку
        /// вслучаии неудачи возвращаем -1, иначе индекс найденной точки координат
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public int containsRectangle(int x, int y, int width, int height)
        {
            int xw = x + width;
            int yh = y + height;
            for (int i = 0; i < m_touchCount; i++)
            {
                Vector2 pos = m_touch[i];
                int pointX = (int)pos.X;
                int pointY = (int)pos.Y;
                if (pointX >= x && pointX < xw &&
                    pointY >= y && pointY < yh)
                {
                    return i;
                }
            }
            return -1;
        }
        ///--------------------------------------------------------------------------------------






        ///=====================================================================================
        ///
        /// <summary>
        /// Проверяем, находится ли нажатые точки в прямоугольнике, возвращаем первую попавшуюся 
        /// точку
        /// вслучаии неудачи возвращаем -1, иначе индекс найденной точки координат
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public int containsRectangle(Rectangle rect)
        {
            return containsRectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }
        ///--------------------------------------------------------------------------------------








        ///=====================================================================================
        ///
        /// <summary>
        /// Уничтожить указанный индекс
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void release(int index)
        {
            if (index >= 0 && index < m_touchCount)
            {
                m_touch[index].X = -1;
                m_touch[index].Y = -1;
            }
        }
        ///--------------------------------------------------------------------------------------





        ///=====================================================================================
        ///
        /// <summary>
        /// Уничтожить все индексы
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public void release()
        {
            for (int i = 0; i < m_touchCount; i++)
            {
                m_touch[i].X = -1;
                m_touch[i].Y = -1;
            }
            m_touchCount = 0;
        }
        ///--------------------------------------------------------------------------------------







        ///=====================================================================================
        ///
        /// <summary>
        /// возвратим координаты тачпада
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public Point touch(int index)
        {
            if (index >= 0 && index < m_touchCount)
            {
                return m_touch[index].toPoint();
            }
            return Point.Zero;
        }
        ///--------------------------------------------------------------------------------------







        ///=====================================================================================
        ///
        /// <summary>
        /// возвратим индекс первых нажатых данных для тчпада
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public int touchIndex()
        {
            if (m_touchCount >= 0)
            {
                for (int i = 0; i < m_touchCount; i++)
                {
                    if (m_touch[i].X >= 0 &&
                        m_touch[i].Y >= 0)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        ///--------------------------------------------------------------------------------------











        ///=====================================================================================
        ///
        /// <summary>
        /// обработка кнопок клавиатуры ход слева
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool isKeyLeft()
        {
            return false;
        }
        ///--------------------------------------------------------------------------------------





        ///=====================================================================================
        ///
        /// <summary>
        /// обработка кнопок клавиатуры ход справа
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool isKeyRight()
        {
            return false;
        }
        ///--------------------------------------------------------------------------------------







        ///=====================================================================================
        ///
        /// <summary>
        /// обработка кнопок клавиатуры ход вверх
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool isKeyUp()
        {
            return false;
        }
        ///--------------------------------------------------------------------------------------







        ///=====================================================================================
        ///
        /// <summary>
        /// обработка кнопок клавиатуры ход вниз
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool isKeyDown()
        {
            return false;
        }
        ///--------------------------------------------------------------------------------------








        ///=====================================================================================
        ///
        /// <summary>
        /// обработка акселерометра
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        /*
        private void accelerometerChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            AccelerometerReading data = e.SensorReading;
            m_avector = new Vector3((float)data.Acceleration.X, (float)data.Acceleration.Y, (float)data.Acceleration.Z);

            // высчитывание новой позиции
            m_filter[m_filterID] = new Vector2(m_avector.Y, m_avector.X) * m_angle - m_zeroZone;
            m_filterID++;
            if (m_filterID >= c_filterCount)
            {
                m_filterID = 0;
            }

            calcFilter();
        }*/
        ///--------------------------------------------------------------------------------------






        ///=====================================================================================
        ///
        /// <summary>
        /// подсчет фильтра
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        /*
        public void calcFilter()
        {
            m_accel = m_filter[0];
            for (int i = 1; i < c_filterCount; i++)
            {
                m_accel += m_filter[i];
            }
            m_accel = m_accel / c_filterCount;

        }*/
        ///--------------------------------------------------------------------------------------






        ///=====================================================================================
        ///
        /// <summary>
        /// Позиция акселерометра
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        /*
        public Vector2 acceleration
        {
            get
            {
                return m_accel;
            }
        }*/
        ///--------------------------------------------------------------------------------------






        ///=====================================================================================
        ///
        /// <summary>
        /// начальная точка позицирования
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        /*
        public Vector2 zeroZone
        {
            get
            {
                return m_zeroZone;
            }
            set
            {
                m_zeroZone = value;
            }
        }*/
        ///--------------------------------------------------------------------------------------






        ///=====================================================================================
        ///
        /// <summary>
        /// выдача текущей калибровки датчика
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        /*
        public Vector2 calibrationZeroZone()
        {
            return new Vector2(m_avector.Y, m_avector.X) * m_angle;
        }*/
        ///--------------------------------------------------------------------------------------







        ///=====================================================================================
        ///
        /// <summary>
        /// Обработка события на ориентации экрана
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        /*
        public void window_OrientationChanged(object sender, EventArgs e)
        {
            GameWindow window = sender as GameWindow;
            if (window != null)
            {
                orientationChanged(window.CurrentOrientation);
            }
        }*/
        ///--------------------------------------------------------------------------------------







        ///=====================================================================================
        ///
        /// <summary>
        /// устанавливаем корректирующие данные с учетом ориентации экрана
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        /*
        private void orientationChanged(DisplayOrientation orintat)
        {
            m_angle = new Vector2(-1, -1);
            if (orintat == DisplayOrientation.LandscapeRight)
            {
                m_angle = new Vector2(1, 1);
            }
        }
         * */



         ///=====================================================================================
        ///
        /// <summary>
        /// запуск браузера
        /// </summary>
        /// 
        ///--------------------------------------------------------------------------------------
        public bool runBrowser(string url)
        {

            return false;
        }
        ///--------------------------------------------------------------------------------------



    }//AInputDevice
}