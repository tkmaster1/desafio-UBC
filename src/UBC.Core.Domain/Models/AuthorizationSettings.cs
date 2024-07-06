namespace UBC.Core.Domain.Models
{
    public class AuthorizationSettings
    {
        public string Secret { get; set; }

        // ExpiracaoHoras
        public int ExpirationHours { get; set; }

        // ExpiracaoDias
        public int ExpirationDays { get; set; }

        // Emissor
        public string Issuer { get; set; }

        // ValidoEm
        public string ValidOn { get; set; }
    }
}