using nscreg.Data.Constants;

namespace nscreg.Server.Common.Models.Lookup
{
    /// <summary>
    /// Вью модель единицы
    /// </summary>
    public interface IUnitVm
    {
        int Id { get; set; }
        StatUnitTypes Type { get; set; }
    }
}
