using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace password_salting
{
    [Serializable]
    class DataPasswordSerializeHolder2
    {

        public byte[] Password { get; set; }
        public string PublicKey { get; set; }

        public DataPasswordSerializeHolder2(EncryptPasswordHelper.Entities.DataPassword dataObject)
        {
            Password = dataObject.Password;
            PublicKey = dataObject.PublicKey;
        }
       
    }
}
