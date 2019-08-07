using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiCoRo.Dialog
{
    public class DialogDisplayManager : MonoBehaviour
    {


        private bool nextIsOption;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        /// <summary>
        /// Displayes the next Dialog
        /// </summary>
        public void DisplayNextDialog(PopulateVerticalToggle populateDialog)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag(DemoRPGMovement.PLAYER_NAME);
            PlayerDialogManager playerDialogManager = playerObject.GetComponent<PlayerDialogManager>();
            LastWasOption(populateDialog, playerDialogManager);
            nextIsOption = DialogTypeChecker.NextIsOption(playerDialogManager);
            ShowNext(playerDialogManager, populateDialog);


        }

        /// <summary>
        /// Shows the next.
        /// </summary>
        /// <param name="playerDialogManager">Player dialog manager.</param>
        /// <param name="populateDialog">Populate dialog.</param>
        private void ShowNext(PlayerDialogManager playerDialogManager, PopulateVerticalToggle populateDialog)
        {
            List<string> dialogRows = playerDialogManager.GetNextDialogPackageFromNPC();
            if (dialogRows != null && dialogRows.Count > 0)
            {
                if (nextIsOption)
                {
                    DialogDisplayManager.DisplayDialogOption(dialogRows, populateDialog);
                }
                else
                {
                    DialogDisplayManager.DisplayDialogText(dialogRows, populateDialog);
                }
            }
        }

        /// <summary>
        /// If last Dialog was Optiondialog.
        /// </summary>
        /// <param name="populateDialog">Populate dialog.</param>
        /// <param name="playerDialogManager">Player dialog manager.</param>
        private void LastWasOption(PopulateVerticalToggle populateDialog, PlayerDialogManager playerDialogManager)
        {
            bool lastWasOption = DialogTypeChecker.LastWasOption(populateDialog);
            if (lastWasOption)
            {
                int seletedIndex = populateDialog.GetSelectedToggleID();
                playerDialogManager.SetChosenOptionIndex(seletedIndex);
            }
        }

        /// <summary>
        /// Displays a simple Dialog in Rows
        /// </summary>
        /// <param name="infos">Infos.</param>
        public static void DisplayDialogText(List<string> dialogRows, PopulateVertical populateDialog)
        {
            populateDialog.ClearDialogBox();
            if (dialogRows.Count > 0)
            {
                foreach (var dialogRow in dialogRows)
                {
                    populateDialog.addDialogText(dialogRow);

                }
            }
        }

        /// <summary>
        /// Displays a Dialog Page 
        /// </summary>
        /// <param name="infos">Infos.</param>
        public static void DisplayDialogOption(List<string> dialogRows, PopulateVertical populateDialog)
        {
            PopulateVerticalToggle popVertical = populateDialog as PopulateVerticalToggle;
            popVertical.ClearDialogBox();
            if (dialogRows.Count > 0)
            {
                foreach (var dialogRow in dialogRows)
                {
                    popVertical.addDialogOption(dialogRow);

                }
            }
        }
    }
}
