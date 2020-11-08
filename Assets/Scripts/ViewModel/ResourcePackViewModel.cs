using System.Linq;
using Game.Shared;
using JetBrains.Annotations;
using UniRx;
using UnityEngine.Assertions;

namespace Game.ViewModel {
	public sealed class ResourcePackViewModel {
		readonly ResourcePack _model;

		readonly ReactiveDictionary<string, ResourceViewModel> _resources;

		public IReadOnlyReactiveDictionary<string, ResourceViewModel> Resources => _resources;

		public ResourcePackViewModel([NotNull] ResourcePack model) {
			Assert.IsNotNull(model, nameof(model));
			_model = model;
			_resources = new ReactiveDictionary<string, ResourceViewModel>(
				_model.Content
					.ToDictionary(r => r.Name, r => new ResourceViewModel(r)));
		}

		[CanBeNull]
		public ResourceViewModel GetViewModel([NotNull] string name) {
			Assert.IsNotNull(name, nameof(name));
			return _resources.TryGetValue(name, out var viewModel) ? viewModel : null;
		}

		public long GetAmount([NotNull] string name) {
			Assert.IsNotNull(name, nameof(name));
			return _resources.TryGetValue(name, out var value) ? value.Amount.Value : 0;
		}

		public bool IsEnough([NotNull] ResourceModel model) {
			Assert.IsNotNull(model, nameof(model));
			return _resources.TryGetValue(model.Name, out var value) && value.IsEnough(model.Amount);
		}

		public bool TryTake([NotNull] ResourceModel model) {
			Assert.IsNotNull(model, nameof(model));
			if ( !IsEnough(model) ) {
				return false;
			}
			var viewModel = GetViewModel(model.Name);
			if ( viewModel == null ) {
				return false;
			}
			viewModel.Take(model.Amount);
			return true;
		}

		public ResourcePack TakeAll() =>
			new ResourcePack(_resources.Select(r => TakeAll(r.Key)));

		ResourceModel TakeAll(string name) {
			var viewModel = GetViewModel(name);
			if ( viewModel == null ) {
				return new ResourceModel(name, 0);
			}
			var amount = viewModel.Amount.Value;
			viewModel.Take(amount);
			return new ResourceModel(name, amount);
		}

		public void Add([NotNull] ResourceModel model) {
			Assert.IsNotNull(model, nameof(model));
			Add(model.Name, model.Amount);
		}

		void Add(string name, long amount) {
			if ( !_resources.TryGetValue(name, out var viewModel) ) {
				var model = new ResourceModel(name, 0);
				_model.Content.Add(model);
				viewModel = new ResourceViewModel(model);
				_resources.Add(name, viewModel);
			}
			viewModel.Add(amount);
		}
	}
}