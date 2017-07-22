using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using System.IO;

public class UMAMaker1 : MonoBehaviour {

	private UMAGeneratorBase generator;
	private SlotLibrary slotLibrary;
	private OverlayLibrary overlayLibrary;
	private RaceLibrary raceLibrary;

	public RuntimeAnimatorController animController;



	private UMADynamicAvatar umaDynamicAvatar;
	private UMAData umaData;
	private UMADnaHumanoid umaDna;
	private UMADnaTutorial umaTutorialDna;

	private int numberOfSlots=20;



	//Uma Save 
	public string SaveString="";
	public bool save;
	public bool load;


	//Other asset loaded
	private bool otherLoaded;

	//AssetName to load
	public string AssetName;

	// Use this for initialization
	void Start () {
							
		//Load relevant Libs
		generator 			= GameObject.FindObjectOfType<UMAGeneratorBase> ();
		slotLibrary 		= GameObject.FindObjectOfType<SlotLibrary> () as SlotLibrary;
		overlayLibrary		=  GameObject.FindObjectOfType<OverlayLibrary> () as OverlayLibrary;
		raceLibrary 		=  GameObject.FindObjectOfType<RaceLibrary> () as RaceLibrary;

		GeneratUMA ();
		LoadAssetName();
		load = true;
		otherLoaded = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (load) {
			load = false;
			LoadAsset ();
		}
	}


	void GeneratUMA(){
		
		//Add UMA Components to GameObject GO
		GameObject GO = new GameObject("MyUMA");
		umaDynamicAvatar = GO.AddComponent<UMADynamicAvatar> ();

		//Initialise avatar and grab a reference to it's data component
		umaDynamicAvatar.Initialize();
		umaData = umaDynamicAvatar.umaData;

		//Attach our generator
		umaDynamicAvatar.umaGenerator = generator;
		umaData.umaGenerator = generator;

		//Setup slot array
		umaData.umaRecipe.slotDataList = new SlotData[numberOfSlots];

		//Setup Morph References
		umaDna = new UMADnaHumanoid();
		umaTutorialDna = new UMADnaTutorial ();
		umaData.umaRecipe.AddDna (umaDna);
		umaData.umaRecipe.AddDna (umaTutorialDna);

		//Make UMA
		CreateMale ();
		umaDynamicAvatar.animationController = animController;
		umaDynamicAvatar.UpdateNewRace ();

		GO.transform.parent = this.gameObject.transform;
		GO.transform.localPosition = Vector3.zero;
		GO.transform.localRotation = Quaternion.identity;

	}

	/// <summary>
	/// Creates the male UMA.
	/// </summary>
	void CreateMale(){
		var umaRecipe = umaDynamicAvatar.umaData.umaRecipe;
		umaRecipe.SetRace (raceLibrary.GetRace("HumanMale"));



		umaData.umaRecipe.slotDataList [0] = slotLibrary.InstantiateSlot ("MaleEyes");
		umaData.umaRecipe.slotDataList [0].AddOverlay(overlayLibrary.InstantiateOverlay ("EyeOverlay"));

		umaData.umaRecipe.slotDataList [1] = slotLibrary.InstantiateSlot ("MaleInnerMouth");
		umaData.umaRecipe.slotDataList [1].AddOverlay(overlayLibrary.InstantiateOverlay ("InnerMouth"));

		umaData.umaRecipe.slotDataList [2] = slotLibrary.InstantiateSlot ("MaleFace");
		umaData.umaRecipe.slotDataList [2].AddOverlay(overlayLibrary.InstantiateOverlay ("MaleHead02"));
		umaData.umaRecipe.slotDataList [2].AddOverlay(overlayLibrary.InstantiateOverlay ("MaleEyebrow01", Color.black));
		umaData.umaRecipe.slotDataList [2].AddOverlay(overlayLibrary.InstantiateOverlay ("MaleHair03", Color.black));
		umaData.umaRecipe.slotDataList [2].AddOverlay(overlayLibrary.InstantiateOverlay ("MaleBeard03", Color.black));


		umaData.umaRecipe.slotDataList [3] = slotLibrary.InstantiateSlot ("MaleTorso");
		umaData.umaRecipe.slotDataList [3].AddOverlay (overlayLibrary.InstantiateOverlay("M_Freebie Robeset Robe"));
		umaData.umaRecipe.slotDataList [3].AddOverlay (overlayLibrary.InstantiateOverlay("MaleShirt01Color"));

		umaData.umaRecipe.slotDataList [4] = slotLibrary.InstantiateSlot ("MaleHands");
		umaData.umaRecipe.slotDataList [4].AddOverlay (overlayLibrary.InstantiateOverlay("MaleBody02"));

		umaData.umaRecipe.slotDataList [5] = slotLibrary.InstantiateSlot ("MaleLegs");
		umaData.umaRecipe.slotDataList [5].AddOverlay (overlayLibrary.InstantiateOverlay("M_Freebie Robeset Robe"));
		umaData.umaRecipe.slotDataList [5].AddOverlay (overlayLibrary.InstantiateOverlay("MaleJeans02", Color.cyan));

		umaData.umaRecipe.slotDataList [6] = slotLibrary.InstantiateSlot ("MaleFeet");
		umaData.umaRecipe.slotDataList [6].AddOverlay (overlayLibrary.InstantiateOverlay("M_Freebie Robeset Boots"));

	}

	///// UMA Morphing scripts
	/// 
	void SetBodyMass(float mass){
		umaDna.upperMuscle = mass;
		umaDna.upperWeight = mass;
		umaDna.lowerMuscle = mass;
		umaDna.lowerWeight = mass;
		umaDna.armWidth = mass;
		umaDna.breastSize = mass;
		umaDna.headSize = mass;
		umaDna.headWidth = mass;
	}

	/// <summary>
	/// Save the UMA Avatar.
	/// </summary>
	public void Save(){
		UMATextRecipe recipe = ScriptableObject.CreateInstance<UMATextRecipe> ();
		recipe.Save (umaDynamicAvatar.umaData.umaRecipe, umaDynamicAvatar.context);
		SaveString = recipe.recipeString;
		Destroy (recipe);

		//Save String to  text file
		string fileName= "Avatar.txt";
		StreamWriter stream = File.CreateText (fileName);
		stream.WriteLine (SaveString);
		stream.Close ();
		
	}

	/// <summary>
	/// Load the UMA Avatar.
	/// </summary>
	public void Load(){

		//Load UMA-string from local text file
		string fileName= "Avatar.txt";
		StreamReader stream = File.OpenText(fileName);
		SaveString = stream.ReadLine ();
		stream.Close ();

		UMATextRecipe recipe = ScriptableObject.CreateInstance<UMATextRecipe> ();
		recipe.recipeString=SaveString;
		umaDynamicAvatar.Load (recipe);
		Destroy (recipe);

	}

	/// <summary>
	/// Save the UMA Avatar.
	/// </summary>
	public void LoadAssetName(){
		string fileName= "Avatar.txt";
		StreamReader stream = File.OpenText(fileName);
		AssetName = stream.ReadLine ();
		stream.Close ();
	}

	/// <summary>
	/// Simple Asset Loader
	/// </summary>
	public void LoadOtherAsset(string assetName){
		if (!otherLoaded) {
			Debug.Log ("einmal " + assetName);
			load = true;
			AssetName = assetName;
			otherLoaded = true;
		}

	}


	/// <summary>
	/// Simple Asset Loader
	/// </summary>
	public void LoadAsset(){
		UMARecipeBase recipe = Resources.Load (AssetName) as UMARecipeBase;
		umaDynamicAvatar.Load (recipe);
	}
}
