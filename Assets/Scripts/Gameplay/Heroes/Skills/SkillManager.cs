using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TPT.Core.Data.Skills;
using UnityEngine;

namespace TPT.Gameplay.Heroes.Skills
{
    public static class SkillManager
    {
        private interface ISkillFactory
        {
            ISkill CreateBase(SkillData data);
        }

        private interface ISkillFactory<TData> where TData : SkillData
        {
            Skill<TData> Create(TData data);
        }

        private sealed class SkillFactory<T, TData> : ISkillFactory<TData>, ISkillFactory
            where T : Skill<TData>, new()
            where TData : SkillData
        {
            Skill<TData> ISkillFactory<TData>.Create(TData data) => Create(data);

            ISkill ISkillFactory.CreateBase(SkillData data)
            {
                if(data is TData skillData)
                    return Create(skillData);

                return null;
            }

            private T Create(TData data)
            {
                T skill = new T();
                skill.Initialize(data);

                return skill;
            }

        }

        private static readonly Dictionary<Type, object> Factories = new();

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Type baseFactoryType = typeof(SkillFactory<,>);
            Type iSkillType = typeof(ISkill);
            Type skillData = typeof(SkillData);

            for (int i = 0; i < assemblies.Length; i++)
            {
                Type[] types = assemblies[i].GetTypes();

                for (int j = 0; j < types.Length; j++)
                {
                    Type type = types[j];
                    if(!iSkillType.IsAssignableFrom(type))
                        continue;

                    if(type.IsAbstract)
                        continue;

                    CreateSkillForAttribute attribute = type.GetCustomAttributes<CreateSkillForAttribute>().FirstOrDefault();
                    if (attribute == null)
                        continue;
                    if (!skillData.IsAssignableFrom(attribute.dataType) || Factories.ContainsKey(attribute.dataType))
                        continue;

                    Type factoryType = baseFactoryType.MakeGenericType(type, attribute.dataType);
                    Factories.Add(attribute.dataType, Activator.CreateInstance(factoryType));
                }
            }
        }

        public static Skill<T> ToSkill<T>(this T skillData) where T : SkillData =>
            TryCreateGetSkillForData(skillData, out Skill<T> skill) ? skill : null;

        public static bool TryCreateGetSkillForData(this SkillData skillData, out ISkill skill)
        {
            Type type = skillData.GetType();
            if (Factories.TryGetValue(type, out object obj) && obj is ISkillFactory factory)
            {
                skill = factory.CreateBase(skillData);
                return true;
            }

            skill = null;
            return false;
        }

        public static bool TryCreateGetSkillForData<T>(this T skillData, out Skill<T> skill) where T : SkillData
        {
            Type type = skillData.GetType();
            if (Factories.TryGetValue(type, out object obj) && obj is ISkillFactory<T> factory)
            {
                skill = factory.Create(skillData);
                return true;
            }

            skill = null;
            return false;
        }
    }
}