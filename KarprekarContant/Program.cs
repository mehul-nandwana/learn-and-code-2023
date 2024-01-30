using KarprekarContant;

namespace Solution
{
    class KarprekarConstant
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Enter the 4 digit number");
            int karprekarNumer = Convert.ToInt32(Console.ReadLine());

            KarparekarConstantEvaluator karprekarContantEvaluator
                = new KarparekarConstantEvaluator();

            if (NumberValidator.IsNumberValid(karprekarNumer))
                Console.WriteLine("It take " +
                    karprekarContantEvaluator.IsValidNumberForKarekarConstant
                    (karprekarNumer)
                    + " iteration to come to Karprekar constant");
            else
                Console.WriteLine("The number entered is not a valid number");

        }

    }

}
