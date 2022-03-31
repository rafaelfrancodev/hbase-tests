using HBaseNet;
using HBaseNet.HRpc;
using HBaseNet.HRpc.Descriptors;
using System;
using System.Threading.Tasks;

namespace TestHBase
{
    class Program
    {
        static async Task Main(string[] args)
        {

            try
            {
                var ZkQuorum = "localhost:16010";

                var admin = await new AdminClient(ZkQuorum).Build();
                if (admin == null) return;
                //var client = await new StandardClient(ZkQuorum).Build();
                //if (client == null) return;

                var table = "student";
                var cols = new[]
                        {
                new ColumnFamily("info")
                {
                    Compression = Compression.GZ,
                    KeepDeletedCells = KeepDeletedCells.TRUE
                },
                new ColumnFamily("special")
                {
                    Compression = Compression.GZ,
                    KeepDeletedCells = KeepDeletedCells.TTL,
                    DataBlockEncoding = DataBlockEncoding.PREFIX
                }
            };
                var create = new CreateTableCall(table, cols)
                {
                    SplitKeys = new[] { "0", "5" }
                };
                var listTable = new ListTableNamesCall();
                var disable = new DisableTableCall(table);
                var delete = new DeleteTableCall(table);

                var ct = await admin.CreateTable(create);

                var tables = await admin.ListTableNames(listTable);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

         }
    }
}
