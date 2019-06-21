using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Selector : Selectable
{
    #region Fields

    [Header("Selector Settings")]
    [SerializeField] private List<string> _options = new List<string>();
    [SerializeField] private TextMeshProUGUI _label;
    [Space]
    [SerializeField] private UnityEvent _onValueChanged;

    private int _value = 0;

    private bool _isHorizontalPressed = false;
    private bool _isSelected = false;
    #endregion

    #region Properties
    public int Value
    {
        get
        {
            return _value;
        }

        set
        {
            // clamp value
            if (value < 0)
            {
                value = _options.Count - 1;
            }
            else if (value >= _options.Count)
            {
                value = 0;
            }

            _value = value;
            UpdateLabel();

            _onValueChanged?.Invoke();
        }
    }

    public UnityEvent OnValueChanged
    {
        get
        {
            if (_onValueChanged == null)
            {
                _onValueChanged = new UnityEvent();
            }

            return _onValueChanged;
        }
    }
    #endregion

    #region MonoBehaviour Callbacks
    protected override void Awake()
    {
        if (_onValueChanged == null)
        {
            _onValueChanged = new UnityEvent();
        }
    }

    void Update()
    {
        if (_isSelected)
        {
            Selected();
        }
    }

    protected override void OnDisable()
    {
        _isSelected = false;
    }
    #endregion

    #region Selectable Callbacks
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        _isSelected = true;
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        _isSelected = false;
    }
    #endregion

    void Selected()
    {
        float horizontal = 0;
        bool isHorizontalDown = (horizontal != 0) && (_isHorizontalPressed == false);

        Debug.LogError("Horizontal input isn't set! Selector can't work!");
        
        if (isHorizontalDown)
        {
            Value += (int)Mathf.Sign(horizontal);
        }

        _isHorizontalPressed = (horizontal != 0);
    }

    void UpdateLabel()
    {
        if (_options.Count > 1)
        {
            _label.text = _options[Value];
        }
    }
}
