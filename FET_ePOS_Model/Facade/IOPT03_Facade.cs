using System;
using System.Data;

namespace FET.POS.Model.Facade
{
    public interface IOPT03_Facade
    {
        void AddNewOne_CreditCardInstallment(FET.POS.Model.DTO.OPT03_CreditCardInstallment_DTO ds);
        System.Data.DataTable Query_CreditCardInstallment(string BankName, string CostCenterNo, string PaySeqment, string Status);
        void UpdateOne_CreditCardInstallment(FET.POS.Model.DTO.OPT03_CreditCardInstallment_DTO ds);

        void AddNew_CreditCardSettlements(FET.POS.Model.DTO.OPT03_CreditCardInstallment_DTO ds);
        System.Data.DataTable Query_CreditCardSettlement(string InstallmentId);
        void Update_CreditCardSettlements(FET.POS.Model.DTO.OPT03_CreditCardInstallment_DTO ds);
        void Delete_CreditCardSettlements(DataTable dt);
    }
}
