using System.Linq;
using Game.Shared;
using UniRx;

namespace Game.ViewModel {
	public sealed class ResourcePackViewModel {
		readonly ResourcePack _model;

		public readonly ReactiveDictionary<string, ReactiveProperty<long>> Resources;

		public bool IsEmpty => (Resources.Count == 0) || Resources.All(r => r.Value.Value == 0);

		public ResourcePackViewModel(ResourcePack model) {
			_model = model;
			Resources = new ReactiveDictionary<string, ReactiveProperty<long>>(
				_model.Content
					.ToDictionary(r => r.Name, CreateResourceProperty));
		}

		internal ResourcePack Take() =>
			new ResourcePack(Resources.Select(r => Take(r.Key)));

		internal ResourceModel Take(string name) {
			var property = Resources[name];
			var amount = property.Value;
			property.Value = 0;
			return new ResourceModel(name, amount);
		}

		internal void Add(ResourcePack pack) {
			foreach ( var pair in pack.Content ) {
				Add(pair.Name, pair.Amount);
			}
		}

		internal void Add(string name, long amount) {
			if ( !Resources.TryGetValue(name, out var property) ) {
				var model = new ResourceModel(name, amount);
				_model.Content.Add(model);
				property = CreateResourceProperty(model);
				Resources.Add(name, property);
			}
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