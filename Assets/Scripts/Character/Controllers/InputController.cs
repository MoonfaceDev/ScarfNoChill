using UnityEngine;

public class InputController : BaseController
{
    private Walk walk;
    private Scarf scarf;

    protected override void Awake()
    {
        base.Awake();
        walk = GetComponent<Walk>();
        scarf = GetComponent<Scarf>();
    }

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            walk.Play(new Walk.Context(horizontal));
        }
        else
        {
            walk.Stop();
        }

        var scarfButton = Input.GetButton("Scarf");
        if (scarfButton)
        {
            scarf.Play(new Scarf.Context());
        }
    }
}