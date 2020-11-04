using System.Collections.Generic;
using System.Linq;
using Game.Model;
using UniRx;

namespace Game.ViewModel {
	public sealed class ResourcePackViewModel {
		readonly ResourcePack _model;

		readonly Dictionary<string, ReactiveProperty<long>> _resources;

		public IReadOnlyDictionary<string, ReactiveProperty<long>> Resources => _resources;

		public ResourcePackViewModel(ResourcePack model) {
			_model = model;
			_resources = _model.Content
				.ToDictionary(r => r.Name, CreateResourceProperty);
		}

		ReactiveProperty<long> CreateResourceProperty(ResourceModel model) {
			var property = new ReactiveProperty<long>(model.Amount);
			property
				.Subscribe(newAmount => OnValueChanged(model.Name, newAmount));
			return property;
		}

		void OnValueChanged(string name, long newAmount) {
			var model = _model.Content.Find(m => m.Name == name);
			model.Amount = newAmount;
		}
	}
}