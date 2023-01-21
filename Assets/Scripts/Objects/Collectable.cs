using UnityEngine.Events;

public class Collectable : BaseComponent
{
    public string objectType;
    public UnityEvent onConsume;

    public void Consume()
    {
        onConsume.Invoke();
        Destroy(gameObject);
    }
}