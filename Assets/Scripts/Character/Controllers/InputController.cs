using UnityEngine;

public class InputController : BaseController
{
    private Walk walk;
    private Scarf scarf;
    private Jump jump;
    private WindSpawner wind;

    protected override void Awake()
    {
        base.Awake();
        walk = GetComponent<Walk>();
        scarf = GetComponent<Scarf>();
        jump = GetComponent<Jump>();
        wind = GetComponent<WindSpawner>();

        wind.Play(new WindSpawner.Context());
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

        if (Input.GetButtonDown("Scarf"))
        {
            scarf.Play(new Scarf.Context());
        }
        if (Input.GetButtonUp("Scarf"))
        {
            scarf.Stop();
        }
        if (Input.GetButtonDown("Jump"))
        {
            jump.Play(new Jump.Context());
        }
        if (Input.GetButtonUp("Jump"))
        {
            jump.StopAccelerate();
        }
    }
}