using UnityEngine;

namespace XaviGames.Audio
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Xavi Games/Audio/AudioData", order = 1)]
    public class AudioData : ScriptableObject
    {
        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float Volume { get; private set; } = 1f;
    }
}