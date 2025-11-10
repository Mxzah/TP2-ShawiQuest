class GameManager
{
    private Player _player1;
    private Player _player2;
    private float _damageRatio;
    private int _timeBetweenTurns;
    private HealthBar _healthBarPlayer1;
    private HealthBar _healthBarPlayer2;
    private Weapon[] _weapons;
    private Defense[] _defenses;
    private GameOverManager _gameOverManager;
    public WeaponDecorator weaponDecorator;
    public GameManager(Player player1, Player player2, float damageRatio = 1f, int timeBetweenTurns = 1000)
    {
        _player1 = player1;
        _player2 = player2;

        _damageRatio = damageRatio;
        _timeBetweenTurns = timeBetweenTurns;
        
        _healthBarPlayer1 = new HealthBar();
        _healthBarPlayer2 = new HealthBar();
        _gameOverManager = new GameOverManager();

        _player1.Attach(_healthBarPlayer1);
        _player1.Attach(_gameOverManager);
        _player2.Attach(_healthBarPlayer2);
        _player2.Attach(_gameOverManager);


        Weapon regularKatana = new Katana(20);
        Weapon regularNunchaku = new Nunchaku(15);
        Weapon regularShuriken = new Shuriken(10);

        _weapons = new Weapon[]
        {
            regularKatana,
            new SharpnessDecorator(regularKatana),
            regularShuriken,
            new SharpnessDecorator(regularShuriken),
            regularNunchaku,
            new MomentumDecorator(regularNunchaku),
            new MomentumDecorator(new SharpnessDecorator(regularKatana)),

        };

        Defense regularSmokeBomb = new SmokeBomb();
        Defense regularShield = new Shield();
        Defense regularRoll = new Roll();
        
        _defenses = new Defense[]
        {
            regularSmokeBomb,
            regularShield,
            regularRoll,
            new QuickReflexeDecorator(regularRoll),
            new QuickReflexeDecorator(regularSmokeBomb),
            new DiamondDecorator(regularShield)
        };
    }

    public void StartGame()
    {
        int counter = 1;
        while (true)
        {
            for (int i = 0; i < counter; i++)
            {
                _player1.EquipWeapon(_weapons[Random.Shared.Next(_weapons.Length)]);
                _player2.EquipWeapon(_weapons[Random.Shared.Next(_weapons.Length)]);

                _player1.EquipDefense(_defenses[Random.Shared.Next(_defenses.Length)]);
                _player2.EquipDefense(_defenses[Random.Shared.Next(_defenses.Length)]);

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

                Thread.Sleep(_timeBetweenTurns);

                if (GameOverManager.IsGameOver)
                {
                    Console.WriteLine("\nPress Space to restart, or 'q' to quit.");
                    while (true)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Spacebar)
                        {
                            GameOverManager.Reset();
                            _player1.Reset();
                            _player2.Reset();
                            counter = 1;
                            Console.WriteLine("Game restarted.");
                            break;
                        }
                        else if (key.Key == ConsoleKey.Q)
                        {
                            Console.WriteLine("Exiting game.");
                            return;
                        }
                    }
                }

                counter++;
            }
        }
    }
}