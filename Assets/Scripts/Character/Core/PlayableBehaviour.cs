public abstract class PlayableBehaviour<T> : CharacterBehaviour
{
    public abstract bool Playing { get; }
    
    public abstract bool CanPlay(T context);

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