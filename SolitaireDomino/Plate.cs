using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireDomino
{
    public delegate void EventPlate(Plate plate);
    public enum State { Open, Hide, Drop, Mark}

    public class Plate
    {
        public int numL;             // Значение левой части домино
        public int numR;             // Значение правой части домино
        public Point point;          // Координаты плашки
        public State state;          // Статус плашки
        public EventPlate showPlate; // Метод, подписанный к плашке
        public Object[] args;        // Дополнительные параметры

        public Plate(int numL, int numR)
        {
            this.numL = numL;
            this.numR = numR;
            this.point = new Point();
            this.state = State.Hide;
            this.showPlate = null;
        }

        public Plate(int numL, int numR, State state)
        {
            this.numL = numL;
            this.numR = numR;
            this.point = new Point();
            this.state = state;
            this.showPlate = null;
        }

        public Plate()
        {
            // TODO: Complete member initialization
        }

        public int Sum()
        {
            return numL + numR;
        }
    }
}
