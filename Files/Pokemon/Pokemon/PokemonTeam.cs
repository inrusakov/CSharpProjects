using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class PokemonTeam : IEnumerable<Pokemon>
    {
        public List<Pokemon> team { get; set; }
        public PokemonTeam(List<Pokemon> team)
        {
            this.team = team;
        }
        public void SendPokemons(PokemonTeam opTeam, List<Pokemon> outTeam)
        {
            if (outTeam != null)
            {
                for (int i = 0; i < outTeam.Count(); i++)
                {
                    opTeam.GetPokemons(outTeam[i]);
                    team.Remove(outTeam[i]);
                }
            }
        }
        public void GetPokemons(Pokemon pok)
        {
            team.Add(pok);
        }
        public int number { get; set; }
        public IEnumerator<Pokemon> GetEnumerator()
        {
            return team.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return $"{number}";
        }
    }
}
