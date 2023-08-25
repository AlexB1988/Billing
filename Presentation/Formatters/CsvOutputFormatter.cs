using Billing.Application.ViewModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace Billing.Application.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
            => typeof(BalancesViewModel).IsAssignableFrom(type)
                || typeof(ICollection<BalancesViewModel>).IsAssignableFrom(type);

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var responce = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is ICollection<BalancesViewModel>)
            {
                foreach (var Balance in (ICollection<BalancesViewModel>)context.Object)
                {
                    FormatCsv(buffer, Balance);
                }
            }
            else
            {
                FormatCsv(buffer, (BalancesViewModel)context.Object);
            }

            await responce.WriteAsync(buffer.ToString(), selectedEncoding);
        }

        private static void FormatCsv(StringBuilder buffer, BalancesViewModel balance)
        {
            buffer.Append($"{balance.Period};{balance.AccountId};{balance.InBalance};{balance.Calculate};{balance.Pay};{balance.OutBalance}\n");
        }
    }
}
