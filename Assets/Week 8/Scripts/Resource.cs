using System;
using UnityEngine;

public class Resource
{
    private float _current; 
    private float _max;

    public event Action Changed;
    public event Action Dead;

    public Resource(float maxResource)
    {
        _max = maxResource;
    }

    public float Current
    {
        get { return _current; }
        set
        {
            _current = Mathf.Clamp(value,0,_max);
            Changed?.Invoke();

            if (_current <= 0)
                Dead?.Invoke();
        }
    }

    public float Max
    {
        get { return _max; }
        set
        {
            _max = value;
        }
    }

    public void Add(float value)
    {
        Current += value;
        Changed?.Invoke();
    }

    public void Remove(float value)
    {
        Current -= value;
        Changed?.Invoke();
    }
}
