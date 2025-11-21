using System.Text.Json.Serialization;

namespace SmartMeter.Models.DTOs
{
    public class MeterreadingDto
    {
        [JsonPropertyName("meterid")]
        public required string  MeterId { get; set; }

        [JsonPropertyName("meterreadingdate")]
        public DateTime MeterReadingDate { get; set; }

        [JsonPropertyName("energyconsumed")]
        public decimal? EnergyConsumed { get; set; }

        [JsonPropertyName("voltage")]
        public decimal Voltage { get; set; }

        [JsonPropertyName("current")]
        public decimal Current { get; set; }
    }
}
