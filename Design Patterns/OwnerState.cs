using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OwnerState<T> : State
{
    protected T _owner;

    public OwnerState(T owner) => _owner = owner;    
}
