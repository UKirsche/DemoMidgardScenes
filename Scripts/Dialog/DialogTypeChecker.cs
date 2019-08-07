using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiCoRo.Dialog
{
    public class DialogTypeChecker
    {

        /// <summary>
        /// Checks whether next dialog from NPC is Optiondialog
        /// </summary>
        /// <returns><c>true</c>, if is option was nexted, <c>false</c> otherwise.</returns>
        /// <param name="playerDialogManager">Player dialog manager.</param>
        public static bool NextIsOption(PlayerDialogManager playerDialogManager)
        {
            return playerDialogManager.IsNextDialogOptionFromNPC();
        }


        /// <summary>
        /// Lasts the was option.
        /// </summary>
        /// <returns><c>true</c>, if was option was lasted, <c>false</c> otherwise.</returns>
        /// <param name="popualteVertical">Popualte vertical.</param>
        public static bool LastWasOption(PopulateVerticalToggle popualteVertical)
        {
            return popualteVertical.HasSelectedToggle();
        }

    }
}
