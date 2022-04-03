using UnityEngine;
using UnityEngine.UI;


namespace Prototype3
{

	public class CharacterUIBehavior : GameBehaviour
	{
		[SerializeField]
		private Text interactionText;

		public void DisableCursor()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		public void EnableCursor()
		{
			Cursor.lockState = CursorLockMode.Confined;
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