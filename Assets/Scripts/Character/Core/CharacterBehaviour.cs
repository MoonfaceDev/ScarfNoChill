using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class CharacterBehaviour : MonoBehaviour
{
    protected Animator animator;

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

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}