using DataSprite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireDomino
{
    public class PlatesSprites : Sprites
    {
        public PlatesSprites(Size size) : base (size)
        {
            InitSprites();
        }

        /// <summary>
        /// Загружаем спрайты в список
        /// </summary>
        public override void InitSprites()
        {
            sprites.Add("l0", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\l0.sprt")));
            sprites.Add("l1", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\l1.sprt")));
            sprites.Add("l2", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\l2.sprt")));
            sprites.Add("l3", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\l3.sprt")));
            sprites.Add("l4", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\l4.sprt")));
            sprites.Add("l5", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\l5.sprt")));
            sprites.Add("l6", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\l6.sprt")));
            sprites.Add("lh", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\lh.sprt")));
            sprites.Add("r0", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\r0.sprt")));
            sprites.Add("r1", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\r1.sprt")));
            sprites.Add("r2", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\r2.sprt")));
            sprites.Add("r3", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\r3.sprt")));
            sprites.Add("r4", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\r4.sprt")));
            sprites.Add("r5", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\r5.sprt")));
            sprites.Add("r6", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\r6.sprt")));
            sprites.Add("rh", new Sprite(SerializerSprite.LoadFromBit(@"..\..\Resources\rh.sprt")));
        }

        /// <summary>
        /// Отрисовует плашку с данными, взятыми из объекта плашки
        /// </summary>
        public void DominoShow(Plate plate)
        {
            string sNameL, sNameR;  // Имена спрайтов в списке

            if (plate.state == State.Hide)   // В зависимости вида плашки инициализируем имена нужных спрайтов
            {
                sNameL = "lh";
                sNameR = "rh";
            }
            else
            {
                sNameL = "l" + plate.numL.ToString();
                sNameR = "r" + plate.numR.ToString();
            }

            Point tempPoint = new Point(plate.point.X, plate.point.Y);

            SpriteShow(sNameL, tempPoint, Setting.PlateZoom);  // Рисуем левую половину плашки
            tempPoint.Offset((int)(Setting.dSpriteX * Setting.PlateZoom), (int)(Setting.dSpriteY * Setting.PlateZoom));  // Смещаем точку
            SpriteShow(sNameR, tempPoint, Setting.PlateZoom);  // Рисуем правую половину плашки
        }

        /// <summary>
        /// Отрисовывает плашку с заданными данными
        /// </summary>
        /// <param name="plate">Плашка</param>
        /// <param name="point">Координаты</param>
        /// <param name="zoomX">Масштаб по X</param>
        /// <param name="zoomY">Масштаб по Y</param>
        public void DominoShow(Plate plate, Point point, float zoomX, float zoomY)
        {
            string sNameL, sNameR;  // Имена спрайтов в списке

            if (plate.state == State.Hide)   // В зависимости от вида плашки инициализируем имена нужных спрайтов
            {
                sNameL = "lh";
                sNameR = "rh";
            }
            else
            {
                sNameL = "l" + plate.numL.ToString();
                sNameR = "r" + plate.numR.ToString();
            }

            SpriteShow(sNameL, point, zoomX, zoomY);  // Рисуем левую половину плашки
            point.Offset((int)(Setting.dSpriteX * zoomX), (int)(Setting.dSpriteY * zoomY));  // Смещаем точку
            SpriteShow(sNameR, point, zoomX, zoomY);  // Рисуем правую половину плашки
        }

        /// <summary>
        /// Отрисовывает закрытую плашку в заданной точке
        /// </summary>
        /// <param name="plate">Координаты плашки</param>
        public void HideDominoShow(Plate  plate)
        {
            SpriteShow("lh", plate.point, Setting.PlateZoom);  // Рисуем левую половину плашки
            SpriteShow("rh", new Point(plate.point.X + (int)(Setting.dSpriteX * Setting.PlateZoom), plate.point.Y + (int)(Setting.dSpriteY * Setting.PlateZoom)), Setting.PlateZoom);  // Рисуем правую половину плашки
        }

        /// <summary>
        /// Отрисовывает закрытую плашку в заданной точке и с заданным общим масштабом
        /// </summary>
        /// <param name="point">Координаты плашки</param>
        /// <param name="zoom">Масштаб плашки</param>
        public void HideDominoShow(Point point, float zoom)
        {
            SpriteShow("lh", point, zoom);  // Рисуем левую половину плашки
            point.Offset((int)(Setting.dSpriteX * zoom), (int)(Setting.dSpriteY * zoom));  // Смещаем точку
            SpriteShow("rh", point, zoom);  // Рисуем правую половину плашки
        }

        /// <summary>
        /// Отрисовывает закрытую плашку в заданной точке и заданным зумом по X и Y
        /// </summary>
        public void HideDominoShow(Point point, float zoomX, float zoomY)
        {
            SpriteShow("lh", point, zoomX, zoomY);  // Рисуем левую половину плашки
            point.Offset((int)(Setting.dSpriteX * zoomX), (int)(Setting.dSpriteY * zoomY));  // Смещаем точку
            SpriteShow("rh", point, zoomX, zoomY);  // Рисуем правую половину плашки
        }

        /// <summary>
        /// Перерисовывет все закрытые плашки
        /// </summary>
        /// <param name="plates">Плашки</param>
        public void HideAllDominoShow(Plates plates)
        {
            foreach(Plate plate in plates)
            {
                if(plate.state == State.Hide)
                {
                    HideDominoShow(plate);
                }
            }
        }

        /// <summary>
        /// Перерисовывет все открытые плашки
        /// </summary>
        /// <param name="plates">Плашки</param>
        public void OpenAllDominoShow(Plates plates)
        {
            foreach (Plate plate in plates)
            {
                if (plate.state == State.Open)
                {
                    DominoShow(plate);
                }
            }
        }

        /// <summary>
        /// Стирает плашку с Bitmap
        /// </summary>
        /// <param name="plate">Плашка</param>
        public void ClearPlate(Plate plate)
        {
            ClearSize(new Point(plate.point.X - 6, plate.point.Y - 6), new Size(Setting.PlateDistanceX, Setting.PlateDistanceY));
        }

        /// <summary>
        /// Стирает плашку с Bitmap
        /// </summary>
        /// <param name="point">Координаты плашки</param>
        public void ClearPlate(Point point)
        {
            ClearSize(new Point(point.X - 6, point.Y - 6), new Size(Setting.PlateDistanceX, Setting.PlateDistanceY));
        }

        /// <summary>
        /// Анимация маркировки плашки
        /// </summary>
        /// <param name="plate">Плашка</param>
        public void MarkAnime(Plate plate)
        {
            ClearPlate(plate);

            int step = (int)plate.args[0] + 1;
            plate.args[0] = step;
            int shiftX = Convert.ToInt32(Math.Cos(step * Math.PI / 4f) * 2);
            int shiftY = Convert.ToInt32(Math.Sin(step * Math.PI / 4f) * 2);

            DominoShow(plate, new Point(plate.point.X + shiftX, plate.point.Y + shiftY), Setting.PlateZoom, Setting.PlateZoom);
        }

        /// <summary>
        /// Анимация удаления плашки
        /// </summary>
        /// <param name="plate">Плашка</param>
        public void DropAnime(Plate plate)
        {
            int step = (int)plate.args[0];       // Инициализируем аргументы для анимации
            float zoom = (float)plate.args[1];
            double shiftX = (double)plate.args[2];
            double shiftY = (double)plate.args[3];
            Plates plates = (Plates)plate.args[4];

            Point point = new Point(plate.point.X + (int)(shiftX * step), plate.point.Y + (int)(shiftY * step)); // Вычисляем координаты

            ClearPlate(point); // Затираем плашку

            step += 3;
            zoom -= 0.001f;

            point.Offset((int)shiftX, (int)shiftY); // Смещаем координаты

            DominoShow(plate, point, zoom, zoom);   // Рисуем плашку в новых координатах, с новым масштабом

            HideAllDominoShow(plates);

            plate.args[0] = step;  // Сохраняем параметры для следующего шага анимации
            plate.args[1] = zoom;

            // Если достигло края экрана или масштаб нуля
            if( zoom < 0.05 || (point.X + Setting.PlateDistanceX) < 0 || point.X > GetSizePicture.Width ||
                              (point.Y + Setting.PlateDistanceY) < 0 || point.Y > GetSizePicture.Height)
            {
                ClearPlate(point);            // Стираем плашку
                plate.showPlate -= DropAnime; // И отписываем метод анимации
                return;
            }
        }

        /// <summary>
        /// Анимация появления плашки
        /// </summary>
        /// <param name="plate">Плашка</param>
        public void RollAnime(Plate plate)
        {
            float zoom = (float)plate.args[0];  // Инициализируем аргументы для анимации
            float dZoom = (float)plate.args[1];
            bool flag = (bool)plate.args[2];

            if (flag)  // Если увеличиваем плашку
            {
                if (zoom < (Setting.PlateZoom + 0.009f))
                {
                    ClearPlate(plate);
                    zoom += dZoom;

                    float x = plate.point.X + (Setting.PlateWidth * Setting.PlateZoom - Setting.PlateWidth * zoom) / 2;
                    float y = plate.point.Y + (Setting.PlateHeight * Setting.PlateZoom - Setting.PlateHeight * zoom) / 2;

                    DominoShow(plate, new Point((int)x, (int)y), zoom, zoom);
                }
                else
                    flag = false;
            }
            else   // Если уменьшаем плашку
            {
                if (zoom > Setting.PlateZoom)
                {
                    ClearPlate(plate);
                    zoom -= 0.003f;

                    float x = plate.point.X + (Setting.PlateWidth * Setting.PlateZoom - Setting.PlateWidth * zoom) / 2;
                    float y = plate.point.Y + (Setting.PlateHeight * Setting.PlateZoom - Setting.PlateHeight * zoom) / 2;

                    DominoShow(plate, new Point(plate.point.X, (int)y), Setting.PlateZoom, zoom);
                }
                else
                {
                    plate.showPlate -= RollAnime;  // Анимация закончилась, отписываем метод
                    return;
                }
            }

            plate.args[0] = zoom;  // Сохраняем параметры для следующего шага анимации
            plate.args[1] = dZoom;
            plate.args[2] = flag;
        }

        /// <summary>
        /// Анимация открывания плашки
        /// </summary>
        /// <param name="plate">Плашка</param>
        public void OpenAnime(Plate plate)
        {
            float zoom = (float)plate.args[0];  // Инициализируем аргументы для анимации
            bool flag = (bool)plate.args[1];
            // Вычисляем смещение плашки по Y
            float y = plate.point.Y + (Setting.PlateHeight * Setting.PlateZoom - Setting.PlateHeight * zoom) / 2;

            if(flag)  // Если уменьшаем толщину плашки
            {
                if (zoom > 0.05f)
                {
                    ClearPlate(plate);
                    zoom -= 0.004f;
                    HideDominoShow(new Point(plate.point.X, (int)y), Setting.PlateZoom, zoom);
                }
                else
                    flag = false;    
            }
            else    // Если увеличиваем толщину плашки
            {
                if (zoom < Setting.PlateZoom)
                {
                    ClearPlate(plate);
                    zoom += 0.004f;
                    DominoShow(plate, new Point(plate.point.X, (int)y), Setting.PlateZoom, zoom);
                }
                else
                {
                    plate.showPlate = DominoShow;
                    return;
                }
            }

            plate.args[0] = zoom;  // Сохраняем параметры для следующего шага анимации
            plate.args[1] = flag;
        }

        /// <summary>
        /// Анимация закрывания плашки
        /// </summary>
        /// <param name="plate">Плашка</param>
        internal void HideAnime(Plate plate)
        {
            plate.showPlate = HideDominoShow;
        }
    }
}
