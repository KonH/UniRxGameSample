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

		public string         Type           => _model.Type;
		public DateTimeOffset LastIncomeTime => DateTimeOffset.FromUnixTimeMilliseconds(_model.LastIncomeTime);

		public readonly ReactiveProperty<int>               Level;
		public readonly ReactiveProperty<Sprite>            Sprite;
		public readonly ReactiveProperty<ResourceViewModel> UpgradePrice;
		public readonly ReactiveProperty<bool>              CanUpgrade;

		public ResourceViewModel Income { get; }

		public UnitViewModel(UnitConfig config, UnitModel model, ResourcePackViewModel resources) {
			_config      = config;
			_model       = model;
			_resources   = resources;
			Sprite       = new ReactiveProperty<Sprite>();
			Level        = new ReactiveProperty<int>(_model.Level);
			Income       = new ResourceViewModel(model.Income);
			UpgradePrice = new ReactiveProperty<ResourceViewModel>(GetUpgradePrice());
			CanUpgrade   = new ReactiveProperty<bool>(IsUpgradeAvailable());
			foreach ( var resource in resources.Resources ) {
				resource.Value.Amount
					.Select(_ => IsUpgradeAvailable())
					.Subscribe(isAvailable => CanUpgrade.Value = isAvailable);
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

		void OnLevelUpdated(int level) {
			UpdateSprite(level);
			UpdateUpgradePrice();
		}

		void UpdateSprite(int level) => Sprite.Value = _config.Levels[level].Sprite;

		void UpdateUpgradePrice() => UpgradePrice.Value = GetUpgradePrice();
	}
}