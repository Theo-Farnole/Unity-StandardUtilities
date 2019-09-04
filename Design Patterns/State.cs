public abstract class State
{
    public abstract void Tick();
    public abstract void FixedTick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
}