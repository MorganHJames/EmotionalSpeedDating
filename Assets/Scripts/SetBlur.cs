////////////////////////////////////////////////////////////
// File: SetBlur.cs
// Author: Morgan Henry James
// Date Created: 14-01-2020
// Brief: Sets the blur of image this script is attached to.
//////////////////////////////////////////////////////////// 

using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Sets the blur of image this script is attached to.
/// </summary>
public class SetBlur : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The size the blur of the image should be.
	/// </summary>
	[Tooltip("The size the blur of the image should be.")]
	[SerializeField] private float blurSize = 2.0f;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Updates the blur of the image.
	/// </summary>
	private void Update()
	{
		GetComponent<Image>().material.SetFloat("_Size", blurSize);
	}
	#endregion
	#region Public

	#endregion
	#endregion
}