using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace PROJECT
{
    class Koneksi
    {
        public NpgsqlConnection Connection = new NpgsqlConnection("Server=Localhost ; Port=5432 ; Database=Project PBO ; User Id= postgres; Password=HEFRILIA20");
    }
}
