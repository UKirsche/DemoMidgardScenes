using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
 * 
 * AI Vision handles GameObjects that are visible and keeps the in a list of visible Gameobjects
 * 
 */
public class AIVision : MonoBehaviour
{
	public float fieldOfView = 90.0f; // Object within this angle are seen.
	public float closeFieldDistance = 1.0f; // Objects below this distance is always seen.

	public List<Collider> colliders = new List<Collider>();
	public List<GameObject> visibles = new List<GameObject>();


	/// <summary>
	/// Inherited
	/// </summary>
	void Start()
	{
	}


	/// <summary>
	/// Inherited
	/// </summary>
	void Update()
	{
		UpdateVisibility();
	}

	/// <summary>
	/// Updates the visibility.
	/// </summary>
	void UpdateVisibility()
	{
		visibles.Clear();
		foreach( var collider in colliders)
		{
			if (collider != null) {
				var attachedGameObject = collider.attachedRigidbody != null ? collider.attachedRigidbody.gameObject: null;
				bool isVisible = false;

				if (attachedGameObject != null)
				{
					isVisible=IsGameObjectVisible(attachedGameObject);
				}

				if (isVisible && !visibles.Contains(attachedGameObject))
				{
					visibles.Add(attachedGameObject);
				}
			}

		}
	}

	/// <summary>
	/// Checks wether a GamoObject is Visible. It is assumed that the self Position has the height of the head!
	/// </summary>
	/// <returns><c>true</c>, if GO visible was checked, <c>false</c> otherwise.</returns>
	/// <param name="go">Go.</param>
	private bool IsGameObjectVisible (GameObject go)
	{
		Vector3 selfHeadPosition = this.transform.position;

		//Winkel vom eigenen Objekt zum 
		float angle = Vector3.Angle (this.transform.forward, go.transform.position - selfHeadPosition);

		bool isInClosedField = Vector3.Distance (go.transform.position, this.transform.position) <= closeFieldDistance;
		bool isInFieldOfView = Mathf.Abs (angle) <= fieldOfView * 0.5f;
		bool isVisible = isInClosedField || (isInFieldOfView && HasLoS (go.gameObject));
		return isVisible;
	}

	/// <summary>
	/// Determines whether this instance has a line of sight to the specified target gameobject.
	/// </summary>
	/// <returns><c>true</c> if this instance is closest to self and not blocked by other go <c>false</c>.</returns>
	/// <param name="target">Target.</param>
	private bool HasLoS( GameObject target )
	{
		bool has = false;
		GameObject closestGameObject = null;

		var targetDirection = (target.transform.position - this.transform.position).normalized;
		float minDistance = float.PositiveInfinity;

		Ray ray = new Ray(this.transform.position, targetDirection);
		var hits = Physics.RaycastAll(ray, minDistance);


		foreach( var h in hits)
		{
			SetClosestGameObject (h, out minDistance, out closestGameObject);
		}

		has = closestGameObject == target;

		return has;
	}

	/// <summary>
	/// Sets th
	/// </summary>
	/// <param name="hit">Hit.</param>
	/// <param name="minDistance">Minimum distance.</param>
	/// <param name="closest">Closest.</param>
	private void SetClosestGameObject (RaycastHit hit, out float minDistance, out GameObject closestGO)
	{
		float distance = Vector3.Distance (hit.point, this.transform.position);
		var gObject = hit.collider.attachedRigidbody != null ? hit.collider.attachedRigidbody.gameObject : hit.collider.gameObject;
		if (distance <= minDistance && gObject != this.gameObject) {
			minDistance = distance;
			closestGO = gObject;
		}
	}


	/// <summary>
	/// Raises the trigger enter event. Only valid for Gameobjects with Rigidbody and that include a trigger type.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter( Collider other )
	{
		if(!colliders.Contains(other) )
		{
			colliders.Add(other);
			colliders.RemoveAll((c) => c == null);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (colliders.Contains(other))
		{
			colliders.Remove(other);
			colliders.RemoveAll((c) => c == null);
		}
	}




}
