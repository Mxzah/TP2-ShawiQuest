class Player : ISubject
{
    private string _name = string.Empty;
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
    public Defense? Defense { get; private set; }

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
    public void EquipDefense(Defense defense)
    {
        Defense = defense;
    }
    public void Attack(Player target)
    {
        int damage = 5;
        string sound = string.Empty;
        var weapon = Weapon;
        if (weapon != null)
        {
            damage = weapon.Attack();
            sound = weapon.AttackSound();
        }
        if (!string.IsNullOrEmpty(sound))
        {
            Console.WriteLine($"{Name} attacks with: {weapon?.ToString() ?? "Unarmed"} - Sound: {sound}");
        }
        target.TakesDamage(damage);
    }
    public void TakesDamage(int damage)
    {
        if (Defense != null)
        {
            int defenseValue = Defense.Defend();
            string defenseSound = Defense.DefendSound();
            damage = (damage - defenseValue);
            if (damage < 0) damage = 0;
            if (!string.IsNullOrEmpty(defenseSound))
            {
                Console.WriteLine($"{Name} defends with: {Defense.ToString()} - Sound: {defenseSound}");
            }
        }
        HealthPoints -= damage;
        Notify(-damage);
    }
    
    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Reset()
    {
        HealthPoints = 100;
        Notify(0);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify(int delta)
    {
        foreach (var observer in observers)
        {
            observer.Update(delta, HealthPoints, this);
        }
    }
}
