using CsvHelper;
using greenswamp.Models;
using System.Globalization;

namespace greenswamp.Services
{
    public class ContactFormService : IContactFormService
    {
        private readonly string _csvFilePath;

        public ContactFormService()
        {
            _csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "contact_form_data.csv");
        }

        public async Task SaveToCsvAsync(ContactFormData formData)
        {
            try
            {
                bool fileExists = File.Exists(_csvFilePath);

                using (var stream = new FileStream(_csvFilePath, FileMode.Append, FileAccess.Write))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    if (!fileExists)
                    {
                        csv.WriteHeader<ContactFormData>();
                        await csv.NextRecordAsync();
                    }

                    csv.WriteRecord(formData);
                    csv.NextRecord();

                    await writer.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to CSV: {ex.Message}");
                throw;
            }
        }
    }
}
