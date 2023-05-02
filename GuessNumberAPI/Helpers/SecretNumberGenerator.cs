namespace GuessNumberAPI.Helpers
{
    public static class SecretNumberGenerator
    {
        public static int SecretNumberForGameGenerator() {
            const int SecretNumberMinValue = 1000;
            const int SecretNumberMaxValue = 9999;
            var randomNumberGenerator = new Random();
            return randomNumberGenerator.Next(SecretNumberMinValue, SecretNumberMaxValue + 1);
        }
    }
}
