using System.Linq;
using Game.Config;
using Game.Model;
using Game.Shared;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Game.ViewModel {
	public sealed class GameViewModel {
		readonly GameConfig _config;
		readonly GameModel  _model;

		public readonly ResourcePackViewModel             Resources;
		public readonly ReactiveCollection<UnitViewModel> Units;

		public GameViewModel(GameConfig config, GameModel model) {
			_config   = config;
			_model    = model;
			Resources = new ResourcePackViewModel(model.Resources);
			Units     = new ReactiveCollection<UnitViewModel>(model.Units.Select(m => new UnitViewModel(m)));
		}

		public void AddUnit(string unitType) {
			var model = new UnitModel {
				Type = unitType
			};
			_model.Units.Add(model);
			Units.Add(new UnitViewModel(model));
		}

		public void BuyUnit(string unitType) {
			var price = GetBuyPrice(unitType);
			Resources.Resources[price.Name].Value -= price.Amount;
			AddUnit(unitType);
		}

		[CanBeNull]
		public Sprite GetResourceIcon(string resourceName) {
			var resourceConfig = _config.Resources.Find(r => r.Name == resourceName);
			return resourceConfig?.Icon;
		}

		public ResourceModel GetBuyPrice(string unitType) {
			var unitConfig = GetUnitConfig(unitType);
			return unitConfig?.Prices[0] ?? new ResourceModel();
		}

		[CanBeNull]
		UnitConfig GetUnitConfig(string unitType) => _config.Units.Find(u => u.Type == unitType);
	}
}