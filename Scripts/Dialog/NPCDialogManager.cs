using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiCoRo.Dialog;

namespace MiCoRo.Dialog
{
    public class NPCDialogManager : ArtifactDialogManager
    {

        private NPC npcDialogs;
        private DialogFilter dialogFilter;

        private bool wasInformand;


        // Use this for initialization
        public override void Start()
        {
            artifactName = gameObject.name;
            LoadDialog();
            InitializeDialogFilter(true);
            artifactDialog = npcDialogs; //upcast
            wasInformand = HasMissions() || HasInfos();

        }

        /// <summary>
        /// Überschreibt die Methode
        /// </summary>
        protected override void LoadDialog()
        {
            GameObject scripts = GameObject.Find("Scripts");
            DialogLoader dialogLoader = scripts.GetComponent<DialogLoader>();
            standardInfos = new StandardNPCInfos(artifactName);
            npcDialogs = dialogLoader.GetDialog<NPC>(artifactName) as NPC;
        }

        /// <summary>
        /// Loads the dialog filter. Responsible für Filtering and returning dialogs
        /// </summary>
        /// <param name="isFilterOn">If set to <c>true</c> is filter on.</param>
        private void InitializeDialogFilter(bool isFilterOn)
        {
            dialogFilter = new DialogFilter(npcDialogs);
            dialogFilter.IsFertigkeitsFilter = isFilterOn;
        }


        protected bool HasMissions()
        {
            bool retVal = (npcDialogs != null && npcDialogs.missionen.Count > 0) ? true : false;
            return retVal;
        }

        /// <summary>
        /// Gets the next info package from Dialogpack for NPC and removes it from the list
        /// </summary>
        public List<string> GetNextDialog()
        {
            return dialogFilter.GetNextDialog();
        }

        /// <summary>
        /// Sets the index of the chosen option.
        /// </summary>
        /// <param name="index">Index.</param>
        public void SetChosenOptionIndex(int index)
        {
            DialogOptionManager.SetChosenOptionIndex(index, dialogFilter.DialogParser);
        }

        /// <summary>
        /// Ermittelt, ob der nächste Dialog ein Optionsdialog ist
        /// </summary>
        /// <returns><c>true</c>, if dialog option was nexted, <c>false</c> otherwise.</returns>
        public bool NextDialogOption()
        {
            return DialogOptionManager.NextDialogOption(dialogFilter.DialogParser);
        }


        /// <summary>
        /// Get Standard Info for a Character
        /// </summary>
        public override List<string> GetStandardInfo()
        {
            if (HasMissions())
            {
                return standardInfos.StandardInfoTalker;
            }
            else
            {
                if (wasInformand)
                {
                    return standardInfos.StandardInfoFinish;
                }
                return standardInfos.StandardInfoName;
            }
        }

    }
}