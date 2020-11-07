using Game.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View {
	public sealed class IncomeView : MonoBehaviour {
		[SerializeField] TMP_Text _text;
		[SerializeField] Image    _image;

		public void Init(GameViewModel game, ResourceViewModel viewModel) {
			_image.sprite = game.GetResourceIcon(viewModel.Model.Name);
			viewModel.Amount
				.Subscribe(UpdateValue);
		}

		void UpdateValue(long newAmount) {
			gameObject.SetActive(newAmount > 0);
			_text.text = newAmount.ToString();
		}
	}
}