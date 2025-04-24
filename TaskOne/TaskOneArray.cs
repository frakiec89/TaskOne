

namespace TaskOne
{
    internal class TaskOneArray
    {
        private int length;
  
        private int startIndex;


        /// <summary>
        /// по умоолчанию width=4/ height= 4 startIndex= 2 
        /// </summary>
        public TaskOneArray ()
        {
            this.length = 4;
            this.startIndex = 2;
        }

        public TaskOneArray(int length , int startIndex)
        {
            if (length <=0)
                throw new ArgumentOutOfRangeException ("length");

            this.length = length;
            this.startIndex = startIndex;
        }


        /// <summary>
        /// выводим матрицу  в  которой  основная диагональ  увеличивается на  1 от  стартовго значения, последний элемент -стартовый  -1
              /// </summary>
        /// <returns></returns>
        public int [,] VariantOne ()
        {
            // например матрица размером 4 при старте  2
            // 2 0 0 0
            // 0 3 0 0
            // 0 0 4 0
            // 0 0 0 1

            int[,] array = new int [length, length];

            for (int i = 0; i < length; i++)
            {
                if (i == 0)
                {
                    array[0, 0] = startIndex;
                    continue;
                }

                if(i == length - 1)
                {
                    array[i, i] = startIndex-1;
                    continue;
                }
                
                array[i, i] = startIndex + i;
            }

            return array;
        }


        public void PrintConsole(int[, ] ints)
        {
            for (int i = 0; i < ints.GetLength(1); i++)
            {
                for (int j = 0; j < ints.GetLength(1); j++)
                {
                    Console.Write($"{ints[i, j],3}|");

                }
                Console.WriteLine();
            }
        }
    }
}
