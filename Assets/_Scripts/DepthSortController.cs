using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class DepthSortController : MonoBehaviour {
	
	[Tooltip("Distance of object from the floor")]
	public int HeightOffset = 0;
	public DepthSortController inheritedDepthSortController;
	[HideInInspector]
	public int sortingOrder;
	private const int IsometricRangePerYUnit = 100;

	void Update()
	{
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		if (inheritedDepthSortController != null)
			sortingOrder = inheritedDepthSortController.sortingOrder + 1;
		else {
			sortingOrder = -(int)((transform.position.y - transform.localScale.y) * IsometricRangePerYUnit) + HeightOffset;
		}
		renderer.sortingOrder = sortingOrder;
	}
}