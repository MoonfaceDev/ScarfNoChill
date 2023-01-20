using UnityEngine;

public class InputController : BaseController
{
    private Walk walk;

    protected override void Awake()
    {
        base.Awake();
        walk = GetComponent<Walk>();
    }

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            walk.Play(new WalkContext(horizontal));
        }
        else
        {
            walk.Stop();
        }
    }
}