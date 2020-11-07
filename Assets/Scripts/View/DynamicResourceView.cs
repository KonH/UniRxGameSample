using Game.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View {
	public sealed class DynamicResourceView : MonoBehaviour {
		[SerializeField] Image    _image;
		[SerializeField] TMP_Text _text;

		public void Init(GameViewModel game, ResourceViewModel viewModel) {
			_image.sprite = game.GetResourceIcon(viewModel.Model.Name);
			viewModel.Amount
				.Subscribe(UpdateValue);
		}

		void UpdateValue(long newAmount) => _text.text = newAmount.ToString();
	}
}