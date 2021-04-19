using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClientAddressManager
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [DisplayName("Pavadinimas")]
        public string Name { get; set; }

        [DisplayName("Adresas")]
        public string Address { get; set; }

        [DisplayName("Pašto kodas")]
        public string PostCode { get; set; }


        public Client()
        {

        }

        public override bool Equals(object obj)
        {
            return obj is Client client &&
                   Id == client.Id &&
                   Name == client.Name &&
                   Address == client.Address &&
                   PostCode == client.PostCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Address, PostCode);
        }
    }
}
