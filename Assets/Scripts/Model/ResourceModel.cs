using System;

namespace Game.Model {
	[Serializable]
	public sealed class ResourceModel : IComparable<ResourceModel>, IEquatable<ResourceModel> {
		public string Name;
		public long   Amount;

		public bool Equals(ResourceModel other) => (Name == other?.Name) && (Amount == other?.Amount);

		public override bool Equals(object obj) =>
			ReferenceEquals(this, obj) || obj is ResourceModel other && Equals(other);

		public override int GetHashCode() {
			unchecked {
				return (((Name != null) ? Name.GetHashCode() : 0) * 397) ^ Amount.GetHashCode();
			}
		}

		public int CompareTo(ResourceModel other) {
			if ( ReferenceEquals(this, other) ) {
				return 0;
			}
			if ( ReferenceEquals(null, other) ) {
				return 1;
			}
			var nameComparison = string.Compare(Name, other.Name, StringComparison.Ordinal);
			return (nameComparison != 0) ? nameComparison : Amount.CompareTo(other.Amount);
		}

		public static bool operator ==(ResourceModel left, ResourceModel right) {
			return Equals(left, right);
		}

		public static bool operator !=(ResourceModel left, ResourceModel right) {
			return !Equals(left, right);
		}
	}
}