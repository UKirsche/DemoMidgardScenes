using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiCoRo.Dialog
{
    /// <summary>
    /// Schaltet Fertigkeitsfilter für Dialog an und aus und kapselt die DialogParser für NPCs
    /// </summary>
    public class DialogFilter
    {

        private DialogParser dialogParser;
        public DialogParser DialogParser
        {
            get
            {
                return dialogParser;
            }
        }


        /// <summary>
        /// Fertigkeitsfilter-Schalter
        /// </summary>
        private bool isFertigkeitsFilter;
        public bool IsFertigkeitsFilter
        {
            get { return isFertigkeitsFilter; }
            set { isFertigkeitsFilter = value; }
        }

        /// <summary>
        /// Referenz auf Dialog zum Filter
        /// </summary>
        private NPC npcDialogs;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogFertigkeitsFilter"/> class.
        /// </summary>
        public DialogFilter(NPC dialogs)
        {
            npcDialogs = dialogs;
            InitializeDialogParser();
        }


        /// <summary>
        /// Initializes the dialog parser with the first Mission. There is always min 1 Mission
        /// </summary>
        private void InitializeDialogParser()
        {
            dialogParser = new DialogParserFertigkeiten();
            dialogParser.StartNode = new DialogNode<object>();
            Mission mission = npcDialogs.missionen[0];
            dialogParser.StartNode.nodeElement = mission;
            dialogParser.StartNode.typeNodeElement = typeof(Mission);
            dialogParser.StartNode.typeParentNodeElement = null;
            dialogParser.StartNode.parentNode = null;
        }



        /// <summary>
        /// Gets the next dialog.
        /// </summary>
        /// <returns>The next dialog.</returns>
        public List<string> GetNextDialog()
        {
            List<string> returnList = null;
            bool isOption = dialogParser.IsOption;
            returnList = GetNextInfosAsString();
            if (isOption)
            {
                returnList = DialogOptionManager.FormatOptions(dialogParser);
            }
            return returnList;
        }


        /// <summary>
        /// Formats the list of infos to list of string.
        /// </summary>
        /// <returns>The infos to string.</returns>
        /// <param name="infos">Infos.</param>
        private List<string> FormatInfosToString(List<Info> infos)
        {
            List<string> infoStrings = new List<string>();
            if (infos != null)
            {
                foreach (var info in infos)
                {
                    infoStrings.Add(info.content);
                }
            }

            return infoStrings;
        }


        /// <summary>
        /// Gets the next infos: 
        /// Liefert die nächsten NPC-Infos als Liste von strings
        /// </summary>
        /// <returns>The next infos.</returns>
        private List<string> GetNextInfosAsString()
        {
            List<Info> infos = dialogParser.GetInfos();
            return FormatInfosToString(infos);
        }

        /// <summary>
        /// Späterer Aufruf zum Dialogabruf. 
        /// Unterscheidet Filtermeachnismen
        /// </summary>
        /// <returns>The dialog.</returns>
        public List<Info> FilterDialog()
        {
            if (IsFertigkeitsFilter)
            {
                DialogParserFertigkeiten dialogParserFertigkeiten = dialogParser as DialogParserFertigkeiten;
                return dialogParserFertigkeiten.GetInfosByFertigkeit();

            }
            else
            {
                return dialogParser.GetInfos();
            }

        }

    }
}