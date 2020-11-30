using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Backup_OOP.CleanAlgorithms;
using Backup_OOP.Enums;
using Backup_OOP.Interfaces;
using Backup_OOP.Tests.Mocks;
using NUnit.Framework;

namespace Backup_OOP.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CreateRestorePoint()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.Write(new FileInformation(100, "a.jjje"));
            Backup backup = new Backup(new SeparateStorageAlgorithm(), new MockDateTimeProvider(DateTime.Now), mockFileSystem, new RestorePointCreator());

            backup.Watch("a.jjje");
            backup.CreateRestorePoint(RestoreType.Full);

            Assert.That(backup.RestorePoints.Count, Is.EqualTo(1));
        }
        
        [Test]
        public void CheckCountCleanAlgorithm()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.Write(new FileInformation(100, "b.jjje"));
            mockFileSystem.Write(new FileInformation(100, "b.jjjf"));
            Backup backup = new Backup(new SeparateStorageAlgorithm(),new MockDateTimeProvider(DateTime.Now), mockFileSystem, new RestorePointCreator());

            backup.Watch("b.jjje");
            backup.Watch("b.jjjf");

            backup.CreateRestorePoint(RestoreType.Full);
            Assert.That(backup.RestorePoints.First().RestoreFiles.Count, Is.EqualTo(2));

            backup.CreateRestorePoint(RestoreType.Full);
            Assert.That(backup.RestorePoints.Count, Is.EqualTo(2));

            backup.Clean(new CountCleanAlgorithm(1));
            Assert.That(backup.RestorePoints.Count, Is.EqualTo(1));
        }

        [Test]
        public void CheckSizeCleanAlgorithm()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.Write(new FileInformation(100, "b.jjje"));
            mockFileSystem.Write(new FileInformation(100, "b.jjjf"));
            Backup backup = new Backup(new SeparateStorageAlgorithm(), new MockDateTimeProvider(DateTime.Now), mockFileSystem, new RestorePointCreator());

            backup.Watch("b.jjje");
            backup.Watch("b.jjjf");

            backup.CreateRestorePoint(RestoreType.Full);
            backup.CreateRestorePoint(RestoreType.Full);

            Assert.That(backup.RestorePoints.Count, Is.EqualTo(2));
            Assert.That(backup.Size, Is.EqualTo(400));

            backup.Clean(new SizeCleanAlgorithm(250));
            Assert.That(backup.RestorePoints.Count, Is.EqualTo(1));
        }

        [Test]
        public void CheckIncrement()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.Write(new FileInformation(100, "b.jjje"));
            mockFileSystem.Write(new FileInformation(100, "b.jjjf"));
            Backup backup = new Backup(new SeparateStorageAlgorithm(), new MockDateTimeProvider(DateTime.Now), mockFileSystem, new RestorePointCreator());

            backup.Watch("b.jjje");
            backup.CreateRestorePoint(RestoreType.Full);
            backup.Watch("b.jjjf");
            backup.CreateRestorePoint(RestoreType.Increment);

            Assert.That(backup.Size, Is.EqualTo(200));
            Assert.That(backup.RestorePoints.Last().RestoreFiles.Count, Is.EqualTo(1));
        }

        [Test]
        public void CheckIncrementWithDiff()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.Write(new FileInformation(100, "b.jjje"));
            Backup backup = new Backup(new SeparateStorageAlgorithm(), new MockDateTimeProvider(DateTime.Now), mockFileSystem, new RestorePointCreator());

            backup.Watch("b.jjje");
            backup.CreateRestorePoint(RestoreType.Full);
            mockFileSystem.Update("b.jjje", 120);
            backup.CreateRestorePoint(RestoreType.Increment);

            Assert.That(backup.Size, Is.EqualTo(120));
            Assert.That(backup.RestorePoints.Last().RestoreFiles.Count, Is.EqualTo(1));
        }
        [Test]
        public void CheckSeparateStorage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.Write(new FileInformation(100, "b.jjje"));
            Backup backup = new Backup(new SeparateStorageAlgorithm(), new MockDateTimeProvider(DateTime.Now), mockFileSystem, new RestorePointCreator());

            backup.Watch("b.jjje");
            backup.CreateRestorePoint(RestoreType.Full);

            Assert.That(backup.RestorePoints.First().StorageType, Is.EqualTo(StorageType.Separate));
        }
        [Test]
        public void CheckSharedStorage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.Write(new FileInformation(100, "b.jjje"));
            Backup backup = new Backup(new SharedStorageAlgorithm(), new MockDateTimeProvider(DateTime.Now), mockFileSystem, new RestorePointCreator());

            backup.Watch("b.jjje");
            backup.CreateRestorePoint(RestoreType.Full);

            Assert.That(backup.RestorePoints.First().StorageType, Is.EqualTo(StorageType.Shared));
        }

        [Test]
        public void CheckTrueHybridAll()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.Write(new FileInformation(100, "b.jjje"));
            Backup backup = new Backup(new SharedStorageAlgorithm(), new MockDateTimeProvider(DateTime.Now), mockFileSystem, new RestorePointCreator());

            backup.Watch("b.jjje");
            backup.CreateRestorePoint(RestoreType.Full);
            backup.CreateRestorePoint(RestoreType.Full);

            backup.Clean(new HybridCleanAlgorithm(new List<ICleanAlgorithm>{new CountCleanAlgorithm(1), new SizeCleanAlgorithm(150)}, HybridType.WhenAll));

            Assert.That(backup.RestorePoints.Count, Is.EqualTo(1));
        }

        [Test]
        public void CheckFalseHybridAll()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.Write(new FileInformation(100, "b.jjje"));
            Backup backup = new Backup(new SharedStorageAlgorithm(), new MockDateTimeProvider(DateTime.Now), mockFileSystem, new RestorePointCreator());

            backup.Watch("b.jjje");
            backup.CreateRestorePoint(RestoreType.Full);
            backup.CreateRestorePoint(RestoreType.Full);

            backup.Clean(new HybridCleanAlgorithm(new List<ICleanAlgorithm> { new CountCleanAlgorithm(2), new SizeCleanAlgorithm(150) }, HybridType.WhenAll));

            Assert.That(backup.RestorePoints.Count, Is.EqualTo(2));
        }

        [Test]
        public void CheckTrueHybridAny()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.Write(new FileInformation(100, "b.jjje"));
            Backup backup = new Backup(new SharedStorageAlgorithm(), new MockDateTimeProvider(DateTime.Now), mockFileSystem, new RestorePointCreator());

            backup.Watch("b.jjje");
            backup.CreateRestorePoint(RestoreType.Full);
            backup.CreateRestorePoint(RestoreType.Full);

            backup.Clean(new HybridCleanAlgorithm(new List<ICleanAlgorithm> { new CountCleanAlgorithm(2), new SizeCleanAlgorithm(150) }, HybridType.WhenAny));

            Assert.That(backup.RestorePoints.Count, Is.EqualTo(1));
        }

        [Test]
        public void CheckFalseHybridAny()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.Write(new FileInformation(100, "b.jjje"));
            Backup backup = new Backup(new SharedStorageAlgorithm(), new MockDateTimeProvider(DateTime.Now), mockFileSystem, new RestorePointCreator());

            backup.Watch("b.jjje");
            backup.CreateRestorePoint(RestoreType.Full);
            backup.CreateRestorePoint(RestoreType.Full);

            backup.Clean(new HybridCleanAlgorithm(new List<ICleanAlgorithm> { new CountCleanAlgorithm(2), new SizeCleanAlgorithm(200) }, HybridType.WhenAny));

            Assert.That(backup.RestorePoints.Count, Is.EqualTo(2));
        }


    }
}