using KarprekarContant;

namespace Solution
{
    public class KARPREKAR_CONSTANT
    {
        public static void Main(string[] args)
        {
            try
            {
                InputReciever inputReciever = new InputReciever();
                int karprekarNumer = inputReciever.TakeInputNumber();

                KarparekarConstantEvaluator karprekarContantEvaluator
                    = new ();

                NumberValidator numberValidator = new ();

                if (numberValidator.IsNumberValid(karprekarNumer))
                {
                    karprekarContantEvaluator.NumberOfIterationToReachKarprekarConstant(
                       karprekarNumer);
                }
                else
                {
                    Console.WriteLine("The number entered is not a valid number");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
