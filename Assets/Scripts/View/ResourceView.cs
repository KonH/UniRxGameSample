using System;
using Game.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;

namespace Game.View {
	public sealed class ResourceView : MonoBehaviour {
		readonly DisposableOwner _owner = new DisposableOwner();

		[SerializeField] string   _name;
		[SerializeField] TMP_Text _text;
		[SerializeField] TMP_Text _appearText;

		long _lastAmount;

		public void Init(ResourcePackViewModel packViewModel) {
			_owner.SetupDisposables();
			var viewModel = packViewModel.GetViewModel(_name);
			UpdateValue(viewModel.Amount.Value);
			StopAppear();
			viewModel.Amount
				.Skip(1)
				.Do(StartAppear)
				.Delay(TimeSpan.FromSeconds(0.75))
				.Do(_ => StopAppear())
				.Subscribe(UpdateValue)
				.AddTo(_owner.Disposables);
		}

		void OnDestroy() => _owner.Dispose();

		void StartAppear(long newAmount) {
			var diff = (newAmount - _lastAmount);
			_appearText.text = $"{((diff >= 0) ? "+" : "")}{diff}";
			_appearText.enabled = true;
		}

		void StopAppear() {
			_appearText.enabled = false;
		}

		void UpdateValue(long newAmount) {
			_text.text = newAmount.ToString();
			_lastAmount = newAmount;
		}
	}
}