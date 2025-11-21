using Microsoft.EntityFrameworkCore;
using Npgsql;
using SmartMeter.Data;
//using SmartMeter.Services.RabbitMQMeterProcessingService.Models;
using System;
using SmartMeter.Models;
using SmartMeter.Models.DTOs;

namespace SmartMeter.Services.RabbitMQMeterProcessingService.Utils
{
    public class DatabaseService
    {
        //private readonly string _connectionString;
        private readonly SmartMeterDbContext _context;

        //public DatabaseService(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}
        public DatabaseService(SmartMeterDbContext context)
        {
            _context = context;
        }



        public async Task InsertMeterReadingAsync(MeterreadingDto data)
        {
            try
            {
                //await using var conn = new NpgsqlConnection(_connectionString);
                //await conn.OpenAsync();               
                //string query = @"
                //    INSERT INTO meterreading (meterid, meterreadingdate, energyconsumed, voltage, current)
                //    VALUES (@meterid, @meterreadingdate, @energyconsumed, @voltage, @current);";

                //await using var cmd = new NpgsqlCommand(query, conn);
                //cmd.Parameters.AddWithValue("@meterid", data.meterid);
                //cmd.Parameters.AddWithValue("@meterreadingdate", DateTime.Parse(data.meterreadingdate));
                //cmd.Parameters.AddWithValue("@energyconsumed", data.energyconsumed);
                //cmd.Parameters.AddWithValue("@voltage", data.voltage);
                //cmd.Parameters.AddWithValue("@current", data.current);

                //await cmd.ExecuteNonQueryAsync();


                //await using var context = await _dbContextFactory.CreateDbContextAsync();
                var meterReading = new Meterreading
                {
                    Meterid = data.MeterId,
                    Meterreadingdate = data.MeterReadingDate,
                    Energyconsumed = data.EnergyConsumed,
                    Voltage = data.Voltage,
                    Current = data.Current
                };

                Console.WriteLine($"Meter data is: {data}");
                _context.Meterreadings.Add(meterReading);
                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DB Error] {ex.Message}");
                throw;
            }
        }
    }
}
