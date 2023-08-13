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
            => typeof(GetBalancesViewModel).IsAssignableFrom(type)
                || typeof(ICollection<GetBalancesViewModel>).IsAssignableFrom(type);

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var responce = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is ICollection<GetBalancesViewModel>)
            {
                foreach (var Balance in (ICollection<GetBalancesViewModel>)context.Object)
                {
                    FormatCsv(buffer, Balance);
                }
            }
            else
            {
                FormatCsv(buffer, (GetBalancesViewModel)context.Object);
            }

            await responce.WriteAsync(buffer.ToString(), selectedEncoding);
        }

        private static void FormatCsv(StringBuilder buffer, GetBalancesViewModel balance)
        {
            buffer.Append($"{balance.Period};{balance.AccountId};{balance.InBalance};{balance.Calculate};{balance.Pay};{balance.OutBalance}\n");
        }
    }
}
