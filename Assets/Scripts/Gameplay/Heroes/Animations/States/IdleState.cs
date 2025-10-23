using UnityEngine;
using Random = UnityEngine.Random;

namespace TPT.Gameplay.Heroes.Animations.States
{
    public class IdleState : StateMachineBehaviour
    {
        [SerializeField]
        private string[] idlesStates;

        [SerializeField]
        private Vector2 minMaxTimeBetweenIdles = new Vector2(4, 20);

        private float timer;

        private void OnEnable()
        {

        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            timer = Random.Range(minMaxTimeBetweenIdles.x, minMaxTimeBetweenIdles.y);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {

        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if(timer <= 0)
                return;

            if(animator.IsInTransition(layerIndex))
                return;

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                animator.Play(idlesStates[Random.Range(0, idlesStates.Length)], layerIndex);
            }
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }
    }
}