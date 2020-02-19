public interface IAttacker
{
    void Attack(CharacterCombat combat);
}


public interface IMelee : IAttacker
{

}


public interface IRanged : IAttacker
{

}
