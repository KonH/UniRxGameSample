using Game.Shared;
using UniRx;

namespace Game.ViewModel {
	public sealed class ResourceViewModel {
		readonly ReactiveProperty<long> _amount;

		public readonly ResourceModel Model;

		public readonly ReadOnlyReactiveProperty<long> Amount;

		public bool IsEmpty => (Amount.Value == 0);

		public ResourceViewModel(ResourceModel model) {
			Model = model;
			_amount = new ReactiveProperty<long>(model.Amount);
			_amount.Subscribe(v => model.Amount = v);
			Amount = _amount.ToReadOnlyReactiveProperty();
		}

		public void Add(long amount) => _amount.Value += amount;

		public ResourceModel Take(long amount) {
			_amount.Value -= amount;
			return new ResourceModel(Model.Name, amount);
		}

		public bool TryTake(long amount) {
			var isEnough = IsEnough(amount);
			if ( isEnough ) {
				_amount.Value -= amount;
			}
			return isEnough;
		}

		public ResourceModel TakeAll() => Take(Amount.Value);

		public bool IsEnough(long amount) => Amount.Value >= amount;
	}
}