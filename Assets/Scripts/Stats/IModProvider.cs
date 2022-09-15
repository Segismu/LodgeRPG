using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModProvider
    {
        IEnumerable<float> GetAdditiveMod(Stat stat);
        IEnumerable<float> GetPercentageMod(Stat stat);
    }
}