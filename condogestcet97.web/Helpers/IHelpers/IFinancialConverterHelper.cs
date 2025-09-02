using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Models;

namespace condogestcet97.web.Helpers.IHelpers
{
    public interface IFinancialConverterHelper
    {
        Quota ToQuota(QuotaViewModel model, bool isNew);

        QuotaViewModel ToQuotaViewModel(Quota quota);
    }
}
