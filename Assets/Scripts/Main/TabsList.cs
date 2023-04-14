using System.Collections.Generic;
using UnityEngine;

public class TabsList : MonoBehaviour
{
    public static bool AllClosed;
    private static readonly Stack<GameObject> _tabs = new();

    private UserInput _input;

    public static void Open(GameObject window)
    {
        if (_tabs.Contains(window)) return;
        
        _tabs.Push(window);
        window.SetActive(true);
        
        if(AllClosed) AllClosed = false;
    }

    public static void CloseLast()
    {
        if (_tabs.Count == 0)
        {
            AllClosed = true;
            return;
        }

        _tabs.Pop().SetActive(false);
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Main.Escape.performed += context => CloseLast();
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Main.Escape.performed -= context => CloseLast();
    }

    private void Awake() => _input = new();
}
