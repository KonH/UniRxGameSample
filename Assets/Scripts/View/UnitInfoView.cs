using Game.ViewModel;
using JetBrains.Annotations;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Game.View {
	public sealed class UnitInfoView : MonoBehaviour {
		readonly DisposableOwner _owner = new DisposableOwner();

		[SerializeField] Image       _unitImage;
		[SerializeField] TMP_Text    _unitText;
		[SerializeField] Button      _closeButton;
		[SerializeField] UpgradeView _upgradeView;
		[SerializeField] StatView[]  _statViews;

		void OnValidate() {
			Assert.IsNotNull(_unitImage, nameof(_unitImage));
			Assert.IsNotNull(_unitText, nameof(_unitText));
			Assert.IsNotNull(_closeButton, nameof(_closeButton));
			Assert.IsNotNull(_upgradeView, nameof(_upgradeView));
		}

		public void Init([NotNull] GameViewModel game, [NotNull] UnitViewModel viewModel) {
			Assert.IsNotNull(game, nameof(game));
			Assert.IsNotNull(viewModel, nameof(viewModel));
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