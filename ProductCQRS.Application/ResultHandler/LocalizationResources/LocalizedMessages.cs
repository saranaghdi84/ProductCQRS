namespace ProductCQRS.Application.ResultHandler.LocalizationResources;

public class LocalizedMessages
{
    private static readonly Dictionary<string, string> _persianMessages = new()
    {
        // Products
        ["Product.NotFound"] = "محصول یافت نشد",
        ["Product.InvalidPrice"] = "قیمت باید بیشتر از صفر باشد",

        // Categories
        ["Category.NotFound"] = "دسته‌بندی یافت نشد",
        ["Category.HasProducts"] = "دسته‌بندی دارای محصول است و قابل حذف نیست",

        // Common
        ["Validation.Required"] = "فیلد {0} الزامی است",
        ["Error.InternalServer"] = "خطای سرور"
    };

    public static string Get(string key, params object[] args)
    {
        if (_persianMessages.TryGetValue(key, out var message))
        {
            return args.Length > 0 ? string.Format(message, args) : message;
        }
        return key;
    }
}
