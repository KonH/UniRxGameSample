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
		[SerializeField] UnitView    _unitPrefab;
		[SerializeField] Transform[] _places;

		UnitBuyView _placeholder;

		List<UnitView> _instances = new List<UnitView>();

		public void Init(UnitBuyView placeholderPrefab, ReactiveCollection<UnitViewModel> viewModel) {
			_placeholder = UnityEngine.Object.Instantiate(placeholderPrefab);
			viewModel
				.ObserveAdd()
				.Where(e => e.Value.Type == _type)
				.Subscribe(OnAddUnit);
			UpdatePlaceholder();
		}

		void OnAddUnit(CollectionAddEvent<UnitViewModel> ev) {
			var place = TryGetNextPlace();
			if ( place == null ) {
				return;
			}
			var instance = UnityEngine.Object.Instantiate(_unitPrefab, place);
			_instances.Add(instance);
			UpdatePlaceholder();
		}

		[CanBeNull]
		Transform TryGetNextPlace() {
			var index = _instances.Count;
			return (index < _places.Length) ? _places[index] : null;
		}

		void UpdatePlaceholder() {
			var place = TryGetNextPlace();
			var trans = _placeholder.transform;
			if ( place != null ) {
				trans.SetParent(place);
				trans.localPosition = Vector3.zero;
			}
			_placeholder.gameObject.SetActive(place != null);
		}
	}
}