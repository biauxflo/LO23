using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Shared.data;

namespace Tests.Shared.data
{
	[TestClass]
	public class DeckTest
	{
		/// <summary>
		/// Check if deck constructor generates 52 cards.
		/// </summary>
		[TestMethod]
		public void DeckGenerate52Cards()
		{
			Deck deckTest = new Deck();

			Assert.AreEqual(deckTest.cards.Count, 52);
		}
	}
}
