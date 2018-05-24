﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

#region NPC

public class Artifacts
{
	[XmlElement("Artifact")]
	public List<Artifact> artifactListe = new List<Artifact>();
}

public class Artifact 
{
	[XmlAttribute("id")]
	public int id { get; set; }

	[XmlAttribute("modifier")]
	public string modifier { get; set; }

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

public class Mission
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("modifier")]
	public string modifier { get; set; }
	[XmlElement("Infopaket")]
	public List<Infopaket> infopakete = new List<Infopaket> ();
}


public class Infopaket
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("modifier")]
	public string modifier { get; set; }
	[XmlElement("Info")]
	public List<Info> infos = new List<Info>();
	[XmlElement("Optionspaket")]
	public Optionspaket optionspaket { get; set;}
}


public class Optionspaket
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("modifier")]
	public string modifier { get; set; }
	[XmlElement("Option")]
	public List<Option> optionen = new List<Option> ();
}

public class Option
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("modifier")]
	public string modifier { get; set; }
	[XmlElement("Beschreibung")]
	public string Beschreibung{ get; set;}
	[XmlElement("Infopaket")]
	public List<Infopaket> infopakete = new List<Infopaket> ();
}


public class Info
{
	[XmlAttribute("id")]
	public int id { get; set; }
	[XmlAttribute("modifier")]
	public string modifier { get; set; }
	[XmlText]
	public string content { get; set;}
}
#endregion


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