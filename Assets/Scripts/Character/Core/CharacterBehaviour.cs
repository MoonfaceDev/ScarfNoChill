using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class CharacterBehaviour : BaseComponent
{
    protected Animator Animator { get; private set; }
    protected Character Character { get; private set; }

    public bool Enabled
    {
        get => disableCount == 0;
        set
        {
            if (value)
            {
                if (disableCount > 0)
                {
                    disableCount--;
                }
            }
            else
            {
                disableCount++;
            }
        }
    }

    private int disableCount;

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Character = GetComponent<Character>();
    }
}