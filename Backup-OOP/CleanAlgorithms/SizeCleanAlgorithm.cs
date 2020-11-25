using System.Collections.Generic;
using System.Linq;
using Backup_OOP.Interfaces;

namespace Backup_OOP.CleanAlgorithms
{
    public class SizeCleanAlgorithm : ICleanAlgorithm
    {
        private readonly int _size;

        public SizeCleanAlgorithm(int size)
        {
            _size = size;
        }
        public int GetForRemove(List<RestorePoint> restorePoints)
        {
            long sum = restorePoints.Sum(x => x.Size);

            for(int i = 0; i < restorePoints.Count; i++)
            {
                if (sum <= _size)
                {
                    return i;
                }
                sum -= restorePoints[i].Size;
            }

            return restorePoints.Count;
        }
    }
}
