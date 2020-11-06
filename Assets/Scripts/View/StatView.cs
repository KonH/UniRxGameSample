using Game.ViewModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Game.View {
	public class StatView : MonoBehaviour {
		[SerializeField] Image    _image;
		[SerializeField] TMP_Text _text;
		[SerializeField] bool     _advance;

		GameViewModel _game;
		string        _type;

		public void Init(GameViewModel game, UnitViewModel viewModel) {
			_game    = game;
			_type    = viewModel.Type;
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