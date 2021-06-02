using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba
{
    abstract class Hero
    {
        protected string name;
        protected int level;
        protected int health;
        public Hero(string Name)
        {
            name = Name;
            level = 1;
            health = 100;
        }
        public abstract void Action(Hero target);
        public abstract void SpecialAbility(Hero target);
        public void LevelUp()
        {
            if (level < 10)
            {
                level++;
                health = 100;
                Console.WriteLine(name + " got new Level");
            }
        }
        public bool IsAlive()
        {
            if (health > 0)
            {
                return true;
            }
            return false;
        }
        public string Name
        {
            get { return name; }
        }
        public int Level
        {
            get { return level; }
        }
        public virtual int Health
        {
            get { return health; }
            set
            {
                if (health == 0)
                {
                    Console.WriteLine("Hero died");
                }
                health = value;
            }
        }
        public virtual int StaminaOrMana
        {
            get { return 1; }
        }
        public abstract string FriendOrNot();
    }
    class Warrior : Hero
    {
        int stamina;
        public Warrior(string Name) : base(Name)
        {
            stamina = 100;
        }
        public override void Action(Hero target)
        {
            if (target.IsAlive())
            {
                target.Health = target.Health - Level * 9;
                Console.WriteLine("Warrior hits " + target.Name + " for " + Level * 9 + " hp ");
            }
        }
        public override void SpecialAbility(Hero target)
        {
            if (level > 1 && stamina > 15)
            {
                target.Health = target.Health / 10;
                target.Health = target.Health * 7;
                Console.WriteLine(target.Name + "Lost 30% of health");
                if (health < 80)
                {
                    health += 20;
                    Console.WriteLine("warrior got 20 HP");
                }
                else
                {
                    health = 100;
                    Console.WriteLine("Warrior was healed to 100 HP");
                }
                stamina = stamina - 15;
                Console.WriteLine("Warrior lost 15 stamina points");
            }
            else
            {
                Console.WriteLine("Your warrior has level < 2 or your warrior's stamina is less than 15");
            }
        }
        public override int Health
        {
            get { return health; }
            set
            {
                if (!Assistant.GetRand(15))
                {
                    health = value;
                }
                else
                {
                    Console.WriteLine("Block");
                }
            }
        }
        public override int StaminaOrMana
        {
            get { return stamina; }
        }
        public override string FriendOrNot()
        {
            return "friend";
        }
    }
    class Healer : Hero
    {
        int mana;
        public Healer(string Name) : base(Name)
        {
            mana = 100;
        }
        public override void Action(Hero target)
        {
            if (target.IsAlive() && target.FriendOrNot() == "friend")
            {
                if (target.Health + level * 9 > 100)
                {
                    target.Health = 100;
                }
                else
                {
                    target.Health = target.Health + level * 9;

                }
                Console.WriteLine(name + " heals " + target.Name + " for " + Level * 9 + " hp ");
            }
            else if (target.IsAlive())
            {
                target.Health = target.Health - level * 6;
                Console.WriteLine(name + " damages " + target.Name + " for " + Level * 6 + " hp ");
            }
        }
        public override void SpecialAbility(Hero target)
        {
            if (target.Level < 9 && level == 1 && mana > 35)
            {
                target.Health = 0;
                mana -= 35;
                Console.WriteLine("Healer kills with special ability " + target.Name);
            }
            else
            {
                Console.WriteLine("Your healer has no mana/lvl > 1 or target level is more than 9");
            }
        }
        public override int StaminaOrMana
        {
            get { return mana; }
        }
        public override string FriendOrNot()
        {
            return "friend";
        }
    }
    class SoulEater : Hero
    {
        public SoulEater(string Name) : base(Name)
        {
        }
        public void StealHealth(int damage)
        {
            if (Assistant.GetRand(20))
            {
                if (health + damage * 1.15 > 100)
                {
                    health = 100;
                }
                else
                {
                    health += (int)(damage * 1.15);
                }
                Console.WriteLine(name + " was healed for " + damage * 1.15 + " HP");
            }
        }
        public override void Action(Hero target)
        {
            if (target.IsAlive())
            {
                Console.WriteLine(name + " hits " + target.Name + " for " + level * 6);
                target.Health -= level * 6;
                StealHealth(level * 6);
            }
        }
        public override void SpecialAbility(Hero target)
        {
            if (target.IsAlive() && target.Level > 1)
            {
                target.Health = (int)(target.Health * 0.7);
                Console.WriteLine(name + " stole 30% of " + target.Name + "'s health");
            }
        }
        public override string FriendOrNot()
        {
            return "enemy";
        }
    }
    static public class Assistant
    {
        static Random rnd = new Random();
        public static bool GetRand(int perc)
        {
            bool[] getter = new bool[100];
            for (int i = 0; i < 100; i++)
            {
                getter[i] = false;
            }
            for (int i = 0; i < perc; i++)
            {
                getter[i] = true;
            }
            return getter[rnd.Next(0, 100)];
        }
    }
}
