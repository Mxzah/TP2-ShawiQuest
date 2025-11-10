class Katana : Weapon
{
    private int damage = 20;

    public Katana(int damage) { }

    public override int Attack()
    {
        return damage;
    }

    public override string AttackSound()
    {
        return "Swish!";
    }

}