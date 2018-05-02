using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

#region NPC
public class NPCS
{
	[XmlElement("NPC")]
	public List<NPC> npcListe = new List<NPC>();
}

public class NPC 
{
	[XmlAttribute("name")]
	public string name { get; set; }
	[XmlElement("Infopaket")]
	public List<Infopaket> infopakete = new List<Infopaket> ();
	[XmlElement("Mission")]
	public List<Mission> missionen = new List<Mission> ();

}

public class Mission
{
	[XmlElement("Infopaket")]
	public List<Infopaket> infopakete = new List<Infopaket> ();
}


public class Infopaket
{
	[XmlElement("Info")]
	public List<Info> infos = new List<Info>();
	[XmlElement("Optionspaket")]
	public Optionspaket optionspaket { get; set;}
}


public class Optionspaket
{
	[XmlElement("Option")]
	public List<Option> optionen = new List<Option> ();
}

public class Option
{
	[XmlElement("Beschreibung")]
	public string Beschreibung{ get; set;}
	[XmlElement("Infopaket")]
	public List<Infopaket> infopakete = new List<Infopaket> ();
}


public class Info
{
	[XmlText]
	public string content { get; set;}
}
#endregion


public class SceneResourceReader
{
	public static string MidgardNPC = "npc.xml";
	public static string MidgardNPCTest = "npctest.xml";
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