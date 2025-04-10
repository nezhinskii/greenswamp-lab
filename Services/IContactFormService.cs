using greenswamp.Models;

namespace greenswamp.Services
{
    public interface IContactFormService
    {
        Task SaveToCsvAsync(ContactFormData formData);
    }
}
