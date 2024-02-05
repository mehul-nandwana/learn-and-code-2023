using Microsoft.VisualBasic;

namespace KarprekarContant
{
    public class NumberValidator
    {
        public bool IsNumberValid(int number)
        {
            bool result = true;
            result = this.CheckIfAllDigitsInNumberAreSame(number);
            if (number < Constants.MINIMUM_RANGE_FOR_KARPEKAR_CONSTANT || number > Constants.MAXIMUM_RANGE_FOR_KARPREKAR_CONSTANT)
            {
                result = false;
            }

            return result;
        }

        public bool CheckIfAllDigitsInNumberAreSame(int number)
        {
            int[] digit = new int[4];
            digit[0] = (number / 1000) % 10;
            digit[1] = (number / 100) % 10;
            digit[2] = (number / 10) % 10;
            digit[3] = number % 10;
            return !(digit[0] == digit[1] && digit[1] == digit[2] && digit[2] == digit[3]);
        }
    }
}
