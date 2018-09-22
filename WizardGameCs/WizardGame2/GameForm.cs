using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizardGame2
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();

            Player = new Wizard("Aretha Keyes");
            tbName.Text = Player.Name;
            Main();
        }

        #region Game Variables
        /// <summary>
        /// The user's wizard character.
        /// </summary>
        Wizard Player;
        #endregion

        /// <summary>
        /// Updates the game log and focuses on the last item in the log.
        /// </summary>
        /// <param name="message">The message to add to the game log.</param>
        private void UpdateGameLog(string message)
        {
            GameLog.Items.Add(message);
            GameLog.TopIndex = GameLog.Items.Count - 1;
        }

        /// <summary>
        /// Adds an element to the user's repotoir of known elements.
        /// </summary>
        /// <param name="element">What element has been learned.</param>
        private void LearnElement(Elements element)
        {
            Panel newElement;

            switch (Player.elementsKnown)
            {
                case 0:
                    newElement = element1;
                    break;
                case 1:
                    newElement = element2;
                    break;
                case 2:
                    newElement = element3;
                    break;
                case 3:
                    newElement = element4;
                    break;
                case 4:
                    newElement = element5;
                    break;
                case 5:
                    newElement = element6;
                    break;
                case 6:
                    newElement = element7;
                    break;
                case 7:
                    newElement = element8;
                    break;
                case 8:
                default:
                    newElement = null;
                    break;
            }

            if (newElement != null && !Player.KnownElements.Contains(element))
            {
                newElement.BackColor = Player.LearnNewElement(element);
                newElement.Visible = true;
                // TODO: tutorial to learn 'None' magic
                if (element != Elements.None)
                {
                    UpdateGameLog(string.Format("Congradulations! You have learned how to use the {0} element!\n", element.ToString()));
                    UpdateGameLog(string.Format("Now you can learn spells with the {0} element.\n", element.ToString()));
                }
                else
                {
                    UpdateGameLog(string.Format("Congradulations! You have learned how to use magic!\n", element.ToString()));
                    UpdateGameLog(string.Format("Now you can learn spells with no element.\n", element.ToString()));
                }
            }
            else
            {
                UpdateGameLog("Congradulations! You have descovered a new element! (Not...)");
            }
        }

        /// <summary>
        /// Adds a spell to the user's repotoir of known spells.
        /// </summary>
        /// <param name="spell">The spell that was learned.</param>
        private void LearnSpell(Spell spell)
        {
            if (!Player.KnownSpells.Contains(spell))
            {
                if (Player.KnownElements.Contains(spell.Element))
                {
                    Player.LearnNewSpell(spell);
                    lbSpellBook.Items.Add(spell.Name);
                    // TODO: add tutorial to learn small light
                    UpdateGameLog(string.Format("Congradulations! You have learned {0}!", spell.Name));
                }
                else
                {
                    UpdateGameLog(string.Format("{0} is unable to learn {1} spells.", Player.FirstName, spell.Element));
                }
            }
        }

        // The region where of all spells in the game are defined.
        #region Spell Library
        #region No Element Spells
        Spell smallLight = new Spell(spellName: "Small Light", spellCost: 5, spellDamage: 0, element: Elements.None, description: "A ball of light.");

        #endregion

        #region Fire Spells
        Spell fireball = new Spell(spellName: "Fireball", spellCost: 10, spellDamage: 5, element: Elements.Fire, description: "It was discovered that by adding the fire element to Small Light, a fire ball could be produced.");

        #endregion

        #region Water Spells
        Spell waterball = new Spell(spellName: "Waterball", spellCost: 10, spellDamage: 5, element: Elements.Water, description: "It was discovered that by adding the water element to Small Light, a water ball could be produced.");

        #endregion

        #region Earth Spells
        Spell earthball = new Spell(spellName: "Earthball", spellCost: 10, spellDamage: 5, element: Elements.Earth, description: "It was discovered that by adding the earth element to Small Light, a earth ball could be produced.");

        #endregion

        #region Air Spells
        Spell airball = new Spell(spellName: "Airball", spellCost: 10, spellDamage: 5, element: Elements.Air, description: "It was discovered that by adding the air element to Small Light, a air ball could be produced.");

        #endregion

        #region Light Spells
        Spell lightball = new Spell(spellName: "Lightball", spellCost: 10, spellDamage: 50, element: Elements.Light, description: "It was discovered that by adding the light element to Small Light, a light ball with healing properties could be produced.");

        #endregion

        #region Dark Spells
        Spell darkball = new Spell(spellName: "Darkball", spellCost: 10, spellDamage: 50, element: Elements.Dark, description: "It was discovered that by adding the dark element to Small Light, a dark ball could be produced.");

        #endregion

        #region Time Spells
        Spell timeball = new Spell(spellName: "Timeball", spellCost: 10_000, spellDamage: 0, element: Elements.Time, description: "It was discovered that by adding the time element to Small Light, a ball of time could be produced. It has no obvious use though...");

        #endregion

        #endregion

        #region Control Methods
        /// <summary>
        /// Updates the spell description box with the selected spell's details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectSpell(object sender, EventArgs e)
        {
            ListBox selectedSpell = (ListBox)sender;
            string spellDescription = "No Spell Selected";

            if (selectedSpell.SelectedItem != null)
            {
                foreach (Spell spell in Player.KnownSpells)
                {
                    if (spell.Name == selectedSpell.SelectedItem.ToString())
                    {
                        spellDescription = spell.Description;
                        break;
                    }
                }
            }

            rtbSpellDescription.Text = spellDescription;
        }

        /// <summary>
        /// Casts the selected spell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CastSpell_Click(object sender, EventArgs e)
        {
            if (lbSpellBook.SelectedItem != null)
            {
                foreach (Spell spell in Player.KnownSpells)
                {
                    if (spell.Name == lbSpellBook.SelectedItem.ToString())
                    {
                        UpdateGameLog(Player.CastSpell(spell));
                        mpBar.Value = (int)(Player.MP / Player.MaxMP * 100);
                        break;
                    }
                }
            }
            else
            {
                UpdateGameLog("No spell was selected...\n");
            }
        }

        /// <summary>
        /// Starts meditation to recover mana.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Meditate_Click(object sender, EventArgs e)
        {
            UpdateGameLog("Begining meditation...");
            Player.CurrentStatus = CurrentStatus.Meditating;
            btnMeditate.Enabled = false;
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                string actionUpdate;
                int elapsed = 0;

                while (elapsed < Wizard._meditationTimeLimit && Player.MP < Player.MaxMP)
                {
                    actionUpdate = Player.Meditate();
                    Invoke((MethodInvoker)delegate
                    {
                        mpBar.Value = (int)(Player.MP / Player.MaxMP * 100);
                        UpdateGameLog(actionUpdate);
                    });
                    elapsed += Player.recoveryTime;
                }

                Invoke((MethodInvoker)delegate
                {
                    UpdateGameLog("Meditation has finished!");
                    Player.CurrentStatus = CurrentStatus.Normal;
                    btnMeditate.Enabled = true;
                });
            }).Start();
        }

        #endregion

        /// <summary>
        /// The method that runs the game.
        /// </summary>
        void Main()
        {
            UpdateGameLog(string.Format("Welcome, {0}!", Player.FirstName));
            // TODO: Game Intro/Tutorial Stuff
            LearnElement(Elements.None);
            LearnSpell(smallLight);
        }


        void SaveGame()
        {
            List<string> gameLog = new List<string> { };
            pbSaveProgress.Maximum = GameLog.Items.Count + 1;
            pbSaveProgress.Visible = true;

            foreach (object log in GameLog.Items)
            {
                gameLog.Add(log.ToString());
                pbSaveProgress.PerformStep();
                Refresh();
            }

            if (!File.Exists(string.Format("{0}\\SavedGames\\{1}.txt", Environment.CurrentDirectory, Player.Name)))
            {
                File.AppendAllText(string.Format("{0}\\SavedGames\\Saved Games.txt", Environment.CurrentDirectory), string.Format("{0}\n", Player.Name));
            }
            File.AppendAllLines(string.Format("{0}\\SavedGames\\{1}.txt", Environment.CurrentDirectory, Player.Name), gameLog.ToArray()); // TODO: include SavedGames in build

            pbSaveProgress.PerformStep();
            MessageBox.Show("Your game has been saved.");
            pbSaveProgress.Visible = false;
            GameLog.Items.Clear();  // TODO: should the game log get cleared after saving the game?
                                    // This would be so that if the player keeps playing, and saves the game again
                                    // it would only copy the new things into the file.
                                    // Could also investigate how to save only the "new" logged items.
        }

        private void btnSaveGame_Click(object sender, EventArgs e)
        {
            SaveGame();
        }

        private void btnChangeCharacterName_Click(object sender, EventArgs e)
        {
            if (tbNewName.Text != "Enter New Name Here")
            {
                string actionMessage = Player.ChangeCharacterName(tbNewName.Text);
                if (actionMessage != string.Empty) { UpdateGameLog(actionMessage); }

                tbName.Text = Player.Name;
                tbNewName.Text = "Enter New Name Here";
            }
            else
            {
                MessageBox.Show("Please enter your new name in the text box.", "Change Character Name");
            }
        }

        private void tbNewName_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = Player.Name;
            textBox.SelectAll();
        }

        private void tbNewName_Leave(object sender, EventArgs e)
        {
            tbNewName.Text = "Enter New Name Here";
            tbNewName.ForeColor = Color.DarkGray;
        }

        private void tbNewName_Click(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.ForeColor == Color.DarkGray)
            {
                textBox.SelectAll();
                textBox.ForeColor = Color.Black;
            }
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            // TODO: figure out how to read the saved games file!
            // for some reason the saved games file doesn't seem to be getting updated...
            string[] savedGames = File.ReadAllLines(string.Format("{0}\\SavedGames\\Saved Games.txt", Environment.CurrentDirectory));
            string gameToLoad;

            if (savedGames.Length == 2)
            {
                gameToLoad = savedGames[0];
            }
            else
            {
                MessageBox.Show("Which game would you like to load?");
                gameToLoad = "smile";
            }

            MessageBox.Show(string.Format("{0}\'s game was loaded.", gameToLoad));
        }
    }
}
