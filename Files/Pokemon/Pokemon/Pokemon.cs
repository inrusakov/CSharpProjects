using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class Pokemon
    {
        public string[] abilities { get; set; }
        public decimal against_bug { get; set; }
        public decimal against_dark { get; set; }
        public decimal against_dragon { get; set; }
        public decimal against_electric { get; set; }
        public decimal against_fairy { get; set; }
        public decimal against_fight { get; set; }
        public decimal against_fire { get; set; }
        public decimal against_flying { get; set; }
        public decimal against_ghost { get; set; }
        public decimal against_grass { get; set; }
        public decimal against_ground { get; set; }
        public decimal against_ice { get; set; }
        public decimal against_normal { get; set; }
        public decimal against_poison { get; set; }
        public decimal against_psychic { get; set; }
        public decimal against_rock { get; set; }
        public decimal against_steel { get; set; }
        public decimal against_water { get; set; }
        public decimal attack { get; set; }
        public decimal base_egg_steps { get; set; }
        public decimal base_happiness { get; set; }
        public decimal base_total { get; set; }
        public string capture_rate { get; set; }
        public string classfication { get; set; }
        public decimal defense { get; set; }
        public decimal experience_growth { get; set; }
        public decimal height_m { get; set; }
        public decimal hp { get; set; }
        public string japanese_name { get; set; }
        public string name { get; set; }
        public decimal percentage_male { get; set; }
        public decimal pokedex_number { get; set; }
        public decimal sp_attack { get; set; }
        public decimal sp_defense { get; set; }
        public decimal speed { get; set; }
        public string type1 { get; set; }
        public string type2 { get; set; }
        public decimal weight_kg { get; set; }
        public decimal generation { get; set; }
        public decimal is_legendary { get; set; }
        public bool is_free { get; set; }
        public Pokemon()
        {
            is_free = true;
        }
        public override string ToString()
        {
            for (int i = 0; i < name.Length % 12; i++)
            {
                name += " ";
            }
            for (int i = 0; i < type1.Length % 10; i++)
            {
                type1 += " ";
            }
            if (is_legendary == 1)
            {
                return $"Name: {name}      #{pokedex_number}     " +
                    $"General Type: {type1}          legendary";
            }
            else
            {
                return $"Name: {name}      #{pokedex_number}     " +
                    $"General Type: {type1}          not legendary";
            }
        }
    }
}
