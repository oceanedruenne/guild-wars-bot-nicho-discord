using System;

namespace NicholasLeVoyageur.Modules
{
	public class Item
	{
		public string nomFR { get; set; }
		public string nomEN { get; set; }
		public int nombreUnite { get; set; }
		public string date { get; set; }
		public string farm { get; set; }
		public string map { get; set; }

		public Item()
		{
		}
		public Item(string nomFR, string nomEN, int nombreUnite, string date, string farm, string map)
		{
			this.nomFR = nomFR;
			this.nomEN = nomEN;
			this.nombreUnite = nombreUnite;
			this.date = date;
			this.farm = farm;
			this.map = map;
		}
	}
}