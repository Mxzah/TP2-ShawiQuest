
static void Main()
{
    Player player1 = new Player("Hero");
    Player player2 = new Player("Villain");

    GameManager gameManager = new GameManager(player1, player2, damageRatio: 2.0f, timeBetweenTurns: 2000);
    gameManager.StartGame();
}

Main();
