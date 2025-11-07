using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riok.Mapperly.Abstractions;
using InternshipNet.Models;
using InternshipNet.ViewModels;

namespace InternshipNet.Mappers
{
    // Атрибут [Mapper] каже Mapperly, що цей клас генеруватиме код
    [Mapper]
    public partial class InternshipMapper
    {
        // Ми визначаємо "сигнатуру" методу, а Mapperly сам напише його реалізацію
        public partial InternshipViewModel Map(Internship internship);

        // Метод для зворотного перетворення (для збереження)
        public partial Internship Map(InternshipViewModel internshipViewModel);
    }
}