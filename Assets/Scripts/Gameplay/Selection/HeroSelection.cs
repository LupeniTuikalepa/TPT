using System;
using System.Collections.Generic;
using TPT.Core.Data.Skills;
using TPT.Gameplay.Heroes;
using TPT.Gameplay.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

namespace TPT.Gameplay.Selection
{
    public class HeroSelection
    {
        public static Action<HeroSelection> OnSelectionBegin;
        public static Action<HeroSelection> OnSelectionEnds;


        public static void Begin(HeroSelectionContext context, IHeroSelectionCallbackProvider provider)
            => Begin(context, provider.OnComplete, provider.OnFail);

        public static void Begin(HeroSelectionContext context, Action<List<Hero>> onComplete, Action onFail)
        {
            var candidates = new List<Hero>();
            FillCandidates(context, candidates);

            if (context.targetType == TargetType.All)
            {
                if (candidates.Count > 0)
                    onComplete?.Invoke(candidates);
                else
                    onFail?.Invoke();
                return;
            }

            if (candidates.Count == 1)
            {
                onComplete?.Invoke(candidates);
                return;
            }

            HeroSelection selection = new HeroSelection()
            {
                Context = context,
                Candidates = candidates,
                onComplete = onComplete,
                onFail = onFail
            };

            _ = selection.PerformSelection();
        }


        private static void FillCandidates(HeroSelectionContext context, List<Hero> heroes)
        {
            PlayerController heroPlayer = context.hero.Player;
            PlayerController opponentPlayer = LevelManager.Instance.GetOtherPlayer(heroPlayer);

            if (context.targetTeam.HasFlagFast(TargetTeam.Allies))
            {
                foreach (Hero hero in heroPlayer.Heroes)
                {
                    if (hero != context.hero)
                    {
                        heroes.Add(hero);
                    }
                }
            }

            if (context.targetTeam.HasFlagFast(TargetTeam.Enemies))
            {
                foreach (Hero hero in opponentPlayer.Heroes)
                {
                    if (hero != context.hero)
                    {
                        heroes.Add(hero);
                    }
                }
            }

            if(context.targetTeam.HasFlagFast(TargetTeam.Self))
                heroes.Add(context.hero);
        }

        private Action onFail;
        private Action<List<Hero>> onComplete;

        public event Action<Hero> OnHeroSelected;
        public HeroSelectionContext Context { get; private set; }
        public List<Hero> Candidates { get; private set; }

        public Hero Selection { get; private set; }
        public bool IsDone { get; private set; }

        private async Awaitable PerformSelection()
        {
            OnSelectionBegin?.Invoke(this);

            while (!IsDone)
                await Awaitable.NextFrameAsync();

            if (Selection == null)
            {
                onFail?.Invoke();
            }
            else
            {
                using (ListPool<Hero>.Get(out var heroes))
                {
                    heroes.Add(Selection);
                    onComplete?.Invoke(heroes);
                }
            }

            OnSelectionEnds?.Invoke(this);
        }

        public void Select(Hero hero)
        {
            Selection = hero;
            OnHeroSelected?.Invoke(hero);
        }

        public void Cancel()
        {
            Select(null);
            Validate();
        }

        public void Validate()
        {
            IsDone = true;
        }
    }
}