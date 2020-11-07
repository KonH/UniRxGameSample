using System.Linq;
using Game.Shared;
using UniRx;

namespace Game.ViewModel {
	public sealed class ResourcePackViewModel {
		readonly ResourcePack _model;

		public readonly ReactiveDictionary<string, ResourceViewModel> Resources;

		public bool IsEmpty => (Resources.Count == 0) || Resources.All(r => r.Value.IsEmpty);

		public ResourcePackViewModel(ResourcePack model) {
			_model = model;
			Resources = new ReactiveDictionary<string, ResourceViewModel>(
				_model.Content
					.ToDictionary(r => r.Name, r => new ResourceViewModel(r)));
		}

		public ResourcePack TakeAll() =>
			new ResourcePack(Resources.Select(r => TakeAll(r.Key)));

		public ResourceModel TakeAll(string name) {
			var viewModel = Resources[name];
			var amount    = viewModel.Amount.Value;
			viewModel.Take(viewModel.Amount.Value);
			return new ResourceModel(name, amount);
		}

		public void Add(ResourcePack pack) {
			foreach ( var model in pack.Content ) {
				Add(model);
			}
		}

		public void Add(ResourceModel model) => Add(model.Name, model.Amount);

		internal void Add(string name, long amount) {
			if ( !Resources.TryGetValue(name, out var viewModel) ) {
				var model = new ResourceModel(name, 0);
				_model.Content.Add(model);
				viewModel = new ResourceViewModel(model);
				Resources.Add(name, viewModel);
			}
			viewModel.Add(amount);
		}
	}
}