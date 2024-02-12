namespace KarprekarContant
{
    public class KarparekarConstantEvaluator
    {
        public void NumberOfIterationToReachKarprekarConstant(int number)
        {
            int[] inputArray1 = new int[4];
            int[] inputArray2 = new int[4];
            int smallestNumber = 0;
            int largestNumber = 0;
            int iterationRequiredToReachKarprekarConstant = 0;

            for (int i = 0; i < 6; ++i)
            {
                inputArray1 = this.ConvertToIntArray(number);
                inputArray2 = this.ConvertToIntArray(number);
                Array.Sort(inputArray1);
                Array.Sort(inputArray2);
                Array.Reverse(inputArray2);
                smallestNumber = ConvertArrayToNumber(inputArray1);
                largestNumber = ConvertArrayToNumber(inputArray2);
                number = largestNumber - smallestNumber;
                if (number == Constants.KARPREKAR_CONSTANT)
                {
                    break;
                }

                ++iterationRequiredToReachKarprekarConstant;
            }

             printIteration(iterationRequiredToReachKarprekarConstant);
        }

        private int[] ConvertToIntArray(int number)
        {
            int[] numberArray = new int[4];

            for (int index = 0; index < 4; ++index)
            {
                numberArray[index] = number % 10;
                number /= 10;
            }

            return numberArray;
        }

        private int ConvertArrayToNumber(int[] numberArray)
        {
            int arrayToNumber = 0;

            for (int index = 0; index < 4; ++index)
            {
                arrayToNumber = (arrayToNumber * 10) + numberArray[index];
            }

            return arrayToNumber;
        }

        private void printIteration(int iterationRequiredToReachKarprekarConstant)
        {
            Console.WriteLine("It take " +
                      iterationRequiredToReachKarprekarConstant
                      + " iteration to come to Karprekar constant");
        }
    }
}
