using Game.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;

namespace Game.View {
	public sealed class ResourceView : MonoBehaviour {
		[SerializeField] string   _name;
		[SerializeField] TMP_Text _text;

		public void Init(ResourcePackViewModel viewModel) {
			var property = viewModel.Resources[_name];
			property
				.Subscribe(UpdateValue);
		}

		void UpdateValue(long newAmount) {
			_text.text = newAmount.ToString();
		}
	}
}