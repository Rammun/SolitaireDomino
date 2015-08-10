using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireDomino
{
    public class PlatesList : IEnumerable, IEnumerator
    {
        Plate[] plates;
        int index = -1;

        public PlatesList(Plate[] plates)
        {
            this.plates = plates;
        }
    
        public object Current
        {
	        get { return plates[index]; }
        }

        public bool MoveNext()
        {
            if (index == plates.Length - 1)
            {
                Reset();
                return false;
            }

            index++;
            return true;
        }

        public void Reset()
        {
 	        index = -1;
        }

        public IEnumerator GetEnumerator()
        {
 	        return this;
        }

        public IEnumerable MyItr(int begin, int end)
        {
            for (int i = begin; i <= end; i++)
            {
                yield return (char)(ch + i);
            }
        }
    }
}
