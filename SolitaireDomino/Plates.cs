using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireDomino
{
    public class Plates
    {
        Plate[] plates;
        public int Count { get; private set; }

        public Plates()
        {
            // Считаем сколько плашек в колоде в зависимости от возможного макс кол-ва очков
            Count = ((Setting.MaxPoint + 1) * (Setting.MaxPoint + 1) + (Setting.MaxPoint + 1)) / 2;
            plates = new Plate[Count];

            // Создает список со всеми возможными плашками домино
            for (int row = 0, i = 0; row <= Setting.MaxPoint; row++)
            {
                for (int column = 0; column <= row; column++)
                {
                    plates[i++] = new Plate(row, column);  // Добавляем плашку в массив
                }
            }
        }

        /// <summary>
        /// Сверяет сумму двух плашек с необходимой суммой на удаление
        /// </summary>
        /// <param name="plate1">Первая плашка</param>
        /// <param name="plate2">Вторая плашка</param>
        /// <returns>true - если равно, false - если не равно</returns>
        public bool SumPlates(Plate plate1, Plate plate2)
        {
            return plate1.Sum() + plate2.Sum() == Setting.SumDroped;
        }

        // Благодаря этому, по плашкам можем пройтись foreach
        public IEnumerator GetEnumerator()
        {
            return plates.GetEnumerator();
        }

        // ----> Именованные энумераторы
        public Plate this[int row, int column]
        {
            get
            {
                return plates[(row * row + row) / 2 + column];
            }
            set
            {
                plates[(row * row + row) / 2 + column] = value;
            }
        }

        public Plate this[int index]
        {
            get
            {
                return plates[index];
            }
            set
            {
                plates[index] = value;
            }
        }
        // <-----
    }
}
