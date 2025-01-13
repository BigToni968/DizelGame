using UnityEngine;

public class Block : MonoBehaviour, IDamage
{
    [SerializeField] private UIBlock _ui;
    [field: SerializeField] public float Health { get; private set; } = 100f;

    private void Start()
    {
        _ui?.Init(Health);
        _ui?.SetValue(Health);
    }

    public void Set(float damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, float.MaxValue);

        if (Health == 0)
        {
            Destroy(gameObject);
        }

        _ui?.SetValue(Health);
    }
}

public interface IDamage
{
    public void Set(float damage);
}