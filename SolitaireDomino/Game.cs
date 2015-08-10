using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolitaireDomino
{
    public class Game
    {
        PlatesSprites sprites;   // Инкапсулируем спрайты и правила игры
        Solitaire solitaire;

        bool flagRoll;  // Для контроля, чтобы плашки открылись только после того как появились на поле
        
        public Game(Size size)
        {
            this.sprites = new PlatesSprites(size);
            this.solitaire = new Solitaire();
            this.flagRoll = true;

            this.solitaire.plate_СhangeState += PlateRefresh;  // Подписываем метод обработки события смены статуса плашки
        }

        /// <summary>
        /// Проверяет статус игры: 1 выигрышь, -1 проигрышь, 0 игра
        /// </summary>
        public int GetStatusGame { get { return solitaire.GetStatusGame; } }

        public void Start()
        {
            solitaire.HidePlate();       // Закрываем все плашки
            solitaire.ChangRandPlates(); // Перемешиваем плашки
            StartDominoShow();           // Инициализируем плашкам координаты на поле и начальный метод анимации
            flagRoll = true;             // Устанавливаем, что плашки еще не появились на поле
        }

        private void StartDominoShow()
        {
            for (int row = 0; row <= Setting.MaxPoint; row++) // Перебираем плашки по двум координатам
            {
                for (int column = 0; column <= row; column++)
                {
                    int shift = (Setting.MaxPoint - row) * Setting.PlateDistanceX / 2;     // Вычисляем смещение по X
                    int tempX = column * Setting.PlateDistanceX + shift + Setting.ShiftX;  // Вычисляем координаты на картинке
                    int tempY = row * Setting.PlateDistanceY + Setting.ShiftY;

                    float dZoom = (float)Solitaire.rnd.Next(2,7)/1000;

                    solitaire.plates[row, column].point = new Point(tempX, tempY); // Привязываем эти координаты к плашке
                    solitaire.plates[row, column].args = new Object[] { 0f, dZoom, true };  // Параметры для анимации: [0] - ZoomStart, [1] - flag
                    solitaire.plates[row, column].showPlate = sprites.RollAnime;   // Подписываем плашку на метод начальной анимации
                }
            }
        }

        /// <summary>
        /// Обработка клика по полю изображения
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public void Click(int x, int y)
        {
            Plate plate = solitaire.GetPlateClick(x, y);   // Определяем какая плашка на месте клика         
            if (plate == null) return;

            solitaire.MarkPlate(plate);  // Логика маркировки плашки
            solitaire.DropMarked();      // Логика удаления плашки
        }

        /// <summary>
        /// Отвечает за подписку методов анимации на плашку в зависимости от статуса плашки
        /// </summary>
        /// <param name="plate">Плашка</param>
        public void PlateRefresh(Plate plate)
        {
            switch(plate.state)  // В зависимости от статуса привязываем к плашке разные методы анимации
            {
                case State.Open:
                    plate.args = new Object[] { Setting.PlateZoom, true }; // [0] - Zoom, [1] - flag
                    plate.showPlate = sprites.OpenAnime;
                    break;

                case State.Mark:
                    plate.args = new Object[] { 0 };      // [0] - Step
                    plate.showPlate = sprites.MarkAnime;
                    break;

                case State.Drop:
                    double x = plate.point.X - sprites.GetSizePicture.Width / 2;   // Вычисляем вектор от центра экрана
                    double y = plate.point.Y - sprites.GetSizePicture.Height / 2;
                    double dX = x / Math.Sqrt(x * x + y * y);                      // Прирост координат при единичном шаге по вектору
                    double dY = y / Math.Sqrt(x * x + y * y);
                    plate.args = new Object[] { 0, Setting.PlateZoom, dX, dY, solitaire.plates }; // [0] - Step, [1] - Zoom, [2] - Прирост по X, [3] - Прирост по Y, [4] - Костыль для отрисовки затертых плашек, это неправильно
                    plate.showPlate = sprites.DropAnime;                    
                    break;

                //case State.Hide:
                //    plate.showPlate = sprites.HideAnime;
                //    break;
            }                
        }
        
        /// <summary>
        /// Обработка счетчика таймера
        /// </summary>
        public void Tick()
        {
            bool flag = true; // Для проверки плашек, которые подписаны на RollAnime (начальная анимация)

            foreach(Plate plate in solitaire.plates)
            {
                if (plate.showPlate != null)  // Если у плашки есть подписанный метод
                {                    
                    if (plate.showPlate.Method.Name == "RollAnime") // Проверяем есть ли плашки, которые только появляются на поле
                        flag = false;

                    plate.showPlate(plate);   // вызываем подписанный к плашке метод
                }
            }

            if (flag && flagRoll) // Сработает только один раз за игру
            {
                solitaire.OpenFreePlates(); // Если спрайты все появились, тогда открываем доступные плашки
                flagRoll = false;
            }
        }

        /// <summary>
        /// Возвращает изображение поля
        /// </summary>
        /// <returns>Image</returns>
        public Image Refresh()
        {
            return sprites.GetBitmap;
        }
    }
}
