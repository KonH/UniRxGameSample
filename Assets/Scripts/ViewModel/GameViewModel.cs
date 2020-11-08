using System;
using System.Linq;
using Game.Config;
using Game.Model;
using Game.Service;
using Game.Shared;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Game.ViewModel {
	public sealed class GameViewModel {
		readonly GameConfig _config;

		public GameModel Model { get; }

		public readonly TimeProvider                      Time;
		public readonly ResourcePackViewModel             Resources;
		public readonly ReactiveCollection<UnitViewModel> Units;

		public GameViewModel(GameConfig config, GameModel model) {
			_config   = config;
			Model     = model;
			Time      = new TimeProvider();
			Resources = new ResourcePackViewModel(model.Resources);
			Units     = new ReactiveCollection<UnitViewModel>(model.Units.Select(CreateViewModel));
		}

		public void Update() {
			Time.Update();
			foreach ( var unit in Units ) {
				var config = GetUnitConfig(unit.Type);
				if ( config == null ) {
					continue;
				}
				var now        = Time.Now;
				var level      = config.Levels[unit.Level.Value];
				var updateTime = level.IncomeTime;
				var interval   = (now - unit.LastIncomeTime);
				while ( interval.TotalSeconds > updateTime ) {
					var income = level.Income.Amount;
					unit.AddIncome(income, now);
					interval = interval.Subtract(TimeSpan.FromSeconds(updateTime));
				}
			}
		}

		public void Collect(UnitViewModel unit) {
			var income = unit.Income.TakeAll();
			Resources.Add(income);
		}

		[CanBeNull]
		public UnitViewModel BuyUnit(string unitType) {
			var price = GetBuyPrice(unitType);
			if ( Resources.TryTake(price) ) {
				return AddUnit(unitType);
			}
			return null;
		}

		UnitViewModel AddUnit(string unitType) {
			var config = GetUnitConfig(unitType);
			if ( config == null ) {
				return null;
			}
			var now        = Time.Now.ToUnixTimeMilliseconds();
			var incomeType = config.Levels[0].Income.Name;
			var model      = new UnitModel(unitType, 0, now, new ResourceModel(incomeType, 0));
			Model.Units.Add(model);
			var viewModel = CreateViewModel(model);
			Units.Add(viewModel);
			return viewModel;
		}

		[CanBeNull]
		public Sprite GetResourceIcon(string resourceName) {
			var resourceConfig = _config.Resources.Find(r => r.Name == resourceName);
			return resourceConfig?.Icon;
		}

		public ResourceModel GetBuyPrice(string unitType) {
			var unitConfig = GetUnitConfig(unitType);
			return unitConfig?.Levels[0].Price ?? new ResourceModel(string.Empty, 0);
		}

		UnitViewModel CreateViewModel(UnitModel model) => new UnitViewModel(GetUnitConfig(model.Type), model, Resources);

		[CanBeNull]
		public UnitConfig GetUnitConfig(string unitType) => _config.Units.Find(u => u.Type == unitType);

		public void UpgradeUnit(UnitViewModel unit) {
			var config = GetUnitConfig(unit.Type);
			if ( config == null ) {
				return;
			}
			if ( unit.Level.Value >= (config.Levels.Count - 1) ) {
				return;
			}
			var upgradePrice = unit.UpgradePrice.Value.Model;
			if ( Resources.TryTake(upgradePrice) ) {
				unit.Upgrade();
			}
		}

		public static GameViewModel Create(GameConfig config) => Create(config, null);

		public static GameViewModel Create(GameConfig config, GameModel model) =>
			new GameViewModel(config, model ?? CreateModel(config));

		static GameModel CreateModel(GameConfig config) {
			var resourceNames = config.Resources.Select(r => r.Name);
			var initResource  = config.InitialResource;
			return new GameModel {
				Resources = new ResourcePack(
					resourceNames
						.Select(name => {
							var amount = (initResource.Name == name) ? initResource.Amount : 0;
							return new ResourceModel {
								Name   = name,
								Amount = amount
							};
						}))
			};
		}
	}
}