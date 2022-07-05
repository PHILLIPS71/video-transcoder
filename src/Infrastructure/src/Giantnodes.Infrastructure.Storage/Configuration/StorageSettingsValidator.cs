using Microsoft.Extensions.Options;

namespace Giantnodes.Infrastructure.Storage.Configuration
{
    public class StorageSettingsValidator : IValidateOptions<StorageSettings>
    {
        public ValidateOptionsResult Validate(string name, StorageSettings? options)
        {
            if (options is null)
                return ValidateOptionsResult.Fail($"The '{nameof(StorageSettings)}' configuration object is null.");

            if (string.IsNullOrWhiteSpace(options.Directory))
                return ValidateOptionsResult.Fail($"Property '{nameof(options.Directory)}' cannot be blank.");

            if (!Directory.Exists(options.Directory))
            {
                if (File.Exists(options.Directory))
                    return ValidateOptionsResult.Fail($"The path '{options.Directory}' needs to be set to a directory.");

                return ValidateOptionsResult.Fail($"The directory '{options.Directory}' cannot be found.");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
