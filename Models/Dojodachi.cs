namespace Dojodachi.Models

{
    public class Tamagochi
    {
        public int Fullness { get; set; }
        public int Happiness { get; set; }

        public int Meals { get; set; }

        public int Energy { get; set; }



        public Tamagochi(int fullness, int happiness, int meals, int energy)
        {
            Fullness = fullness;
            Happiness = happiness;
            Meals = meals;
            Energy = energy;
        }
    }

}