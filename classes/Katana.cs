class Katana : Weapon
{
    private int damage;

    public Katana(int damage)
    {
        this.damage = damage;
    }

    public override int Attack()
    {
        return damage;
    }

    public override string AttackSound()
    {
        return "Swish!";
    }

}