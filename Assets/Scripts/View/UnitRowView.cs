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
		[SerializeField] Transform[] _places;

		UnitView     _unitPrefab;
		UnitBuyView  _placeholder;
		UnitInfoView _infoView;

		GameViewModel _game;

		List<UnitView> _instances = new List<UnitView>();

		public void Init(
			GameViewModel game, UnitView unitPrefab, UnitBuyView placeholderPrefab, UnitInfoView infoView) {
			_game        = game;
			_unitPrefab  = unitPrefab;
			_placeholder = UnityEngine.Object.Instantiate(placeholderPrefab);
			_infoView    = infoView;
			foreach ( var unit in game.Units ) {
				if ( unit.Type == _type ) {
					OnAddUnit(unit);
				}
			}
			game.Units
				.ObserveAdd()
				.Where(e => e.Value.Type == _type)
				.Subscribe(e => OnAddUnit(e.Value));
			UpdatePlaceholder();
		}

		void OnAddUnit(UnitViewModel unit) {
			var place = TryGetNextPlace();
			if ( place == null ) {
				return;
			}
			var instance = UnityEngine.Object.Instantiate(_unitPrefab, place);
			instance.Init(_game, _infoView, unit);
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
			_placeholder.Init(_game, _type);
		}
	}
}