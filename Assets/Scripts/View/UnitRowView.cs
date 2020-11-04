using System;
using System.Collections.Generic;
using Game.ViewModel;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Game.View {
	[Serializable]
	public sealed class UnitRowView {
		[SerializeField] string      _type;
		[SerializeField] UnitView    _prefab;
		[SerializeField] Transform[] _places;

		List<UnitView> _instances = new List<UnitView>();

		public void Init(ReactiveCollection<UnitViewModel> viewModel) {
			viewModel
				.ObserveAdd()
				.Where(e => e.Value.Type == _type)
				.Subscribe(OnAddUnit);
		}

		void OnAddUnit(CollectionAddEvent<UnitViewModel> ev) {
			var place = TryGetNextPlace();
			if ( place == null ) {
				return;
			}
			var instance = UnityEngine.Object.Instantiate(_prefab, place);
			_instances.Add(instance);
		}

		[CanBeNull]
		Transform TryGetNextPlace() {
			var index = _instances.Count;
			return (index < _places.Length) ? _places[index] : null;
		}
	}
}