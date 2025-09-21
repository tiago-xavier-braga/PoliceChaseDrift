using UnityEditor.Animations;
using UnityEngine;
using XaviEssencials.Runtime;

namespace XaviGames.Props
{
    public class PropCarAnimator : MonoBehaviour
    {
        private enum CarDirection
        {
            Side,
            Front
        }

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private CarDirection _carDirection = CarDirection.Side;

        private const string paramSide = "IsSide";
        private const string paramFront = "IsLongitudinal";

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Car Trigger Enter");
            
            _animator.ResetTrigger(paramFront);
            _animator.ResetTrigger(paramSide);

            if (_carDirection == CarDirection.Front)
            {
                _animator.SetTrigger(paramFront);

            }
            else
            {
                _animator.SetTrigger(paramSide);
            }
        }
    }
}
