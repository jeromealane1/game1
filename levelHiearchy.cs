
public class Rootobject
{
    public Level level { get; set; }
}

public class Level
{
    public string levelnumber { get; set; }
    public Enemies enemies { get; set; }
}

public class Enemies
{
    public Enemielist enemielist { get; set; }
}

public class Enemielist
{
    public RootEnemy[] enemyType1 { get; set; }
}

public class RootEnemy
{
    public string[] enemy { get; set; }
}
