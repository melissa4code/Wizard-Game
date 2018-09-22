using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizardGame2
{
    class Character
    {
        public Character(string name, string job, double hp, double mp)
        {
            _Name = name;
            _Job = job;
            _MaxHP = hp;
            HP = hp;
            _MaxMP = mp;
            MP = mp;
        }

        #region Class Variables
        // Private Variables

        // Protected Variables
        /// <summary>
        /// The character's name.
        /// </summary>
        protected string _Name;
        /// <summary>
        /// The character's job/role.
        /// </summary>
        protected string _Job;
        /// <summary>
        /// The character's maximum HP value.
        /// </summary>
        protected double _MaxHP;
        /// <summary>
        /// The character's maximum MP value.
        /// </summary>
        protected double _MaxMP;
        /// <summary>
        /// The character's current level.
        /// </summary>
        protected int _Level = 0;

        // Public Variables
        /// <summary>
        /// The character's name.
        /// </summary>
        public string Name
        {
            get { return _Name; }
        }
        /// <summary>
        /// The character's first name.
        /// </summary>
        public string FirstName
        {
            get { return _Name.Split(' ')[0]; }
        }
        /// <summary>
        /// The character's job/role.
        /// </summary>
        public string Job
        {
            get { return _Job; }
        }
        /// <summary>
        /// The character's maximum HP value.
        /// </summary>
        public double MaxHP
        {
            get { return _MaxHP; }
        }
        /// <summary>
        /// The character's maximum MP value.
        /// </summary>
        public double MaxMP
        {
            get { return _MaxMP; }
        }
        /// <summary>
        /// The character's current level.
        /// </summary>
        public int Level
        {
            get { return _Level; }
        }
        /// <summary>
        /// The character's current HP.
        /// </summary>
        public double HP { get; protected set; }
        /// <summary>
        /// The character's current MP.
        /// </summary>
        public double MP { get; protected set; }
        /// <summary>
        /// The character's current status.
        /// </summary>
        public CurrentStatus CurrentStatus = CurrentStatus.Normal;
        #endregion

        #region Public Methods
        /// <summary>
        /// Changes the character's name.
        /// </summary>
        /// <remarks>
        /// This method will query the user if they would really like to change their character's name.
        /// If they select 'Yes' their name will be changed and a confirmation message will be displayed.
        /// If they select 'No' their name will not be changed.
        /// </remarks>
        /// <param name="name">The name they wish to change to.</param>
        public string ChangeCharacterName(string name)
        {
            DialogResult results = MessageBox.Show(string.Format("Would you really like to change your character's name to {0}?", name), "Change Name", MessageBoxButtons.YesNo);

            if (results == DialogResult.Yes)
            {
                _Name = name;
                return string.Format("Your name was changed to {0}!\n", name);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// What happens when the character drinks a potion.
        /// </summary>
        /// <param name="potion">What potion the character drank.</param>
        /// <returns>The results of drinking the potion.</returns>
        public string DrinkPotion(Potion potion)
        {
            string actionMessage;

            switch (potion.Type)
            {
                case PotionType.HP:
                    actionMessage = RecoverHealth(potion.StatAdjustment);
                    break;
                case PotionType.MP:
                    actionMessage = RecoverMana(potion.StatAdjustment);
                    break;
                case PotionType.Both:
                    actionMessage = RecoverHealth(potion.StatAdjustment);
                    actionMessage += RecoverMana(potion.StatAdjustment);
                    break;
                case PotionType.Poison:
                case PotionType.Paralysis:
                    // TODO: create a poisoned method
                    actionMessage = "Oh no! You drank Poison!!!\n";
                    break;
                default:
                    actionMessage = "Are you sure you can drink this potion!!\n";
                    break;
            }

            return actionMessage;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Attempts to restore the current HP to the max HP by recovering a certain amount of HP.
        /// </summary>
        /// <param name="recovered">How much HP was recovered.</param>
        /// <returns></returns>
        protected string RecoverHealth(double recovered)
        {
            // If the HP recovered goes over the maximum HP, reduce how much HP is recovered
            if (HP + recovered > _MaxHP)
            {
                recovered = _MaxHP - HP;
            }

            if (recovered != 0)
            {
                HP += recovered;
                return string.Format("{0} recovered {1} HP!\n", FirstName, recovered);
            }
            else
            {
                return "No health was recovered...\n";
            }
        }

        /// <summary>
        /// Attempts to restore the current MP to the max MP by recovering a certain amount of MP.
        /// </summary>
        /// <param name="recovered">How much MP was recovered.</param>
        /// <returns></returns>
        protected string RecoverMana(double recovered)
        {
            // If the HP recovered goes over the maximum HP, reduce how much HP is recovered
            if (MP + recovered > _MaxMP)
            {
                recovered = _MaxMP - MP;
            }

            if (recovered != 0)
            {
                MP += recovered;
                return string.Format("{0} recovered {1} MP!\n", FirstName, recovered);
            }
            else
            {
                return "No mana was recovered...\n";
            }
        }
        #endregion
    }
}
