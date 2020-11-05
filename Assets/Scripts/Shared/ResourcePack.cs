using System;
using System.Collections.Generic;

namespace Game.Shared {
	[Serializable]
	public sealed class ResourcePack : IEquatable<ResourcePack> {
		public List<ResourceModel> Content = new List<ResourceModel>();

		public bool Equals(ResourcePack other) {
			if ( other == null ) {
				return false;
			}
			if ( Content.Count != other.Content.Count ) {
				return false;
			}
			for ( var i = 0; i < Content.Count; i++ ) {
				if ( Content[i] != other.Content[i] ) {
					return false;
				}
			}
			return true;
		}

		public override bool Equals(object obj) =>
			ReferenceEquals(this, obj) || obj is ResourcePack other && Equals(other);

		public override int GetHashCode() => (Content != null ? Content.GetHashCode() : 0);
	}
}