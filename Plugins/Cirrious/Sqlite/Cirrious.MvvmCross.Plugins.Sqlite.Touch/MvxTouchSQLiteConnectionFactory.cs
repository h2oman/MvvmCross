// MvxTouchSQLiteConnectionFactory.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com
using System;
using System.IO;
using SQLite;

namespace Cirrious.MvvmCross.Plugins.Sqlite.Touch
{
    public class MvxTouchSQLiteConnectionFactory : ISQLiteConnectionFactory
    {
        public ISQLiteConnection Create(string address)
        {
            return Create(address, new ConnectionOptions());
        }

        public ISQLiteConnection Create(string address, ConnectionOptions connectionOptions)
        {
            var databasePath = string.Empty;
            switch(connectionOptions.DatabasePathFormat)
            {
                case DatabasePathFormat.Absolute:
                    databasePath = address;
                    break;
                case DatabasePathFormat.SpecialFolderPersonal:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), address);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Invalid DatabasePathFormat passed to ISQLiteConnection->Create via connectionOptions");
                    break;
            }
            return new SQLiteConnection(databasePath, connectionOptions.DateFormat == DateFormat.AsTicks);
        }
    }
}
