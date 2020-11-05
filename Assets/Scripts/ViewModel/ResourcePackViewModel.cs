using System.Collections.Generic;
using System.Linq;
using Game.Shared;
using UniRx;

namespace Game.ViewModel {
	public sealed class ResourcePackViewModel {
		readonly ResourcePack _model;

		readonly Dictionary<string, ReactiveProperty<long>> _resources;

		public IReadOnlyDictionary<string, ReactiveProperty<long>> Resources => _resources;

		public bool IsEmpty => (_resources.Count == 0) || _resources.All(r => r.Value.Value == 0);

		public ResourcePackViewModel(ResourcePack model) {
			_model = model;
			_resources = _model.Content
				.ToDictionary(r => r.Name, CreateResourceProperty);
		}

		internal Dictionary<string, long> Take() =>
			_resources
				.ToDictionary(p => p.Key, p => Take(p.Key));

		internal long Take(string name) {
			var property = _resources[name];
			var amount = property.Value;
			property.Value = 0;
			return amount;
		}

		internal void Add(Dictionary<string, long> resources) {
			foreach ( var pair in resources ) {
				Add(pair.Key, pair.Value);
			}
		}

		internal void Add(string name, long amount) {
			var property = _resources[name];
			property.Value += amount;
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