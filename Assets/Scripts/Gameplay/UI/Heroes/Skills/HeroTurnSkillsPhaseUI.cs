using System.Collections.Generic;
using Helteix.Tools;
using TPT.Gameplay.Heroes;
using TPT.Gameplay.Heroes.Skills;
using TPT.Gameplay.Level;
using TPT.Gameplay.Selection;
using UnityEngine;

namespace TPT.Gameplay.UI.Heroes.Skills
{
    public class HeroTurnSkillsPhaseUI : HeroTurnPhaseUI<HeroSkillPhase>
    {
        [SerializeField]
        private HeroSkillUI skillUIPrefab;

        [SerializeField]
        private Transform root;

        private Dictionary<HeroSkillUI, ISkill> skills = new();


        protected override void OnOpen(HeroSkillPhase phase)
        {
            Hero hero = phase.hero;
            root.ClearChildren();
            skills.Clear();

            foreach (var skill in hero.Skills)
            {
                HeroSkillUI instance = Instantiate(skillUIPrefab, root);
                instance.Sync(skill, this);

                skills.Add(instance, skill);
            }
        }

        protected override void OnClose(HeroSkillPhase phase)
        {
            root.ClearChildren();
        }

        public void Select(HeroSkillUI heroSkillUI)
        {
            if (skills.TryGetValue(heroSkillUI, out ISkill skill))
            {
                HeroSelection.Begin(new HeroSelectionContext()
                {
                    hero = controller.Hero,
                    targetType = skill.TargetType,
                    targetTeam = skill.TargetTeam,
                },
                list =>
                {
                    SkillUsageContext context = new SkillUsageContext()
                    {
                        from = controller.Hero,
                        targets = list,
                    };

                    skill.Use(context);
                    controller.StopCurrentPhase(true);

                    LevelManager.Instance.MoveToNextTurn();
                },
                () =>
                {
                    controller.StopCurrentPhase(false);
                });
            }
        }


    }
}