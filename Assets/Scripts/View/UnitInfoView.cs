using Game.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View {
	public sealed class UnitInfoView : MonoBehaviour {
		readonly DisposableOwner _owner = new DisposableOwner();

		[SerializeField] Image       _unitImage;
		[SerializeField] TMP_Text    _unitText;
		[SerializeField] Button      _closeButton;
		[SerializeField] UpgradeView _upgradeView;
		[SerializeField] StatView[]  _statViews;

		public void Init(GameViewModel game, UnitViewModel viewModel) {
			_owner.SetupDisposables();
			_closeButton.onClick.AsObservable()
				.Subscribe(_ => OnCloseClick())
				.AddTo(_owner.Disposables);
			_unitText.text = viewModel.Type;
			viewModel.Sprite
				.Subscribe(s => _unitImage.sprite = s)
				.AddTo(_owner.Disposables);
			_upgradeView.Init(game, viewModel);
			foreach ( var statView in _statViews ) {
				statView.Init(game, viewModel);
			}
			Show();
		}

		void OnDestroy() => _owner.Dispose();

		void OnCloseClick() => Hide();

		void Show() => gameObject.SetActive(true);

		void Hide() => gameObject.SetActive(false);
	}
}