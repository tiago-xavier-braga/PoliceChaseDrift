using UnityEngine;

namespace XaviGames.Sounds
{
    public class CollisionSoundGroup : MonoBehaviour
    {
        [field: SerializeField]
        public AudioClip AudioClip { get; private set; }
    }
}
