public abstract class PlayableBehaviour<T> : CharacterBehaviour
{
    public abstract bool Playing { get; }

    public virtual bool CanPlay(T context)
    {
        return Enabled;
    }

    public void Play(T context)
    {
        if (CanPlay(context))
        {
            Execute(context);
        }
    }

    protected abstract void Execute(T context);

    public abstract void Stop();
}