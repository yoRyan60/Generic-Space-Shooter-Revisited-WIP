using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static readonly Evt OnEnemyAttackInactive = new();
}

public class Evt
{
    event Action _action = delegate { };

    public void Invoke() => _action.Invoke();
    public void AddListener(Action listener) => _action += listener;
    public void RemoveListener(Action listener) => _action -= listener;
}
