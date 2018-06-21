// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using DomainServices;
    using Model;
    using SQLite;

    public class MapRepository
        : IDisposable, IMapRepository
    {
        private const string DbName = "map2.sqlite";

        private static readonly object Locking = new object();
        private readonly SQLiteConnection conn;

        private bool disposedValue;

        public MapRepository()
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dbFilePath = Path.Combine(folderPath, DbName);
            this.conn = new SQLiteConnection(dbFilePath);
            lock (Locking)
            {
                this.conn.CreateTable<MapHashable>();
            }
        }

        public MapHashable RetrieveRoot()
        {
            lock (Locking)
            {
                return this.conn.Table<MapHashable>().FirstOrDefault(t => t.ParentId == null);
            }
        }

        public IEnumerable<MapHashable> RetrieveEntries(Guid parentId)
        {
            lock (Locking)
            {
                return this.conn
                    .Table<MapHashable>()
                    .Where(t => t.ParentId == parentId)
                    .ToList();
            }
        }

        public void PersistEntry(MapHashable entry)
        {
            lock (Locking)
            {
                this.conn.InsertOrReplace(entry, typeof(MapHashable));
            }
        }

        public string Mark()
        {
            lock (Locking)
            {
                var marker = Guid.NewGuid().ToString();

                this.conn.Execute("UPDATE Hashable SET Marker = ?", marker);

                return marker;
            }
        }

        public void CleanUp(string marker)
        {
            lock (Locking)
            {
                this.conn.Execute("DELETE FROM Hashable WHERE Marker = ?", marker);
            }
        }

        public void PersistEntries(IEnumerable<MapHashable> entries)
        {
            lock (Locking)
            {
                var marker = Guid.NewGuid().ToString();

                this.conn.Execute("UPDATE Hashable SET Marker = ?", marker);

                foreach (var entry in entries)
                {
                    this.conn.InsertOrReplace(entry);
                }

                this.conn.Execute("DELETE FROM Hashable WHERE Marker = ?", marker);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.conn?.Close();
                }

                this.disposedValue = true;
            }
        }
    }
}
