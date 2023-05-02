namespace GuessNumberAPI.Helpers
{
    public class SecretNumberGenerator
    {

        private int _number;

        private int _firstDigitMin = 1;
        private int _digitMin = 0;
        private int _digitMax = 9;

        private int _minValue = 1000;

        public int GenerateUniqueDigitNumber()
        {
            int firstDigit = GenerateFirstDigit();
            _number = firstDigit;

            while (_number < _minValue)
            {
                int randomDigit = GenerateRandomDigit();

                AddDigitToNumberIfUnique(randomDigit);
            }

            return _number;
        }

        private int GenerateFirstDigit()
        {
            Random random = new Random();
            return random.Next(_firstDigitMin, _digitMax);
        }

        private int GenerateRandomDigit()
        {
            Random random = new Random();
            return random.Next(_digitMin, _digitMax);
        }

        private bool IsUniqueDigit(int digit)
        {
            bool result = !_number.ToString().Contains(digit.ToString());

            return result;
        }

        private void AddDigitToNumberIfUnique(int digit)
        {
            if (IsUniqueDigit(digit))
            {
                _number = (_number * 10) + digit;
            }
        }
    }
}
