public class Collectable : BaseComponent
{
    public string objectType;

    public void Consume()
    {
        Destroy(gameObject);
    }
}