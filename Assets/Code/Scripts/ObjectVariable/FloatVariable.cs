using UnityEngine;
using UnityEngine.Events;
using XaviEssencials.Runtime;

namespace XaviGames.ObjectVariable
{
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Xavi Games/Variable/FloatVariable", order = 1)]
    public class FloatVariable : ScriptableObject
    {
        [SerializeField]
        private float _value;

        [field: SerializeField]
        [field: ReadOnly]
        public float Value { get; private set; }

        public UnityAction<float> OnValueChanged;

        private void OnEnable()
        {
            Value = _value;
        }

        public void SetValue(float value)
        {
            Value = value;
            OnValueChanged?.Invoke(Value);
        }
    }
}
