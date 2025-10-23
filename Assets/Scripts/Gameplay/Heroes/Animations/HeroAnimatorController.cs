using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TPT.Gameplay.Heroes.Animations
{
    public class HeroAnimatorController : MonoBehaviour
    {
        private static readonly int IsAlive = Animator.StringToHash("IsAlive");
        private static readonly int IsPlaying = Animator.StringToHash("IsPlaying");

        [SerializeField]
        private Animator animator;
        [SerializeField]
        private Hero hero;

        [SerializeField]
        private Transform weapon;

        [SerializeField]
        private Transform handParent, sheathedParent;

        public Animator Animator => animator;

        private void Start()
        {
            SheathWeapon(null);
        }

        private void Update()
        {
            animator.SetBool(IsAlive, hero.IsAlive);
            animator.SetBool(IsPlaying, hero.IsPlaying);
        }

        public void DrawWeapon(AnimationEvent animationEvent)
        {
            weapon.SetParent(handParent);
            weapon.DOKill();
            weapon.DOLocalMove(Vector3.zero, .5f);
            weapon.DOLocalRotate(Vector3.zero, .5f);
        }


        public void SheathWeapon(AnimationEvent animationEvent)
        {

            weapon.SetParent(sheathedParent);
            weapon.DOKill();
            weapon.DOLocalMove(Vector3.zero, .5f);
            weapon.DOLocalRotate(Vector3.zero, .5f);
        }
    }
}