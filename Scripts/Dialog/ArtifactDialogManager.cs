using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiCoRo.Dialog
{
    /// <summary>
    /// Lädt die Informationen zu einem NPC bzw. Artifact mit dem passenden Namen aus dem übergeordneten GO (Capsule bzw. NPC)
    /// Der <see cref="artifactName"/> wird aus dem GO ausgelesen und muss mit der Bezeichnung in der xml-Ressource übereinstimmen.
    /// <see cref="LoadDialog"/> lädt das <see cref="Artifact"/> aus der  Resource.
    /// </summary>
    /// <remarks>
    /// Die Einbindung in die Game Scene in Unity erfolgt über die "Infopoints", die in der Scene gesetzt werden.
    /// Nähere hierzu findet sich im Manual [](xref:anbindung.md). Klasses ist Basisklasse für NPC-bezogene Dialogverwaltung.
    /// </remarks>
    public class ArtifactDialogManager : MonoBehaviour
    {

        /// <summary>
        /// Der Name bezieht sich auf den Namen des Artifacts, der auch in der Resourcen XML angelegt ist.
        /// </summary>
        public string artifactName;
        protected Artifact artifactDialog;
        protected StandardNPCInfos standardInfos;


        /// <summary>
        /// Setzt den <see cref="artifactName"/> aus GO, initialisiert die <see cref="StandardNPCInfos"/> mit dem mit demselben
        /// Namen und lädt die relevanten Dialog aus dem XML
        /// </summary>
        public virtual void Start()
        {
            artifactName = gameObject.name;
            standardInfos = new StandardNPCInfos(artifactName);
            LoadDialog();
        }

        /// <summary>
        /// Lädt den relevanten Dialog bzw. die Infos über die <see cref="DialogLoader"/> aus dem Scripts-Objekt
        /// </summary>
        protected virtual void LoadDialog()
        {
            GameObject scripts = GameObject.Find("Scripts");
            DialogLoader dialogLoader = scripts.GetComponent<DialogLoader>();
            artifactDialog = dialogLoader.GetDialog<Artifact>(name) as Artifact;
        }

        /// <summary>
        /// Gibt Information, ob ein Artifact alle Infos geliefert hat
        /// </summary>
        /// <returns>ja/nein</returns>
        protected bool HasInfos()
        {
            bool retVal = (artifactDialog != null && artifactDialog.infopakete.Count > 0) ? true : false;
            return retVal;
        }

        /// <summary>
        /// Liefert die Verabschiedungsklausel aus der <see cref="StandardNPCInfos"/>
        /// </summary>
        /// <remarks>
        /// Abschiedsklausel ist immer neutral und nicht personenbezogen, deswegen hier verwendbar
        /// </remarks>
        public virtual List<string> GetStandardInfo()
        {
            return standardInfos.StandardInfoFinish;
        }

        /// <summary>
        /// Holt das nächste Infopaket und löscht es aus der Liste.
        /// Altes Interface für einfache Infopakete.
        /// </summary>
        /// <returns>
        /// List der Infos aus einem Paket
        /// </returns>
        public virtual List<string> GetNextInfos()
        {
            Infopaket infPackage = null;
            List<string> infoStrings = new List<string>();
            //get first element of the list
            if (HasInfos())
            {
                infPackage = artifactDialog.infopakete[0];
                artifactDialog.infopakete.Remove(infPackage);
                List<Info> infos = infPackage.infos;
                if (infos != null)
                {
                    foreach (var info in infos)
                    {
                        infoStrings.Add(info.content);
                    }
                }
            }

            return infoStrings;
        }
    }

}
