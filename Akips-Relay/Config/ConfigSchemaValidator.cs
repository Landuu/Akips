using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Akips_Relay.Config
{
    internal class ConfigSchemaValidator : AbstractValidator<ConfigSchema>
    {
        public ConfigSchemaValidator()
        {
            RuleFor(x => x.HttpIp)
                .NotNull()
                .Must(BeAValidIp);

            RuleFor(x => x.HttpPort)
                .NotNull()
                .GreaterThanOrEqualTo(1000)
                .LessThanOrEqualTo(65535);

            RuleFor(x => x.SocketIp)
                .NotNull()
                .Must(BeAValidIp);

            RuleFor(x => x.SocketPort)
                .NotNull()
                .GreaterThanOrEqualTo(1000)
                .LessThanOrEqualTo(65535);
        }

        private bool BeAValidIp(string? value)
        {
            if (value == null) return false;
            if (value == "localhost") return true;
            var regex = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
            return regex.IsMatch(value);
        }
    }
}
