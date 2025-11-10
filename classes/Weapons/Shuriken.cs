class Shuriken : Weapon
{
    private int damage = 10;

    public Shuriken(int damage) { }

    public override int Attack()
    {
        return damage;
    }

    public override string AttackSound()
    {
        return "Ding!";
    }


}