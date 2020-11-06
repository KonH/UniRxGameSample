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

		public string         Type           => _model.Type;
		public DateTimeOffset LastIncomeTime => DateTimeOffset.FromUnixTimeMilliseconds(_model.LastIncomeTime);

		public readonly ReactiveProperty<int>    Level;
		public readonly ReactiveProperty<Sprite> Sprite;

		public ResourcePackViewModel Income { get; }

		public UnitViewModel(UnitConfig config, UnitModel model) {
			_config = config;
			_model  = model;
			Sprite  = new ReactiveProperty<Sprite>();
			Level   = new ReactiveProperty<int>(_model.Level);
			Income  = new ResourcePackViewModel(model.Income);
			Level
				.Do(l => _model.Level = l)
				.Subscribe(UpdateSprite);
		}

		public void AddIncome(ResourceModel resource, DateTimeOffset time) {
			Income.Add(resource.Name, resource.Amount);
			_model.LastIncomeTime = time.ToUnixTimeMilliseconds();
		}

		void UpdateSprite(int level) => Sprite.Value = _config.Levels[level].Sprite;
	}
}