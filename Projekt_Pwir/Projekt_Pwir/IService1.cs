using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Projekt_Pwir
{
    [ServiceContract]
    public interface IService1
    {

        //contracts operations for the corresponding methods
        [OperationContract]
        double CalculateEnergy(double catWeight, double multiplier);

        [OperationContract]
        double[] CalculateDensity(double protein, double fat, double ash, double fiber, double humidity, double other);


        [OperationContract]
        double CalculateGrams(double catCalories, double foodDensity);
    }
}
