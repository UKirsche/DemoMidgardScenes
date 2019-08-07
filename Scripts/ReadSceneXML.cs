using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;



#region NPC

public interface IModifier{
	int modifier { get; set; }
}

public interface ICheckType{
	string checkType { get; set; }
}

public interface IFertigkeitsCheck: IID, IModifier, ICheckType
{
}

public class Artifacts
{
	[XmlElement("Artifact")]
	public List<Artifact> artifactListe = new List<Artifact>();
}

public class Artifact : IFertigkeitsCheck
{
	[XmlAttribute("id")]
	public int id { get; set; }

	[XmlAttribute("check")]
	public string checkType { get; set; }

	[XmlAttribute("modifier")]
	public int modifier { get; set; }

	[XmlAttribute("name")]
	public string name { get; set; }

	[XmlElement("Infopaket")]
	public List<Infopaket> infopakete = new List<Infopaket> ();

}

public class NPCS
{
	[XmlElement("NPC")]
	public List<NPC> npcListe = new List<NPC>();
}

public class NPC : Artifact
{
	[XmlElement("Mission")]
	public List<Mission> missionen = new List<Mission> ();
}

public class Mission : IFertigkeitsCheck
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("modifier")]
	public int modifier { get; set; }
	[XmlAttribute("check")]
	public string checkType { get; set; }
	[XmlElement("Infopaket")]
	public List<Infopaket> infopakete = new List<Infopaket> ();
}


public class Infopaket : IFertigkeitsCheck
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("modifier")]
	public int modifier { get; set; }
	[XmlAttribute("check")]
	public string checkType { get; set; }
	[XmlElement("Info")]
	public List<Info> infos = new List<Info>();
	[XmlElement("Optionspaket")]
	public Optionspaket optionspaket { get; set;}
}


public class Optionspaket :IFertigkeitsCheck
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("modifier")]
	public int modifier { get; set; }
	[XmlAttribute("check")]
	public string checkType { get; set; }
	[XmlElement("Option")]
	public List<Option> optionen = new List<Option> ();
}

public class Option : IFertigkeitsCheck
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("modifier")]
	public int modifier { get; set; }
	[XmlAttribute("check")]
	public string checkType { get; set; }
	[XmlElement("Beschreibung")]
	public string Beschreibung{ get; set;}
	[XmlElement("Infopaket")]
	public List<Infopaket> infopakete = new List<Infopaket> ();
}


public class Info : IFertigkeitsCheck
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("modifier")]
	public int modifier { get; set; }
	[XmlAttribute("check")]
	public string checkType { get; set; }
	[XmlText]
	public string content { get; set;}
}
#endregion

/// <summary>
/// Liest die Resourcen-Dateien aus für
/// 1. NPCs
/// 2. Artifacts
/// 3. Tests
///
/// Vorsicht: beim Erstellen des Projekts für einen Release muss der Pfad für den StreamReader angepasst werden
/// </summary>
public class SceneResourceReader
{
	public static string MidgardNPC = "npc.xml";
	public static string MidgardArtficats = "artifact.xml";
	public static string MidgardNPCTest = "npctest.xml";
	public static string MidgardNPCTest2 = "npctest2.xml";

	/// Holt die als XML gespeichert Resource in Objekt
	/// </summary>
	/// <returns>The midgard resource.</returns>
	/// <param name="fileName">File name.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T GetMidgardResource<T>(string fileName) where T:class{
		XmlSerializer deserializerResource = new XmlSerializer(typeof(T));
		//TextReader SceneXMLReader = new StreamReader(@"./" + fileName);
		TextReader SceneXMLReader = new StreamReader(@"/Users/Shared/Unity/Midgard/Assets/Photon Unity Networking/Demos/DemoWorker/Resources/"+ fileName);
		T listResource = deserializerResource.Deserialize(SceneXMLReader) as T;
		SceneXMLReader.Close();
		return listResource;
	}
}