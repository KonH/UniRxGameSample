using System;
using Game.Config;
using Game.Model;
using Game.Shared;
using UnityEngine;

namespace Game.ViewModel {
	public sealed class UnitViewModel {
		readonly UnitModel _model;

		public string         Type           => _model.Type;
		public int            Level          => _model.Level;
		public DateTimeOffset LastIncomeTime => DateTimeOffset.FromUnixTimeMilliseconds(_model.LastIncomeTime);

		public Sprite                Sprite { get; }
		public ResourcePackViewModel Income { get; }

		public UnitViewModel(UnitConfig config, UnitModel model) {
			_model = model;
			Sprite = config.Levels[model.Level].Sprite;
			Income = new ResourcePackViewModel(model.Income);
		}

		public void AddIncome(ResourceModel resource, DateTimeOffset time) {
			Income.Add(resource.Name, resource.Amount);
			_model.LastIncomeTime = time.ToUnixTimeMilliseconds();
		}
	}
}