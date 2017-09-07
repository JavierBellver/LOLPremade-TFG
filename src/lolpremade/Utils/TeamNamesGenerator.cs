using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.Utils
{
    public static class TeamNamesGenerator
    {
        private static string[] Adjectives =
        {
            "Fantastic","Awesome","Wonderous","Horrific","Melodramatic","Great","Long","Fancy","Witty","Zealous","Mysterious","Brave","Eager","Silly","Powerful","Abnormal",
            "Absurd","Ambiguous","Axiomatic","Defiant","Dirty","Divergent","Electric","Envious","Exhuberant","Fallacious","Flawless","Fluffy","Glorious","Goofy","Groovy","Grotesque",
            "Handy","Honorable","Incredible","Invincible","Jaded","Jagged","Magnificent","Nimble","Noisy","Outgoing","Outrageous","Powerful","Ragged","Rebel","Salty"
        };

        private static string[] Names =
        {
            "Hawks","Fireballs","Tigers","Windows","Vegans","Buccaneers","Walkamolies","Puckles","Puckies","Mafia","Secret","Busters","Mirrors","Monkey Pimps","Wizards","Rogues","Clerics","Warriors","Beer",
            "Tube Men","Riders","Duckies","Drinkers","Stunts","Batters","Dwarfs","Knuckles","Donkeys","Wackers","Pigs","Bombers","Killers","Bunnies","Assassins","Oompa-Loompas","Squirrels","Pandas","Destiny",
            "Brains","Noodle Squad","Ninjas","McThundersticks","Minds","Wonders","Zeros","Chickens","Foots","Aces","Hotties","Ducks","Brawlers","Doctors","Tasters","Eagles","Elites","Hell Raisers","Loops",
            "Executioners"
        };

        public static string GetTeamName()
        {
            int Seed = (int)DateTime.Now.Ticks;
            var random = new Random(Seed);
            string adjective = Adjectives[random.Next(Adjectives.Length)];
            string name = Names[random.Next(Names.Length)];
            return "The " + adjective + " " + name;
        }
    }
}
