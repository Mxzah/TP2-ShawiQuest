class Player : ISubject
{
    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    private int _healthPoints;
    public int HealthPoints
    {
        get { return _healthPoints; }
        set
        {
            _healthPoints = value;
            if (_healthPoints < 0)
            {
                _healthPoints = 0;
            }
            if (_healthPoints > 100)
            {
                _healthPoints = 100;
            }
        }
    }
    public Weapon? Weapon { get; private set; }
    private List<IObserver> observers = new List<IObserver>();
    public Player(string name)
    {
        Name = name;
        HealthPoints = 100;
    }
    public void EquipWeapon(Weapon weapon)
    {
        Weapon = weapon;
    }
    public void Attack(Player target)
    {
        int damage = 10;
        string sound = "";
        if (Weapon != null)
        {
            damage = Weapon.Attack();
            sound = Weapon.AttackSound();
        }
        if (!string.IsNullOrEmpty(sound))
        {
            Console.WriteLine($"{Name} attacks with: {sound}");
        }
        target.TakesDamage(damage);
    }
    public void TakesDamage(int damage)
    {
        HealthPoints -= damage;
        Notify(-damage);
    }

    public void Heal()
    {
        Random rnd = new Random();
        int heal = rnd.Next(5, 21);
        HealthPoints += heal;
        Notify(heal);
    }
    
    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify(int delta)
    {
        foreach (var observer in observers)
        {
            observer.Update(delta, HealthPoints);
        }
    }
}
