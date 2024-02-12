namespace KarprekarContant
{
    public class InputReciever
    {
        public int TakeInputNumber()
        {
            Console.WriteLine("Enter the 4 digit number");
            int number = Convert.ToInt32(Console.ReadLine());
            return number;
        }
    }
}
