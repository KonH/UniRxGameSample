using Game.ViewModel;
using JetBrains.Annotations;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Game.View {
	public class StatView : MonoBehaviour {
		[SerializeField] Image    _image;
		[SerializeField] TMP_Text _text;
		[SerializeField] bool     _advance;

		GameViewModel _game;
		string        _type;

		void OnValidate() {
			Assert.IsNotNull(_image, nameof(_image));
			Assert.IsNotNull(_text, nameof(_text));
		}

		public void Init([NotNull] GameViewModel game, [NotNull] UnitViewModel viewModel) {
			Assert.IsNotNull(game, nameof(game));
			Assert.IsNotNull(viewModel, nameof(viewModel));
			_game = game;
			_type = viewModel.Type;
			viewModel.Level
				.Subscribe(OnLevelChanged);
		}

		void OnLevelChanged(int currentLevel) {
			var wantedLevel = currentLevel + (_advance ? 1 : 0);
			var config      = _game.GetUnitConfig(_type);
			if ( config == null ) {
				return;
			}
			if ( wantedLevel >= config.Levels.Count ) {
				return;
			}
			var level  = config.Levels[wantedLevel];
			var income = level.Income;
			var time   = level.IncomeTime;
			_image.sprite = _game.GetResourceIcon(income.Name);
			_text.text    = $"{income.Amount} / {time} sec";
		}
	}
}