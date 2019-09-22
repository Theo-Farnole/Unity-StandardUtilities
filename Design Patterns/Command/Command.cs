using UnityEngine;

public abstract class Command
{
    protected GameObject _owner;

    public Command(GameObject owner)
    {
        _owner = owner;
    }

    public abstract void Execute();
}
