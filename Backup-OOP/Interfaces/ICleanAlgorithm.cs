using System.Collections.Generic;

namespace Backup_OOP.Interfaces
{
    public interface ICleanAlgorithm
    { 
        public int GetForRemove(List<RestorePoint> restorePoints);
    }
}
