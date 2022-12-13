using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Shared.data;

namespace Tests.Shared.data
{
	[TestClass]
	public class DeckTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			Deck deckTest = new Deck();

			Assert.AreEqual(deckTest.cards.Count, 52, message: "Deck is not generated.");
		}
	}
}
