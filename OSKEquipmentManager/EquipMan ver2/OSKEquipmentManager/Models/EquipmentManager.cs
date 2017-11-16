using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSKEquipmentManager.Models
{
    static class Manager
    {
        //以下、暫定的なメソッド
        static IEnumerable<EquipmentInformation> GetItems(string keyword)
        {
            return new List<EquipmentInformation>();
        }

        static EquipmentInformation AddItems(EquipmentInformation model)
        {
            return new EquipmentInformation();
        }

        static EquipmentInformation UpdateItem(EquipmentInformation model)
        {
            return new EquipmentInformation();
        }

        static EquipmentInformation DeleteItem(EquipmentInformation model)
        {
            return new EquipmentInformation();
        }
    }
}
