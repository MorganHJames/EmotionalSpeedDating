////////////////////////////////////////////////////////////
// File: FaceDetectionDemo.cs
// Author: Morgan Henry James
// Date Created: 18-10-2019
// Brief: Face detection using HAAR Cascade.
//////////////////////////////////////////////////////////// 

using UnityEngine;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System;

/// <summary>
/// Face detection using HAAR Cascade.
/// </summary>
public class FaceDetectionDemo : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The loading screen.
	/// </summary>
	[Tooltip("The loading screen.")]
	[SerializeField] private GameObject loadingScreen;

	/// <summary>
	/// The video capture.
	/// </summary>
	private Capture cvCapture;

	/// <summary>
	/// The HAAR cascade classifier trained file.
	/// </summary>
	private CascadeClassifier _cascadeClassifier;

	/// <summary>
	/// What is currently in the cameras frame.
	/// </summary>
	private Image<Bgr, byte> currentFrameBgr;

	/// <summary>
	/// An array of pixels of the image in grey scale.
	/// </summary>
	private float[] greyscalePixelArrayOfFace;

	public Text tesadsdas;
	#endregion
	#region Public
	/// <summary>
	/// The output of the camera image mixed with the green outline.
	/// </summary>
	[Tooltip("The output of the camera image mixed with the green outline.")]
	public RawImage camWithCascadeOutput;
	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Starts the face detection and turns off the loading screen.
	/// </summary>
	private void Start()
	{
		cvCapture = new Capture(Emgu.CV.CvEnum.CaptureType.ANY);
		_cascadeClassifier = new CascadeClassifier(Application.dataPath + "\\StreamingAssets\\Training Data\\Haar-cascade Detection\\haarcascade_frontalface_alt.xml");
		cvCapture.Start();
		loadingScreen.SetActive(false);
	}

	/// <summary>
	/// Updates the face detection.
	/// </summary>
	private void Update()
	{
		FaceDetector();
	}

	/// <summary>
	/// Takes the input of what the web cam sees and then converts it into a gray scale byte array.
	/// Passes that through a HAAR cascade classifier.
	/// Returns the raw input with the output of the HAAR classifier applied over the top.
	/// </summary>
	private void FaceDetector()
	{
		currentFrameBgr = cvCapture.QueryFrame();

		Texture2D tex = new Texture2D(640, 480);

		if (currentFrameBgr != null)
		{
			// Convert the raw input into gray scale.
			Image<Gray, byte> grayFrame = currentFrameBgr.Convert<Gray, byte>();

			//// Apply the classifier and see if there are any faces.
			Rectangle[] faces = _cascadeClassifier.DetectMultiScale(grayFrame, 2.5, 3, new Size(30, 30), Size.Empty);

			// Draw the face outline.
			foreach (Rectangle face in faces)
			{
				currentFrameBgr.Draw(face, new Bgr(0, 255, 0), 4);
			}

			// If there are faces.
			if (faces.Length != 0)
			{
				// Resize the outlines to the raw input.
				grayFrame.ROI = faces[0];
				grayFrame = grayFrame.Resize(48, 48, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
				greyscalePixelArrayOfFace = grayFrame.Bytes.Select(x => (float)x).ToArray();
				for (int i = 0; i < greyscalePixelArrayOfFace.Length; i++)
				{
					greyscalePixelArrayOfFace[i] = ((greyscalePixelArrayOfFace[i] / 255f) - 0.5f) * 2.0f;//Image pre process.
				}
			}

			// Had to save it as it wouldn't work in the build without doing so :/
			currentFrameBgr.Bitmap.Save(Application.dataPath + "\\StreamingAssets\\CurrentImage.png", ImageFormat.Png);

			// Load the image.
			tex.LoadImage(System.IO.File.ReadAllBytes(Application.dataPath + "\\StreamingAssets\\CurrentImage.png"));

			// Set the image.
			camWithCascadeOutput.texture = tex;
		}
	}

	/// <summary>
	/// Stops the camera from remaining on.
	/// </summary>
	private void OnDestroy()
	{
		// Release from memory.
		cvCapture.Dispose();
		cvCapture.Stop();
	}
	#endregion
	#region Public
	/// <summary>
	/// Goes back to the menu
	/// </summary>
	public void GoBackToMenu()
	{
		SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
	}
	#endregion
	#endregion
}