namespace KarprekarContant
{
    public static class NumberValidator
    {
        public static bool IsNumberValid(int number)
        {
            int digit1 = number / 1000 % 10;
            int digit2 = number / 100 % 10;

            if (number < 999 || number > 9999)
                return false;

            if (digit1 == digit2)
            {
                return false;
            }
            return true;

        }
    }
}
