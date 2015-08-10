using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireDomino
{
    /// <summary>
    /// Описывает основные методы правил игры
    /// </summary>
    public class Solitaire
    {
        public event EventPlate plate_СhangeState;  // Событие, для генерации сообщения о смене статуса плашки
        public static Random rnd;
        public Plates plates { get; private set; }  // Набор домино
        public int GetStatusGame { get; private set; } // Состояние игры

        public Solitaire()
        {
            plate_СhangeState = null;
            plates = new Plates();
            rnd = new Random();
        }

        /// <summary>
        /// Перемешивает плашки
        /// </summary>
        public void ChangRandPlates()
        {
            for (int i = 0; i < plates.Count; i++)
            {
                int index = rnd.Next(0, plates.Count);

                Plate temp = plates[i];
                plates[i] = plates[index];
                plates[index] = temp;
            }
        }        

        /// <summary>
        /// Проверяет допустимость нахождения плашки в данных координатах
        /// </summary>
        /// <param name="row">ряд</param>
        /// <param name="column">столбец</param>
        /// <returns>true - допустимо  false - не допустимо</returns>
        bool InRange(int row, int column)
        {
            if (row >= 0 && row <= Setting.MaxPoint && column >= 0 && column <= row)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверяет плашка удалена или нет
        /// </summary>
        /// <param name="row">строка</param>
        /// <param name="column">столбец</param>
        /// <returns>true - если плашка помеченна как удаленная, false - если плашка не удалена</returns>
        bool IsDropped(int row, int column)
        {
            if(InRange(row, column))
            {
                return plates[row, column].state == State.Drop;
            }
            return true;
        }

        /// <summary>
        /// Устанавливает статус плашки
        /// </summary>
        /// <param name="plate">Плашка</param>
        /// <param name="state">Статус</param>
        public void SetState(Plate plate, State state)
        {
            plate.state = state;

            if (plate_СhangeState != null) plate_СhangeState(plate); // Генерируем событие о изменении статуса плашки
        }

        /// <summary>
        /// Закрывает все плашки
        /// </summary>
        public void HidePlate()
        {
            foreach(Plate plate in plates)
            {
                SetState(plate, State.Hide);
            }
        }

        /// <summary>
        /// Открывает все плашки, которые не прикрыты другими
        /// </summary>
        public void OpenFreePlates()
        {
            for (int row = 0; row <= Setting.MaxPoint; row++)   // Перебираем все плашки
            {
                for (int column = 0; column <= row; column++)
                {
                    if (plates[row, column].state == State.Hide)
                    {
                        OpenHidedPlate(row, column);
                    }
                }
            }
            SetStatusGame(); // После открытия плашек устанавливаем состояние игры на выигрышь-проигрышь
        }

        /// <summary>
        /// Если плашку не прикрывают другие, помечает ее как "открытую"
        /// </summary>
        /// <param name="row">строка</param>
        /// <param name="column">столбец</param>
        public void OpenHidedPlate(int row, int column)
        {
            if (IsDropped(row + 1, column) && IsDropped(row + 1, column + 1) ||
                IsDropped(row - 1, column) && IsDropped(row - 1, column - 1))
            {
                if(InRange(row, column))
                    SetState(plates[row, column], State.Open);
            }

            //SetStatusGame(); // После открытия плашек устанавливаем состояние игры на выигрышь-проигрышь
        }

        /// <summary>
        /// Устанавливает состояние игры: 1 Все плашки убраны, -1 Нет ходов, 0 Процесс игры
        /// </summary>
        private void SetStatusGame()
        {
            if (IsWinner()) GetStatusGame = 1;     // Лень было перечисление делать
            else    if (IsLosser()) GetStatusGame = -1;
                    else GetStatusGame = 0;
        }

        /// <summary>
        /// Маркировка плашки
        /// </summary>
        public void MarkPlate(Plate plate)
        {
            // Если плашка открыта
            if (plate.state == State.Open)
                SetState(plate, State.Mark);     // Помечаем ее маркированной
            else if (plate.state == State.Mark)  // Если она уже отмаркированна
                    SetState(plate, State.Open); // Снимаем маркировку
        }

        /// <summary>
        /// Убирает с поля, если возможно, маркированные плашки
        /// </summary>
        public void DropMarked()
        {
            int index1 = -1, index2 = -1;

            for (int i = 0; i < plates.Count; i++)   // Перебираем все плашки
            {
                if (plates[i].state == State.Mark)   // Если находим маркированную плашку
                {
                    if (index1 < 0) index1 = i;      // И она первая, то запоминаем
                    else if (index2 < 0)             // Иначе, если она вторая -
                    {
                        index2 = i;                  // запоминаем

                        if (plates.SumPlates(plates[index1], plates[index2]))  // Если сумма чисел пары плашек равна 12
                        {
                            SetState(plates[index1], State.Drop);  // Помечаем на удаление
                            SetState(plates[index2], State.Drop);
                            OpenFreePlates();                      // Открываем новые доступные плашки
                        }
                        else                                       // Если не сумма не совпадает
                        {
                            SetState(plates[index1], State.Open);  // Снимаем маркировку
                            SetState(plates[index2], State.Open);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Проверяет возможность хода
        /// </summary>
        /// <returns>true - есть ход, false - нет хода</returns>
        public bool IsLosser()
        {
            for (int j = 0; j < plates.Count - 1; j++)
            {
                for (int i = j + 1; i < plates.Count; i++)
                {
                    if ((plates[j].state == State.Open || plates[j].state == State.Mark) &&
                        (plates[i].state == State.Open || plates[i].state == State.Mark) &&
                        plates.SumPlates(plates[i], plates[j]))
                        return false;
                }
            }                
            return true;
        }

        /// <summary>
        /// Проверяет окончание игры
        /// </summary>
        /// <returns>true - все плашки удалены, false - на поле остались плашки</returns>
        public bool IsWinner()
        {
            foreach(Plate plate in plates)
            {
                if (plate.state != State.Drop)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Возвращает плашку, если координаты входят в ее зону
        /// </summary>
        /// <param name="x">координата X</param>
        /// <param name="y">координата Y</param>
        /// <returns>null - если координаты не входят в зону ни одной плашки, иначе возвращает найденную плашку</returns>
        public Plate GetPlateClick(int x, int y)
        {
            foreach(Plate plate in plates)
            {
                if (x > plate.point.X && x < plate.point.X + Setting.PlateDistanceX &&
                    y > plate.point.Y && y < plate.point.Y + Setting.PlateDistanceY)
                    return plate;
            }
            return null;
        }
    }
}
