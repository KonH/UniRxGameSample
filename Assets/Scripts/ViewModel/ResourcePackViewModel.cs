using System;
using System.Collections.Generic;
using System.Linq;
using Game.Shared;
using JetBrains.Annotations;
using UniRx;

namespace Game.ViewModel {
	public sealed class ResourcePackViewModel {
		readonly ResourcePack _model;

		readonly ReactiveDictionary<string, ResourceViewModel> _resources;

		public IReadOnlyReactiveDictionary<string, ResourceViewModel> Resources => _resources;

		public bool IsEmpty => (_resources.Count == 0) || _resources.All(r => r.Value.IsEmpty);

		public ResourcePackViewModel(ResourcePack model) {
			_model = model;
			_resources = new ReactiveDictionary<string, ResourceViewModel>(
				_model.Content
					.ToDictionary(r => r.Name, r => new ResourceViewModel(r)));
		}

		[CanBeNull]
		public ResourceViewModel GetViewModel(string name) =>
			_resources.TryGetValue(name, out var viewModel) ? viewModel : null;

		public long GetAmount(string name) =>
			_resources.TryGetValue(name, out var value) ? value.Amount.Value : 0;

		public bool IsEnough(ResourceModel model) =>
			_resources.TryGetValue(model.Name, out var value) && value.IsEnough(model.Amount);

		public bool TryTake(ResourceModel model) {
			var isEnough = IsEnough(model);
			if ( isEnough ) {
				_resources[model.Name].Take(model.Amount);
			}
			return isEnough;
		}

		public ResourcePack TakeAll() =>
			new ResourcePack(_resources.Select(r => TakeAll(r.Key)));

		public ResourceModel TakeAll(string name) {
			var viewModel = GetViewModel(name);
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