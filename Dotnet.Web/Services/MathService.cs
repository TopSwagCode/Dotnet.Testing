using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Testing.Web.Services
{
    public interface IMathService
    {
        int Sum(int[] args);
    }

    public class MathService : IMathService
    {
        public int Sum(int[] args)
        {
            var sum = 0;
            foreach(var number in args)
            {
                sum += number;
            }

            return sum;
        }
    }
}
