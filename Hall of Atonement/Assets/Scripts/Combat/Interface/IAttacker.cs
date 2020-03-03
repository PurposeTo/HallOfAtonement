public interface IAttacker
{
    void Attack(CharacterCombat combat);
}


public interface IMelee : IAttacker
{
    float MeleeAttackRange { get; set; }
}


public interface IRanged : IAttacker
{

}
