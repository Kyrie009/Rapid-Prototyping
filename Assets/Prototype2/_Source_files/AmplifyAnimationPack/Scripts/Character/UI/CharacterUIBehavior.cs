// Amplify Animation Pack - Third-Person Character Controller
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>

using UnityEngine;
using UnityEngine.UI;


namespace AmplifyAnimationPack
{

	public class CharacterUIBehavior : MonoBehaviour
	{
		[SerializeField]
		private Text interactionText;

		public void DisableCursor()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		public void InteractionText_Enable( string _textToShow )
		{
			interactionText.gameObject.SetActive( true );
			interactionText.text = _textToShow;

		}

		public void InteractionText_Disable()
		{
			interactionText.gameObject.SetActive(false);
		}

	}
}