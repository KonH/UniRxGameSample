using Game.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View {
	public sealed class UnitView : MonoBehaviour {
		[SerializeField] Image _image;

		public void Init(UnitViewModel viewModel) {
			_image.sprite = viewModel.Sprite;
		}
	}
}