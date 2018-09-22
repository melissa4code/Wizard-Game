using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizardGame2
{
    /// <summary>
    /// The structure that defines spells.
    /// </summary>
    public struct Spell
    {
        /// <summary>
        /// The name of the spell.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// The description of the spell.
        /// </summary>
        public readonly string Description;
        /// <summary>
        /// The spell's element.
        /// </summary>
        public readonly Elements Element;
        /// <summary>
        /// The cost of the spell.
        /// </summary>
        public readonly double Cost;
        /// <summary>
        /// The damage that the spell causes.
        /// </summary>
        public readonly double Damage;

        /// <summary>
        /// The structure that defines spells.
        /// </summary>
        /// <param name="spellName">The name of the spell.</param>
        /// <param name="spellCost">The mana cost of the spell.</param>
        /// <param name="spellDamage">The damage that the spell causes.</param>
        /// <param name="element">The spell's element.</param>
        /// <param name="description">The description of the spell.</param>
        public Spell(string spellName, double spellCost, double spellDamage, Elements element, string description)
        {
            Name = spellName;
            Description = string.Format("Element: {0}\nCost: {1} MP\n\n{2}", element, spellCost, description);
            Element = element;
            Cost = spellCost;
            Damage = spellDamage;
        }
    }

    /// <summary>
    /// The structure that defines potions.
    /// </summary>
    public struct Potion
    {
        /// <summary>
        /// The name of the potion.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// The description of the potion.
        /// </summary>
        public readonly string Description;
        /// <summary>
        /// The type of potion.
        /// </summary>
        public readonly PotionType Type;
        /// <summary>
        /// The status adjustment made by the potion.
        /// </summary>
        public readonly double StatAdjustment;
        /// <summary>
        /// The damage the potion does.
        /// </summary>
        public readonly double Damage;

        /// <summary>
        /// The structure that defines potions.
        /// </summary>
        /// <param name="potionName">The name of the potion.</param>
        /// <param name="potionType">The type of status potion.</param>
        /// <param name="statAdjustment">The status asjustment applied.</param>
        /// <param name="description">The description of the potion.</param>
        /// <param name="damage">The damaged caused by the potion.</param>
        public Potion(string potionName, PotionType potionType, double statAdjustment, string description, double damage = 0)
        {
            Name = potionName;
            Description = description;
            Type = potionType;
            StatAdjustment = statAdjustment;
            Damage = damage;
        }
        /// <summary>
        /// The structure that defines potions.
        /// </summary>
        /// <param name="potionName">The name of the potion.</param>
        /// <param name="potionType">The type of status potion.</param>
        /// <param name="damage">The damaged caused by the potion.</param>
        /// <param name="statAdjustment">The status asjustment applied.</param>
        /// <param name="description">The description of the potion.</param>
        public Potion(string potionName, PotionType potionType, double damage, double statAdjustment, string description)
        {
            Name = potionName;
            Description = description;
            Type = potionType;
            StatAdjustment = statAdjustment;
            Damage = damage;
        }
    }

    /// <summary>
    /// The eight elements that define all spells,
    /// including no element.
    /// </summary>
    public enum Elements
    {
        Fire,
        Water,
        Earth,
        Air,
        Light,
        Dark,
        Time,
        None
    }

    /// <summary>
    /// The five types of potions available.
    /// </summary>
    public enum PotionType
    {
        HP,
        MP,
        Both,
        Poison,
        Paralysis
    }

    public enum CurrentStatus
    {
        Meditating,
        Sleeping,
        Poisoned,
        Paralyzed,
        Normal
    }
}