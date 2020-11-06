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
					.ToDictionary(r => r.Name, CreateResourceViewModel));
		}

		internal ResourcePack Take() =>
			new ResourcePack(Resources.Select(r => Take(r.Key)));

		internal ResourceModel Take(string name) {
			var viewModel = Resources[name];
			var amount    = viewModel.Amount.Value;
			viewModel.Take(viewModel.Amount.Value);
			return new ResourceModel(name, amount);
		}

		internal void Add(ResourcePack pack) {
			foreach ( var model in pack.Content ) {
				Add(model);
			}
		}

		internal void Add(ResourceModel model) => Add(model.Name, model.Amount);

		internal void Add(string name, long amount) {
			if ( !Resources.TryGetValue(name, out var viewModel) ) {
				var model = new ResourceModel(name, 0);
				_model.Content.Add(model);
				viewModel = CreateResourceViewModel(model);
				Resources.Add(name, viewModel);
			}
			viewModel.Add(amount);
		}

		ResourceViewModel CreateResourceViewModel(ResourceModel model) =>
			new ResourceViewModel(model);
	}
}