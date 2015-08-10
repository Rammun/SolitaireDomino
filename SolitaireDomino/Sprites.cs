using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSprite;
using System.Drawing;
using System.Resources;

namespace SolitaireDomino
{
    public class Sprites
    {
        protected SortedList<string, Sprite> sprites;  // Список всех спрайтов
        protected Graph graph;
        protected Size sizePicture;                    // Размер экрана

        public Sprites(Size size)
        {
            sprites = new SortedList<string, Sprite>();
            graph = new Graph(size.Width, size.Height);

            this.sizePicture = size;
        }

        public Bitmap GetBitmap { get { return graph.GetBitmap; } }
        public Size GetSizePicture { get { return sizePicture; } }

        public virtual void InitSprites() { }
        
        /// <summary>
        /// Рисует указанный по имени спрайт в укзанных координатах и масштабе
        /// </summary>
        /// <param name="spriteName">Имя спрайта</param>
        /// <param name="shiftX">координата X</param>
        /// <param name="shiftY">координата Y</param>
        /// <param name="zoom">масштаб</param>
        public void SpriteShow(string spriteName, Point vector, float zoom)
        {
            graph.Draw(sprites[spriteName], vector.X, vector.Y, zoom);
        }

        /// <summary>
        /// Рисует указанный по имени спрайт в укзанных координатах и масштабе по X и Y
        /// </summary>
        /// <param name="spriteName">Имя спрайта</param>
        /// <param name="shiftX">координата X</param>
        /// <param name="shiftY">координата Y</param>
        /// <param name="zoomX">масштаб по X</param>
        /// <param name="zoomY">масштаб по Y</param>
        public void SpriteShow(string spriteName, Point vector, float zoomX, float zoomY)
        {
            graph.Draw(sprites[spriteName], vector.X, vector.Y, zoomX, zoomY);
        }

        /// <summary>
        /// Рисует спрайт с параметрами по умолчанию
        /// </summary>
        public void SpriteShow(string spriteName)
        {
            graph.Draw(sprites[spriteName]);
        }        

        /// <summary>
        /// Очищает картинку
        /// </summary>
        public void Clear()
        {
            graph.Clear();
        }

        /// <summary>
        /// Очищает картинку в указанном секторе
        /// </summary>
        /// <param name="point">Точка</param>
        /// <param name="size">Размер</param>
        public void ClearSize(Point point, Size size)
        {
            graph.Clear(point, size, Color.White);
        }
    }
}
