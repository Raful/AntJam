public class HP
{
    public int currentHp { get; private set; }
    public int maxHp { get; private set; }
    
    public HP(int maxHp)
    {
        this.maxHp = maxHp;
        this.currentHp = maxHp;
    }

    public void DealDamage(int damage)
    {

    }
}
