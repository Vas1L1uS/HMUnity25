using System;

public interface IAbility
{
    public event EventHandler Destroyed;

    public void Execute();
}
