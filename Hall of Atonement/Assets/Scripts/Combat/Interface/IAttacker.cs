using UnityEngine;

public interface IAttacker
{
    void Attack(CharacterCombat combat);

    Transform AttackPoint { get; }
}


public interface IMelee : IAttacker
{
    float MeleeAttackRange { get; set; }
}


public interface IRanged : IAttacker
{

}
