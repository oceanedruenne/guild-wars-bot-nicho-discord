using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using System.Globalization;
using CsvHelper;
using System.IO;
using CsvHelper.Configuration;

namespace NicholasLeVoyageur.Modules
{

    public class Commands :  ModuleBase<SocketCommandContext>
    {

        [Command("ping")]
        public async Task Ping()
        {
            await ReplyAsync("Pong");
        }

        [Command("nicho")]
        public async Task Nicho([Remainder] string nomFR)
        {
            try
            {
                List<Item> items = this.chargerFichier();
                Item item = items.Find(i => i.nomFR == nomFR);
                if (item == null)
                {
                    await ReplyAsync("Je ne demande pas cet item");
                    return;
                }
                await ReplyAsync("Nom anglais : " + item.nomEN);
                await ReplyAsync("Je demande " + item.nombreUnite.ToString() + " par cadeau");
                await ReplyAsync("Je les demanderai le " + item.date);
                await ReplyAsync(item.farm);
                await ReplyAsync("Tu pourras me retrouver ici : " + item.map);
            }
            catch(Exception e)
            {
                await ReplyAsync("Erreur " + e.StackTrace);
            }


        }

        [Command("ouaf")]
        public async Task Ouaf()
        {
            await ReplyAsync("ouaf");
        }

        [Command("doggo")]
        public async Task doggo()
        {
            await ReplyAsync("ouaf");
        }


        public List<Item> chargerFichier()
        {
            var streamReader = File.OpenText("nicho.csv");
            var csvReader = new CsvReader(streamReader,new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = "," });
            var items = csvReader.GetRecords<Item>();

            return items.ToList<Item>();
        }


    }
}
