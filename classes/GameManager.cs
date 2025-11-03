class GameManager
{
    private Player _player1;
    private Player _player2;
    private HealthBar _healthBarPlayer1;
    private HealthBar _healthBarPlayer2;

    private GameOverManager _gameOverManager;
    public GameManager(Player player1, Player player2)
    {
        _player1 = player1;
        _player2 = player2;

        _healthBarPlayer1 = new HealthBar();
        _healthBarPlayer2 = new HealthBar();
        _gameOverManager = new GameOverManager();

        _player1.Attach(_healthBarPlayer1);
        _player1.Attach(_gameOverManager);
        _player2.Attach(_healthBarPlayer2);
        _player2.Attach(_gameOverManager);
    }

    public void StartGame()
    {
        int counter = 1;
        while (true)
        {
            for (int i = 0; i < counter; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine($"\n{_player1.Name}'s turn:");
                    _player1.Attack(_player2);
                }
                else
                {
                    Console.WriteLine($"\n{_player2.Name}'s turn:");
                    _player2.Attack(_player1);
                }
                System.Threading.Thread.Sleep(1000);
                counter++;
            }
            
        }
    }
}