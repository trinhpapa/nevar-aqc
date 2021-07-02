namespace NEVAR_AQC.Core.Models.Managements
{
    public class CTGReturnInvoiceResultTypeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public bool? Status { get; set; } = true;
    }
}