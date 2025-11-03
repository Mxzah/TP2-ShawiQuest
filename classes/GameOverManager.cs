class GameOverManager : IObserver
{
    public void Update(int delta, int currentHealth)
    {
        if (currentHealth <= 0)
        {
            Console.WriteLine($"Player has been defeated! Game Over!");
            Environment.Exit(0);
        }
    }
}