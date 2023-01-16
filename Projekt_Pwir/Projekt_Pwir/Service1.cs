using System;

namespace Projekt_Pwir
{
    public class Service1 : IService1
    {
        //method that counts the caloric requirements for a cat, taking the cat's weight (catWeight) and type (multiplier) as double values
        //returns the value of the result
        public double CalculateEnergy(double catWeight, double multiplier)
        {
            double result = 70 * Math.Pow(catWeight, 0.67) * multiplier;
            Console.WriteLine("CatWeight: ({0}, CatType: {1})", catWeight, multiplier);
            
            return result;
        }

        //Method of counting energy density of food, takes double values: protein, fat, ash, fiber, humidity
        //other (other ingredients)
        //Returns an array whose first value is the % carbohydrate content, and whose second value is the energy density
        public double[] CalculateDensity(double protein, double fat, double ash, double fiber, double humidity, double other)
        {
            double[] result = new double[2];
            result[0] = 100 - protein - fat - ash - fiber - humidity - other;
            Console.WriteLine("Carbohydrates count ({0}", result[0]);
           
            result[1] = ((protein + result[0]) * 3.5 + fat * 8.5);
            Console.WriteLine("Energy density {0}", result[1]);
            return result;

        }

        //method that counts the amount of food a cat should eat per day, takes the values of double, catCalories (the caloric needs of the cat) and foodDensity (the energy density of the food)
        //returns the amount of food
        public double CalculateGrams(double catCalories, double foodDensity)
        {
            double grams = catCalories * 100 / foodDensity;
            Console.WriteLine("Food amount in (g) ", grams);
            return grams;


        }
    }
}
