using System;
using System.Collections.Generic;
using Game.Model;
using NUnit.Framework;
using UnityEngine;

namespace Game.Tests {
	public sealed class ModelTests {
		[Test]
		public void IsResourcesSerialized() {
			var sourceGame = new GameModel();
			sourceGame.Resources.Content.Add(new ResourceModel {
				Name = "ResourceName",
				Amount = 100
			});
			var targetGame = Serialize(sourceGame);

			var source = sourceGame.Resources.Content;
			var target = targetGame.Resources.Content;
			Assert.AreEqual(source.Count, target.Count);
			Assert.AreEqual(source[0], target[0]);
		}

		[Test]
		public void IsUnitsSerialized() {
			var sourceGame = new GameModel();
			sourceGame.Units.Add(new UnitModel {
				Type = "UnitType",
				Level = 3,
				LastIncomeTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
				Income = new ResourcePack {
					Content = new List<ResourceModel> {
						new ResourceModel {
							Name   = "ResourceName",
							Amount = 100
						}
					}
				}
			});
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