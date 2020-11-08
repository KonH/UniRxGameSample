using System;
using Game.Config;
using Game.Model;
using Game.Shared;
using UniRx;
using UnityEngine;

namespace Game.ViewModel {
	public sealed class UnitViewModel {
		readonly UnitConfig _config;
		readonly UnitModel  _model;

		readonly ResourcePackViewModel _resources;

		readonly ReactiveProperty<int>               _level;
		readonly ReactiveProperty<Sprite>            _sprite;
		readonly ReactiveProperty<ResourceViewModel> _upgradePrice;
		readonly ReactiveProperty<bool>              _canUpgrade;

		public string         Type           => _model.Type;
		public DateTimeOffset LastIncomeTime => DateTimeOffset.FromUnixTimeMilliseconds(_model.LastIncomeTime);

		public readonly ReadOnlyReactiveProperty<int>               Level;
		public readonly ReadOnlyReactiveProperty<Sprite>            Sprite;
		public readonly ReadOnlyReactiveProperty<ResourceViewModel> UpgradePrice;
		public readonly ReadOnlyReactiveProperty<bool>              CanUpgrade;

		public ResourceViewModel Income { get; }

		public UnitViewModel(UnitConfig config, UnitModel model, ResourcePackViewModel resources) {
			_config       = config;
			_model        = model;
			_resources    = resources;
			_sprite       = new ReactiveProperty<Sprite>();
			Sprite        = _sprite.ToReadOnlyReactiveProperty();
			_level        = new ReactiveProperty<int>(_model.Level);
			Level         = _level.ToReadOnlyReactiveProperty();
			Income        = new ResourceViewModel(model.Income);
			_upgradePrice = new ReactiveProperty<ResourceViewModel>(GetUpgradePrice());
			UpgradePrice  = _upgradePrice.ToReadOnlyReactiveProperty();
			_canUpgrade   = new ReactiveProperty<bool>(IsUpgradeAvailable());
			CanUpgrade    = _canUpgrade.ToReadOnlyReactiveProperty();
			foreach ( var resource in resources.Resources ) {
				resource.Value.Amount
					.Select(_ => IsUpgradeAvailable())
					.Subscribe(isAvailable => _canUpgrade.Value = isAvailable);
			}
			Level
				.Do(l => _model.Level = l)
				.Subscribe(OnLevelUpdated);
		}

		ResourceViewModel GetUpgradePrice() => new ResourceViewModel(GetUpgradePriceModel());

		ResourceModel GetUpgradePriceModel() {
			var nextLevel = Level.Value + 1;
			if ( nextLevel >= _config.Levels.Count ) {
				return new ResourceModel(string.Empty, 0);
			}
			return _config.Levels[nextLevel].Price;
		}

		bool IsUpgradeAvailable() {
			var price = GetUpgradePriceModel();
			if ( string.IsNullOrEmpty(price.Name) ) {
				return false;
			}
			return _resources.Resources[price.Name].IsEnough(price.Amount);
		}

		public void AddIncome(long amount, DateTimeOffset time) {
			Income.Add(amount);
			_model.LastIncomeTime = time.ToUnixTimeMilliseconds();
		}

		public void Upgrade() => _level.Value++;

		void OnLevelUpdated(int level) {
			UpdateSprite(level);
			UpdateUpgradePrice();
			_canUpgrade.Value = IsUpgradeAvailable();
		}

		void UpdateSprite(int level) => _sprite.Value = _config.Levels[level].Sprite;

		void UpdateUpgradePrice() => _upgradePrice.Value = GetUpgradePrice();
	}
}