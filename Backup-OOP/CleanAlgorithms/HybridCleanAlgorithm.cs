using System;
using System.Collections.Generic;
using System.Linq;
using Backup_OOP.Enums;
using Backup_OOP.Interfaces;

namespace Backup_OOP.CleanAlgorithms
{
    public class HybridCleanAlgorithm : ICleanAlgorithm
    {
        private readonly List<ICleanAlgorithm> _cleanAlgorithms;
        private readonly HybridType _hybridType;

        public HybridCleanAlgorithm(List<ICleanAlgorithm> cleanAlgorithms, HybridType hybridType)
        {
            _cleanAlgorithms = cleanAlgorithms;
            _hybridType = hybridType;
        }
        public int GetForRemove(List<RestorePoint> restorePoints)
        {
            List<int> results = new List<int>();
            foreach (var algorithm in _cleanAlgorithms)
            {
                int count = algorithm.GetForRemove(restorePoints);
                results.Add(count);
            }
            switch (_hybridType)
            {
                case HybridType.WhenAll:
                    return results.Min();
                case HybridType.WhenAny:
                    return results.Max();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
