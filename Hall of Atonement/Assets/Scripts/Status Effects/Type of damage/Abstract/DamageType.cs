﻿using System;

public abstract class DamageType : ICloneable
{
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}