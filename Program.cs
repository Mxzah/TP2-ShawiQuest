
static void Main()
{
    Player player1 = new Player("Hero");
    Player player2 = new Player("Villain");

    Weapon weapon1 = new Katana(20);
    player1.EquipWeapon(weapon1);


    GameManager gameManager = new GameManager(player1, player2);
    gameManager.StartGame();
}

Main();
