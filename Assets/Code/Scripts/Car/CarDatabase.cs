using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XaviEssencials.Runtime;

namespace XaviGames.Car
{
    [CreateAssetMenu(fileName = "CarDatabase", menuName = "Xavi Games/Car/Car Database")]
    public class CarDatabase : ScriptableObject
    {
        [field: SerializeField]
        public List<CarParameter> CarsParameters { get; private set; } = new List<CarParameter>();

        public CarParameter GetCarParameter(string id)
        {
            var carParameter = CarsParameters.FirstOrDefault(parameter => parameter.Id == id);
            return carParameter;
        }
    }
}
