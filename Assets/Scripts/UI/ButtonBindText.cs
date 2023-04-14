using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class ButtonBindText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [Dropdown(nameof(_inputNames))]
    [SerializeField] private string _selectedKeyBind;

    private readonly List<string> _inputNames = new();
    private UserInput _input;

    [Button]
    private void UpdateBindList()
    {
        var selected = _selectedKeyBind;

        _inputNames.Clear();

        _input = new();

        foreach (var binding in _input.bindings.ToList())
            if (!binding.isComposite)
                _inputNames.Add(binding.path);

        if (_inputNames.Contains(selected)) _selectedKeyBind = selected;
    }

    [Button]
    private void UpdateText()
    {
        if (_inputNames.Count == 0) UpdateBindList();

        if (string.IsNullOrEmpty(_selectedKeyBind)) return;

        string dividedString = _selectedKeyBind.Split('/').Last();
        string firstChar = dividedString[0].ToString().ToUpper();
        string resultString = firstChar + dividedString.Remove(0, 1);

        _text.text = $"[{resultString}]";
    }

    private void OnValidate() => UpdateText();
    private void Start() => UpdateText();
}
