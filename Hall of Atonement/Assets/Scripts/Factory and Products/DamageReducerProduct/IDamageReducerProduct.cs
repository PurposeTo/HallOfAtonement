﻿public interface IDamageReducerProduct
{
    float ReduceDamage(UnitStats targetStats, float damage, out bool isBlocked);
}
