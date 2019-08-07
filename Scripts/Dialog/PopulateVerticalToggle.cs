using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace MiCoRo.Dialog
{
    public class PopulateVerticalToggle : PopulateVertical
    {

        public GameObject prefabDialogToggle;
        public ToggleGroup toggleGroup;
        private List<GameObject> dialogToggleElements;

        public bool SelectionDone { get; set; }


        // Use Initialization from BaseClass
        public override void Awake()
        {
            base.Awake();
            dialogToggleElements = new List<GameObject>();
            toggleGroup = GetComponent<ToggleGroup>();
            SelectionDone = false;
        }


        /// <summary>
        /// Clears the dialg text.
        /// </summary>
        public override void ClearDialogBox()
        {
            base.ClearDialogBox();
            SelectionDone = false;
            if (dialogToggleElements.Count > 0)
            {
                foreach (var item in dialogToggleElements)
                {
                    Destroy(item);
                }
            }
        }

        /// <summary>
        /// Determines whether this instance has selected toggle.
        /// </summary>
        /// <returns><c>true</c> if this instance has selected toggle; otherwise, <c>false</c>.</returns>
        public bool HasSelectedToggle()
        {
            Toggle numberToggles = toggleGroup.ActiveToggles().FirstOrDefault();
            if (numberToggles != null)
            {
                Toggle selected = toggleGroup.ActiveToggles().First();
                if (selected != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the selected toggle Id
        /// </summary>
        public int GetSelectedToggleID()
        {
            int retVal = -1;
            Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
            int max = toggles.Length;
            for (int i = 0; i < max; i++)
            {
                if (toggles[i].isOn)
                {
                    retVal = i;
                    break;
                }
            }
            return retVal;
        }

        /// <summary>
        /// Adds the dialog option to the Vertical Group
        /// </summary>
        public void addDialogOption(string dialogString)
        {
            GameObject newDialog;
            newDialog = Instantiate(prefabDialogToggle, transform); //ensures that element listed in canvas 
            Toggle newToggle = newDialog.GetComponent<Toggle>();
            toggleGroup.RegisterToggle(newToggle);
            newDialog.GetComponent<Toggle>().group = toggleGroup;
            newDialog.GetComponentInChildren<Text>().text = dialogString;
            dialogToggleElements.Add(newDialog);
        }

    }
}
