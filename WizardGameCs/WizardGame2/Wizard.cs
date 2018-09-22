using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WizardGame2
{
    class Wizard : Character
    {
        public Wizard(string name) : base(name, "Wizard", 50, 100)
        {
            _Level = 1;
        }

        #region Class Variables
        // Private Variables
        /// <summary>
        /// The time in ms it takes to recover some MP while meditating.
        /// </summary>
        private int _meditationRecoveryTime = 10_000;
        /// <summary>
        /// The limit meditation time is 60s.
        /// </summary>
        public const double _meditationTimeLimit = 60_000;
        /// <summary>
        /// The MP recovered every interval of meditation.
        /// </summary>
        private double _meditationMPRecovered = 1;
        /// <summary>
        /// List of known elements.
        /// </summary>
        private List<Elements> _knownElements = new List<Elements> { };
        /// <summary>
        /// List of known spells.
        /// </summary>
        private List<Spell> _knownSpells = new List<Spell> { };
        // Protected Variables

        // Public Variables
        /// <summary>
        /// Number of known elements.
        /// </summary>
        public int elementsKnown = 0;
        /// <summary>
        /// List of known elements.
        /// </summary>
        public List<Elements> KnownElements
        {
            get { return _knownElements; }
        }
        /// <summary>
        /// List of known spells.
        /// </summary>
        public List<Spell> KnownSpells
        {
            get { return _knownSpells; }
        }
        public int recoveryTime { get { return _meditationRecoveryTime; } }
        #endregion

        #region Public Methods
        /// <summary>
        /// Casts a spell using MP.
        /// </summary>
        /// <param name="spell">The spell to cast.</param>
        /// <returns></returns>
        public string CastSpell(Spell spell)
        {
            string actionMessage;

            if (MP >= spell.Cost && CurrentStatus != CurrentStatus.Meditating)
            {
                MP -= spell.Cost;
                actionMessage = string.Format("{0} has cast {1}!", FirstName, spell.Name);
            }
            else if (CurrentStatus == CurrentStatus.Meditating)
            {
                actionMessage = "You can't cast spells while you are meditating!";
            }
            else
            {
                actionMessage = string.Format("Not enough mana to cast {0}!", spell.Name);
            }

            return actionMessage;
        }

        /// <summary>
        /// By meditating mana can be recovered over a period of time.
        /// </summary>
        /// <returns>A message about meditating.</returns>
        public string Meditate()
        {
            Thread.Sleep(_meditationRecoveryTime);
            RecoverMana(_meditationMPRecovered);

            return string.Format("{0} recovered {1} MP while meditating...\n", FirstName, _meditationMPRecovered); ;
        }
       
        /// <summary>
        /// Adds the element to the list of known elements.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <returns></returns>
        public Color LearnNewElement(Elements element)
        {
            Color elementColour;

            _knownElements.Add(element);

            switch (element)
            {
                case Elements.Fire:
                    elementColour = Color.OrangeRed;
                    break;
                case Elements.Water:
                    elementColour = Color.SkyBlue;
                    break;
                case Elements.Earth:
                    elementColour = Color.RosyBrown;
                    break;
                case Elements.Air:
                    elementColour = Color.SpringGreen;
                    break;
                case Elements.Light:
                    elementColour = Color.White;
                    break;
                case Elements.Dark:
                    elementColour = Color.Black;
                    break;
                case Elements.Time:
                    elementColour = Color.RoyalBlue;
                    break;
                case Elements.None:
                default:
                    elementColour = Color.Transparent;
                    break;
            }

            return elementColour;
        }

        public void LearnNewSpell(Spell spell)
        {
            _knownSpells.Add(spell);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Decreases the time for recovering MP.
        /// </summary>
        /// <param name="improvement">The percentage to decrease the time by.</param>
        private void ImproveRecoveryTime(double improvement)
        {
            _meditationRecoveryTime -= (int)(_meditationRecoveryTime * improvement);
        }
        /// <summary>
        /// Increases the MP recovered during meditation.
        /// </summary>
        /// <param name="improvement">The percentage to increase the recovery amount by.</param>
        private void IncreaseMeditationRecovery(double improvement)
        {
            _meditationMPRecovered += (int)(_meditationMPRecovered * improvement);
        }
        #endregion
    }
}
