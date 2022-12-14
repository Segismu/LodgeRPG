using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Utils;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int baseLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleVFX = null;
        [SerializeField] bool usingMods = false;

        public event Action onLevelUp;

        LazyValue<int> currentLevel;

        Experience experience;

        private void Awake()
        {
            experience = GetComponent<Experience>();
            currentLevel = new LazyValue<int>(CalculateLevel);  
        }

        private void Start()
        {
            currentLevel.ForceInit();
        }

        private void OnEnable()
        {
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void OnDisable()
        {
            if (experience != null)
            {
                experience.onExperienceGained -= UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                LevelUpVFX();
                onLevelUp();
            }
        }

        private void LevelUpVFX()
        {
            Instantiate(levelUpParticleVFX, transform);
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveMod(stat)) * (1 + GetPercertageMod(stat)/100);
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            return currentLevel.value;
        }


        private float GetAdditiveMod(Stat stat)
        {
            if (!usingMods) return 0;

            float total = 0;
            foreach (IModProvider provider in GetComponents<IModProvider>())
            {
                foreach (float modifier in provider.GetAdditiveMod(stat))
                {
                    total += modifier;
                }

            }
            return total;
        }

        private float GetPercertageMod(Stat stat)
        {
            if (!usingMods) return 0;

            float total = 0;
            foreach (IModProvider provider in GetComponents<IModProvider>())
            {
                foreach (float modifier in provider.GetPercentageMod(stat))
                {
                    total += modifier;
                }

            }
            return total;
        }

        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return baseLevel;

            float currentXP = experience.GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);

            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }
    }
}