using BillingSystemLogic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemLogic.Logic
{
    public class TarrifChargeProcessor
    {
        private TarrifCharge tarrifCharge;
        private Processor processor = new Processor();
        public TarrifChargeProcessor(TarrifCharge tarrifCharge)
        {
            this.tarrifCharge = tarrifCharge;
        }

        public List<TarrifCharge> GetTarrifCharges()
        {
            List<TarrifCharge> tarrifCharges = new List<TarrifCharge>();
            try
            {
                DataTable dataTable = processor.GetTarrifCharges();
                if (dataTable.Rows.Count>0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        TarrifCharge tarrifCharge = new TarrifCharge();
                        tarrifCharge.Amount = dr["Amount"].ToString();
                        tarrifCharge.ChargePeriod = dr["ChargePeriod"].ToString();
                        tarrifCharge.RecordDate = dr["RecordDate"].ToString();
                        tarrifCharge.RecordedBy = dr["RecordedBy"].ToString();
                        tarrifCharge.TarrifChargeId = dr["TarrifChargeId"].ToString();
                        tarrifCharges.Add(tarrifCharge);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tarrifCharges;
        }
    }
}
