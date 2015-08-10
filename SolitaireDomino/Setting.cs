using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireDomino
{
    public static class Setting
    {
        public const int ShiftX = 50;  // Отступ от края экрана
        public const int ShiftY = 50;

        public const int MaxPoint = 6;     // Макс кол-во точек на плашке
        public const int SumDroped = 12;   // Сумма точек, необходимая для удаления плашек

        public const int PlateDistanceX = 135; // Расстояние между плашками по X
        public const int PlateDistanceY = 75;  // Расстояние между плашками по Y
        public const float PlateZoom = 0.14f;  // Масштаб плашки домино

        public const int dSpriteX = 400;    // Смещение правой части спрайта домино относительно левой
        public const int dSpriteY = 0;

        public const int PlateWidth = 800;  // Предполагаемые размеры плашки
        public const int PlateHeight = 400;

    }
}
