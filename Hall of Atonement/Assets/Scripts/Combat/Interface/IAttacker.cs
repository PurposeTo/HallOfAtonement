public interface IAttacker
{
    void Attack();
}


public interface IMelee : IAttacker
{

}


public interface IRanged : IAttacker
{

}
