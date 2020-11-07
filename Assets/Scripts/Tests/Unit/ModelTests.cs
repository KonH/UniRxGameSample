using System;
using System.Collections.Generic;
using Game.Model;
using Game.Shared;
using NUnit.Framework;
using UnityEngine;

namespace Game.Tests.Unit {
	public sealed class ModelTests {
		[Test]
		public void IsResourcesSerialized() {
			var resources  = new ResourcePack(new ResourceModel("ResourceName", 100));
			var sourceGame = new GameModel(resources, Array.Empty<UnitModel>());
			var targetGame = Serialize(sourceGame);

			var source = sourceGame.Resources.Content;
			var target = targetGame.Resources.Content;
			Assert.AreEqual(source.Count, target.Count);
			Assert.AreEqual(source[0], target[0]);
		}

		[Test]
		public void IsUnitsSerialized() {
			var units = new List<UnitModel> {
				new UnitModel(
					"UnitType",
					3,
					DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					new ResourceModel("ResourceName", 100))
			};
			var sourceGame = new GameModel(new ResourcePack(Array.Empty<ResourceModel>()), units);
			var targetGame = Serialize(sourceGame);

			var source = sourceGame.Units;
			var target = targetGame.Units;
			Assert.AreEqual(source.Count, target.Count);
			Assert.AreEqual(source[0].Type, target[0].Type);
			Assert.AreEqual(source[0].Level, target[0].Level);
			Assert.AreEqual(source[0].LastIncomeTime, target[0].LastIncomeTime);
			Assert.AreEqual(source[0].Income, target[0].Income);
		}

		GameModel Serialize(GameModel sourceGame) {
			var json       = JsonUtility.ToJson(sourceGame);
			var targetGame = JsonUtility.FromJson<GameModel>(json);
			return targetGame;
		}
	}
}