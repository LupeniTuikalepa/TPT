using System;
using TPT.Gameplay.Level;
using Unity.Cinemachine;
using UnityEngine;

namespace TPT.Gameplay.Heroes.Cameras
{
    public class HeroCameraGroup : MonoBehaviour
    {
        [SerializeField]
        private CinemachineStateDrivenCamera stateDrivenCamera;

        private Hero hero;

        private void Awake()
        {
            hero = GetComponentInParent<Hero>();
            OnHeroTurnEnded();
        }

        private void OnEnable()
        {
            hero.OnTurnStarted += OnHeroTurnStarted;
            hero.OnTurnEnded += OnHeroTurnEnded;
            hero.OnHeroSpawn += OnHeroSpawn;
        }

        private void OnDisable()
        {
            hero.OnTurnStarted -= OnHeroTurnStarted;
            hero.OnTurnEnded -= OnHeroTurnEnded;
            hero.OnHeroSpawn -= OnHeroSpawn;
        }

        private void OnHeroSpawn(Hero h)
        {
            stateDrivenCamera.LookAt = LevelManager.Instance.GetOtherPlayer(h.Player).TargetGroup.transform;
        }

        private void OnHeroTurnStarted()
        {
            stateDrivenCamera.gameObject.SetActive(true);
        }

        private void OnHeroTurnEnded()
        {
            stateDrivenCamera.gameObject.SetActive(false);
        }
    }
}