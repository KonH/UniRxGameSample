using Game.ViewModel;
using JetBrains.Annotations;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Game.View {
	public sealed class IncomeView : MonoBehaviour {
		[SerializeField] TMP_Text _text;
		[SerializeField] Image    _image;

		void OnValidate() {
			Assert.IsNotNull(_text, nameof(_text));
			Assert.IsNotNull(_image, nameof(_image));
		}

		public void Init([NotNull] GameViewModel game, [NotNull] ResourceViewModel viewModel) {
			Assert.IsNotNull(game, nameof(game));
			Assert.IsNotNull(viewModel, nameof(viewModel));
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