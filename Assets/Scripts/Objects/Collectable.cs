using UnityEngine.Events;

public class Collectable : BaseComponent
{
    public string objectType;
    public UnityEvent onConsume;
    public int score;

    public void Consume()
    {
        onConsume.Invoke();
        Destroy(gameObject);
    }
}