using System.Collections.Generic;
using Backup_OOP.Interfaces;

namespace Backup_OOP.CleanAlgorithms
{
    public class CountCleanAlgorithm : ICleanAlgorithm
    {
        private readonly int _count;

        public CountCleanAlgorithm(int count)
        {
            _count = count;
        }

        public int GetForRemove(List<RestorePoint> restorePoints)
        {
            int removeCount = restorePoints.Count - _count;
            if (removeCount <= 0)
            {
                return 0;
            }
            return removeCount;
        }
    }
}
