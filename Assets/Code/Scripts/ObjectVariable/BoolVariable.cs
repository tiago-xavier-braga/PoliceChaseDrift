using UnityEngine;
using UnityEngine.Events;
using XaviEssencials.Runtime;

namespace XaviGames.ObjectVariable
{
    [CreateAssetMenu(fileName = "BoolVariable", menuName = "Xavi Games/Variable/BoolVariable")]
    public class BoolVariable : ScriptableObject
    {
        [SerializeField]
        private bool _value;

        [field: SerializeField]
        [field: ReadOnly]
        public bool Value { get; private set; }

        public UnityAction<bool> OnValueChanged;

        public void OnEnable()
        {
            Value = _value;
        }

        public void SetValue(bool newValue)
        {
            Value = newValue;
            OnValueChanged?.Invoke(newValue);
        }

        public void ToggleValue()
        {
            Value = !Value;
        }
    }
}
