using UnityEngine;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(Animator))]
public abstract class CharacterBehaviour : BaseComponent
{
    protected Animator Animator => Character.Animator;
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
        Character = GetComponent<Character>();
    }
}